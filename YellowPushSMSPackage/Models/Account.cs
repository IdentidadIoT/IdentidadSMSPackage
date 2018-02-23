namespace YellowPushSMSPackage.Models
{
    /// <summary>
    /// Account Class
    /// </summary>
    internal class Account
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the car identifier.
        /// </summary>
        /// <value>
        /// The car identifier.
        /// </value>
        public string Car_id { get; set; }

        /// <summary>
        /// Gets or sets the balance updated.
        /// </summary>
        /// <value>
        /// The balance updated.
        /// </value>
        public string Balance_updated { get; set; }

        /// <summary>
        /// Gets or sets the currency code.
        /// </summary>
        /// <value>
        /// The currency code.
        /// </value>
        public string Currency_code { get; set; }

        /// <summary>
        /// Gets or sets the balance.
        /// </summary>
        /// <value>
        /// The balance.
        /// </value>
        public string Balance { get; set; }

        /// <summary>
        /// Gets or sets the in credit limit.
        /// </summary>
        /// <value>
        /// The in credit limit.
        /// </value>
        public string In_credit_limit { get; set; }

        /// <summary>
        /// Gets or sets the acc send balance alerts.
        /// </summary>
        /// <value>
        /// The acc send balance alerts.
        /// </value>
        public string Acc_send_balance_alerts { get; set; }
    }
}