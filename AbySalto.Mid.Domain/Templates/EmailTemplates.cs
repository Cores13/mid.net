namespace AbySalto.Mid.Domain.Templates
{
    public class EmailTemplates
    {
        public static string GetEmailTemplatesPath (string currentPath = null)
        {
            var directory = new DirectoryInfo(
            currentPath ?? Directory.GetCurrentDirectory());
            while (directory != null && !directory.GetFiles("*.sln").Any())
            {
                directory = directory.Parent;
            }
            var path = Path.Combine(directory.FullName, "AbySalto.Mid.Domain\\Templates").ToString();
            
            return path;
        }
    }
}
