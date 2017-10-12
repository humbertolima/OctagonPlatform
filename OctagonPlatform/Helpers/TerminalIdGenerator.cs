using System;

namespace OctagonPlatform.Helpers
{
    public static class TerminalIdGenerator
    {
        public static string Generator(int id)
        {
            return "TOD" + DateTime.UtcNow.ToString("yyMMdd") + id;
        }
    }
}