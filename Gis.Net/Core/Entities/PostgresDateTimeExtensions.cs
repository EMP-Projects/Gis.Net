using System;
namespace TeamSviluppo
{
    /// <summary>
    /// Estensione del tipo DateTime necessario per gestire correttamente le date con Postgres
    /// </summary>
    public static class PostgresDateTimeExtensions
    {
        /// <summary>
        /// Analogo a <see cref="DateTime.Date"/>, ma tenendo conto del fatto che la timezone corrente potrebbe non essere Utc
        /// </summary>
        /// <remarks>
        /// <para>Origine del problema, il fatto che la data "xxxT22:00:00Z" se si fa il .Date diventa "xxxT00:00:00Z"</para>
        /// </remarks>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static DateTime UtcDate(this DateTime dateTime) => new DateTimeOffset(dateTime).DateTime;
    }
}

