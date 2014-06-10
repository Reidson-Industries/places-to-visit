using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlacesToVisit.ServiceModel
{
    public class SearchUserRequest
    {
        public string SearchQuery { get; set; }
    }

    public class SearchUserResponse
    {
        public List<User> Users { get; set; }
    }
}
