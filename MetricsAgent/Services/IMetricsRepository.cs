namespace MetricsAgent.Services
{
    public interface IMetricsRepository<T> where T : class
    {
        
        IList<T> GetAll();
        T GetById(int id);
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
