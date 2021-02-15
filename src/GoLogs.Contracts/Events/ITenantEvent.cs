using GoLogs.Interfaces;

namespace GoLogs.Events
{
    public interface ITenantEvent
    {
        ITenant Tenant { get; }
    }
}
