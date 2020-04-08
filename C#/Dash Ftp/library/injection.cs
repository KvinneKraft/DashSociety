
// Author: Dashie
// Version: 6.0


using System;
using System.Linq;
using System.Drawing;
using System.Resources;
using System.Reflection;
using System.Drawing.Text;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Collections.Generic;
using System.Runtime.InteropServices;


/*[####################################]*/
/*[(Friendship is Magic Main Namespace)]*/
/*[####################################]*/
namespace DashCore
{
    /*[############################]*/
    /*[(Form Control Modules Class)]*/
    /*[############################]*/
    public class Cawntrols : Form
    {
        /*[####################]*/
        /*[(Furrball Resources)]*/
        /*[####################]*/
        public static readonly ResourceManager furball_resources = new ResourceManager(($"DashCore.Properties.Resources"), Assembly.GetExecutingAssembly());


        /*[###########################]*/
        /*[(Calculate Font Size Thing)]*/
        /*[###########################]*/
        
        public Size CalculateFontThing(string text, Font font)
        {
            using (Bitmap tmp = new Bitmap(400, 400))
            {
                SizeF size = Graphics.FromImage(tmp).MeasureString(text, font);
                return new Size(size.ToSize().Width + 1, size.ToSize().Height); // size.ToSize();
            };
        }


        /*[###################################]*/
        /*[(Dashies Magical Button Push Thing)]*/
        /*[###################################]*/
        public void FluffyMagicalButton(Control surface, Button inherit, string text, int font_size, Point flubb_location, Size flubby_size, int border_radius, Color[] back_colour, Color[] fore_colour)
        {
            try
            {
                inherit.BackColor = back_colour[0];
                inherit.ForeColor = fore_colour[0];

                inherit.Font = FlubbyFont(font_size, false);
                inherit.Text = text;

                inherit.Location = flubb_location;

                Size thing_ding_size = flubby_size;

                inherit.Size = thing_ding_size;
                inherit.MinimumSize = thing_ding_size;
                inherit.MaximumSize = thing_ding_size;

                inherit.FlatStyle = FlatStyle.Flat;

                inherit.FlatAppearance.BorderColor = back_colour[0];
                inherit.FlatAppearance.BorderSize = 0;

                if (back_colour.Length > 1)
                {
                    inherit.FlatAppearance.MouseOverBackColor = back_colour[1];
                    inherit.FlatAppearance.MouseDownBackColor = back_colour[1];

                    MakeFluff(inherit, back_colour);
                };

                if (border_radius > 0)
                {
                    inherit.Paint += (s, e) =>
                    {
                        try
                        {
                            e.Graphics.SmoothingMode = SmoothingMode.HighQuality;

                            base.OnPaint(e);

                            Rectangle rectum = new Rectangle(0, 0, inherit.Width, inherit.Height);

                            GraphicsPath graphics_path = new GraphicsPath();

                            int radius = (border_radius) * 2;

                            graphics_path.AddArc(rectum.X, rectum.Y, radius, radius, 180, 90);
                            graphics_path.AddArc((rectum.X + rectum.Width - radius), rectum.Y, radius, radius, 270, 90);
                            graphics_path.AddArc((rectum.X + rectum.Width - radius), (rectum.Y + rectum.Height - radius), radius, radius, 0, 90);
                            graphics_path.AddArc(rectum.X, (rectum.Y + rectum.Height - radius), radius, radius, 90, 90);

                            Region reg = new Region(graphics_path);
                            inherit.Region = reg;
                        }

                        catch { };
                    };
                };
            }

            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            };
            
            surface.Controls.Add(inherit);
        }


        /*[##############################]*/
        /*[(Inject Fluffy TextBox Thingy)]*/
        /*[##############################]*/
        private readonly List<PictureBox> textbox_containers = new List<PictureBox>();

        public void FluffyTextBoxy(Control surface, TextBox inherit, bool addContainer, int font_size, string[] text, Point location, Size size, Color fluffyness, Color morefluff)
        {
            inherit.BorderStyle = BorderStyle.None;
            inherit.Font = FlubbyFont(font_size, false);

            inherit.BackColor = fluffyness;
            inherit.ForeColor = morefluff;

            if (text.Length > 1)
            {
                inherit.ScrollBars = ScrollBars.Vertical;

                inherit.Multiline = true;
                inherit.ReadOnly = true;
            };

            foreach (string line in text)
            {
                inherit.AppendText($"{line}\r\n");
            };

            if (addContainer)
            {
                Point container_location = location;

                location.Y = (size.Height - inherit.PreferredHeight) / 2;
                location.X = 5;

                Size container_size = size;

                size = new Size(size.Width - (location.X * 2), inherit.PreferredHeight);

                if (container_location.X < 0)
                {
                    int X, Y;

                    X = (surface.Width - size.Width) / 2;
                    Y = container_location.Y;

                    container_location = new Point(X, Y);
                };

                textbox_containers.Add(new PictureBox());

                // Change to PictureBox
                Picture(surface, textbox_containers[textbox_containers.Count - 1], String.Empty, container_location);

                textbox_containers[textbox_containers.Count - 1].BackColor = fluffyness;
                textbox_containers[textbox_containers.Count - 1].Click += (s, q) => inherit.Focus();

                ResizeBawl(textbox_containers[textbox_containers.Count - 1], container_size);
            };

            inherit.MinimumSize = size;
            inherit.MaximumSize = size;
            inherit.Size = size;

            if (location.X < 0)
            {
                int X, Y;

                X = (surface.Width - size.Width) / 2;
                Y = location.Y;

                location = new Point(X, Y);
            };

            inherit.Location = location;

            if (addContainer)
                textbox_containers[textbox_containers.Count - 1].
                    Controls.Add(inherit);
            else
                surface.
                    Controls.Add(inherit);
        }


