using System.Collections.Generic;

namespace EventLogCreator.classes
{
    public class LogConfig
    {
        public bool OverrideMaximumLength { get; set; }

        public LogConfig()
        {
            // set default values
            OverrideMaximumLength = false;
        }

        public bool ParseArgument(string s)
        {
            if (s.Length != 2) return false;

            if (s.StartsWith("-") || s.StartsWith("/"))
            {
                switch (s[1])
                {
                    case 'L': OverrideMaximumLength = true; break;
                    default: break;
                }
                return true;
            }
            return false;
        }

        public List<string> ParseArguments(string[] sa)
        {
            List<string> ret = new List<string>();

            foreach(string s in sa)
            {
                if (!ParseArgument(s)) ret.Add(s);
            }

            return ret;
        }
    }
}
