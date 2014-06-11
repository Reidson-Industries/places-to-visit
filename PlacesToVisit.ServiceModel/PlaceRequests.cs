using ServiceStack;
using System.Collections.Generic;

namespace PlacesToVisit.ServiceModel
{
    [Route("/places/{Id}", Verbs = "GET")]
    public class PlaceToVisitRequest : IReturn<PlaceToVisitResponse>
    {
        public int Id { get; set; }
    }
    [Route("/places",Verbs = "GET")]
    public class AllPlacesToVisitRequest : IReturn<PlaceToVisitResponse>
    {

    }
    [Route("/places", Verbs = "POST")]
    public class CreatePlaceToVisit : IReturn<PlaceToVisitResponse>
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }

    [Route("/places/{Id}", Verbs = "PUT")]
    public class UpdatePlaceToVisit : IReturn<PlaceToVisitResponse>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }

    //[Route("/places/{Id}", Verbs = "DELETE")]
    //public class DeletePlaceToVisit : IReturn<PlaceToVisitResponse>
    //{
    //    public int Id { get; set; }
    //}

    public class PlaceToVisitResponse
    {
        public int? Id { get; set; }
        public Place Place { get; set; }
        public List<Place> Places { get; set; }
    }
}
