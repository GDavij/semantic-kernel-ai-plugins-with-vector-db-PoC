namespace Infra.Auth;

public class CurrentTenant : ICurrentTenant
{
    public string TenantId => "default-tenant-id"; //TODO: Replace with actual logic to retrieve tenant ID
}