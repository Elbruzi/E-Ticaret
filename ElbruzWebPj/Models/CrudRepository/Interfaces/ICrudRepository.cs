namespace ElbruzWebPj.Models.CrudRepository.Interfaces
{
    public interface ICrudRepository<T> where T : class

    {
        Task<bool> Create(T entity);
        Task<List<T>> Read(string Main_Or_All);
        Task<bool> Update(T Entity);
        Task Delete(T Entity);
        Task<bool> CreateControl(T Entity);
        Task<T> GetDetails(int Entity);


    }
}
