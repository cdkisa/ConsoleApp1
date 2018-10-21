using System;

namespace ConsoleApp1.Common
{
    public static class Extensions
    {
        public static bool IsExpired(this long ticks) => DateTime.Now.Ticks > ticks;
    }
}
