namespace YellowPushSMSPackage
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Text;
    using Newtonsoft.Json;
    using RestSharp;
    using YellowPushSMSPackage.Models;

    /// <summary>
    /// YellowPushSMS Class
    /// </summary>
    public class YellowPushSMS
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="YellowPushSMS"/> class.
        /// </summary>
        /// <param name="username">The username registered in the system.</param>
        /// <param name="password">The password associated with the user in the system.</param>
        public YellowPushSMS(string username, string password)
        {
            this.Username = username;
            this.Password = password;
        }

        /// <summary>
        /// Gets the username.
        /// </summary>
        /// <value>
        /// The username.
        /// </value>
        public string Username { get; }

        /// <summary>
        /// Gets the password.
        /// </summary>
        /// <value>
        /// The password.
        /// </value>
        public string Password { get; }

        /// <summary>
        /// Sends the SMS.
        /// </summary>
        /// <param name="from">Sender name.</param>
        /// <param name="message">The message.</param>
        /// <param name="cellphoneNumbers">The cellphone numbers to send the text message (The cellphone number must also include the country code).</param>
        /// <returns>The API response <see cref="YellowPushSMSResponse"/></returns>
        public YellowPushSMSResponse SendSMS(string from, string message, params string[] cellphoneNumbers)
        {
            string to = ConvertParamsToString(cellphoneNumbers, ",");
            YellowPushSMSResponse response = SendMessage(from, message, to);
            return response;
        }

        /// <summary>
        /// Sends the SMS.
        /// </summary>
        /// <param name="from">Sender name.</param>
        /// <param name="message">The message.</param>
        /// <param name="cellphoneNumbers">The cellphone numbers separated by commas to send the text message (The cellphone number must also include the country code).</param>
        /// <returns>The API response <see cref="YellowPushSMSResponse"/></returns>
        public YellowPushSMSResponse SendSMS(string from, string message, string cellphoneNumbers)
        {
            YellowPushSMSResponse response = SendMessage(from, message, cellphoneNumbers);
            return response;
        }

        /// <summary>
        /// Bulks the send SMS.
        /// </summary>
        /// <param name="listMessages">The list messages.</param>
        /// <returns>The API response <see cref="YellowPushSMSResponse"/></returns>
        public YellowPushSMSResponse BulkSendSMS(List<BulkSMS> listMessages)
        {
            try
            {
                string acc_id = string.Empty;
                string token = string.Empty;

                IRestResponse<List<Account>> accountResponse = GetAccount(Username, Password);

                if (accountResponse.StatusCode == HttpStatusCode.OK)
                    acc_id = accountResponse.Data.FirstOrDefault().Id;
                else
                    return Mapper(accountResponse);

                IRestResponse<Dictionary<string, string>> authResponse = GetAuth(Username, Password);

                if (authResponse.StatusCode == HttpStatusCode.OK)
                    token = authResponse.Data["token"];
                else
                    return Mapper(authResponse);

                IRestResponse sendResponse = BulkSendMessage(listMessages, token, acc_id);
                return Mapper(sendResponse);
            }
            catch (Exception ex)
            {
                return new YellowPushSMSResponse
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    ErrorMessage = ex.Message
                };
            }
        }

        /// <summary>
        /// Gets the message status.
        /// </summary>
        /// <param name="messsageId">The messsage identifier.</param>
        /// <param name="sendDate">The send date.</param>
        /// <returns>The API response <see cref="YellowPushSMSResponse"/></returns>
        public YellowPushSMSResponse GetMessageStatus(string messsageId, DateTime sendDate)
        {
            string token = string.Empty;

            try
            {
                IRestResponse<Dictionary<string, string>> authResponse = GetAuth(Username, Password);

                if (authResponse.StatusCode == HttpStatusCode.OK)
                    token = authResponse.Data["token"];
                else
                    return Mapper(authResponse);

                IRestResponse smsEdrResponse = SmsEdr(token, messsageId, sendDate);

                return Mapper(smsEdrResponse);
            }
            catch (Exception ex)
            {
                return new YellowPushSMSResponse
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    ErrorMessage = ex.Message
                };
            }
        }

        /// <summary>
        /// Gets the account.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <returns>The API response. <see cref="YellowPushSMSResponse"/></returns>
        private IRestResponse<List<Account>> GetAccount(string username, string password)
        {
            RestClient client = new RestClient(Constant.URL_API_REST_ACCOUNT);
            string credentials = Convert.ToBase64String(Encoding.GetEncoding("ISO-8859-1").GetBytes($@"{username}:{password}"));
            RestRequest request = new RestRequest(Method.GET);
            request.AddHeader("Authorization", $@"Basic {credentials}");
            IRestResponse<List<Account>> response = client.Execute<List<Account>>(request);

            return response;
        }

        /// <summary>
        /// Gets the authentication.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <returns>The API response</returns>
        private IRestResponse<Dictionary<string, string>> GetAuth(string username, string password)
        {
            RestClient client = new RestClient(Constant.URL_API_REST_AUTH);
            string credentials = Convert.ToBase64String(Encoding.GetEncoding("ISO-8859-1").GetBytes($@"{username}:{password}"));
            RestRequest request = new RestRequest(Method.GET);
            request.AddHeader("Authorization", $@"Basic {credentials}");
            IRestResponse<Dictionary<string, string>> response = client.Execute<Dictionary<string, string>>(request);

            return response;
        }

        /// <summary>
        /// Sends the message.
        /// </summary>
        /// <param name="from">Sender name.</param>
        /// <param name="message">The message.</param>
        /// <param name="to">The cellphone numbers separated by commas to send the text message (The cellphone number must also include the country code).</param>
        /// <returns>The API response </returns>
        private YellowPushSMSResponse SendMessage(string from, string message, string to)
        {
            try
            {
                string acc_id = string.Empty;
                string token = string.Empty;

                IRestResponse<List<Account>> accountResponse = GetAccount(Username, Password);

                if (accountResponse.StatusCode == HttpStatusCode.OK)
                    acc_id = accountResponse.Data.FirstOrDefault().Id;
                else
                    return Mapper(accountResponse);

                IRestResponse<Dictionary<string, string>> authResponse = GetAuth(Username, Password);

                if (authResponse.StatusCode == HttpStatusCode.OK)
                    token = authResponse.Data["token"];
                else
                    return Mapper(authResponse);

                IRestResponse sendResponse = SendMessage(token, acc_id, to, from, message);
                return Mapper(sendResponse);
            }
            catch (Exception ex)
            {
                return new YellowPushSMSResponse
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    ErrorMessage = ex.Message
                };
            }
        }

        /// <summary>
        /// Sends the message.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <param name="acc_id">The acc identifier.</param>
        /// <param name="to">The cellphone numbers separated by commas to send the text message (The cellphone number must also include the country code).</param>
        /// <param name="from">Sender name.</param>
        /// <param name="message">The message.</param>
        /// <returns>The API response</returns>
        private IRestResponse SendMessage(string token, string acc_id, string to, string from, string message)
        {
            RestClient client = new RestClient(Constant.URL_API_REST_SENDSMS);
            RestRequest request = new RestRequest(Method.POST);
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddHeader("authorization", $@"Bearer {token}");
            var postData = $@"acc_id={acc_id}&to={to}&from={from}&message={message}";
            request.AddParameter("application/x-www-form-urlencoded", postData, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            return response;
        }

        /// <summary>
        /// Bulks send message.
        /// </summary>
        /// <param name="listMessages">The list messages.</param>
        /// <param name="token">The token.</param>
        /// <param name="acc_id">The acc identifier.</param>
        /// <returns>The API response</returns>
        private IRestResponse BulkSendMessage(List<BulkSMS> listMessages, string token, string acc_id)
        {
            string baseUrl = $@"{Constant.URL_API_REST_BULKSENDSMS}?acc_id={acc_id}";

            var client = new RestClient(baseUrl);
            var request = new RestRequest(Method.POST);
            request.AddHeader("Authorization", $@"Bearer {token}");
            request.AddHeader("Content-Type", "application/json");

            string json = JsonConvert.SerializeObject(listMessages);

            request.AddParameter("undefined", json, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            return response;
        }

        /// <summary>
        /// Gets the information about message status.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <param name="messsageId">The messsage identifier.</param>
        /// <param name="sendDate">The send date.</param>
        /// <returns>The API response</returns>
        private IRestResponse SmsEdr(string token, string messsageId, DateTime sendDate)
        {
            DateTime startDate = sendDate.AddDays(-1);
            DateTime endDate = sendDate.AddDays(1);

            string baseUrl = $@"{Constant.URL_API_REST_SMSEDR}?client_message_id={messsageId}&start_date={startDate.ToString("yyyy-MM-dd")}&end_date={endDate.ToString("yyyy-MM-dd")}";
            RestClient client = new RestClient(baseUrl);
            RestRequest request = new RestRequest(Method.GET);
            request.AddHeader("Authorization", $@"Bearer {token}");
            IRestResponse response = client.Execute(request);

            return response;
        }

        /// <summary>
        /// Mappers the specified rest response.
        /// </summary>
        /// <param name="restResponse">The rest response.</param>
        /// <returns>The YellowPushSMSResponse <see cref="YellowPushSMSResponse"/></returns>
        private YellowPushSMSResponse Mapper(IRestResponse restResponse)
        {
            return new YellowPushSMSResponse(restResponse.IsSuccessful, restResponse.Headers)
            {
                Content = restResponse.Content,
                ContentEncoding = restResponse.ContentEncoding,
                ContentLength = restResponse.ContentLength,
                ContentType = restResponse.ContentType,
                ErrorException = restResponse.ErrorException,
                ProtocolVersion = restResponse.ProtocolVersion,
                ErrorMessage = restResponse.ErrorMessage,
                RawBytes = restResponse.RawBytes,
                Request = restResponse.Request,
                ResponseStatus = restResponse.ResponseStatus,
                ResponseUri = restResponse.ResponseUri,
                Server = restResponse.Server,
                StatusCode = restResponse.StatusCode,
                StatusDescription = restResponse.StatusDescription
            };
        }

        /// <summary>
        /// Converts the parameters to string.
        /// </summary>
        /// <param name="texstList">The texst list.</param>
        /// <param name="character">The character.</param>
        /// <returns>The text separated by a character</returns>
        private string ConvertParamsToString(string[] texstList, string character)
        {
            StringBuilder text = new StringBuilder();
            texstList.ToList().ForEach(cell =>
            {
                text.Append($@"{cell}{character}");
            });

            return text.ToString().Remove(text.Length - 1);
        }
    }
}