namespace GoLogs.Interfaces
{
    public interface IHistory : IEntity
    {
        // ReSharper disable once InconsistentNaming
        string DOOrderNumber { get; }
        int StateId { get; }
    }
}
