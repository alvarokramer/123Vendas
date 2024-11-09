namespace _123Vendas.Vendas.Domain.DomainObj;

public interface IUnitOfWork
{
    Task<bool> Commit(CancellationToken cancellationToken = default);
}
