/*

(c) All Rights Reserved, Dashies Software Inc. 
 
*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Resources;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace src
{
    public class DashBase
    {
        public class Error_Implementation
        {

        }





        public class Collector
        {
            public string LogStartMessage()
            {
                string Message =
                (
                    "==================================\r\n\r\n" +

                    "Dashies Software Corporations \\(≧◡≦✿)  2018\r\n\r\n" +

                    "=====================\r\n\r\n" +

                    "Welcome fellow creature of this planet we call\r\n" +
                    "earth, today I am here to show you tha features\r\n" +
                    "of the Dashies Port Scanner.\r\n\r\n" +

                    "correct target Formats :\r\n" +
                    " >! HTTP urls, http://www.url.com\r\n" +
                    " >! HTTPS urls, https://www.url.com\r\n" +
                    " >! Private IPv4, 192.0.0.1\r\n" +
                    " >! Public IPv4, 82.65.102.200\r\n\r\n" +

                    "correct port Formats :\r\n" + 
                    " >! specific ports, 1, 2, 3, 4, 5\r\n" + 
                    " >! range of ports, 80-8080\r\n" + 
                    " >! specific port 443\r\n\r\n" +

                    "Please know that the formats are specific\r\n" +
                    "and real strict, if you fail to implement\r\n" +
                    "the same format into your own format then\r\n" +
                    "you may fail to scan your target.\r\n\r\n" +

                    "Now, this thing is still in \"BETA\" so bugs may\r\n" +
                    "be present during runtime unfortunately.\r\n\r\n" +

                    "Keep on reading if you are interested in any of\r\n" +
                    "the optional information and or notes about this\r\n" +
                    "amazing, beautiful and magnificant application.\r\n\r\n" +

                    "This first release is purely focused on functionality\r\n" +
                    "which means that it may not look as sexy as you may\r\n" +
                    "expect from me, but it will be more beautiful in the\r\n" +
                    "near upcoming future!\r\n\r\n" +
                    
                    "This first release comes with the option to select a\r\n" +
                    "custom Protocol which will be used to determine open\r\n" +
                    "Ports on the targeted ipv4 address.\r\n\r\n" +
                    
                    "Besides all the scan types, we also have scan modules\r\n" +
                    "such as TCP ASync Scanning, UDP Multi Scanning and\r\n" +
                    "a lot of other cool features, these things can be\r\n" +
                    "found inside of the Methods section.\r\n\r\n" +

                    "This entire application has been written in C# and\r\n" +
                    "C which means that the codes of this thing are long\r\n" + 
                    "and awful.\r\n\r\n" +

                    "I have personally implemented my preference for\r\n" +
                    "style and layout and also my personal code\r\n" +
                    "structures, I have tried to make them as optimized\r\n" + 
                    "as possible, to keep in mind that I have compiled\r\n" + 
                    "this main EXE using the remake of the .NET Framework\r\n" + 
                    "4.7 Microsoft Visual Studio Enterprise 2017 Licensed!\r\n\r\n" +

                    "I am not responsible for none-legal actions taken by\r\n" +
                    "the user of this program, thereby you, the user of\r\n" +
                    "this application is responsible for whatever you do\r\n" +
                    "with this, it is prohibited to keep me responsible\r\n" +
                    "for your own inresponsible actions, so thereby I\r\n" +
                    "define you as responsible for anything you do with\r\n" +
                    "this amazing piece of art.\r\n\r\n" +

                    "Now, all and all, this sounds pretty neat, right, to\r\n" +
                    "be completely honest, I dunno what else to type over\r\n" +
                    "here besides that I am happy that you have taken the\r\n" + 
                    "time to read all of this bullshit, have a nice day and\r\n" + 
                    "know, you are being appreciated by tha Dashie!\r\n\r\n" +

                    "=================================="
                );

                return Message;
            }

            public string SettingsInfo()
            {
                string Message = 
                    (
                        "\r\n OPTIONAL INFORMATION ABOUT THIS SECTION\r\n\r\n" +

                        " ======================================\r\n\r\n" +
                        
                        " Methods are basically modules and\r\n" +
                        " configuration options for the Types,\r\n" +
                        " as I have detailed bellow.\r\n\r\n" +
                        
                        " Types is the section where you can\r\n" + 
                        " select optional options, such as\r\n" + 
                        " Stealth Scanning or Kick Scanning.\r\n\r\n" +
                        
                        " Presets are like the name suggests,\r\n" +
                        " already set options that you may\r\n" +
                        " use or and modify to your liking.\r\n\r\n" +
                        
                        " More information about these\r\n" + 
                        " Options, Methods and Types are\r\n" +
                        " available on the Right." );

                return Message;
            }

            public class Configurations
            {
                public class Button
                {
                    public int[] RGB = new int[]
                    {
                        16, 16, 16,
                        235, 235, 235,
                        18, 18, 18,
                        255, 255, 255,

                        16, 16, 16,
                        235, 235, 235,
                        18, 18, 18,
                        255, 255, 255,

                        35, 5, 140,
                        255, 255, 255,
                        42, 5, 173,
                        255, 255, 255,

                        35, 5, 140,
                        255, 255, 255,
                        42, 5, 173,
                        255, 255, 255,

                        // Scan Methods
                        35, 5, 140,
                        255, 255, 255,
                        42, 5, 173,
                        255, 255, 255,

                        // Scan Types
                        35, 5, 140,
                        255, 255, 255,
                        42, 5, 173,
                        255, 255, 255
                    };

                    public int[] Point = new int[]
                    {
                        0, 0,  
                        0, 0,   
                        20, 150,  
                        187, 150,  
                        20, 115,  // Scan Methods
                        187, 115, // Scan Types
                    };

                    public int[] Size = new int[]
                    {
                        135, 24,
                        135, 24,
                        159, 28,
                        159, 28,
                        159, 28, // Scan Methods 
                        159, 28, // Scan Types
                    };

                    public int[] FontSize = new int[]
                    {
                        12,
                        12,
                        9,
                        9,
                        9, // Scan Methods
                        9, // Scan Types
                    };

                    public string[] Tags = new string[]
                    {
                        "X", "-", "Start Scan", "Abort Scan", "Scan Methods", "Scan Types"
                    };
                }
            }

            public static Button Scan_Methods = new Button(), Scan_Types = new Button(), Start_Scanning = new Button(), Stop_Scanning = new Button(), Quit = new Button(), Minimize = new Button();

            public Button[] Buttons = new Button[]
            {
                Quit, Minimize, Start_Scanning, Stop_Scanning, Scan_Methods, Scan_Types
            };

            public TextBox Log = new TextBox(), Target = new TextBox(), Port = new TextBox();
            public Label _Target = new Label(), _Port = new Label();

            public List<Control> database = new List<Control>() { /* Yaaay */ };
            public List<object> drawing = new List<object>() { /* Yaaay */ };
        }
    }
}
