
/* Book Library 1.0 - Dashies Software (c) All Rights Reserved. */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Resources;
using System.Reflection;
using System.Collections;
using System.Threading.Tasks;

namespace Book_Library
{
    public partial class Chapters
    {
        public string ChapterFromResource(string File)
        { return File; }
    }

    class Program
    {
        public static void ResetConsole()
        { Console.ForegroundColor = ConsoleColor.Gray; }

        public static void Error(string Error)
        { Console.ForegroundColor = ConsoleColor.Red;
          Console.Write(Error); }

        public static string Tag(ConsoleColor c)
        { Console.ForegroundColor = ConsoleColor.DarkCyan;
          Console.Write("[:/Book>");
          Console.ForegroundColor = c; return ""; }

        public static int[] chapters = { 1, 2 };
        public static string[] chapter_listing = { Book_Library.Properties.Resources._1, Book_Library.Properties.Resources._2 };

        public static Boolean readChapter(int chapter)
        {
            try
            {
                Console.WriteLine($"{Tag(ConsoleColor.DarkGreen)} Trying to initialize chapter {chapter} ....");

                Chapters view = new Chapters();

                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write(view.ChapterFromResource(chapter_listing[chapter-1]));

                Console.Write($"{Tag(ConsoleColor.DarkGreen)} Successfully initialized chapter {chapter} c:");

                return true;
            }

            catch //(Exception e)
            {
                //Console.WriteLine(e.ToString());
                return false;
            }
        }

        static void Main(string[] args)
        {
            Console.Title = "Dashies Software 2019 (c) All Rights Reserved";

            if (args.Length - 1 >= 2)
            {
                if (args[0].ToLower() == "--view")
                {
                    if (args[1].ToLower() == "--chapter")
                    {
                        bool feedback = false;

                        foreach (int chapter in chapters)
                        {
                            if (args[2].ToLower() == chapter.ToString())
                            {
                                feedback = true;

                                if(readChapter(chapter) != true) Error($"{Tag(ConsoleColor.Red)} Unable to initialize chapter {args[2]} :\'c");

                                break;
                            }
                        }

                        if (feedback == false) Error($"{Tag(ConsoleColor.Red)} Chapter \"{args[2]}\" has not been found in our library!");
                    }

                    else Error($"{Tag(ConsoleColor.Red)} You must specify a chapter :c");
                }

                else Error($"{Tag(ConsoleColor.Red)} Unrecognized argument received for first parameter :\'c");
            }

            ResetConsole();
        }
    }
}
