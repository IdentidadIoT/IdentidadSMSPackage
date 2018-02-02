using System;
using System.Net;
using YellowPushSMSPackage;
using YellowPushSMSPackage.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace YellowPushSMSPackageUnitTest
{
    [TestClass]
    public class YellowPushSMSUnitTest
    {
        private YellowPushSMS SMS;

        public YellowPushSMSUnitTest()
        {
            SMS = new YellowPushSMS("prueda@identidadtelecom.net", "Ruedamillos1991");
        }

        [TestMethod]
        public void SendSms()
        {
            YellowPushSMSResponse response = SMS.SendSms("Damian", "Esto es una prueba desde Test Unitario", "573163985157");
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode, $@"El status code de la respuesta del mensaje es {response.StatusCode.ToString()}");
        }

        [TestMethod]
        public void GetMessageStatus()
        {
            YellowPushSMSResponse response = SMS.GetMessageStatus("5a5600d4-f60a-e9fa-dd05-e53a4b385450", new DateTime(2018, 1, 30));
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode, $@"El status code de la respuesta del mensaje es {response.StatusCode.ToString()}");
        }
    }
}
