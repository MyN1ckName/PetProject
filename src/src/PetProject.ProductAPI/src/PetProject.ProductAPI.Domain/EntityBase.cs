namespace PetProject.ProductAPI.Domain;
public abstract class EntityBase<T> : IEntity<T>
{
    public T Id { get; init; }
}
