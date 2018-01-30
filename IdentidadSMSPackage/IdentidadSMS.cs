﻿using IdentidadSMSPackage.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace IdentidadSMSPackage
{
    /// <summary>
    /// IdentidadSMS Class
    /// </summary>
    public class IdentidadSMS
    {
        /// <summary>
        /// Username registered in the system
        /// </summary> 
        public string Username { get; }

        /// <summary>
        /// Password associated with the user in the system
        /// </summary>
        public string Password { get; }

        /// <summary>
        /// Creating an Instance of IdentidadSMS Class
        /// </summary>
        /// <param name="username">Username registered in the system</param>
        /// <param name="password">Password associated with the user in the system</param>
        public IdentidadSMS(string username, string password)
        {
            this.Username = username;
            this.Password = password;
            
        }

        /// <summary>
        /// Send the message
        /// </summary>      
        /// <param name="from">The name of the sender</param>
        /// <param name="message">Text message</param>
        /// <param name="cellphoneNumbers">Mobile numbers to send the text message(The mobile number must also include the country code)</param>
        /// <returns></returns>
        public IdentidadSmsResponse SendSms(string from, string message, params string[] cellphoneNumbers)
        {
            string to = ConvertParamsToString(cellphoneNumbers, ",");
            IdentidadSmsResponse response = SendMessage(from, message, to);
            return response;
        }

        /// <summary>
        /// Send the text message
        /// </summary>      
        /// <param name="from">The name of the sender</param>
        /// <param name="message">Text message</param>
        /// <param name="cellphoneNumbers">Mobile numbers separated by commas to send the text message(The mobile number must also include the country code)</param>
        /// <returns></returns>
        public IdentidadSmsResponse SendSms(string from, string message, string cellphoneNumbers)
        {
            IdentidadSmsResponse response = SendMessage(from, message, cellphoneNumbers);
            return response;
        }

        /// <summary>
        /// Get the information about message status
        /// </summary>
        /// <param name="messsageId">Message identifier</param>
        /// <param name="sendDate">Date of dispatch the message</param>
        /// <returns></returns>
        public IdentidadSmsResponse GetMessageStatus(string messsageId, DateTime sendDate)
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
                return new IdentidadSmsResponse
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    ErrorMessage = ex.Message
                };
            }

        }
        
        /// <summary>
        /// Get information data about the Account
        /// </summary>
        /// <param name="username">Username registered in the system</param>
        /// <param name="password">Password associated with the user in the system</param>
        /// <returns></returns>
        private IRestResponse<List<Account>> GetAccount(string username, string password)
        {
            RestClient client = new RestClient(Constant.URL_API_REST_ACCOUNT);
            string credentials = Convert.ToBase64String(Encoding.GetEncoding("ISO-8859-1").GetBytes(username + ":" + password));
            RestRequest request = new RestRequest(Method.GET);
            request.AddHeader("Authorization", $@"Basic {credentials}");
            IRestResponse<List<Account>> response = client.Execute<List<Account>>(request);

            return response;
        }

        /// <summary>
        /// Get token from the API
        /// </summary>
        /// <param name="username">Username registered in the system</param>
        /// <param name="password">Password associated with the user in the system</param>
        /// <returns></returns>
        private IRestResponse<Dictionary<string, string>> GetAuth(string username, string password)
        {
            RestClient client = new RestClient(Constant.URL_API_REST_AUTH);
            string credentials = Convert.ToBase64String(Encoding.GetEncoding("ISO-8859-1").GetBytes(username + ":" + password));
            RestRequest request = new RestRequest(Method.GET);
            request.AddHeader("Authorization", $@"Basic {credentials}");
            IRestResponse<Dictionary<string, string>> response = client.Execute<Dictionary<string, string>>(request);

            return response;
        }

        /// <summary>
        /// Send the text message
        /// </summary>
        /// <param name="from">The name of the sender</param>
        /// <param name="message">Text message</param>
        /// <param name="to">Mobile numbers separated by commas to send the text message(The mobile number must also include the country code)</param>
        /// <returns></returns>
        private IdentidadSmsResponse SendMessage(string from, string message, string to)
        {
            try
            {
                string acc_id = string.Empty;
                string token = string.Empty;

                IRestResponse<List<Account>> accountResponse = GetAccount(Username, Password);

                if (accountResponse.StatusCode == HttpStatusCode.OK)
                    acc_id = accountResponse.Data.FirstOrDefault().id;
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
                return new IdentidadSmsResponse
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    ErrorMessage = ex.Message
                };
            }
        }

        /// <summary>
        /// Send the text message to Alaris API
        /// </summary>
        /// <param name="token">Token generated by the API to authentication</param>
        /// <param name="acc_id">Account ID (for trusted connection only)</param>
        /// <param name="to">Mobile numbers separated by commas to send the text message(The mobile number must also include the country code)</param>
        /// <param name="from">The name of the sender</param>
        /// <param name="message">Text message</param>
        /// <returns></returns>
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
        /// Get the information about message status
        /// </summary>
        /// <param name="token">Token generated by the API to authentication</param>
        /// <param name="messsageId">Message identifier</param>
        /// <param name="sendDate">Date of dispatch the message</param>
        /// <returns></returns>
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
        /// Mapping IRestResponse entity to RestResponse entity
        /// </summary>
        /// <param name="restResponse">Type IRestResponse object to map</param>
        /// <returns></returns>
        private IdentidadSmsResponse Mapper(IRestResponse restResponse)
        {
            return new IdentidadSmsResponse(restResponse.IsSuccessful, restResponse.Headers)
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
        /// Convert string array to string separated by a character
        /// </summary>
        /// <param name="texstList">String array</param>
        /// <param name="charcter">Character to separate string array</param>
        /// <returns></returns>
        private string ConvertParamsToString(string[] texstList, string charcter)
        {
            string text = string.Empty;
            texstList.ToList().ForEach(cell =>
            {
                text += cell + charcter;
            });

            text = text.Remove(text.Length - 1);
            return text;
        }
    }
}
