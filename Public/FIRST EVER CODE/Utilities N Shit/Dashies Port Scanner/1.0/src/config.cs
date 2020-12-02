
/*

(c) All Rights Reserved, Dashies Software Inc. 
 
*/

using System;

namespace src 
{
    public class SplashScreenConfiguration
    {
        public string Image = "Splash_Screen", Embeded_Key = "src.embeded", Title = "Dashness <3";

        public int[] Size = { 600, 337 };
        public int[] Coordination = { 0, 0 };

        public bool IsCentered = true, IsBordered = false, IsResource = true;
    }




    public class MainConfiguration
    {
        private static string GenerateTitle()
        {
            String[] Titles = { "≧◡≦", "Dashies Amazing Port Scanner", "I <3 You", "Ponyness", "Dashies Software (c) 2018", "It is almost 2019!", "cupcakes and muffins :3" };
            String result = String.Empty;
            Random rdn = new Random();

            result = Titles[rdn.Next(0, Titles.Length-1)].ToString();

            if(result == String.Empty)
            {
                result = "invalid title received.";
            }

            return result;
        }

        public int[] Size = { 800, 205 };
        public int[] Coordination = { 0, 0 };
        public int[] RGB = { 48, 48, 48 };

        public string Title = GenerateTitle(), Icon = "application_icon", Embeded_Key = "src.embeded";

        public bool IsCentered = true, IsBordered = false;
    }
}
