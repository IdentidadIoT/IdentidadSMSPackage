namespace YellowPushSMSPackageUnitTest
{
    using System;
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
        /// Initializes a new instance of the <see cref="YellowPushSMSUnitTest"/> class.
        /// </summary>
        public YellowPushSMSUnitTest()
        {
            sms = new YellowPushSMS("prueda@identidadtelecom.net", "Ruedamillos1991");
        }

        /// <summary>
        /// The SMS
        /// </summary>
        private YellowPushSMS sms;

        /// <summary>
        /// Sends the SMS.
        /// </summary>
        [TestMethod]
        public void SendSms()
        {
            YellowPushSMSResponse response = sms.SendSms("Damian", "Esto es una prueba desde Test Unitario", "573163985157");
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