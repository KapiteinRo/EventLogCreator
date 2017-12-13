using System;
using System.Diagnostics;
using EventLogCreator.classes.Helpers;
using EventLogCreator.classes.Helpers.NCUI;
using Microsoft.Win32;

namespace EventLogCreator.classes
{
    /// <summary>
    /// Log create class.
    /// </summary>
    public class LogCreate
    {
        /// <summary>
        /// Logname, must be 8 characters at most, due to Windows saving the files in 8.3 format.
        /// </summary>
        public string LogName { get; set; }
        /// <summary>
        /// Logfile
        /// </summary>
        public ExpandableString File { get; set; }
        /// <summary>
        /// Eventmessage file, will assume .NET 4.0 EventLogMessages.dll
        /// </summary>
        public ExpandableString EventMessageFile { get; set; }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="sLogName"></param>
        public LogCreate(string sLogName)
        {
            LogName = sLogName;
        }

        /// <summary>
        /// Initialise properties
        /// </summary>
        public void Init()
        {
            File = new ExpandableString(@"%SystemRoot%\System32\winevt\Logs\" + LogName + ".evtx");
            EventMessageFile = new ExpandableString(@"%SystemRoot%\Microsoft.NET\Framework\v4.0.30319\EventLogMessages.dll"); // TODO: make this less hardcoded-hackier
        }

        /// <summary>
        /// Check the properties
        /// </summary>
        /// <param name="cfg"></param>
        public void Prepare(LogConfig cfg)
        {
            if (LogName.Length > 8 && !cfg.OverrideMaximumLength) throw new PrepareInstallException(LogName + " is too long, maximum 8 characters.");
            if (LogName.Length > 8 && cfg.OverrideMaximumLength) NCW.Warning(LogName + " is longer than 8 characters, but will be ignored.");

            if (EventLog.Exists(LogName)) throw new PrepareInstallException(LogName + " already exists in the eventlogs.");
            // if (File.FileExists) throw new PrepareInstallException(LogName + " already exist."); // not necessary, really
            if (!EventMessageFile.FileExists) throw new PrepareInstallException(EventMessageFile.AbsoluteValue + " does not exist.");
        }

        /// <summary>
        /// Create the eventlog
        /// </summary>
        public void Run()
        {
            try
            {
                using (
                    RegistryKey logKey =
                        Registry.LocalMachine.CreateSubKey(@"SYSTEM\CurrentControlSet\services\eventlog\" + LogName))
                {
                    NCW.Print("Set File to '" + File.AbsoluteValue + "'");
                    logKey.SetValue("File", File.Value, RegistryValueKind.ExpandString);
                    NCW.Status(NCStatus.OK);

                    NCW.Print("Set Sources to '" + LogName + "'");
                    logKey.SetValue("Sources", new[] {LogName}, RegistryValueKind.MultiString);
                    NCW.Status(NCStatus.OK);

                    using (RegistryKey subLogKey = logKey.CreateSubKey(LogName))
                    {
                        NCW.Print("Set EventMessageFile to '" + EventMessageFile.AbsoluteValue + "'");
                        subLogKey.SetValue("EventMessageFile", EventMessageFile.Value, RegistryValueKind.ExpandString);
                        NCW.Status(NCStatus.OK);
                    }
                }

            }
            catch (Exception ex)
            {
                throw new ExecuteInstallException("Couldn't create a key?", ex);
            }
        }

        /// <summary>
        /// Test whether we can write something to the eventlog.
        /// </summary>
        public void Test()
        {
            if (!EventLog.Exists(LogName)) throw new ExecuteInstallException(LogName + " does not exist!");
            try
            {
                using (EventLog log = new EventLog {Source = LogName})
                {
                    log.WriteEntry(
                        "Eventlog " + LogName + " is succesfully created on " +
                        DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") +
                        "!", EventLogEntryType.Information, 999);
                }
            }
            catch (Exception ex)
            {
                throw new TestInstallException("Something wrong while testing the new eventlog!", ex);
            }
        }

    }
}
