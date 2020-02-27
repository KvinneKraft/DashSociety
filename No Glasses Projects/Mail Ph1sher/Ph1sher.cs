
// Author: Dashie
// Version: 1.0


using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Drawing.Drawing2D;

namespace Mail_Ph1sher
{
    class Program
    {
        public static Ph1sher ph1shy;

        [STAThread]
        static void Main()
        {
            Application.SetCompatibleTextRenderingDefault(true);

            ph1shy = new Ph1sher();

            Application.Run(ph1shy);
        }
    };



    public partial class Ph1sher : Form
    {
        private void ClientLayout()
        {
            Hide();
            BringToFront();
    
            Size client_size = new Size(350, 350);

            Size = client_size;
            MaximumSize = client_size;
            MinimumSize = client_size;

            MinimizeBox = false;
            MaximizeBox = false;

            //Icon = (Icon) Properties.Resources.Icon;

            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.None;

            BackColor = Color.FromArgb(24, 24, 24);
            Text = " Dashies Mail Ph1sher  ☽⛤☾ ";

            Show();
        }


        public Ph1sher()
        {
            ClientLayout();

            Paint += (s, e) =>
            {
                Moon.paint_border(e, Color.FromArgb(1, 1, 1), 2, Size, Point.Empty);
            };
        }
    };



    public static class Moon
    {
        public static void drag_material(Control t, Control d)
        {
            Point point = Point.Empty;

            t.MouseDown += (s, e) => 
                point = new Point(e.X, e.Y);
            
            t.MouseUp += (s, e) => 
                point = Point.Empty;

            t.MouseMove += (s, e) =>
            {
                if(point.IsEmpty)
                {
                    return;
                };

                d.Location = new Point(d.Location.X + (e.X - point.X), d.Location.Y + (e.Y - point.Y));
            };
        }


        public static void paint_border(PaintEventArgs e, Color color, float width, Size size, Point point)
        {
            Graphics graphics = e.Graphics;
            
            using (Pen pen = new Pen(color, width))
            {
                graphics.DrawRectangle(pen, new Rectangle(point, size));
            };
        }


        private static List<ContainerControl> containers = new List<ContainerControl>();


        public static void setup_container()
        {
            Color container_color = Color.FromArgb(16, 16, 16);
            Point container_point = new Point(5, 5);

            Size container_size = new Size(Ph1sher.ActiveForm.Width - 10, Ph1sher.ActiveForm.Height - 10);

            containers.Add(
                new ContainerControl()
                {
                    Location = container_point,
                    Size = container_size,
                    BackColor = container_color,
                }
            );

            int container_key = containers.Count - 1;

            containers[container_key].Paint += (s, e) =>
                Moon.paint_border(e, Color.FromArgb(1, 1, 1), 2, containers[container_key].Size, Point.Empty);

            Ph1sher.
                ActiveForm.
                    Controls.
                        Add(containers[container_key]);
        }


        public static void border_control(Control ctrl)
        {
            ctrl.Paint += (s, e) =>
            {
                GraphicsPath graphics_path = new GraphicsPath();
                Rectangle rectangle = ctrl.ClientRectangle;

                rectangle.Inflate(10, 8);

                e.Graphics.DrawEllipse(Pens.Transparent, rectangle);

                rectangle.Inflate(-1, -1);
                graphics_path.AddEllipse(rectangle);

                ctrl.Region = new Region(graphics_path);
            };
        }
    };
};
