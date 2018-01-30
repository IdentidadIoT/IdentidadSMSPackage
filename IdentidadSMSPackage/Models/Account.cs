namespace IdentidadSMSPackage.Models
{
    /// <summary>
    /// Object Response of the Alaris API
    /// </summary>
    public class Account
    {
        /// <summary>
        /// Account ID
        /// </summary>
        public string id { get; set; }

        /// <summary>
        /// Carrier ID 
        /// </summary>
        public string car_id { get; set; }

        /// <summary>
        /// Balance updated date
        /// </summary>
        public string balance_updated { get; set; }

        /// <summary>
        /// Currency code
        /// </summary>
        public string currency_code { get; set; }

        /// <summary>
        /// Balance 
        /// </summary>
        public string balance { get; set; }

        /// <summary>
        /// Client credit limit. Unlimited credit limit (null value in agreement) displayed as 99999999999 
        /// </summary>
        public string in_credit_limit { get; set; }

        /// <summary>
        /// Send balance alerts ([1] - send, 0 - do not send) 
        /// </summary>
        public string acc_send_balance_alerts { get; set; }
    }
}
