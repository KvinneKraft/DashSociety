

// Author: Dashie
// Version: 1.0


using System;
using System.IO;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Security.Principal;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Drawing.Text;

namespace Dash_IP_Fluffer
{
    public static class Add
    {
	public static void AButton(Control srf, Button obj, Size siz, Point loc, Color bcl, Color fcl, String tex, Int32 type, Int32 pts)
	{
	    obj.Size = siz;
	    obj.MinimumSize = siz;
	    obj.MaximumSize = siz;

	    if (loc.X < 0)
	    {
		loc.X = (srf.Width - obj.Width) / 2;
	    };

	    if (loc.Y < 0)
	    {
		loc.Y = (srf.Height - siz.Height) / 2;
	    };

	    obj.Location = loc;

	    obj.Font = Get.CustomFont(pts, type);
	    obj.Text = tex;

	    obj.BackColor = bcl;
	    obj.ForeColor = fcl;

	    obj.FlatAppearance.BorderColor = Color.FromArgb(0, 0, 0, 255);
	    obj.FlatAppearance.BorderSize = 0;
	    obj.FlatStyle = FlatStyle.Flat;

	    srf.Controls.Add(obj);
	}

	public static void ThaLabel(Control srf, Label obj, Point loc, Color fcl, String tex, Int32 type, Int32 pts)
	{
	    obj.Font = Get.CustomFont(pts, type);
	    obj.Text = tex;

	    Size siz = Get.FontSize(tex, obj.Font);

	    obj.Size = siz;
	    obj.MinimumSize = siz;
	    obj.MaximumSize = siz;

	    obj.BorderStyle = BorderStyle.None;
	    obj.FlatStyle = FlatStyle.Flat;

	    if (loc.X < 0)
	    {
		loc.X = (srf.Width - siz.Width) / 2;
	    };

	    if(loc.Y < 0)
	    {
		loc.Y = (srf.Height - siz.Height) / 2;
	    };

	    obj.Location = loc;

	    obj.BackColor = Color.FromArgb(0, 0, 0, 255);
	    obj.ForeColor = fcl;

	    srf.Controls.Add(obj);
	}

	public static void ZeTextBox(Control srf, TextBox obj, Size siz, Point loc, Color fcl, Color bcl, String tex, Int32 type, Int32 pts)
	{
	    obj.Font = Get.CustomFont(pts, type);
	    obj.Text = tex;

	    obj.Size = siz;
	    obj.MinimumSize = siz;
	    obj.MaximumSize = siz;

	    obj.BorderStyle = BorderStyle.None;
	    
	    if (loc.X < 0)
	    {
		loc.X = (srf.Width - obj.Width) / 2;
	    };

	    if (loc.Y < 0)
	    {
		loc.Y = (srf.Height - obj.Height) / 2;
	    };

	    obj.Location = loc;

	    obj.BackColor = bcl;
	    obj.ForeColor = fcl;

	    // Containers can be added manually, later on.

	    srf.Controls.Add(obj);
	}

	public static void RuImage(Control srf, PictureBox obj, Image img, Size siz, Point loc)
	{
	    obj.Size = siz;
	    obj.MinimumSize = siz;
	    obj.MaximumSize = siz;

	    if (loc.X < 0)
	    {
		loc.X = (srf.Width - obj.Width) / 2;
	    };

	    if (loc.Y < 0)
	    {
		loc.Y = (srf.Height - obj.Height) / 2;
	    };

	    obj.Location = loc;

	    if (img != null)
	    {
		obj.Image = img;
	    };

	    srf.Controls.Add(obj);
	}

	public static void InteractiveToolBar(Form obj)
	{
	    PictureBox mbr = Get.menu_bar;
	    PictureBox log = Get.logo;

	    Button qut = Get.quit_button;
	    Label ttl = Get.title;

	    RuImage(obj, mbr, (Image)null, new Size(obj.Width, 26), new Point(1, 0));
	    mbr.BackColor = Color.FromArgb(217, 145, 181);

	    Add.ThaLabel(mbr, ttl, new Point(-1, -1), Color.FromArgb(255, 255, 255), "P o n y     I P     F l u f f e r", 0, 11);
	    Add.AButton(mbr, qut, new Size(66, 26), new Point(mbr.Width - 67, 0), mbr.BackColor, Color.FromArgb(255, 255, 255), "X", 1, 10);

	    qut.Click += (s, e) =>
	    {
		Environment.Exit(-1);
	    };

	    Add.RuImage(mbr, log, Properties.Resources.logo, new Size(28, 45), new Point(2, 2));
	    Add.RuImage(obj, new PictureBox(), log.Image, log.Size, log.Location);

	    log.BackColor = Color.FromArgb(0, 0, 0, 255);

	    Set.Draggable(log, obj);
	    Set.Draggable(qut, obj);
	    Set.Draggable(ttl, obj);
	    Set.Draggable(mbr, obj);
	    
	    obj.Paint += (s, e) =>
	    {
		Rectangle(e, mbr.BackColor, 2, obj.Size, Point.Empty);
	    };
	}

