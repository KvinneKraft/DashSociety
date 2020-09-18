
// This file is a pure pile of shit, I have to update this.
// For now, let us just focus on the functional part.  There is always
// sufficient room for improvements ;)

// Author: Dashie
// Version: 1.0

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
    class Exceptions
    {
	public static readonly Exception UNKNOWN = new Exception("The object requested does not exist.");
	public static readonly Exception NULL = new Exception("A possible null pointer exception has occurred.");
    };

    class Mod
    {
	public static void Rectangle(PaintEventArgs e, Color _color, float _width, Size _size, Point _loca)
	{
	    Graphics graphics = e.Graphics;

	    using (Pen pen = new Pen(_color, _width))
	    {
		graphics.SmoothingMode = SmoothingMode.AntiAlias;
		graphics.DrawRectangle(pen, new Rectangle(_loca, _size));
	    };
	}

	[DllImport("user32.dll")] static extern IntPtr CreateIconFromResource(byte[] presbits, uint dwResSize, bool fIcon, uint dwVer);

	public static bool ResourceCursor(Control _ctrl, byte[] _reso)
	{
	    try
	    {
		Cursor cursor = new Cursor(CreateIconFromResource(_reso, (uint) _reso.Length, false, 0x00030000));

		_ctrl.Cursor = cursor;
		_ctrl.Update();

		return true;
	    }

	    catch
	    {
		throw Exceptions.UNKNOWN;
	    };
	}

	public static void Moveable(Control _trig, Control _obj)
	{
	    Point _loca = Point.Empty;

	    _trig.MouseDown += (s, e) =>
	    {
		_loca = new Point(e.X, e.Y);
	    };

	    _trig.MouseUp += (s, e) =>
	    {
		_loca = Point.Empty;
	    };

	    _trig.MouseMove += (s, e) =>
	    {
		if (_loca.IsEmpty)
		{
		    return;
		};

		int x = _obj.Location.X + (e.X - _loca.X);
		int y = _obj.Location.Y + (e.Y - _loca.Y);

		_obj.Location = new Point(x, y);
	    };
	}

	class PortableForm : Form
	{
	    public void PaintOwner(PaintEventArgs e)
	    {
		e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
		base.OnPaint(e);
	    }
	};

	static readonly PortableForm portableForm = new PortableForm();

	public static void Border(Control _ctrl, int _rad)
	{
	    _ctrl.Paint += (s, e) =>
	    {
		portableForm.PaintOwner(e);

		Rectangle rect = new Rectangle(0, 0, _ctrl.Width, _ctrl.Height);
		GraphicsPath grapp = new GraphicsPath();

		_rad = _rad * 3;

		grapp.AddArc(rect.X, rect.Y, _rad, _rad, 170, 90);
		grapp.AddArc(rect.X + rect.Width - _rad, rect.Y, _rad, _rad, 270, 90);
		grapp.AddArc(rect.X + rect.Width - _rad, rect.Y + rect.Height - _rad, _rad, _rad, 0, 90);
		grapp.AddArc(rect.X, rect.Y + rect.Height - _rad, _rad, _rad, 80, 90);

		_ctrl.Region = new Region(grapp);
	    };
	}

	public static bool Centerize(Control _ctrl, Control _pare, bool _horiz, bool _verti)
	{
	    try
	    {
		Point loca = _ctrl.Location;

		if (_verti) loca.Y = (_pare.Height - _ctrl.Height) / 2;
		if (_horiz) loca.X = (_pare.Width - _ctrl.Width) / 2;

		_ctrl.Location = loca;
		_ctrl.Update();

		return true;
	    }

	    catch
	    {
		throw Exceptions.NULL;
	    };
	}

	public static bool Resize(Control _ctrl, Size _size)
	{
	    try
	    {
		if (_size.Height < 1) _size.Height = _ctrl.PreferredSize.Height;
		if (_size.Width < 1) _size.Width = _ctrl.PreferredSize.Width;

		_ctrl.MaximumSize = _size;
		_ctrl.MinimumSize = _size;
		_ctrl.Update();

		return true;
	    }

	    catch
	    {
		throw Exceptions.NULL;
	    };
	}
    };

    class Get
    {
	public static class Font
	{
	    public static readonly int NORMAL = 0;
	    public static readonly int CUTE = 1;
	};
    };

    class Add
    {
	[DllImport("gdi32.dll")] 
	private static extern IntPtr AddFontMemResourceEx(IntPtr pbFont, uint cbFont, IntPtr pdv, [In] ref uint pcFonts);
	private static readonly PrivateFontCollection FontCollection = new PrivateFontCollection();

	static Font GetFont(int _ftyp, int _fsiz)
	{
	    try
	    {
		if (FontCollection.Families.Length < 2)
		{
		    List<byte[]> _fcol = new List<byte[]>()
		    {
			(byte[])Properties.Resources.main,
			(byte[])Properties.Resources.cute
		    };

		    foreach (byte[] _fbyt in _fcol)
		    {
			IntPtr _ptr = Marshal.AllocCoTaskMem(_fbyt.Length);
			Marshal.Copy(_fbyt, 0, _ptr, _fbyt.Length);

			uint _cache = 0;

			AddFontMemResourceEx(_ptr, (uint) _fbyt.Length, IntPtr.Zero, ref _cache);
			FontCollection.AddMemoryFont(_ptr, _fbyt.Length);
		    };
		};

		return new Font(FontCollection.Families[_ftyp], _fsiz, FontStyle.Regular);
	    }

	    catch
	    {
		throw Exceptions.UNKNOWN;
	    };
	}

	public static void PictureBox(Control _base, PictureBox _pict, Image _imag, Point _iloc, Color _pcol)
	{
	    try
	    {
		Mod.Centerize(_pict, _base, _iloc.X < 1, _iloc.Y < 1);

		_pict.MinimumSize = _imag.Size;
		_pict.MaximumSize = _imag.Size;

		_pict.BorderStyle = BorderStyle.None;
		
		_pict.BackColor = _pcol;
		_pict.Location = _iloc;

		_base.Controls.Add(_pict);
		_base.Update();
	    }

	    catch (Exception e)
	    {
		// Error Handler
	    };
	}

	public static void Label(Control _base, Label _labl, string _text, int _fozi, int _font, Size _size, Point _loca, Color _bcol, Color _fcol)
	{
	    try
	    {
		_labl.Location = _loca;

		Mod.Centerize(_labl, _base, _loca.X < 1, _loca.Y < 1);

		_labl.BorderStyle = BorderStyle.None;
		_labl.FlatStyle = FlatStyle.Flat;

		if (_size.Equals(Size.Empty)) _size = TextRenderer.MeasureText(_text, GetFont(_font, _fozi));

		_labl.MinimumSize = _size;
		_labl.MaximumSize = _size;

		_labl.Text = _text;

		if (_bcol == null) _bcol = Color.FromArgb(0, 0, 0, 255);
		if (_fcol == null) _fcol = Color.FromArgb(0, 0, 0, 255);

		_labl.BackColor = _bcol;
		_labl.ForeColor = _fcol;

		_base.Controls.Add(_labl);
		_base.Update();
	    }

	    catch
	    {
		// Error Handler
	    };
	}

	public static void Button(Control _base, Button _butt, string _text, int _fozi, int _font, Size _size, Point _loca, Color _bcol, Color _fcol)
	{
	    try
	    {
		_butt.MinimumSize = _size;
		_butt.MaximumSize = _size;

		_butt.Font = GetFont(_font, _fozi);
		_butt.Text = $"{_text}";

		_butt.Location = _loca; 
		
		Mod.Centerize(_butt, _base, _loca.X < 1, _loca.Y < 1);

		_butt.FlatAppearance.BorderSize = 0;
		_butt.FlatStyle = FlatStyle.Flat;

		_butt.BackColor = _bcol;
		_butt.ForeColor = _fcol;

		_base.Controls.Add(_butt);
		_base.Update();
	    }

	    catch (Exception e)
	    {
		// Error Handler
	    };
	}
    };
}
