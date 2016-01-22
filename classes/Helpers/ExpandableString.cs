using System;
using System.Collections.Generic;
using System.IO;

namespace EventLogCreator.classes.Helpers
{
    /// <summary>
    /// Expandable string wrapper
    /// </summary>
    public class ExpandableString
    {
        /// <summary>
        /// Find absolute value
        /// </summary>
        public string AbsoluteValue
        {
            get
            {
                string sTmpRet = Value;
                foreach (KeyValuePair<string, string> kvpVar in StandardVars)
                    sTmpRet = sTmpRet.Replace("%" + kvpVar.Key + "%", kvpVar.Value);
                return sTmpRet;
            }
        }
        /// <summary>
        /// Value of the string
        /// </summary>
        public string Value { get; set; }
        /// <summary>
        /// ctor. Currently only accepting %SystemRoot% as variable.
        /// </summary>
        /// <param name="s"></param>
        public ExpandableString(string s)
        {
            Value = s;
        }

        private Dictionary<string, string> _dssStdVars = null;
        /// <summary>
        /// Standard variables, we need something better for that
        /// </summary>
        private Dictionary<string, string> StandardVars
        {
            get
            {
                return _dssStdVars ?? (_dssStdVars = new Dictionary<string, string>
                {
                    {"SystemRoot", Environment.GetFolderPath(Environment.SpecialFolder.Windows)}
                });
            }
        }
        /// <summary>
        /// Does this file exist?
        /// </summary>
        public bool FileExists
        {
            get
            {
                return File.Exists(AbsoluteValue);
            }
        }
    }
}
