namespace chatBotIaCore.Services.Interfaces.Base
{
    public interface IBaseServices<T> where T : class
    {
        Task<List<T>> getAllAsync();
        Task<T?> getByIdAsync(int id);
        Task<T> createAsync(T model);
        Task<T> updateAsync(T model);
        Task<bool> deleteAsync(int id);
    }
}
