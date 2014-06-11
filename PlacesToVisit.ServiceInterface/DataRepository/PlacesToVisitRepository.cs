using PlacesToVisit.ServiceModel;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;

namespace PlacesToVisit.ServiceInterface.DataRepository
{
    public class PlacesToVisitRepository : IPlacesToVisitRepository
    {
        public IDbConnectionFactory DbConnectionFactory { get; set; }

        public Place CreatePlace(Place place)
        {
            Place result = place;
            using (var db = DbConnectionFactory.OpenDbConnection())
            {
                db.Save<Place>(place);
            }
            return result;
        }

        public Place UpdatePlace(Place place)
        {
            Place result = place;
            using (var db = DbConnectionFactory.OpenDbConnection())
            {
                db.Update<Place>(place);
            }
            return result;
        }

        public void DeletePlace(int id)
        {
            using (var db = DbConnectionFactory.OpenDbConnection())
            {
                db.DeleteById<Place>(id);
            }
        }

        public Place PlaceById(int id)
        {
            Place result = null;
            using (var db = DbConnectionFactory.OpenDbConnection())
            {
                result = db.SingleById<Place>(id);
            }
            return result;
        }

        public List<Place> AllPlaces()
        {
            List<Place> result;
            using (var db = DbConnectionFactory.OpenDbConnection())
            {
                result = db.Select<Place>();
            }
            return result;
        }

        public UserPlace CreateUserPlace(string userId, int placeId, string description)
        {
            UserPlace result = new UserPlace();
            result.UserId = UserByUserId(userId).Id;
            result.PlaceId = placeId;
            result.UserDescription = description;
            using (var db = DbConnectionFactory.OpenDbConnection())
            {
                result.Id = (int)db.Insert<UserPlace>(result);
            }
            return result;
        }

        public UserPlace UpdateUserPlace(int userPlaceId, string description)
        {
            UserPlace place;
            using (var db = DbConnectionFactory.OpenDbConnection())
            {
                place = db.SingleById<UserPlace>(userPlaceId);
                place.UserDescription = description;
                db.Update<UserPlace>(place);
            }
            return place;
        }

        public void DeleteUserPlace(int id)
        {
            using (var db = DbConnectionFactory.OpenDbConnection())
            {
                db.DeleteById<UserPlace>(id);
            }
        }

        public UserPlace UserPlaceById(int id)
        {
            UserPlace result;
            using (var db = DbConnectionFactory.OpenDbConnection())
            {
                result = db.SingleById<UserPlace>(id);
            }
            return result;
        }

        public List<UserPlace> AllUserPlaces(int userId)
        {
            List<UserPlace> result = new List<UserPlace>();
            using (var db = DbConnectionFactory.OpenDbConnection())
            {
                result = db.Select<UserPlace>(x => x.UserId == userId);
            }
            return result;
        }

        public List<User> SearchUsersByUserDetails(string query)
        {
            List<User> result = new List<User>();
            using (var db = DbConnectionFactory.OpenDbConnection())
            {
                result = db.Select<User>(x =>
                    x.UserName.Contains(query) ||
                    x.DisplayName.Contains(query) ||
                    x.RealName.Contains(query));
            }

            return result;
        }

        public List<User> AllUsers()
        {
            List<User> result = new List<User>();
            using (var db = DbConnectionFactory.OpenDbConnection())
            {
                result = db.Select<User>();
            }
            return result;
        }

        public User UserByUserId(string userName)
        {
            User result;
            using (var db = DbConnectionFactory.OpenDbConnection())
            {
                result = db.Single<User>(x => x.UserName == userName);
            }
            return result;
        }

        public User UserById(int id)
        {
            User result;
            using (var db = DbConnectionFactory.OpenDbConnection())
            {
                result = db.SingleById<User>(id);
            }
            return result;
        }

        public void CreateUserIfNotExists(User user)
        {
            var existingUser = UserByUserId(user.UserName);
            if (existingUser != null)
            {
                return;
            }
            using (var db = DbConnectionFactory.OpenDbConnection())
            {
                db.Insert(user);
            }
        }

        public List<User> SearchUsersByPlaceName(string placeQuery)
        {
            throw new NotImplementedException();
        }

        //For ease of testing
        public void InitializeSchema()
        {
            using (var db = DbConnectionFactory.OpenDbConnection())
            {
                db.DropAndCreateTable<User>();
                db.DropAndCreateTable<Place>();
                db.DropAndCreateTable<UserPlace>();
            }
        }

        public bool PlaceExists(int id)
        {
            bool result;
            using (var db = DbConnectionFactory.OpenDbConnection())
            {
                result = db.Exists<Place>(new Place { Id = id });
            }
            return result;
        }

        public bool UserPlaceExists(int id)
        {
            bool result;
            using (var db = DbConnectionFactory.OpenDbConnection())
            {
                result = db.Exists<UserPlace>(new UserPlace { Id = id });
            }
            return result;
        }
    }
}
