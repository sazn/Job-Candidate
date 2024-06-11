using JobCandidate.ORM.Abstractions.RepositoryPattern;
using JobCandidate.ORM.Context;

namespace JobCandidate.ORM.Abstractions.UnitOfWorkPattern
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly JobCandidateDbContext _dbContext;
        private readonly Dictionary<Type, object> _repositories = new();

        public UnitOfWork(JobCandidateDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IGenericRepository<T> Repository<T>() where T : class
        {
            if (_repositories.ContainsKey(typeof(T)))
            {
                return _repositories[typeof(T)] as IGenericRepository<T>;
            }

            var repository = new GenericRepository<T>(_dbContext);
            _repositories.Add(typeof(T), repository);
            return repository;
        }

        public async Task<int> CompleteAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
