namespace YellowPushSMSPackageUnitTest
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using YellowPushSMSPackage;
    using YellowPushSMSPackage.Models;

    /// <summary>
    /// YellowPushSMSUnitTest Class
    /// </summary>
    [TestClass]
    public class YellowPushSMSUnitTest
    {
        /// <summary>
        /// The SMS
        /// </summary>
        private YellowPushSMS sms;

        /// <summary>
        /// Initializes a new instance of the <see cref="YellowPushSMSUnitTest"/> class.
        /// </summary>
        public YellowPushSMSUnitTest()
        {
            sms = new YellowPushSMS("achavez@identidadiot.com", "1d3nt1d@d");
        }

        /// <summary>
        /// Sends the SMS.
        /// </summary>
        [TestMethod]
        public void SendSMS()
        {
            YellowPushSMSResponse response = sms.SendSMS("Damian", "Esto es una prueba desde Test Unitario", "573163985157");
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode, $@"El status code de la respuesta del mensaje es {response.StatusCode.ToString()}");
        }

        /// <summary>
        /// Bulks the send SMS.
        /// </summary>
        [TestMethod]
        public void BulkSendSMS()
        {
            List<BulkSMS> messages = new List<BulkSMS>()
            {
                new BulkSMS() { From = "Damian", MobileNumber = "573163985157", Message = "Prueba desde Test Unitario Bulk Damian" },
                new BulkSMS() { From = "Jose", MobileNumber = "573175564608", Message = "Prueba desde Test Unitario Bulk Jose" }
            };

            YellowPushSMSResponse response = sms.BulkSendSMS(messages);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode, $@"El status code de la respuesta del mensaje es {response.StatusCode.ToString()}");
        }

        /// <summary>
        /// Gets the message status.
        /// </summary>
        [TestMethod]
        public void GetMessageStatus()
        {
            YellowPushSMSResponse response = sms.GetMessageStatus("5a5600d4-f60a-e9fa-dd05-e53a4b385450", new DateTime(2018, 1, 30));
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode, $@"El status code de la respuesta del mensaje es {response.StatusCode.ToString()}");
        }
    }
}