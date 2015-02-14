using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using EnlightFountainControlLibrary;

namespace EnlightFountainControlLibraryTest
{
    /// <summary>
    /// Summary description for EnlightFountainServiceInstantiationTest
    /// </summary>
    [TestClass]
    public class EnlightFountainServiceInstantiationTest
    {
        [TestMethod]
        public void SingletonTest()
        {
            var service = EnlightFountainService.GetInstance("url", "key");
            var service2 = EnlightFountainService.GetInstance(null, null);

            Assert.ReferenceEquals(service, service2);
        }
    }
}
