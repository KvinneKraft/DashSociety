using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace src
{
    public partial class Optical
    {
        public class Information
        {
            public void information_dictionary(int id)
            {
                String[] load_dictionary_component = {
                    (
                        "The  User Datagram Protocol  offers  some\r\n" +
                        "amazing capabilities that we can use  for\r\n" +
                        "our own purposes, such as this tool ;)\r\n\r\n" +
                        
                        "One of those capabilities is  the A.Y.C.E\r\n" +
                        "Technique, which basically means, All You\r\n" +
                        "You Can Eat, in other words, send as much\r\n" +
                        "Data as you can, we have used this vulne-\r\n" +
                        "rability in such a way  that you  can now\r\n" +
                        "send up to 10 Gb/Ps!"
                    ),


                    ( // Rain in Jiang Nan_Erhu Cover <--- LISTEN TO DISSSS
                        "" +
                        "" +
                        "" +
                        "" +
                        "" +
                        "" +
                        "" +
                        "2"
                    ),


                    (
                        "" +
                        "" +
                        "" +
                        "" +
                        "" +
                        "" +
                        "" +
                        "3"
                    ),


                    (
                        "" +
                        "" +
                        "" +
                        "" +
                        "" +
                        "" +
                        "" +
                        "4"
                    ),


                    (
                        "" +
                        "" +
                        "" +
                        "" +
                        "" +
                        "" +
                        "" +
                        "5"
                    ),

                    (
                        "" +
                        "" +
                        "" +
                        "" +
                        "" +
                        "" +
                        "" +
                        "6"
                    ),


                    (
                        "" +
                        "" +
                        "" +
                        "" +
                        "" +
                        "" +
                        "" +
                        "7"
                    ),


                    (
                        "" +
                        "" +
                        "" +
                        "" +
                        "" +
                        "" +
                        "" +
                        "8"
                    ),


                    (
                        "" +
                        "" +
                        "" +
                        "" +
                        "" +
                        "" +
                        "" +
                        "9"
                    )
                };

                String b = ("=========================================");

                Console.WriteLine($"{b}");

                Console.WriteLine(load_dictionary_component[id - 1]);

                Console.WriteLine($"{b}");

                return;
            }
        }




        public class Utilities
        {
            private static readonly char path_identifier1 = ('-'), path_identifier2 = ('>');




            public void integer_conversion_error()
            { function_error("error", "Integer conversion failed, please try again :c\r\n"); }



            public void function_error(String module, String error)
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write("[");

                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.Write($"{module}");

                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write(":/> ");

                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write($"{error}");

                Console.ForegroundColor = ConsoleColor.Magenta;
            }



            public void function_module(String module)
            {
                // ([dashcore->modules->networking->{module}])>

                string complete = String.Empty;
                string[] parts;

                module = $"([->dashcore->modules->net_api->{module}->])";
                parts = module.Split(path_identifier1);

                foreach (string part in parts)
                { complete += part; }

                parts = complete.Split(path_identifier2);

                foreach (string part in parts)
                {
                    if ((part != "([") && (part != "])"))
                    {
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        Console.Write($"{part}");

                        if (part != parts[parts.Length - 2])
                        {
                            Console.ForegroundColor = ConsoleColor.DarkCyan;
                            Console.Write($"{path_identifier1}{path_identifier2}");
                        }
                    }

                    else
                    {
                        if (part == "([")
                        {
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.Write("(");

                            Console.ForegroundColor = ConsoleColor.DarkCyan;
                            Console.Write("[");
                        }

                        else
                        if (part == "])")
                        {
                            Console.ForegroundColor = ConsoleColor.DarkCyan;
                            Console.Write("]");

                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.Write(")> ");
                        }
                    }
                }

                Console.ForegroundColor = ConsoleColor.Magenta;
            }
        }
    }
}
