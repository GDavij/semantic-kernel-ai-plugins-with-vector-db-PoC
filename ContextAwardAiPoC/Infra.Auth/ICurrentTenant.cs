namespace Infra.Auth;

public interface ICurrentTenant
{
    public string TenantId { get; }
}