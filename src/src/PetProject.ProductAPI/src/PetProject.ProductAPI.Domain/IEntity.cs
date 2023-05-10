namespace PetProject.ProductAPI.Domain;

public interface IEntity<TKey>
{
    TKey Id { get; init; }
}