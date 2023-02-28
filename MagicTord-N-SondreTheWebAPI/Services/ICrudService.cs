namespace MagicTord_N_SondreTheWebAPI.Services
{
    public interface ICrudService<T, ID>
    {
        Task<ICollection<T>> GetAllAsync();

        Task<T> GetByIdAsync(ID id);

        Task AddAsync(T entity);

        Task UpdateAsync(T entity);

        Task DeleteAsync(ID id);
    }
}
