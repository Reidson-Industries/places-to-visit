using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlacesToVisit.ServiceModel
{
    [Route("/myplaces", Verbs = "GET")]
    public class AllUserPlacesRequest : IReturn<UserPlaceResponse>
    {

    }
    [Route("/myplaces", Verbs = "POST")]
    public class CreateUserPlaceRequest : IReturn<UserPlaceResponse>
    {
        public int PlaceId { get; set; }
        public string UserDescription { get;set;}
    }
    [Route("/myplaces/{Id}", Verbs = "POST")]
    public class UpdateUserPlaceRequest : IReturn<UserPlaceResponse>
    {
        public int Id { get; set; }
        public string UserDescription { get;set;}
    }
    [Route("/myplaces/{Id}", Verbs = "DELETE")]
    public class DeleteUserPlaceRequest : IReturn<UserPlaceResponse>
    {
        public int Id { get; set; }
    }

    public class UserPlaceResponse
    {
        public int? Id { get; set; }
        public UserPlace UserPlace { get; set; }
        public List<UserPlace> UserPlaces { get; set; }
        public string UserDescription { get; set; }
    }
}
