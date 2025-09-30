namespace SharedKernel.Interfaces.Base
{
    public interface IReadRepository<T> where T : class
    {
        Task<T?> GetByIdAsync(int id);
        Task<List<T>> GetAllAsync();
    }
}
