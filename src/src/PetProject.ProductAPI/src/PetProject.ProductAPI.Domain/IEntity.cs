namespace PetProject.ProductAPI.Domain;

public interface IEntity<TKey> : IEntity
{
    TKey Id { get; init; }
}

public interface IEntity
{

}