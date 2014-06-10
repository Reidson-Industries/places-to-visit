using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using PlacesToVisit.ServiceInterface;
using PlacesToVisit.ServiceInterface.DataRepository;
using PlacesToVisit.ServiceModel;
using ServiceStack;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using ServiceStack.Testing;

namespace PlacesToVisit.IntegrationTests
{

    [TestFixture]
    public class PlacesServiceIntegrationTests
    {
        private ServiceStackHost appHost;

        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            appHost = new TestAppHost().Init().Start("http://*:1234/api/");           
        }

        [TestFixtureTearDown]
        public void TestFixtureTearDown()
        {
            appHost.Dispose();
        }
        
        [Test]
        public void ShouldGetAllPlaces()
        {
            var target = appHost.Container.Resolve<PlaceService>();
            var testRequest = new AllPlacesToVisitRequest();
            var response = target.Get(testRequest);
            Assert.AreEqual("Test Object Name",response.Places.FirstOrDefault().Name);
        }
    }
}
