// Hey there, here you can find several folders filled with code.</br>
//</br>
// Keep in mind that all of these codes are for personal use and were put together for such use only.</br>
//</br>
// You are the one that is responsible for that what you do with that what I offer.</br> 
//</br>
// README.md:</br>
</br>
using System;</br>
using System.Collections.Generic;</br>
</br>
namespace README
{
    public class Program
    {
        readonly public var DESCRIPTION = new List<List<string>>()
        {
            // I correspond to the c,c++,c# folder:
            new List<string>() 
            {
                "Here you can find some of my experimental software projects, the majority",
                "of projects that you will be able to find here will be for purchase on my website",
                "unless you want to clone them here and compile it for yourself."
            },
            // I correspond to the FIRST EVER CODE folder:
            new List<string>()
            {
                "You can find my recovered projects in here.  Some executables may be",
                "missing because they got lost when I ran a program I made at the time. (dates back to 2015-2018)"
            },
            // I correspond to the html folder:
            new List<string>()
            {
                "You may find beta-versions of web based applications in here, nothing too",
                "fancy, I am not a web developer, I am a software engineer.",
            },
            // I correspond to the java folder:
            new List<string>()
            {
                "This folder corresponds to my carreer as a Minecraft Java Developer, the story",
                "behind this goes back pretty far, but to keep it short though, you can find",
                "all of my old work in these folders, have fun!"
            },
        };

        public static void Main(string[] args)
        {
            try
            {
                var index = int.Parse(args[0]);

                foreach (var s in DESCRIPTION[index])
                {
                    Console.WriteLine($"- {s}");
                };
            }

            catch
            {
                Console.WriteLine($"Max index: [0-{DESCRIPTION.Count}]");
            };
        }
    }
}