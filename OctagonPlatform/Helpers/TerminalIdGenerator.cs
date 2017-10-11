using System;

namespace OctagonPlatform.Helpers
{
    public static class TerminalIdGenerator
    {
        public static string Generator(int id)
        {
            return "TOD" + DateTime.UtcNow.Year + DateTime.UtcNow.Month +
                         DateTime.UtcNow.Day + id;
        }
    }
}