using System;
using System.Runtime.Serialization;

namespace EventLogCreator.classes.Helpers
{
    /// <summary>
    /// Install exception
    /// </summary>
    public class InstallException : Exception, ISerializable
    {
        public InstallExceptionPhase Phase { get; set; }

        public InstallException() { }
        /// <summary>
        /// Exception with message.
        /// </summary>
        /// <param name="s"></param>
        public InstallException(string s) : base(s) { }
        /// <summary>
        /// Exception with message and innerexception.
        /// </summary>
        /// <param name="s"></param>
        /// <param name="inner"></param>
        public InstallException(string s, Exception inner) : base(s, inner) { }

        public InstallException(SerializationInfo info, StreamingContext ctx) : base(info, ctx) { }
    }

    /// <summary>
    /// Exception during preparing
    /// </summary>
    public class PrepareInstallException : InstallException
    {
        /// <summary>
        /// Exception with message.
        /// </summary>
        /// <param name="s"></param>
        public PrepareInstallException(string s) : base(s) { Phase = InstallExceptionPhase.Prepare; }
    }

    /// <summary>
    /// Exception during running
    /// </summary>
    public class ExecuteInstallException : InstallException
    {
        /// <summary>
        /// Exception with message.
        /// </summary>
        /// <param name="s"></param>
        public ExecuteInstallException(string s) : base(s) { Phase = InstallExceptionPhase.Execute; }
        /// <summary>
        /// Exception with message and innerexception.
        /// </summary>
        /// <param name="s"></param>
        /// <param name="inner"></param>
        public ExecuteInstallException(string s, Exception inner) : base(s, inner) { Phase = InstallExceptionPhase.Execute; }
    }

    /// <summary>
    /// Exception during testing
    /// </summary>
    public class TestInstallException : InstallException
    {
        /// <summary>
        /// Exception with message.
        /// </summary>
        /// <param name="s"></param>
        public TestInstallException(string s) : base(s) { Phase = InstallExceptionPhase.Finalize; }
        /// <summary>
        /// Exception with message and innerexception.
        /// </summary>
        /// <param name="s"></param>
        /// <param name="inner"></param>
        public TestInstallException(string s, Exception inner) : base(s, inner) { Phase = InstallExceptionPhase.Finalize; }
    }

    /// <summary>
    /// Phase of installation
    /// </summary>
    public enum InstallExceptionPhase
    {
        Init,
        Prepare,
        Execute,
        Finalize,
        Rollback
    };
}
