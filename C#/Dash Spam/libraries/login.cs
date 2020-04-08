
// Author: Dashie
// Version: 1.0

using System;
using System.Collections.Generic;

namespace MailSpammer
{
    public class Login
    {
        readonly static ConsoleColor DarkGray = ConsoleColor.DarkGray;
        readonly static ConsoleColor White = ConsoleColor.White;
        readonly static ConsoleColor Yellow = ConsoleColor.Yellow;
        readonly static ConsoleColor Red = ConsoleColor.Red;

        // I know this will not be doing anything but at least will it prevent skiddies from using this application; Assuming that the skript kidders these days do not know how this code works or compiles; And if one so happens to know how, well, that means I am fucked, which if the case, I will never know about, or will I? To who am I talking? I need help....Why am I still, you know waht, nvm, wtf am I doing with my life right now, uhm, is anyone still here? What are you doing here, Go DOWN and compile this shit, or are you scared? Oh wait, shiat, are you on my Github? Ahhhh, I see. Well...You know what, I am going to call this  thing, this class, the skript kiddy blocker ;p
        public static List<List<string>> Logins = new List<List<string>>()
        {
            new List<string>() { "DashSociety@protonmail.com", "DashSociety", "yteicoShsaD" },
            new List<string>() { "Friendo@protonmail.com", "Friendo", "odneirF" },
        };

        private static readonly List<string> login_messages = new List<string>()
        {
            "Please give me your registration email.", "Email",
            "Now, please give me your registration username.", "Username",
            "And now finally your registration password.", "Password"
        };

        private static List<string> credential_cache = new List<string>();

        public static void AuthenticateUser()
        {
            // Block second instances; only allow one instance at the time.
            int attempt_counter = 1;

        retype:

            if (credential_cache.Count > 0)
            {
                credential_cache.Clear();
            };

            for (int key = 0; key < login_messages.Count; key += 2)
            {
                Dash.WriteLine(">> ", DarkGray);
                Dash.WriteLine(login_messages[key] + "\r\n", Yellow);

                string credential = Dash.ReadLine(login_messages[key + 1]);

                credential_cache.Add(credential);
            };

            foreach (List<string> column in Logins)
            {
                int corrects = 0;

                for (int key = 0; key < column.Count; key += 1)
                {
                    if (!column[key].Equals(credential_cache[key]))
                    {
                        if (attempt_counter >= 3)
                        {
                            Dash.WriteLine("[-] ", White);
                            Dash.WriteLine("You have exceeded the maximum attempts, you may retry after an application restart.\r\n", Red);

                            Dash.WriteLine("[-] ", White);
                            Dash.WriteLine("If you  want to purchase a license or whatever, then feel free to contact the Developer at KvinneKraft@protonmail.com\r\n", Red);

                            Dash.WriteLine("[-] ", White);
                            Dash.WriteLine("Press any key to exit this application.", Red);

                            Console.ReadKey();
                            Environment.Exit(-1);
                        };

                        Dash.WriteLine("[-] ", White);
                        Dash.WriteLine("Either the email, username and or password was and thus is incorrect.\r\n", Red);

                        Dash.WriteLine("[-] ", White);
                        Dash.WriteLine($"Please retry, tries left: [{attempt_counter}/3]\r\n", Red);

                        attempt_counter += 1;

                        goto retype;
                    }

                    else
                    {
                        corrects += 1;
                    };
                };

                if (corrects >= 3)
                {
                    break;
                };
            };
        }
    };
};
