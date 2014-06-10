using PlacesToVisit.ServiceInterface.DataRepository;
using PlacesToVisit.ServiceModel;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlacesToVisit.ServiceInterface
{
    [Authenticate]
    public class UserPlaceService : Service
    {
        public IPlacesToVisitRepository PlacesToVisitRepository { get; set; }

        public UserPlaceResponse Get(AllUserPlacesRequest request)
        {
            var user = PlacesToVisitRepository.UserByUserId(GetSession().UserName);
            return new UserPlaceResponse
            {
                UserPlaces = PlacesToVisitRepository.AllUserPlaces(user.Id)
            };
        }

        public UserPlaceResponse Post(CreateUserPlaceRequest request)
        {
            var place = PlacesToVisitRepository.CreateUserPlace(GetSession().UserName,request.PlaceId,request.UserDescription);
            return new UserPlaceResponse
            {
                UserPlace = place
            };
        }

        public UserPlaceResponse Put(UpdateUserPlaceRequest request)
        {
            if (!PlacesToVisitRepository.UserPlaceExists(request.Id))
            {
                throw HttpError.NotFound("Place not found");
            }
            var user = PlacesToVisitRepository.UserByUserId(GetSession().UserName);
            var place = PlacesToVisitRepository.UpdateUserPlace(request.Id, request.UserDescription);
            return new UserPlaceResponse
            {
                UserPlace = place
            };
        }

        public UserPlaceResponse Delete(DeleteUserPlaceRequest request)
        {
            if (!PlacesToVisitRepository.UserPlaceExists(request.Id))
            {
                throw HttpError.NotFound("Place not found");
            }
            PlacesToVisitRepository.DeleteUserPlace(request.Id);
            base.Response.StatusCode = 204;
            return null;
        }
    }
}
