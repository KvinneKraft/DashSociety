
using System;
using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Security.Principal;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Lunarilicious
{
    /*OLD*/
    class Injector
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
		obj.TextAlign = ContentAlignment.MiddleCenter;

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

		if (loc.Y < 0)
		{
		    loc.Y = (srf.Height - siz.Height) / 2;
		};

		obj.Location = loc;

		obj.BackColor = Color.FromArgb(0, 0, 0, 255);
		obj.ForeColor = fcl;

		obj.UseCompatibleTextRendering = true;

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

	    public static void Rectangle(PaintEventArgs e, Color color, float width, Size size, Point point)
	    {
		Graphics graphics = e.Graphics;

		using (Pen pen = new Pen(color, width))
		{
		    graphics.DrawRectangle(pen, new Rectangle(point, size));
		};
	    }

	    private readonly static External ext = new External();

	    public static void ControlBorder(Control control, int border_radius)
	    {
		control.Paint += (s, e) =>
		{
		    ext.LemonSquish(e);

		    Rectangle rectum = new Rectangle(0, 0, control.Width, control.Height);
		    GraphicsPath graphics_path = new GraphicsPath();

		    int radius = (border_radius) * 3;

		    graphics_path.AddArc(rectum.X, rectum.Y, radius, radius, 170/*180*/, 90);
		    graphics_path.AddArc((rectum.X + rectum.Width - radius), rectum.Y, radius, radius, 270, 90);
		    graphics_path.AddArc((rectum.X + rectum.Width - radius), (rectum.Y + rectum.Height - radius), radius, radius, 0, 90);
		    graphics_path.AddArc(rectum.X, (rectum.Y + rectum.Height - radius), radius, radius, 80/*90*/, 90);

		    Region reg = new Region(graphics_path);
		    control.Region = reg;
		};
	    }
	};

	class External : Form
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
		    if (point.IsEmpty)
		    {
			return;
		    };

		    obj.Location = new Point(obj.Location.X + (e.X - point.X), obj.Location.Y + (e.Y - point.Y));
		};
	    }

	    [DllImport("user32.dll")]
	    static extern IntPtr CreateIconFromResource(byte[] presbits, uint dwResSize, bool fIcon, uint dwVer);

	    static void ResourceCursor(Control srf, byte[] res)
	    {
		srf.Cursor = new Cursor(CreateIconFromResource(res, (uint)res.Length, false, 0x00030000));
	    }
	};


	public static class Get
	{
	    public static bool IsAdministrator()
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
		return TextRenderer.MeasureText(data, font);
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
		Byte[] external_font_data;

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

		return new Font(FontCollection.Families[type], points, FontStyle.Regular);
	    }
	};
    }
}
