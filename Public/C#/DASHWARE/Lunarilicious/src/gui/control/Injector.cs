
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
	public static bool Centerize(Control _ctrl, Control _pare, bool _horiz, bool _verti)
	{
	    try
	    {
		Point _loca = _ctrl.Location;

		if (_verti) _loca.Y = (_pare.Height - _ctrl.Height) / 2;
		if (_horiz) _loca.X = (_pare.Width - _ctrl.Width) / 2;

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
		_butt.MinimumSize = _size;
		_butt.MaximumSize = _size;

		_butt.Font = GetFont(_font, _fozi);
		_butt.Text = $"{_text}";

		_butt.Location = _loca; Mod.Centerize(_base, _butt, _loca.X < 1, _loca.Y < 1);

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
