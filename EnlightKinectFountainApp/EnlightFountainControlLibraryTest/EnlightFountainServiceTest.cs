﻿using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using EnlightFountainControlLibrary;
using EnlightFountainControlLibrary.Messages;
using EnlightFountainControlLibrary.Models;

namespace EnlightFountainControlLibraryTest
{
    [TestClass]
    public class EnlightFountainServiceTest
    {
        private const string SERVER_URL = @"http://private-anon-492379f70-enlight.apiary-mock.com";
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
            var service = EnlightFountainService.GetInstance(SERVER_URL, "some alphanumeric api key");
            var msg = new EnlightFountainControlLibrary.Messages.Control.QueryControlList();
            FountainControllerList list = service.SendMessage<FountainControllerList>(msg);

            // TODO no idea why this test fails. The URIException makes no sense!
            // in actual production use, no exception is thrown - >:(

            Assert.IsNotNull(list);
        }

        [TestMethod]
        public void MethodPostTest()
        {
            var service = EnlightFountainService.GetInstance(SERVER_URL, "some alphanumeric api key");
            var msg = new EnlightFountainControlLibrary.Messages.Control.RequestControl(15);
            RequestControlResponse model = service.SendMessage<RequestControlResponse>(msg);

            // will fail as long as docs give incorrect JSON

            Assert.IsNotNull(model);
        }
    }
}