        /*[############################]*/
        /*[(Inject Flubby Label Thingy)]*/
        /*[############################]*/
        public void FlubbyLabel(Control surface, Label inherit, string text, int font_size, Point flubb_location, Color fluff_colour)
        {
            inherit.BackColor = Color.FromArgb(0, 0, 0, 255);
            inherit.ForeColor = fluff_colour;

            inherit.Font = FlubbyFont(font_size, false);
            inherit.Text = text;

            Size thingy_size = CalculateFontThing(text, FlubbyFont(font_size, false));

            inherit.Size = thingy_size;
            inherit.MinimumSize = thingy_size;
            inherit.MaximumSize = thingy_size;

            if (flubb_location.X < 0)
            {
                int X, Y;

                X = (surface.Width - thingy_size.Width) / 2;
                Y = flubb_location.Y;

                flubb_location = new Point(X, Y);
            };

            inherit.Location = flubb_location;

            inherit.BorderStyle = BorderStyle.None;
            inherit.FlatStyle = FlatStyle.Flat;

            surface.Controls.Add(inherit);
        }


        /*[##########################]*/
        /*[(Inject Image Ball Thingy)]*/
        /*[##########################]*/
        public void Picture(Control surface, PictureBox inherit, string Image, Point location)
        {
            inherit.BackColor = Color.FromArgb(0, 0, 0, 255);

            inherit.BorderStyle = BorderStyle.None;
            inherit.Location = location;

            if (Image != String.Empty)
            {
                inherit.Image = (Image)furball_resources.GetObject(Image);

                inherit.Size = inherit.PreferredSize;
                inherit.MinimumSize = inherit.Size;
                inherit.MaximumSize = inherit.Size;
            };

            surface.Controls.Add(inherit);
        }

        
        /*[###############################]*/
        /*[(Make Flubby things FluffFluff)]*/
        /*[###############################]*/
        public void MakeFluff(Control inherit, Color[] tableOf)
        {
            inherit.MouseClick += (s, q) => inherit.BackColor = tableOf[1];
            inherit.MouseDown += (s, q) => inherit.BackColor = tableOf[1];
            inherit.MouseUp += (s, q) => inherit.BackColor = tableOf[0];

            return;
        }


        /*[############################]*/
        /*[(Make Fluffy things Flubber)]*/
        /*[############################]*/
        public void Drag(Control trigger, Control affect, Point mouseDownPoint)
        {
            trigger.MouseDown += (s, q) =>
            {
                mouseDownPoint = new Point(q.X, q.Y);
                return;
            };

            trigger.MouseMove += (s, q) =>
            {
                if (mouseDownPoint.IsEmpty)
                    return;

                int X = (affect.Location.X + (q.X - mouseDownPoint.X));
                int Y = (affect.Location.Y + (q.Y - mouseDownPoint.Y));

                affect.Location = new Point(X, Y);
                return;
            };

            trigger.MouseUp += (s, q) =>
            {
                mouseDownPoint = Point.Empty;
                return;
            };
        }


        /*[########################]*/
        /*[(Rewidth anythang bruh!)]*/
        /*[########################]*/
        public void ResizeBawl(Control inherit, Size new_size)
        {
            inherit.MaximumSize = new_size;
            inherit.MinimumSize = new_size;
            inherit.Size = new_size;

            return;
        }


        /*[#############################]*/
        /*[(Draw Floofy Border DingDong)]*/
        /*[#############################]*/
        public void FloofyBorder(PaintEventArgs q, Color colour, Point location, Size size)
        {
            Pen pen = new Pen(colour, 2);
            q.Graphics.DrawRectangle(pen, location.X, location.Y, size.Width, size.Height);
            return;
        }


        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        private static extern IntPtr AddFontMemResourceEx(IntPtr pbFont, uint cbFont, IntPtr pdv, [System.Runtime.InteropServices.In] ref uint pcFonts);

        private static readonly ResourceManager resource_manager = new ResourceManager(($"DashCore.Properties.Resources"), Assembly.GetExecutingAssembly());
        private static readonly byte[] font_data = (byte[])resource_manager.GetObject("custom");


        /*[#####################]*/
        /*[(Inject Flubby Fawnt)]*/
        /*[#####################]*/
        private static PrivateFontCollection PermFontCollection = new PrivateFontCollection();

        public static void InitFont()
        {
            IntPtr font_ptr = Marshal.AllocCoTaskMem(font_data.Length);
            Marshal.Copy(font_data, 0, font_ptr, font_data.Length);
            uint fluff = 0;
            AddFontMemResourceEx(font_ptr, (uint)font_data.Length, IntPtr.Zero, ref fluff);
            PermFontCollection.AddMemoryFont(font_ptr, font_data.Length);
        }

        public static Font FlubbyFont(int pt, bool bold)
        {
            if (PermFontCollection.Families.Length < 1)
                InitFont();

            FontStyle fontStyle = FontStyle.Bold;

            if (!bold)
                fontStyle = FontStyle.Regular;

            return new Font(PermFontCollection.Families[0], pt, fontStyle);
        }
    };
};
