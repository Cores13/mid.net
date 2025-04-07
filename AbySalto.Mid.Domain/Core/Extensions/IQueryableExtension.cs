using System.Linq.Expressions;
using System.Reflection;

namespace AbySalto.Mid.Domain.Core.Extensions
{
    public static class IQueryableExtension
    {
        private static Expression expression;
        public static IOrderedQueryable<T> OrderBy<T>(this IQueryable<T> source, string propertyName, bool isAscending = false)
        {

            if (source == null)
            {
                throw new ArgumentNullException();
            }

            if (propertyName == null)
            {
                throw new ArgumentNullException();
            }

            Type type = typeof(T);
            ParameterExpression arg = Expression.Parameter(type, "x");

            PropertyInfo pi = type.GetProperties().FirstOrDefault(x => x.Name.ToLower() == propertyName.ToLower());
            if (pi == null)
            {
                throw new ArgumentNullException();
            }
            Expression expr = Expression.Property(arg, pi);
            type = pi.PropertyType;

            // make null values appear last always
            var nullFilteredQuery = OrderNullLast(source, pi, type);

            Type delegateType = typeof(Func<,>).MakeGenericType(typeof(T), type);
            LambdaExpression lambda = Expression.Lambda(delegateType, expr, arg);

            string methodName = isAscending ? "ThenBy" : "ThenByDescending";

            var result = typeof(Queryable).GetMethods().Single(
                method => method.Name == methodName
                        && method.IsGenericMethodDefinition
                        && method.GetGenericArguments().Length == 2
                        && method.GetParameters().Length == 2)
                .MakeGenericMethod(typeof(T), type)
                .Invoke(null, new object[] { nullFilteredQuery, lambda });

            return result as IOrderedQueryable<T>;
        }

        private static IOrderedQueryable<T> OrderNullLast<T>(this IQueryable<T> source, PropertyInfo propertyInfo, Type propertyType)
        {
            Type type = typeof(T);

            ParameterExpression arg = Expression.Parameter(type, "x");


            if (propertyType.Name == "Double")
            {
                expression = Expression.Equal(Expression.Property(arg, propertyInfo), Expression.Constant(double.Epsilon));
            }
            else if (propertyType.Name == "DateTime")
            {
                expression = Expression.Equal(Expression.Property(arg, propertyInfo), Expression.Constant(DateTime.MinValue));
            }
            else
            {
                expression = Expression.Equal(Expression.Property(arg, propertyInfo), Expression.Constant(null));
            }

            Type delegateType = typeof(Func<,>).MakeGenericType(typeof(T), typeof(bool));
            LambdaExpression lambda = Expression.Lambda(delegateType, expression, arg);

            string methodName = "OrderBy";

            var result = typeof(Queryable).GetMethods().Single(
                method => method.Name == methodName
                        && method.IsGenericMethodDefinition
                        && method.GetGenericArguments().Length == 2
                        && method.GetParameters().Length == 2)
                .MakeGenericMethod(typeof(T), typeof(bool))
                .Invoke(null, new object[] { source, lambda });

            return result as IOrderedQueryable<T>;
        }
    }
}
