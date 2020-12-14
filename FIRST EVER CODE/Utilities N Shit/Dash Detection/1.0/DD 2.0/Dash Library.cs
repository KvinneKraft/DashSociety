
/* (c) All Rights Reserved, Dashies Software Inc. */



using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Resources;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace DD_2._0 {
    class Dash_Library {
        public struct Menu {
            public int CMA_LEFT, CMA_RIGHT;
        };

        Menu menu;

        public String resourceId = ("DD_2._0.Embeded");

        public Boolean InjectImage(Form Inject, PictureBox Element, int X, int Y, int Width, int Height, String Resource, String File, int red, int green, int blue) {
            if((Width <= 0) || (Height <= 0)) {
                return false;
            } else Element.Size = new System.Drawing.Size(Width, Height);

            if(Element.Size != new System.Drawing.Size(Width, Height)) {
                return false;
            }

            if(X <= 0) {
                X = (Inject.Width - Width) / 2;
            }

            Element.Location = new System.Drawing.Point(X, Y);

            if(Element.Location != new System.Drawing.Point(X, Y)) {
                return false;
            }

            if((red < 0) && (green < 0) && (blue < 0)) {
                return false;
            } else Element.BackColor = Color.FromArgb(red, green, blue);

            if(Resource.Length <= 0) {
                if(File.Length <= 0) {
                    return false;
                } Element.Image = Image.FromFile(File);
            } else {
                ResourceManager resLoader = new ResourceManager((resourceId), Assembly.GetExecutingAssembly());
                Element.Image = (Image)resLoader.GetObject(File);
            }

            Inject.Controls.Add(Element);
            return true;
        }

        public Boolean CreateMenu(Form Inject, int X, int Y, int Width, int Height, String Profile, int pWidth, int pHeight, int pAlign, int red, int green, int blue) {
            if(menu.CMA_LEFT != pWidth) menu.CMA_LEFT = pWidth;
            if(menu.CMA_RIGHT != Width-pWidth) menu.CMA_RIGHT = Width-pWidth;

            PictureBox Blade = new PictureBox();
            PictureBox ProfileId = new PictureBox();

            if(Profile.Length > 0) {
                if((pWidth <= 0) || (pHeight <= 0)) {
                    return false;
                } else {
                    if(InjectImage(Inject, ProfileId, 0, 0, pWidth, pHeight, (Profile), (""), red, green, blue) != true) {
                        return false;
                    }
                }
            }

            if(InjectImage(Inject, Blade, X, Y, Width, Height, ("not real"), (""), red, green, blue) != true) {
                return false;
            }

            return false;
        }


    }
}
