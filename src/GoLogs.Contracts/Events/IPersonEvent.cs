using GoLogs.Interfaces;

namespace GoLogs.Events
{
    public interface IPersonEvent
    {
        IPerson Person { get; }
    }
}
