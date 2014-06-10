using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceStack.Configuration;

namespace PlacesToVisit.ConsoleAppHost
{
    public class PlacesToVisitAppSettings : AppSettings
    {
        public string Salt
        {
            get { return this.Get("Salt", "devSalt"); }
        }

        public ApplicationEnvironment Environment
        {
            get { return this.Get("Environment", ApplicationEnvironment.Dev); }
        }

        public List<string> AdministratorEmails
        {
            get
            {
                return this.Get<List<string>>("AdminEmailAddresses",
                    new List<string>());
            }
        }

        public enum ApplicationEnvironment
        {
            Dev,
            Test,
            Prod
        }
    }
}