	public static void Rectangle(PaintEventArgs e, Color color, float width, Size size, Point point)
	{
	    Graphics graphics = e.Graphics;

	    using (Pen pen = new Pen(color, width))
	    {
		graphics.DrawRectangle(pen, new Rectangle(point, size));
	    };
	}

	private static External ext = new External();

	public static void ControlBorder(Control control, int border_radius)
	{
	    control.Paint += (s, e) =>
	    {
		ext.LemonSquish(e);

		Rectangle rectum = new Rectangle(0, 0, control.Width, control.Height);
		GraphicsPath graphics_path = new GraphicsPath();

		int radius = (border_radius) * 3;

		graphics_path.AddArc(rectum.X, rectum.Y, radius, radius, 180, 90);
		graphics_path.AddArc((rectum.X + rectum.Width - radius), rectum.Y, radius, radius, 270, 90);
		graphics_path.AddArc((rectum.X + rectum.Width - radius), (rectum.Y + rectum.Height - radius), radius, radius, 0, 90);
		graphics_path.AddArc(rectum.X, (rectum.Y + rectum.Height - radius), radius, radius, 90, 90);

		Region reg = new Region(graphics_path);
		control.Region = reg;
	    };
	}
    };

    public class External : Form
    {
	public void LemonSquish(PaintEventArgs e)
	{
	    e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
	    base.OnPaint(e);
	}
    };

    public static class Set
    {
	public static void Draggable(Control trigger, Control obj)
	{
	    Point point = Point.Empty;

	    trigger.MouseDown += (s, e) =>
	    {
		point = new Point(e.X, e.Y);
	    };

	    trigger.MouseUp += (s, e) =>
	    {
		point = Point.Empty;
	    };

	    trigger.MouseMove += (s, e) =>
	    {
		if ( point.IsEmpty )
		{
		    return;
		};

		obj.Location = new Point(obj.Location.X + (e.X - point.X), obj.Location.Y + (e.Y - point.Y));
	    };
	}

	[DllImport("user32.dll")]
	static extern IntPtr CreateIconFromResource(byte[] presbits, uint dwResSize, bool fIcon, uint dwVer);
	
	public static void ResourceCursor(Control srf, byte[] res)
	{
	    srf.Cursor = new Cursor(CreateIconFromResource(res, (uint) res.Length, false, 0x00030000));
	}
    };


    public static class Get
    {
	public static readonly PictureBox menu_bar = new PictureBox();
	public static readonly PictureBox logo = new PictureBox();

	public static readonly Button quit_button = new Button();

	public static readonly Label title = new Label();

	public static bool isAdministrator()
	{
	    using (WindowsIdentity id = WindowsIdentity.GetCurrent())
	    {
		WindowsPrincipal princess = new WindowsPrincipal(id);

		if (princess.IsInRole(WindowsBuiltInRole.Administrator))
		{
		    return true;
		};
	    };

	    return false;
	}

	public static Size FontSize(String data, Font font)
	{
	    Size size = TextRenderer.MeasureText(data, font, new Size(int.MaxValue, int.MaxValue));
	    return new Size(size.Width + data.Length, size.Height);
	}

	[DllImport("gdi32.dll")]
	private static extern IntPtr AddFontMemResourceEx(IntPtr pbFont, uint cbFont, IntPtr pdv, [In] ref uint pcFonts);

	private static readonly PrivateFontCollection FontCollection = new PrivateFontCollection();

	private static readonly List<Byte[]> fonts = new List<Byte[]>()
	{
	    (byte[])Properties.Resources.cute,
	    (byte[])Properties.Resources.main
	};

	public static readonly int FONT_TYPE_CUTE = 0;
	public static readonly int FONT_TYPE_MAIN = 1;

	public static Font CustomFont(int points, int type)
	{
	    Byte[] external_font_data = null;

	    if (FontCollection.Families.Length < 2)
	    {
		foreach (Byte[] bytes in fonts)
		{
		    external_font_data = bytes;

		    IntPtr pointer = Marshal.AllocCoTaskMem(external_font_data.Length);
		    Marshal.Copy(external_font_data, 0, pointer, external_font_data.Length);

		    uint cache_size = 0;

		    AddFontMemResourceEx(pointer, (uint)external_font_data.Length, IntPtr.Zero, ref cache_size);
		    FontCollection.AddMemoryFont(pointer, external_font_data.Length);
		};
	    };

	    if (type == 0)
	    {
		external_font_data = fonts[type];
	    }

	    else
	    {
		external_font_data = fonts[type];
	    };

	    return new Font(FontCollection.Families[type], points, FontStyle.Regular);
	}
    };
};
