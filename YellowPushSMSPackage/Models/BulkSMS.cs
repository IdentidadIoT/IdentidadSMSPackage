namespace YellowPushSMSPackage.Models
{
    using Newtonsoft.Json;

    /// <summary>
    /// BulkSMS Class
    /// </summary>
    public class BulkSMS
    {
        /// <summary>
        /// Gets or sets from.
        /// </summary>
        /// <value>
        /// The From.
        /// </value>
        [JsonProperty("from")]
        public string From { get; set; }

        /// <summary>
        /// Gets or sets CellphoneNumber.
        /// </summary>
        /// <value>
        /// The CellphoneNumber.
        /// </value>
        [JsonProperty("to")]
        public string CellphoneNumber { get; set; }

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        /// <value>
        /// The message.
        /// </value>
        [JsonProperty("message")]
        public string Message { get; set; }
    }
}