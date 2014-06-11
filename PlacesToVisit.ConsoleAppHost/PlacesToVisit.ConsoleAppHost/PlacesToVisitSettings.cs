using System.Collections.Generic;
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
