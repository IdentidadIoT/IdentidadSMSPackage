using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;

namespace IdentidadSMSPackage.Models
{
    /// <summary>
    /// Object Response 
    /// </summary>
    public class IdentidadSmsResponse : IRestResponse
    {
        /// <summary>
        /// Creating an Instance of IdentidadSmsResponse Class
        /// </summary>
        public IdentidadSmsResponse()
        {
        }

        /// <summary>
        /// Creating an Instance of IdentidadSmsResponse Class
        /// </summary>
        /// <param name="isSuccessful"></param>
        /// <param name="headers"></param>
        public IdentidadSmsResponse(bool isSuccessful, IList<Parameter> headers)
        {
            IsSuccessful = isSuccessful;
            Headers = headers;
        }

        /// <summary>
        /// 
        /// </summary>
        public int MyProperty { get; set; }

        /// <summary>
        /// API Request
        /// </summary>
        public IRestRequest Request { get; set; }

        /// <summary>
        /// Content Type of the request
        /// </summary>
        public string ContentType { get; set; }

        /// <summary>
        /// Content Length of the request
        /// </summary>
        public long ContentLength { get; set; }

        /// <summary>
        /// Content Encoding of the request
        /// </summary>
        public string ContentEncoding { get; set; }

        /// <summary>
        /// Content of the response
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// HTTP Status Code of the response
        /// </summary>
        public HttpStatusCode StatusCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsSuccessful { get; }

        /// <summary>
        /// Status Description
        /// </summary>
        public string StatusDescription { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public byte[] RawBytes { get; set; }

        /// <summary>
        /// Response Uri
        /// </summary>
        public Uri ResponseUri { get; set; }

        /// <summary>
        /// Server
        /// </summary>
        public string Server { get; set; }


        /// <summary>
        /// Cookies
        /// </summary>
        public IList<RestResponseCookie> Cookies { get; }

        /// <summary>
        /// Headers of the response
        /// </summary>
        public IList<Parameter> Headers { get; }

        /// <summary>
        /// Response Status
        /// </summary>
        public ResponseStatus ResponseStatus { get; set; }

        /// <summary>
        /// Error Message
        /// </summary>
        public string ErrorMessage { get; set; }

        /// <summary>
        /// Error Exception
        /// </summary>
        public Exception ErrorException { get; set; }

        /// <summary>
        /// Protocol Version
        /// </summary>
        public Version ProtocolVersion { get; set; }

    }
}
