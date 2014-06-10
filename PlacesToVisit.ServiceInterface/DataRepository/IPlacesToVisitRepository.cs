using PlacesToVisit.ServiceModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlacesToVisit.ServiceInterface.DataRepository
{
    public interface IPlacesToVisitRepository
    {
        //Place actions
        Place CreatePlace(Place place);
        Place UpdatePlace(Place place);
        void DeletePlace(int id);
        Place PlaceById(int id);
        List<Place> AllPlaces();
        bool PlaceExists(int id);

        //UserPlace actions
        UserPlace CreateUserPlace(string userId, int placeId, string description);
        UserPlace UpdateUserPlace(int userPlaceId, string description);
        void DeleteUserPlace(int id);
        UserPlace UserPlaceById(int id);
        List<UserPlace> AllUserPlaces(int userId);
        bool UserPlaceExists(int id);

        //User actions
        List<User> SearchUsersByUserDetails(string query);
        List<User> AllUsers();
        User UserByUserId(string userId);
        User UserById(int id);
        void CreateUserIfNotExists(User user);
        List<User> SearchUsersByPlaceName(string placeQuery);

        //Initialize
        void InitializeSchema();
    }
}
