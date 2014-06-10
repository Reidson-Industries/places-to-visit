using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Funq;
using PlacesToVisit.ServiceInterface;
using PlacesToVisit.ServiceInterface.DataRepository;
using PlacesToVisit.ServiceModel;
using ServiceStack;
using ServiceStack.Data;
using ServiceStack.OrmLite;

namespace PlacesToVisit.IntegrationTests
{
    public class TestAppHost : AppSelfHostBase
    {
        public TestAppHost()
            : base("Testing host", typeof(PlaceService).Assembly)
        {

        }

        public override void Configure(Container container)
        {
            container.Register<IDbConnectionFactory>(
               new OrmLiteConnectionFactory(":memory:", SqliteDialect.Provider));

            container.RegisterAutoWiredAs<PlacesToVisitRepository, IPlacesToVisitRepository>();
            container.RegisterAutoWired<PlaceService>();

            using (var db = container.Resolve<IDbConnectionFactory>().Open())
            {
                db.DropAndCreateTable<Place>();
                db.Insert(new Place { Description = "Test Object Description", Name = "Test Object Name" });
            }
        }
    }
}
