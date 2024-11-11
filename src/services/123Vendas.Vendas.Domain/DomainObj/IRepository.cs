namespace _123Vendas.Vendas.Domain.DomainObj;

public interface IRepository<T> : IDisposable where T : IAggregateRoot
{
    IUnitOfWork UnitOfWork { get; }
}
