namespace chatBotIaCore.Infra.Interfaces
{
    public interface IBaseInterface<T> where T : class
    {
        Task<List<T>> getAllAsync();
        Task<T?> getByIdAsync(int id);
        Task<T> createAsync(T model);
        Task<T> updateAsync(T model);
        Task<bool> deleteAsync(int id); 
    }
}
