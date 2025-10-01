namespace Application.Mappers.Primitives;

public interface IEntityMapper<T, TDto>
{
    TDto ToDto(T entity);
}
