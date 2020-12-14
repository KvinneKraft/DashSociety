// Hey there, here you can find several folders filled with code.</br>
//</br>
// Keep in mind that all of these codes are for personal use and were put together for such use only.</br>
//</br>
// You are the one that is responsible for that what you do with that what I offer.</br> 
//</br>
// README.md:</br>
</br>
<textarea>
using System;</br>
using System.Collections.Generic;</br>
</br>
namespace README</br>
{</br>
    public class Program</br>
    {</br>
        readonly public var DESCRIPTION = new List<List<string>>()</br>
        {</br>
            // I correspond to the c,c++,c# folder:</br>
            new List<string>() </br>
            {</br>
                "Here you can find some of my experimental software projects, the majority",</br>
                "of projects that you will be able to find here will be for purchase on my website",</br>
                "unless you want to clone them here and compile it for yourself."</br>
            },
            // I correspond to the FIRST EVER CODE folder:</br>
            new List<string>()</br>
            {</br>
                "You can find my recovered projects in here.  Some executables may be",</br>
                "missing because they got lost when I ran a program I made at the time. (dates back to 2015-2018)"</br>
            },</br>
            // I correspond to the html folder:</br>
            new List<string>()</br>
            {</br>
                "You may find beta-versions of web based applications in here, nothing too",</br>
                "fancy, I am not a web developer, I am a software engineer.",</br>
            },</br>
            // I correspond to the java folder:</br>
            new List<string>()</br>
            {</br>
                "This folder corresponds to my carreer as a Minecraft Java Developer, the story",</br>
                "behind this goes back pretty far, but to keep it short though, you can find",</br>
                "all of my old work in these folders, have fun!"</br>
            },</br>
        };</br>
</br>
        public static void Main(string[] args)</br>
        {</br>
            try</br>
            {</br>
                var index = int.Parse(args[0]);</br>
</br>
                foreach (var s in DESCRIPTION[index])</br>
                {</br>
                    Console.WriteLine($"- {s}");</br>
                };</br>
            }</br>
</br>
            catch</br>
            {</br>
                Console.WriteLine($"Max index: [0-{DESCRIPTION.Count}]");</br>
            };</br>
        }</br>
    }</br>
}</br>
</textarea>