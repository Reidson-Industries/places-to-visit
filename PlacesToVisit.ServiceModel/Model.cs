using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlacesToVisit.ServiceModel
{
    public class Place
    {
        [Index]
        [PrimaryKey]
        [AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class User
    {
        [Index]
        [PrimaryKey]
        [AutoIncrement]
        public int Id { get; set; }
        [Index(Unique = true)]
        public string UserName { get; set; }
        public string Email { get; set; }
        public string DisplayName { get; set; }
        public string RealName { get; set; }
        public string Website { get; set; }
    }

    public class UserPlace
    {
        [Index]
        [PrimaryKey]
        [AutoIncrement]
        public int Id { get; set; }

        [References(typeof(User))]
        public int UserId { get; set; }
        [References(typeof(Place))]
        public int PlaceId { get; set; }

        [Reference]
        public User User { get; set; }
        [Reference]
        public Place Place { get; set; }

        public string UserDescription { get; set; }
    }
}
