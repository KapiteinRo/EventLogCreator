using System;

namespace EventLogCreator.classes.Helpers.NCUI
{
    /// <summary>
    /// Console UI stuff.
    /// </summary>
    public static class NCW
    {
        /// <summary>
        /// Write string to console with leading newline.
        /// </summary>
        /// <param name="s"></param>
        public static void Print(string s)
        {
            Console.Write(Environment.NewLine + s);
        }
        /// <summary>
        /// Write colored string to console with leading newline.
        /// </summary>
        /// <param name="f"></param>
        /// <param name="s"></param>
        public static void Print(ConsoleColor f, string s)
        {
            ConsoleColor cCur = Console.ForegroundColor;
            Console.ForegroundColor = f;
            Print(s);
            Console.ForegroundColor = cCur;
        }

        /// <summary>
        /// Write error-message in red.
        /// </summary>
        /// <param name="s"></param>
        public static void Error(string s)
        {
            Print(ConsoleColor.Red, s);
        }
        /// <summary>
        /// Write warning-message in yellow
        /// </summary>
        /// <param name="s"></param>
        public static void Warning(string s)
        {
            Print(ConsoleColor.Yellow, s);
        }

        /// <summary>
        /// Write Linux-like trailing status-message. Note: use NCStatus.PAUSE for a wait for keypress.
        /// </summary>
        /// <param name="status"></param>
        /// <param name="s"></param>
        public static void Status(NCStatus status, string s = null)
        {
            if (s != null)
                Print(s);
            ConsoleColor cCur = Console.ForegroundColor;
            Console.CursorLeft = Console.BufferWidth - 8;
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("[");
            switch (status)
            {
                case NCStatus.OK: Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write(" OK  "); break;
                case NCStatus.ERROR: Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("ERROR"); break;
                case NCStatus.PAUSE: Console.ForegroundColor = ConsoleColor.Blue;
                    Console.Write("PAUSE"); break;
            }
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("]");
            Console.ForegroundColor = cCur;
            if (status == NCStatus.PAUSE) Console.ReadKey(true);
        }
    }

    /// <summary>
    /// Status types
    /// </summary>
    public enum NCStatus
    {
        /// <summary>
        /// Write lightgreen OK
        /// </summary>
        OK,
        /// <summary>
        /// Write red ERROR
        /// </summary>
        ERROR,
        /// <summary>
        /// Write blue PAUSE, and then wait for keypress
        /// </summary>
        PAUSE
    };
}
