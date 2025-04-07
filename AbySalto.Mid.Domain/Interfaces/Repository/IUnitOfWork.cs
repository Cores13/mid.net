namespace AbySalto.Mid.Domain.Interfaces.Repository
{
    public interface IUnitOfWork
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

        public void SaveChanges();
    }
}
