
//
// Author: Dashie
// Version: 1.0
//
// All the code associated with this project belongs to me, I wrote it.
//
// You are allowed to redistribute my software, feel free to donate at 
// KvinneKraft@protonmail.com at Paypal if you want to help a lil Dashie
// out, ahaha.
//

using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using System.Collections.Generic;

namespace ThaDasher
{
    public partial class Interface : Form
    {
	readonly static string ERROR_FORMAT = "Hey there, I am unfortunate to tell you but an error has occurred which has caused this application to be useless right now.\r\n\r\nYou can help me prevent this from happening in the future by sending me the following error code at KvinneKraft@protonmail.com:\r\n{\r\n%m%\r\n}\r\n\r\nRegardless, Thank you.\r\n\r\nClick OK to close this application or click cancel to restart this application.";

	readonly static DashControls CONTROL = new DashControls();
	readonly static DashTools TOOL = new DashTools();

	private void InitializeBody()
	{
	    try
	    {
		var GUI_COLOR = Color.MidnightBlue;

		BackColor = GUI_COLOR;

		var RECTANGLE_SIZE = new Size(220, 121);
		var RECTANGLE_LOCA = new Point(4, 0);
		var RECTANGLE_COLA = BackColor;

		TOOL.PaintRectangle(this, 4, RECTANGLE_SIZE, RECTANGLE_LOCA, RECTANGLE_COLA);
		TOOL.Round(this, 6);
	    }

	    catch
	    {
		throw new Exception("InitializeBody()");
	    }
	}

	private void SendMessage(string mes)
	{
	    LogContainer.LOG.AppendText($"{mes}\r\n");
	}

	private void HandleKeys(KeyEventArgs e)
	{
	    switch (e.KeyCode)
	    {
		case Keys.F1:
		    
		    break;
		case Keys.F2:
		    break;
		case Keys.F3:
		    break;
		case Keys.F4:
		    break;
	    }
	}

	readonly static public InfoDialog HelpDialog = new InfoDialog();

	private void Initialization()
	{
	    LoginDialog.AuthenticatorInterf(this);
	    
	    LoginDialog.LOGIN.Click += (c, d) =>
	    {
		LoginDialog.UndoChanges(this);

		InitializeComponent();

		MenuBar.InitializeMBar(this);

		InitializeBody();

		TargetContainer.InitializeHCon(this);
		SettingsContainer.InitializeMCon(this);
		LogContainer.InitializeLCon(this);
		TaskbarContainer.InitializeTCon(this);

		foreach (Control c1 in Controls)
		{
		    foreach (Control c2 in c1.Controls)
		    {
			MouseDown += (s, q) =>
			{
			    HandleKeys(q);
			};
		    }
		}
	    };
	}

	public Interface()
	{
	    try
	    {
		FormClosing += (s, e) => Application.Exit();

		Initialization();
	    }

	    catch (Exception e)
	    {
		var resu = MessageBox.Show(ERROR_FORMAT.Replace("%m%", e.Message), "ERROR!", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

		switch (resu)
		{
		    case DialogResult.OK:
			Close();
			break;

		    default:
			Application.Restart();
			break;
		};
	    };
	}
    }
}
