using System;

namespace Cordseye.Runtime
{
    public static class Program
    {
        private static void Main(string[] args)
        {
            using (var window = new RuntimeWindow(1280, 720, "Cordseye"))
            {
                window.Run(60.0f);
            }
        }
    }
}