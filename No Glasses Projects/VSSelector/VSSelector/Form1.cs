using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace VSSelector
{
    public partial class Form1 : Form
    {
	private void start_process(string exe)
	{
	    Process p = new Process()
	    {
		StartInfo = new ProcessStartInfo()
		{
		    FileName = exe,
		}
	    };

	    p.Start();
	}

	private readonly Button VS2017 = new Button(), VS2019 = new Button();
	
	public Form1()
	{
	    FormBorderStyle = FormBorderStyle.None;
	    StartPosition = FormStartPosition.CenterScreen;

	    BackColor = Color.FromArgb(7, 3, 36);
	    ForeColor = Color.FromArgb(255, 255, 255);

	    Size cs = new Size(250, 65);

	    Size = cs;
	    MinimumSize = cs;
	    MaximumSize = cs;

	    Icon = Properties.Resources.icon;

	    Moon mon = new Moon();
	    
	    // Formula= (base.Width - ((button.Width * 2) + 5) ) / 2;
	    // Sum= 205
	    // Total= 350 // 350 - 205 = 145 / 2 = 72.5
	    // Total= 250 // 250 - 205 = 45 / 2 = 22.5

	    mon.Button(this, VS2017, "VSE 2017", 12, new Size(100, 35), new Point(20, 15), Color.FromArgb(52, 47, 115), Color.FromArgb(255, 255, 255), 8);

	    VS2017.Click += (s, e) =>
	    {
		start_process("C:\\Program Files (x86)\\Microsoft Visual Studio\\2017\\Enterprise\\Common7\\IDE\\devenv.exe");
		Environment.Exit(-1);
	    };

	    mon.Button(this, VS2019, "VSE 2019", 12, VS2017.Size, new Point(VS2017.Left + VS2017.Width + 10, VS2017.Top), VS2017.BackColor, VS2017.ForeColor, 8);

	    VS2019.Click += (s, e) =>
	    {
		start_process("C:\\Program Files (x86)\\Microsoft Visual Studio\\2019\\Enterprise\\Common7\\IDE\\devenv.exe");
		Environment.Exit(-1);
	    };

	    Paint += (s, e) => mon.paint_border(e, Color.FromArgb(8, 8, 8), 2, Size, Point.Empty);

	    mon.drag_material(this, this);

	    Text = "Dashies VSE Selector";
	}
    }

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

	    if (image != null) img.Image = image;

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

	    Size container_size = new Size(Form1.ActiveForm.Width - 10, Form1.ActiveForm.Height - 10);

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

	    Form1.
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
