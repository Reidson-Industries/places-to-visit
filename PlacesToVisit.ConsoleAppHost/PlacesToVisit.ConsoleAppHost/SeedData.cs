using Funq;
using PlacesToVisit.ServiceInterface.DataRepository;
using PlacesToVisit.ServiceModel;
using ServiceStack;
using ServiceStack.Auth;
using ServiceStack.Data;

namespace PlacesToVisit.ConsoleAppHost
{
    public class SeedData
    {
        public void CreateUsers(Container container)
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

        public void SeedPlaces(IPlacesToVisitRepository repository)
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