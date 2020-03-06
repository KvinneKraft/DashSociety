

// Author: Dashie
// Version: 1.0


using System;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Collections.Generic;


namespace Dash_IP_Stresser
{
    public static class MOON
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


        public static void border_control(Control c)
        {
            c.Paint += (s, e) =>
            {
                GraphicsPath graphics_path = new GraphicsPath();
                Rectangle rectangle = c.ClientRectangle;

                rectangle.Inflate(10, 8);

                e.Graphics.DrawEllipse(Pens.Transparent, rectangle);

                rectangle.Inflate(-1, -1);
                graphics_path.AddEllipse(rectangle);

                c.Region = new Region(graphics_path);
            };
        }


        public static void install_menubar(Control frame, Control soil)
        {
            Size frame_size = new Size(soil.Width, 28);

            frame.Size = frame_size;
            frame.MinimumSize = frame_size;
            frame.MaximumSize = frame_size;

            drag_material(frame, soil);

            frame.BackColor = Color.FromArgb(8, 8, 8);

            soil.Controls.Add(frame);
        }


        public static void add_image(Control soil, PictureBox picturebox, Image image, Size image_size, Point image_point)
        {
            picturebox.Size = image_size;
            picturebox.MinimumSize = image_size;
            picturebox.MaximumSize = image_size;

            picturebox.BackColor = Color.FromArgb(0, 0, 0, 255);
            picturebox.BorderStyle = BorderStyle.None;

            picturebox.Location = image_point;
            picturebox.Image = image;

            soil.Controls.Add(picturebox);
        }


        public static void add_button(Control soil, Button button, string button_tag, int button_text_size, Color[] button_colors, int button_border_size, Size button_size, Point button_point)
        {
            button.Size = button_size;
            button.MinimumSize = button_size;
            button.MaximumSize = button_size;

            button.Font = new Font("Modern", button_text_size, FontStyle.Regular);
            button.Text = button_tag;

            button.BackColor = button_colors[0];
            button.ForeColor = button_colors[1];

            if(button_border_size > 0)
            {
                border_control(button);
            };

            button.FlatAppearance.BorderColor = Color.FromArgb(0, 0, 0, 255);
            button.FlatAppearance.BorderSize = 0;

            button.FlatStyle = FlatStyle.Flat;
            button.Location = button_point;

            soil.Controls.Add(button);
        }
    };
};
