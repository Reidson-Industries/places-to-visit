using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using PlacesToVisit.ConsoleAppHost;
using PlacesToVisit.IntegrationTests;
using PlacesToVisit.ServiceInterface.DataRepository;
using PlacesToVisit.ServiceModel;
using ServiceStack;
using ServiceStack.Auth;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using ServiceStack.Testing;
using ServiceStack.Validation;

namespace PlacesToVisit.FunctionalTests
{
    
    [TestFixture]
    public class PlacesToVisitFunctionalTests
    {
        private readonly SeedData _seedData = new SeedData();
        private ServiceStackHost appHost;
        private string _listeningOn;

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
                                                     new IAuthProvider[] {new BasicAuthProvider()}));
                }

            }.Init().Start(_listeningOn);
        }

        [TestFixtureTearDown]
        public void TearDownFixture()
        {
            appHost.Dispose();
        }

        [Test]
        public void JsonClientShouldGetPlacesToVisit()
        {
            var client = new JsonServiceClient(_listeningOn);
            var testRequest = new AllPlacesToVisitRequest();
            var response = client.Get(testRequest);
            Assert.AreEqual("Capital city of Australia",response.Places.FirstOrDefault().Description);
        }
    }
}
