namespace Meetme.ProfileService.BLL.Interfaces;

public interface ICrud<TModel> where TModel : class
{
	Task<IEnumerable<TModel>> GetAllAsync(CancellationToken cancellationToken);

	Task<TModel> GetByIdAsync(Guid id, CancellationToken cancellationToken);

	Task AddAsync(TModel model, CancellationToken cancellationToken);

	Task UpdateAsync(TModel model, CancellationToken cancellationToken);

	Task DeleteAsync(Guid id, CancellationToken cancellationToken);
}
