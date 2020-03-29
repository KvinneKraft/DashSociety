

// Author: Dashie
// Version: 1.0


using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing.Text;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System;


namespace Login_Form
{
    public class Program
    {
	[STAThread]
	static void Main()
	{
	    Application.EnableVisualStyles();
	    Application.SetCompatibleTextRenderingDefault(false);
	    Application.Run(new LoginInterface());
	}
    };


    public partial class LoginInterface : Form
    {
	private readonly PictureBox menu_bar = new PictureBox();
	private readonly PictureBox logo = new PictureBox();

	private readonly Button quit = new Button();

	private readonly Label title = new Label();

	private readonly Moon mon = new Moon();

	private void SetupLayout()
	{
	    mon.drag_material(menu_bar, this);
	    mon.drag_material(title, this);
	    mon.drag_material(logo, this);

	    Size size = new Size(300, 150);

	    Size = size;
	    MinimumSize = size;
	    MaximumSize = size;

	    mon.Image(this, menu_bar, null, new Size(Width - 2, 26), new Point(1, 0));

	    menu_bar.BackColor = Color.FromArgb(0, 3, 41);

	    FormBorderStyle = FormBorderStyle.None;
	    StartPosition = FormStartPosition.CenterScreen;

	    Text = ("Login Authentication");
	    BackColor = Color.FromArgb(11, 16, 71);

	    Icon = Properties.Resources.icon;

	    Paint += (s, e) =>
	    {
		mon.paint_border(e, Color.FromArgb(0, 3, 41), 2, Size, Point.Empty);
	    };
	    
	    mon.Image(menu_bar, logo, Properties.Resources.logo, new Size(24, 24), new Point(0, 1));
	    mon.Label(menu_bar, title, Text, 9, Point.Empty, Color.FromArgb(25, 197, 0));

	    title.Location = new Point((menu_bar.Width - title.Width) / 2, (menu_bar.Height - title.Height) / 2);

	    mon.Button(menu_bar, quit, "X", 10, new Size(50, 24), new Point(menu_bar.Width - 51, 1), menu_bar.BackColor, Color.FromArgb(67, 220, 0), 0);

	    quit.Click += (s, e) =>
	    {
		Environment.Exit(-1);
	    };
	}

	private readonly TextBox username = new TextBox();
	private readonly TextBox password = new TextBox();

	private readonly Label username_l = new Label();
	private readonly Label password_l = new Label();

	private readonly Button login = new Button();

	private readonly PictureBox container_u1 = new PictureBox();
	private readonly PictureBox container_u2 = new PictureBox();

	private void resize_full(Control obj, Size size)
	{
	    obj.MaximumSize = size;
	    obj.MinimumSize = size;
	    obj.Size = size;
	}

	private readonly PictureBox container_m = new PictureBox();

	public LoginInterface()
	{
	    SetupLayout();

	    mon.Image(this, container_m, null, Size.Empty, Point.Empty);

	    resize_full(container_m, new Size(240, 120));
	    container_m.Location = new Point((Width - container_m.Width) / 2, 26);

	    Color textbox_fcolor = Color.FromArgb(0, 249, 173);
	    Color textbox_bcolor = Color.FromArgb(17, 0, 71);

	    Color label_fcolor = Color.FromArgb(255, 255, 255);

	    Size container_size = new Size(150, 24);

	    mon.Label(container_m, username_l, "Username: ", 10, new Point(0, 15), label_fcolor);
	    mon.Image(container_m, container_u1, null, container_size, new Point(username_l.Left + username_l.Width, username_l.Top - 3));

	    container_u1.BackColor = textbox_bcolor;
	    resize_full(container_u1, container_size);

	    mon.TextBox(container_u1, username, "Dashie", 10, Size.Empty, Point.Empty, textbox_bcolor, textbox_fcolor);
	    resize_full(username, new Size(146, username.PreferredHeight));
	    username.Location = new Point((container_u1.Width - username.Width) / 2, (container_u1.Height - username.Height) / 2);

	    mon.Label(container_m, password_l, "Password: ", 10, new Point(username_l.Left, username_l.Top + username_l.Height + 20), label_fcolor);
	    mon.Image(container_m, container_u2, null, container_size, new Point(container_u1.Left, password_l.Top - 3));

	    container_u2.BackColor = textbox_bcolor;
	    resize_full(container_u2, container_size);

	    password.PasswordChar = '*';

	    mon.TextBox(container_u2, password, "Dashie", 18, Size.Empty, Point.Empty, textbox_bcolor, textbox_fcolor);
	    resize_full(password, new Size(146, password.PreferredHeight));
	    password.Location = new Point((container_u2.Width - password.Width) / 2, (container_u2.Height - password.Height) / 2);

	    container_m.Paint += (s, e) =>
	    {
		mon.paint_border(e, menu_bar.BackColor, 2, container_u1.Size, container_u1.Location);
		mon.paint_border(e, menu_bar.BackColor, 2, container_u2.Size, container_u2.Location);
	    };

	    mon.Button(container_m, login, "Login", 12, new Size(150, 26), new Point((container_m.Width - 150) / 2, container_m.Height - 34), Color.FromArgb(148, 0, 218), Color.FromArgb(255, 255, 255), 8);
	}
    };


    public class Moon : Form
    {
	public bool isAdministrator()
	{
	    using (System.Security.Principal.WindowsIdentity identity = System.Security.Principal.WindowsIdentity.GetCurrent())
	    {
		System.Security.Principal.WindowsPrincipal principal = new System.Security.Principal.WindowsPrincipal(identity);

		if(!principal.IsInRole(System.Security.Principal.WindowsBuiltInRole.Administrator))
		{
		    return false;
		};
	    };

	    return true;
	}

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

	    box.Font = FlubbyFont(points, false); //new Font("Modern", points);
	    box.Text = text;

	    box.BackColor = bgcolor;
	    box.ForeColor = frcolor;

	    box.BorderStyle = BorderStyle.None;
	    box.ReadOnly = false;

	    obj.Controls.Add(box);
	}

	public Size CalculateFontThing(string text, Font font)
	{
	    Size modify_size = TextRenderer.MeasureText(text, font, new Size(int.MaxValue, int.MaxValue));
	    return new Size(modify_size.Width + (text.Length - text.Count()), modify_size.Height);
	}

	public void Label(Control obj, Label label, string text, int points, Point location, Color color)
	{
	    label.Font = FlubbyFont(points, false); //new Font("Modern", points);
	    label.Text = text;

	    Size size = CalculateFontThing(text, label.Font);

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

	    button.Font = FlubbyFont(points, false); //new Font("Modern", points);
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

	    Size container_size = new Size(LoginInterface.ActiveForm.Width - 10, LoginInterface.ActiveForm.Height - 10);

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

	    LoginInterface.
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

	[DllImport("gdi32.dll")]
	private static extern IntPtr AddFontMemResourceEx(IntPtr pbFont, uint cbFont, IntPtr pdv, [In] ref uint pcFonts);

	private static readonly byte[] font_data = (byte[])Properties.Resources.font;

	private static PrivateFontCollection PermFontCollection = new PrivateFontCollection();

	public static void InitFont()
	{
	    IntPtr font_ptr = Marshal.AllocCoTaskMem(font_data.Length);
	    Marshal.Copy(font_data, 0, font_ptr, font_data.Length);
	    uint fluff = 0;
	    AddFontMemResourceEx(font_ptr, (uint) font_data.Length, IntPtr.Zero, ref fluff);
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
