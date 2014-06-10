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
            appHost = new BasicAppHost().Init();
            var container = appHost.Container;

            container.Register<IDbConnectionFactory>(
                new OrmLiteConnectionFactory(":memory:", SqliteDialect.Provider));

            container.RegisterAutoWiredAs<PlacesToVisitRepository,IPlacesToVisitRepository>();
            container.RegisterAutoWired<PlaceService>();

            using (var db = container.Resolve<IDbConnectionFactory>().Open())
            {
                db.DropAndCreateTable<Place>();
                db.Insert(new Place {Description = "Test Object Description", Name = "Test Object Name"});
            }
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
