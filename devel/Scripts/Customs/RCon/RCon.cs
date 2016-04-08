using System;
using System.Collections.Generic;
using System.Text;
using System.Net;

namespace Customs.RCon
{
    class RCon
    {
        public const int ListenPort = 2594; // Port to listen on.
        public const string ListenDomain = "http://*"; //Domain to listen for Default "http://*" for all domains.
        public const bool AllowCommands = false;
        public const bool RequireAuthentication = false;

        public static void Initialize()
        {
            Console.WriteLine("Initializing TechUO Remote Control");
            if (!IsListenPortValid())



            {
                Console.WriteLine("Error Initializing TechUO Remote Control: Invalid ListenPort");
                return;
            }
            if (!ListenDomain.ToLower().Contains("http://"))
            {
                Console.WriteLine("Eorror Initializing TechUO Remote Control: ListenDomian must contain http:// or https://");
                return;
            }



        }

        //Todo: Add additional logic here: test to see if port is open.
        private static bool IsListenPortValid()
        {
            if (ListenPort < 1)
            {
                Console.WriteLine("Error Initializing TechUO Remote Control: Invalid ListenPort");
                return false;
            }

            return true;
        }
    }
}
