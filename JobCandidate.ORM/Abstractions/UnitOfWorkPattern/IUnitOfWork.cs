using JobCandidate.ORM.Abstractions.RepositoryPattern;

namespace JobCandidate.ORM.Abstractions.UnitOfWorkPattern
{
    public interface IUnitOfWork
    {
        IGenericRepository<T> Repository<T>() where T : class;
        Task<int> CompleteAsync();
    }
}
