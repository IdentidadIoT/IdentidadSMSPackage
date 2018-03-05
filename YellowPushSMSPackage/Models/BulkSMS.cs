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
        /// Gets or sets the mobile number.
        /// </summary>
        /// <value>
        /// The mobile number.
        /// </value>
        [JsonProperty("to")]
        public string MobileNumber { get; set; }

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