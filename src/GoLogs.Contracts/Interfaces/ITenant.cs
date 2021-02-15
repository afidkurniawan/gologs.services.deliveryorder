namespace GoLogs.Interfaces
{
    public interface ITenant : IEntity
    {
        int CompanyId { get; }

        /// <summary>
        ///     The name of the tenant to be used for db name. The name must be a valid SQL identifier.
        /// </summary>
        string TenantName { get; }
    }
}
