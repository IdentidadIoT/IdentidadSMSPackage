namespace YellowPushSMSPackage.Models
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using RestSharp;

    /// <summary>
    /// Object Response
    /// </summary>
    public class YellowPushSMSResponse : IRestResponse
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="YellowPushSMSResponse"/> class.
        /// </summary>
        public YellowPushSMSResponse()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="YellowPushSMSResponse"/> class.
        /// </summary>
        /// <param name="headers">The headers.</param>
        public YellowPushSMSResponse(IList<Parameter> headers)
        {
            Headers = headers;
        }

        /// <summary>
        /// Gets or sets my property.
        /// </summary>
        /// <value>
        /// My property.
        /// </value>
        public int MyProperty { get; set; }

        /// <summary>
        /// Gets or sets the request.
        /// </summary>
        /// <value>
        /// The request.
        /// </value>
        public IRestRequest Request { get; set; }

        /// <summary>
        /// Gets or sets the type of the content.
        /// </summary>
        /// <value>
        /// The type of the content.
        /// </value>
        public string ContentType { get; set; }

        /// <summary>
        /// Gets or sets the length of the content.
        /// </summary>
        /// <value>
        /// The length of the content.
        /// </value>
        public long ContentLength { get; set; }

        /// <summary>
        /// Gets or sets the content encoding.
        /// </summary>
        /// <value>
        /// The content encoding.
        /// </value>
        public string ContentEncoding { get; set; }

        /// <summary>
        /// Gets or sets the content.
        /// </summary>
        /// <value>
        /// The content.
        /// </value>
        public string Content { get; set; }

        /// <summary>
        /// Gets or sets the status code.
        /// </summary>
        /// <value>
        /// The status code.
        /// </value>
        public HttpStatusCode StatusCode { get; set; }

        /// <summary>
        /// Gets or sets the status description.
        /// </summary>
        /// <value>
        /// The status description.
        /// </value>
        public string StatusDescription { get; set; }

        /// <summary>
        /// Gets or sets the raw bytes.
        /// </summary>
        /// <value>
        /// The raw bytes.
        /// </value>
        public byte[] RawBytes { get; set; }

        /// <summary>
        /// Gets or sets the response URI.
        /// </summary>
        /// <value>
        /// The response URI.
        /// </value>
        public Uri ResponseUri { get; set; }

        /// <summary>
        /// Gets or sets the server.
        /// </summary>
        /// <value>
        /// The server.
        /// </value>
        public string Server { get; set; }

        /// <summary>
        /// Gets the cookies.
        /// </summary>
        /// <value>
        /// The cookies.
        /// </value>
        public IList<RestResponseCookie> Cookies { get; }

        /// <summary>
        /// Gets the headers.
        /// </summary>
        /// <value>
        /// The headers.
        /// </value>
        public IList<Parameter> Headers { get; }

        /// <summary>
        /// Gets or sets the response status.
        /// </summary>
        /// <value>
        /// The response status.
        /// </value>
        public ResponseStatus ResponseStatus { get; set; }

        /// <summary>
        /// Gets or sets the error message.
        /// </summary>
        /// <value>
        /// The error message.
        /// </value>
        public string ErrorMessage { get; set; }

        /// <summary>
        /// Gets or sets the error exception.
        /// </summary>
        /// <value>
        /// The error exception.
        /// </value>
        public Exception ErrorException { get; set; }

        /// <summary>
        /// Gets or sets the protocol version.
        /// </summary>
        /// <value>
        /// The protocol version.
        /// </value>
        public Version ProtocolVersion { get; set; }
    }
}