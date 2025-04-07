using AbySalto.Mid.Domain.Interfaces.Repository;
using MediatR;
using System.Transactions;

namespace AbySalto.Mid.Application.Behaviors
{
    public sealed class UnitOfWorkBehavior<TRequest, TResponse>
        : IPipelineBehavior<TRequest, TResponse>
        where TRequest : notnull
    {
        private readonly IUnitOfWork _unitOfWork;
        public UnitOfWorkBehavior(IUnitOfWork unitOfWork) 
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<TResponse> Handle(
            TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken = default)
        {
            if (IsNotCommand())
            {
                return await next();
            }

            //using var transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

            var response = await next();

            await _unitOfWork.SaveChangesAsync(cancellationToken);
            //_unitOfWork.SaveChanges();

            //transactionScope.Complete();

            return response;
        }

        private static bool IsNotCommand()
        {
            return !typeof(TRequest).Name.EndsWith("Command");
        }
    }
}
