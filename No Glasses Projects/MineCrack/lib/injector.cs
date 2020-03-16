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
    public class Moon : Form
    {
        public void Image(Control obj, PictureBox img, Image image, Size size, Point location)
        {
            img.Size = size;
            img.MinimumSize = size;
            img.MaximumSize = size;

            img.BorderStyle = BorderStyle.None;
            img.BackColor = Color.FromArgb(0, 0, 0, 255);

            if (location.X < 0)
            {
                location.X = (obj.Width - size.Width) / 2;
            };

            if (location.Y < 0)
            {
                location.Y = (obj.Height - size.Height) / 2;
            };

            img.Location = location;

            if(image != null) img.Image = image;

            obj.Controls.Add(img);
        }

        public void TextBox(Control obj, TextBox box, string text, int points, Size size, Point location, Color bgcolor, Color frcolor)
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

        public void Label(Control obj, Label label, string text, int points, Size size, Point location, Color color)
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

        public void Button(Control obj, Button button, string text, int points, Size size, Point location, Color bgcolor, Color frcolor, int border_radius)
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

            button.TextAlign = ContentAlignment.MiddleCenter;
            button.FlatStyle = FlatStyle.Flat;

            if (border_radius > 0)
            {
                button.Paint += (s, e) =>
                {
                    try
                    {
                        e.Graphics.SmoothingMode = SmoothingMode.HighQuality;

                        base.OnPaint(e);

                        Rectangle rectum = new Rectangle(0, 0, button.Width, button.Height);

                        GraphicsPath graphics_path = new GraphicsPath();

                        int radius = (border_radius) * 2;

                        graphics_path.AddArc(rectum.X, rectum.Y, radius, radius, 170, 90);
                        graphics_path.AddArc((rectum.X + rectum.Width - radius), rectum.Y, radius, radius, 270, 90);
                        graphics_path.AddArc((rectum.X + rectum.Width - radius), (rectum.Y + rectum.Height - radius), radius, radius, 0, 90);
                        graphics_path.AddArc(rectum.X, (rectum.Y + rectum.Height - radius), radius, radius, 90, 90);

                        Region reg = new Region(graphics_path);
                        button.Region = reg;
                    }

                    catch { };
                };
            };

            obj.Controls.Add(button);
        }

        public void drag_material(Control t, Control d)
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

        public void paint_border(PaintEventArgs e, Color color, float width, Size size, Point point)
        {
            Graphics graphics = e.Graphics;

            using (Pen pen = new Pen(color, width))
            {
                graphics.DrawRectangle(pen, new Rectangle(point, size));
            };
        }

        private List<ContainerControl> containers = new List<ContainerControl>();

        public void setup_container()
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
                paint_border(e, Color.FromArgb(1, 1, 1), 2, containers[container_key].Size, Point.Empty);

            KvinneKraft.
                ActiveForm.
                    Controls.
                        Add(containers[container_key]);
        }

        public void border_control(Control ctrl)
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
