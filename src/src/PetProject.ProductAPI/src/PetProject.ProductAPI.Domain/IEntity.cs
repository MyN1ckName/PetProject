namespace PetProject.ProductAPI.Domain;

public interface IEntity<T>
{
    T Id { get; init; }
}