using System;
using System.IO;
using DependencyElimination;


namespace DependencyElimination
{
    public class FileWriter : IDisposable
    {
        public StreamWriter StreamWriter { get; private set; }

        public FileWriter(string fileName)
        {
            StreamWriter = new StreamWriter(fileName, false);
        }


        public void Write(string[] content)
        {
            if (content == null) return;
            foreach (var line in content)
            {
                StreamWriter.WriteLine(line);
            }
        }

        public void Dispose()
        {
            StreamWriter.Dispose();
        }
    }
}
