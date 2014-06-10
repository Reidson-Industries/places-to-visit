using PlacesToVisit.ServiceInterface.DataRepository;
using PlacesToVisit.ServiceModel;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PlacesToVisit.ServiceInterface
{
    public class PlaceService : Service
    {
        public IPlacesToVisitRepository PlacesToVisitRepository { get; set; }

        public PlaceToVisitResponse Get(PlaceToVisitRequest request)
        {
            if (!PlacesToVisitRepository.PlaceExists(request.Id))
            {
                throw HttpError.NotFound("Place not found");
            }
            return new PlaceToVisitResponse
            {
                Place = PlacesToVisitRepository.PlaceById(request.Id)
            };
        }

        public PlaceToVisitResponse Get(AllPlacesToVisitRequest request)
        {
            return new PlaceToVisitResponse
            {
                Places = PlacesToVisitRepository.AllPlaces()
            };
        }

        public PlaceToVisitResponse Post(CreatePlaceToVisit request)
        {
            var place = PlacesToVisitRepository.CreatePlace(request.ConvertTo<Place>());
            return new PlaceToVisitResponse
            {
                Place = place
            };
        }

        public PlaceToVisitResponse Put(UpdatePlaceToVisit request)
        {
            if (!PlacesToVisitRepository.PlaceExists(request.Id))
            {
                throw HttpError.NotFound("Place not found");
            }
            var place = PlacesToVisitRepository.UpdatePlace(request.ConvertTo<Place>());
            return new PlaceToVisitResponse
            {
                Place = place
            };
        }

        //public PlaceToVisitResponse Delete(DeletePlaceToVisit request)
        //{
        //    if (!PlacesToVisitRepository.PlaceExists(request.Id))
        //    {
        //        throw HttpError.NotFound("Place not found");
        //    }
        //    PlacesToVisitRepository.DeletePlace(request.Id);
        //    base.Response.StatusCode = 204;
        //    return null;
        //}
    }
}
