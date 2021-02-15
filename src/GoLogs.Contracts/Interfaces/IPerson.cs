namespace GoLogs.Interfaces
{
    public interface IPerson : IEntity
    {
        /// <summary>
        ///     Person's e-mail address.
        /// </summary>
        string Email { get; }

        /// <summary>
        ///     Person's first name.
        /// </summary>
        string Firstname { get; }

        /// <summary>
        ///     Person's last name.
        /// </summary>
        string Lastname { get; }

        /// <summary>
        ///     Nomor pokok wajib pajak. Unformatted.
        /// </summary>
        string Npwp { get; }

        int? CompanyId { get; }
        int? CompanyRoleId { get; }
    }
}
