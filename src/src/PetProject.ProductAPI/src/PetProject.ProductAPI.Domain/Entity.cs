namespace PetProject.ProductAPI.Domain;
public abstract class Entity<T> : IEntity<T>
{
    public T Id { get; init; }
}
