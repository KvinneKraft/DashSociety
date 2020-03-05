

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
    };
};
