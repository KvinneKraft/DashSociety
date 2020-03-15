using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Collections.Generic;


namespace MineCrack
{
    public static class Moon
    {
        public static void Image()
        {

        }


        public static void TextBox(Form obj, TextBox box, string text, int points, Size size, Point location, Color bgcolor, Color frcolor)
        {
            box.Size = size;
            box.MinimumSize = size;
            box.MaximumSize = size;

            if (location.X < 0)
            {
                location.X = (obj.Width - size.Width) / 2;
            };

            if (location.Y < 0)
            {
                location.Y = (obj.Height - size.Height) / 2;
            };

            box.Location = location;

            box.Font = new Font("Modern", points);
            box.Text = text;

            box.BackColor = bgcolor;
            box.ForeColor = frcolor;

            box.BorderStyle = BorderStyle.None;
            box.ReadOnly = false;

            obj.Controls.Add(box);
        }


        public static void Label(Form obj, Label label, string text, int points, Size size, Point location, Color color)
        {
            label.Size = size;
            label.MinimumSize = size;
            label.MaximumSize = size;

            if (location.X < 0)
            {
                location.X = (obj.Width - size.Width) / 2;
            };

            if (location.Y < 0)
            {
                location.Y = (obj.Height - size.Height) / 2;
            };

            label.Location = location;

            label.Font = new Font("Modern", points);
            label.Text = text;

            label.BackColor = Color.FromArgb(0, 0, 0, 255);
            label.ForeColor = color;

            obj.Controls.Add(label);
        }


        public static void Button(Form obj, Button button, string text, int points, Size size, Point location, Color bgcolor, Color frcolor)
        {
            button.Size = size;
            button.MinimumSize = size;
            button.MaximumSize = size;

            if (location.X < 0)
            {
                location.X = (obj.Width - size.Width) / 2;
            };

            if (location.Y < 0)
            {
                location.Y = (obj.Height - size.Height) / 2;
            };

            button.Location = location;

            button.Font = new Font("Modern", points);
            button.Text = text;

            button.BackColor = bgcolor;
            button.ForeColor = frcolor;

            button.FlatAppearance.BorderColor = Color.FromArgb(0, 0, 0, 255);
            button.FlatAppearance.BorderSize = 0;

            button.FlatStyle = FlatStyle.Flat;

            obj.Controls.Add(button);
        }


        public static void drag_material(Control t, Control d)
        {
            Point point = Point.Empty;

            t.MouseDown += (s, e) =>
                point = new Point(e.X, e.Y);

            t.MouseUp += (s, e) =>
                point = Point.Empty;

            t.MouseMove += (s, e) =>
            {
                if (point.IsEmpty)
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

            Size container_size = new Size(KvinneKraft.ActiveForm.Width - 10, KvinneKraft.ActiveForm.Height - 10);

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

            KvinneKraft.
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

                rectangle.Inflate(10, 8); // 10 - 8

                e.Graphics.DrawEllipse(Pens.Transparent, rectangle);

                rectangle.Inflate(-1, -1);
                graphics_path.AddEllipse(rectangle);

                ctrl.Region = new Region(graphics_path);
            };
        }
    };
}
