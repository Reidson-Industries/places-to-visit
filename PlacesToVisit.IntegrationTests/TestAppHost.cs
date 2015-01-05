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
using ServiceStack.Testing;

namespace PlacesToVisit.IntegrationTests
{
    public class TestAppHost : AppSelfHostBase
    {
        public TestAppHost()
            : base("Testing host", typeof(PlaceService).Assembly)
        {

        }

        public Action<Container> ConfigureContainer { get; set; }
        public Action<ServiceStackHost> ConfigureAppHost { get; set; }

        public override void Configure(Container container)
        {
            if (this.ConfigureContainer == null)
                return;
            this.ConfigureContainer(container);
        }

        public override void OnBeforeInit()
        {
            if (this.ConfigureAppHost != null)
                this.ConfigureAppHost(this);
            base.OnBeforeInit();
        }


    }
}
