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
        public ConsoleAppHost()
            : base("Console Places To Visit", typeof (PlaceService).Assembly)
        { }


        public override void Configure(Container container)
        {
            var dbFactory = new OrmLiteConnectionFactory(
                "Data Source=mydb.db;Version=3;", SqliteDialect.Provider);
            container.Register<IDbConnectionFactory>(dbFactory);
            container.RegisterAutoWiredAs<PlacesToVisitRepository, IPlacesToVisitRepository>();

            container.Resolve<IPlacesToVisitRepository>().InitializeSchema();
            CreateUsers(container);
            SeedPlaces(container.Resolve<IPlacesToVisitRepository>());
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

        private void CreateUsers(Container container)
        {
            var userRep = new OrmLiteAuthRepository(container.Resolve<IDbConnectionFactory>());
            userRep.DropAndReCreateTables();
            container.Register<IUserAuthRepository>(userRep);
            var appSettings = container.Resolve<PlacesToVisitAppSettings>();
            var dataRepository = container.Resolve<IPlacesToVisitRepository>();

            userRep.DropAndReCreateTables();
            string pwHash;
            string pwSalt;
            new SaltedHash().GetHashAndSaltString(
                appSettings.Get("Salt", "debugSalt"),
                out pwHash,
                out pwSalt);

            var userAuth1 = userRep.CreateUserAuth(new UserAuth
            {
                Email = "darren.reid@reidsoninsdustries.net",
                DisplayName = "Darren",
                UserName = "darrenreid",
                FirstName = "Darren",
                LastName = "Reid",
                PasswordHash = pwHash,
                Salt = pwSalt,
                Roles = {"Admin"}
            }, "abc123");

            var user1 = userAuth1.ConvertTo<User>();
            dataRepository.CreateUserIfNotExists(user1);

            var userAuth2 = userRep.CreateUserAuth(new UserAuth
            {
                Email = "kyle.hodgson@reidsoninsdustries.net",
                DisplayName = "Kyle",
                UserName = "kylehodgson",
                FirstName = "Kyle",
                LastName = "Hodgson",
                PasswordHash = pwHash,
                Salt = pwSalt,
                Roles = {"Admin"}
            }, "123abc");

            var user2 = userAuth2.ConvertTo<User>();
            dataRepository.CreateUserIfNotExists(user2);
        }

        private void SeedPlaces(IPlacesToVisitRepository repository)
        {
            repository.CreatePlace(new Place
            {
                Name = "Canberra",
                Description = "Capital city of Australia"
            });

            repository.CreatePlace(new Place
            {
                Name = "Toronto",
                Description = "Provincial capital of Ontario"
            });

            repository.CreatePlace(new Place
            {
                Name = "Auckland, New Zealand",
                Description = "A city in the north island"
            });
        }
    }
}