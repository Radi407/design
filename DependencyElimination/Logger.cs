using System;

namespace DependencyElimination
{
    public class Logger
    {
        public static void WriteLine(string input,params object[] args)
        {
            Console.WriteLine(input,args);
        }
    }
}