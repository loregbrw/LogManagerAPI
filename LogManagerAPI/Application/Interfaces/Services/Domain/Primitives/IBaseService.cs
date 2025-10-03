namespace Application.Interfaces.Services.Domain.Primitives;

using System.Linq.Expressions;
using Application.Entities.Primitives;
using Application.Models.Entities.Primitives;
using Application.Models.Pagination;


public interface IBaseService<T, TDto> where T : BaseEntity where TDto : BaseDto
{
   
    Task<TDto?> GetByIdAsync(Guid id);

    Task<IEnumerable<TDto>> GetAllAsync();

    Task<PaginatedResult<TDto>> GetPaginatedAsync(int page, int size, Expression<Func<T, bool>>? filter = null);

   
    Task<TDto> CreateAsync(T entity);

  
    Task<TDto> UpdateAsync(T entity);


    Task SoftDeleteAsync(Guid id);
}