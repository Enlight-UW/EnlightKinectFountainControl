using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using EnlightFountainControlLibrary;

namespace EnlightFountainControlLibraryTest
{
    [TestClass]
    public class EnlightFountainServiceTest
    {
        [TestMethod]
        public void SingletonTest()
        {
            var service = EnlightFountainService.GetInstance("url", "key");
            var service2 = EnlightFountainService.GetInstance(null, null);

            Assert.ReferenceEquals(service, service2);
        }

        [TestMethod]
        public void MethodGetTest()
        {
            var service = EnlightFountainService.GetInstance("http://private-anon-d80e94b59-enlight.apiary-mock.com", "some alphanumeric api key");
            var msg = new EnlightFountainControlLibrary.Messages.Control.QueryControlList();
            string result = service.SendMessage(msg);

            // TODO no idea why this test fails. The URIException makes no sense!
            // in actual production use, no exception is thrown - >:(

            Assert.IsNotNull(result);
        }
    }
}
