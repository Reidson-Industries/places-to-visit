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
        public void JsonClientShouldGetPlacesToVisit()
        {
            var client = new JsonServiceClient("http://localhost:1337/");
            var testRequest = new AllPlacesToVisitRequest();
            var response = client.Get(testRequest);
            Assert.AreEqual("Capital city of Australia",response.Places.FirstOrDefault().Description);
        }
    }
}
