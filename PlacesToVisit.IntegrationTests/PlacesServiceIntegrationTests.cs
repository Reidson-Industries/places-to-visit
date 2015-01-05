using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using PlacesToVisit.ConsoleAppHost;
using PlacesToVisit.ServiceInterface;
using PlacesToVisit.ServiceInterface.DataRepository;
using PlacesToVisit.ServiceModel;
using ServiceStack;
using ServiceStack.Auth;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using ServiceStack.Testing;
using ServiceStack.Validation;

namespace PlacesToVisit.IntegrationTests
{

    [TestFixture]
    public class PlacesServiceIntegrationTests
    {
        private ServiceStackHost appHost;
        private string _listeningOn;
        private readonly SeedData _seedData = new SeedData();

        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            _listeningOn = "http://localhost:1234/api/";

            appHost = new TestAppHost
            {
                ConfigureContainer = container =>
                {
                    var dbFactory = new OrmLiteConnectionFactory(
                        "Data Source=mydb.db;Version=3;", SqliteDialect.Provider);
                    container.Register<IDbConnectionFactory>(dbFactory);
                    container.RegisterAutoWiredAs
                        <PlacesToVisitRepository, IPlacesToVisitRepository>();
                    container.Register(new PlacesToVisitAppSettings());
                    var repo = container.Resolve<IPlacesToVisitRepository>();
                    repo.InitializeSchema();
                    _seedData.CreateUsers(container);
                    _seedData.SeedPlaces(repo);

                },
                ConfigureAppHost = host =>
                {
                    host.Plugins.Add(new ValidationFeature());
                    host.Plugins.Add(new AuthFeature(() =>
                                                     new AuthUserSession(),
                                                     new IAuthProvider[] { new BasicAuthProvider() }));
                }

            }.Init().Start(_listeningOn);        
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
            Assert.AreEqual("Canberra",response.Places.FirstOrDefault().Name);
        }
    }
}
