using System;

namespace DependencyElimination
{
    public class Logger
    {
        public static void WriteLine(string input,params object[] args)
        {
            if(input!=null)
                Console.WriteLine(input,args);
        }
    }
}