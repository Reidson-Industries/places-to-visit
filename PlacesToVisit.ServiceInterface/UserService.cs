using PlacesToVisit.ServiceInterface.DataRepository;
using PlacesToVisit.ServiceModel;
using ServiceStack;
using ServiceStack.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlacesToVisit.ServiceInterface
{
    public class UserService : Service
    {
        public IPlacesToVisitRepository PlacesToVisitRepository { get; set; }
        public SearchUserResponse Get(SearchUserRequest request)
        {
            SearchUserResponse response = new SearchUserResponse();

            response.Users = PlacesToVisitRepository.SearchUsersByUserDetails(request.SearchQuery);

            return response;
        }
    }
}
