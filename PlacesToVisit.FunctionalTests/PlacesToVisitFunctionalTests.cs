using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using PlacesToVisit.ServiceModel;
using ServiceStack;

namespace PlacesToVisit.FunctionalTests
{

    [TestFixture]
    public class PlacesToVisitFunctionalTests
    {
        [Test]
        public void ShouldGetPlacesToVisit()
        {
            var client = new JsonServiceClient("http://localhost:1337/");
            var testRequest = new AllPlacesToVisitRequest();
            var response = client.Get(testRequest);
            Assert.AreEqual(true,true,"Make sure we have a working test harness.");
        }
    }
}
