using Funq;
using PlacesToVisit.ServiceInterface;
using PlacesToVisit.ServiceInterface.DataRepository;
using PlacesToVisit.ServiceModel;
using ServiceStack;
using ServiceStack.Auth;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using ServiceStack.Validation;

namespace PlacesToVisit.ConsoleAppHost
{
    internal class ConsoleAppHost : AppSelfHostBase
    {
        private readonly SeedData _seedData;

        public ConsoleAppHost()
            : base("Console Places To Visit", typeof (PlaceService).Assembly)
        {
            _seedData = new SeedData();
        }


        public override void Configure(Container container)
        {
            var dbFactory = new OrmLiteConnectionFactory(
                "Data Source=mydb.db;Version=3;", SqliteDialect.Provider);
            container.Register<IDbConnectionFactory>(dbFactory);
            container.RegisterAutoWiredAs<PlacesToVisitRepository, IPlacesToVisitRepository>();

            container.Resolve<IPlacesToVisitRepository>().InitializeSchema();
            _seedData.CreateUsers(container);
            _seedData.SeedPlaces(container.Resolve<IPlacesToVisitRepository>());
            Plugins.Add(new PostmanFeature());
            Plugins.Add(new CorsFeature());
            Plugins.Add(new ValidationFeature());
            Plugins.Add(
                new AuthFeature(() =>
                    new AuthUserSession(),
                    new IAuthProvider[]
                    {
                        new BasicAuthProvider()
                    }));
        }
    }
}