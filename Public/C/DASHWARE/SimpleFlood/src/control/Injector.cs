
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

namespace SimpleFlood
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

	public static void Border(Control _ctrl, int _rar)
	{
	    _ctrl.Paint += (s, e) =>
	    {
		try
		{
		    portableForm.PaintOwner(e);

		    using (GraphicsPath grapp = new GraphicsPath())
		    {
		        Rectangle rect = new Rectangle(0, 0, _ctrl.Width, _ctrl.Height);

			int _rad = (_rar) * 3;

			grapp.AddArc(rect.X, rect.Y, _rad, _rad, 170, 90);
			grapp.AddArc((rect.X + rect.Width - _rad), rect.Y, _rad, _rad, 270, 90);
			grapp.AddArc((rect.X + rect.Width - _rad), (rect.Y + rect.Height - _rad), _rad, _rad, 0, 90);
			grapp.AddArc(rect.X, (rect.Y + rect.Height - _rad), _rad, _rad, 80, 90);

			_ctrl.Region = new Region(grapp);
		    };
		}

		catch (Exception es) { MessageBox.Show(es.ToString()); };
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
		_ctrl.Size = _size;

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
	    public static readonly int NORMAL = 1;
	    public static readonly int CUTE = 0;
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

		    byte[] _fra;

		    foreach (byte[] _fbyt in _fcol)
		    {
			_fra = _fbyt;

			IntPtr _ptr = Marshal.AllocCoTaskMem(_fra.Length);
			Marshal.Copy(_fra, 0, _ptr, _fra.Length);

			uint _cache = 0;

			AddFontMemResourceEx(_ptr, (uint) _fra.Length, IntPtr.Zero, ref _cache);
			FontCollection.AddMemoryFont(_ptr, _fra.Length);
		    };
		};

		return new Font(FontCollection.Families[_ftyp], _fsiz, FontStyle.Regular);
	    }

	    catch
	    {
		return null;
	    };
	}

	public static void PictureBox(Control _base, PictureBox _pict, Image _imag, Size _size, Point _iloc, Color _pcol)
	{
	    try
	    {
		if (_imag != null)
		{
		    _pict.Image = _imag;

		    if (_size == Size.Empty)
		    {
			_size = _pict.Image.Size;
		    };
		};

		_pict.MinimumSize = _size;
		_pict.MaximumSize = _size;

		_pict.Location = _iloc;

		if (_iloc != Point.Empty)
		{
		    Mod.Centerize(_pict, _base, _iloc.X < 0, _iloc.Y < 0);
		};

		_pict.BorderStyle = BorderStyle.None;

		if (_pcol == Color.Empty || _pcol == null)
		{
		    _pcol = Color.FromArgb(0, 0, 0, 255);
		};

		_pict.BackColor = _pcol;

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

		_labl.BorderStyle = BorderStyle.None;
		_labl.FlatStyle = FlatStyle.Flat;
		_labl.Font = GetFont(_font, _fozi);

		if (_size.Equals(Size.Empty)) _size = TextRenderer.MeasureText(_text, _labl.Font);

		_labl.MinimumSize = _size;
		_labl.MaximumSize = _size;

		Mod.Centerize(_labl, _base, _loca.X < 0, _loca.Y < 0);

		_labl.Text = _text;

		if (_bcol == Color.Empty) _bcol = Color.FromArgb(0, 0, 0, 255);
		if (_fcol == Color.Empty) _fcol = Color.FromArgb(0, 0, 0, 255);

		_labl.BackColor = _bcol;
		_labl.ForeColor = _fcol;

		_base.Controls.Add(_labl);
		_base.Update();
	    }

	    catch (Exception e)
	    {
		// Error Handler 
	    };
	}

	public static void Button(Control _base, Button _butt, string _text, int _fozi, int _font, Size _size, Point _loca, Color _bcol, Color _fcol)
	{
	    try
	    {
		Mod.Resize(_butt, _size);

		_butt.Font = GetFont(_font, _fozi);
		_butt.Text = $"{_text}";

		_butt.Location = _loca; 
		
		Mod.Centerize(_butt, _base, _loca.X < 0, _loca.Y < 0);

		_butt.FlatAppearance.BorderColor = _bcol;
		_butt.FlatAppearance.BorderSize = 0;
		_butt.FlatStyle = FlatStyle.Flat;

		_butt.Click += (s, e) => 
		{
		    _butt.FlatAppearance.BorderColor = _bcol;
		    _butt.FlatAppearance.BorderSize = 0;
		    _butt.FlatStyle = FlatStyle.Flat;
		};

		_butt.MouseClick += (s, e) =>
		{
		    _butt.FlatAppearance.BorderColor = _bcol;
		    _butt.FlatAppearance.BorderSize = 0;
		    _butt.FlatStyle = FlatStyle.Flat;
		};

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

	static readonly Button MINIMIZE = new Button();
	static readonly Button QUIT = new Button();

	public static void MenuBar(Control _base, PictureBox _mbar, Color _bcol, Label _mtit, string _title, int _font, int _fsize, Color _fcol, [Optional] bool _border, [Optional] Color _borderColor, [Optional] bool _quit, [Optional] bool _minimize, [Optional] bool _draggable)
	{
	    try
	    {
		if (_base is Form)
		{
		    ((Form)_base).FormBorderStyle = FormBorderStyle.None;
		    ((Form)_base).MinimizeBox = false;
		    ((Form)_base).MaximizeBox = false;
		};

		PictureBox(_base, _mbar, null, new Size(_base.Size.Width - 2, 26), new Point(1, 1), _bcol);
		Label(_mbar, _mtit, _title, _fsize, _font, Size.Empty, new Point(10, -1), _bcol, _fcol);

		if (_draggable)
		{
		    Mod.Moveable(_mbar, _base);
		    Mod.Moveable(_mtit, _base);
		};

		if (_quit)
		{
		    Button(_mbar, QUIT, "X", _fsize, _font, new Size(65, 26), new Point(_base.Width - 67, 0), _bcol, _fcol);
		    QUIT.Click += (s, e) => Environment.Exit(-1);
		};

		if (_minimize)
		{
		    Point _loca = new Point(_base.Width - 67, 0);

		    if (_base.Controls.Contains(QUIT))
		    {
			_loca.X = _base.Width - 132;
		    };

		    Button(_mbar, MINIMIZE, "-", _fsize, _font, new Size(65, 26), new Point(_base.Width - 67, 0), _bcol, _fcol);
		    MINIMIZE.Click += (s, e) => Environment.Exit(-1);
		};

		if (_border)
		{
		    _base.Paint += (s, e) =>
		    {
			Mod.Rectangle(e, _borderColor, 2, new Size(_base.Width - 1, _base.Height - 1), Point.Empty);
		    };
		};

		_base.Update();
	    }

	    catch (Exception e)
	    {
		// Error Handler
	    };
	}

	public static void InputBox(Control _base, TextBox _ibox, Size _size, Point _loca, string _text, int _fsize, int _font, Color _bcol, Color _fcol, [Optional] bool _readOnly, [Optional] bool _centerText, [Optional] bool _border, [Optional] Color _borderColor)
	{
	    try
	    {
		_ibox.BorderStyle = BorderStyle.None;
		_ibox.ReadOnly = _readOnly;

		_ibox.BackColor = _bcol;
		_ibox.ForeColor = _fcol;

		_ibox.Font = GetFont(_font, _fsize);
		_ibox.Text = _text;

		if (_centerText)
		{
		    PictureBox(_base, new System.Windows.Forms.PictureBox(), null, new Size(_size.Width - 4, _size.Height), _loca, _bcol);

		    PictureBox _cese = ((PictureBox)_base.Controls[_base.Controls.Count - 1]);

		    Size size = _cese.Size;

		    if (_border)
		    {
			_base.Paint += (s, e) =>
			{
			    _loca = _ibox.Location;
			    Mod.Rectangle(e, _borderColor, 1, new Size(size.Width + 1, _cese.Height + 1), new Point(_cese.Location.X - 1, _cese.Location.Y - 1));
			};
		    };

		    _ibox.TextAlign = HorizontalAlignment.Center;

		    _cese.Click += (s, e) =>
		    {
			_ibox.Select();
		    };

		    Mod.Resize(_ibox, new Size(_ibox.PreferredSize.Width - 10, _ibox.PreferredSize.Height - 5));
		    Mod.Centerize(_ibox, _cese, true, true);

		    _cese.Controls.Add(_ibox);
		    
		    return;
		};

		Mod.Resize(_ibox, _size);

		_ibox.Location = _loca;

		Mod.Centerize(_ibox, _base, _loca.X < 0, _loca.Y < 0);

		if (_border)
		{
		    _base.Paint += (s, e) =>
		    {
			_loca = _ibox.Location;
			Mod.Rectangle(e, _borderColor, 1, new Size(_size.Width + 1, _size.Height + 1), new Point(_loca.X - 1, _loca.Y - 1));
		    };
		};

		_base.Controls.Add(_ibox);
		_base.Update();
	    }

	    catch (Exception e)
	    {
		// Error Handler
	    };
	}

	public class SecondaryApp : Form
	{
	    public SecondaryApp(
		[Optional] string appTitle, 
		[Optional] Font appFont, 
		[Optional] Icon appIcon, 
		[Optional] Size appSize, 
		[Optional] Point appLocation, 
		[Optional] Color appBackColor, 
		[Optional] FormBorderStyle appBorderStyle, 
		[Optional] bool showInTaskBar,
		[Optional] bool hasStartMenu 
	    ){
		try
		{
		    if (hasStartMenu)
		    {
			MenuBar(this, new System.Windows.Forms.PictureBox(), Color.FromArgb(8, 8, 8), new System.Windows.Forms.Label(), appTitle, 1, 10, Color.White, _border:true, _borderColor:appBackColor, _quit:true, _minimize:true, _draggable:true);
		    };

		    this.BackColor = appBackColor;
		    this.Icon = appIcon;

		    if (appLocation.IsEmpty)
		    {
			this.StartPosition = FormStartPosition.CenterScreen;
		    };

		    this.FormBorderStyle = appBorderStyle;
		    this.ShowInTaskbar = showInTaskBar;

		    this.MaximumSize = appSize;
		    this.MinimumSize = appSize;
		    this.Size = appSize;

		    this.Text = appTitle;
		    this.Font = appFont;
		}

		catch (Exception e)
		{
		    // Error Handler
		};
	    }
	};
    };
}
