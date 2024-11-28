namespace Meetme.ProfileService.BLL.Interfaces;

public interface IGenericService<TModel, TCreateModel, TUpdateModel>
{
	Task<IEnumerable<TModel>> GetAllAsync(CancellationToken cancellationToken);

	Task<TModel> GetByIdAsync(Guid id, CancellationToken cancellationToken);

	Task AddAsync(TCreateModel model, CancellationToken cancellationToken);

	Task UpdateAsync(Guid id,TUpdateModel model, CancellationToken cancellationToken);

	Task DeleteAsync(Guid id, CancellationToken cancellationToken);
}
