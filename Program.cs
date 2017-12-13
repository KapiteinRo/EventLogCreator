using System;
using EventLogCreator.classes;
using EventLogCreator.classes.Helpers;
using EventLogCreator.classes.Helpers.NCUI;
using System.Collections.Generic;

namespace EventLogCreator
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Usage: " + AppDomain.CurrentDomain.FriendlyName + " [LOGNAME1 [LOGNAME2 ...]]");
                Console.WriteLine("\r\n\tCreate windows eventlogs under the Application and Services Logs.");
                Console.WriteLine("\tPlease note that a logname cannot contain more than 8 characters.");
                Console.WriteLine("\tUnless overridden with the /L option.");
                return;
            }

            LogConfig config = new LogConfig();
            List<string> arguments = config.ParseArguments(args);

            // Usage: EventLogCreator [Name] will create an eventlog
            try
            {
                foreach (string sLogName in arguments)
                {
                    LogCreate lc = new LogCreate(sLogName);

                    NCW.Print("Init LogCreate for " + lc.LogName);
                    lc.Init();
                    NCW.Status(NCStatus.OK);

                    NCW.Print("Checking requirements for " + lc.LogName);
                    lc.Prepare(config);
                    NCW.Status(NCStatus.OK);

                    NCW.Print("Going to create eventlog " + lc.LogName);
                    lc.Run();

                    NCW.Print("Testing eventlog " + lc.LogName);
                    lc.Test();
                    NCW.Status(NCStatus.OK);
                }
                NCW.Print("Close and open your Event Viewer to see the new log(s).");
            }
            catch (InstallException ex)
            {
                NCW.Status(NCStatus.ERROR);
                NCW.Print(ex.Phase + ":");
                NCW.Print("--------------------------");
                NCW.Error(ex + "\r\n");

                // TODO: rollback here
            }

#if DEBUG
            NCW.Status(NCStatus.PAUSE, "Press a key...");
#endif
        }
    }
}
