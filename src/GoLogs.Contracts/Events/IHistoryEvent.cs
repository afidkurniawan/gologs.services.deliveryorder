using GoLogs.Interfaces;

namespace GoLogs.Contracts.Events
{
    public interface IHistoryEvent
    {
        IHistory History { get; }
    }
}
