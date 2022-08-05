using RedisPagination.Data;


namespace RedisPagination.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppEfDbContext dbContext;
        public bool disposed;

        public UnitOfWork(AppEfDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task CompleteAsync()
        {
            await dbContext.SaveChangesAsync();
        }
        protected virtual void Clean(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    dbContext.Dispose();
                }
            }
            this.disposed = true;
        }
        public void Dispose()
        {
            Clean(true);
            GC.SuppressFinalize(this);
        }
    }
}
