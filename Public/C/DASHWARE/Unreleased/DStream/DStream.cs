
// Author: Dashie
// Version: 1.0

using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using System.Collections.Generic;

namespace DStream
{
    public partial class DStream : Form
    {
	readonly private static DashControls CONTROL = new DashControls();
	readonly private static DashTools TOOL = new DashTools();

	readonly public static string ERROR_FORMAT = "Hey there, I am unfortunate to say but this application has ran into a problem.\r\n\r\nYou can either try restarting this application to see if that solves the problem if not though you could perhaps consider reaching out to me at KvinneKraft@protonmail.com.\r\n\r\nIf you do though, please put the following in your message:\r\n%m%\r\n\r\nIf you want to restart this application right now you can click OK if not you can click CANCEL.";

	public static DStream APP;

	private void InitializeMainGUI()
	{
	    try
	    {
		BackColor = Color.FromArgb(32, 32, 32);
		Icon = Properties.Resources.ICON_ICO;

		TOOL.Interactive(this, this);
	    }

	    catch
	    {
		throw new Exception("--InitializeMainGUI() in DStream.cs <----");
	    };
	}

	private class MENUBAR
	{
	    readonly public static PictureBox CONTAINER = new PictureBox();
	    readonly public static PictureBox ICON = new PictureBox();

	    readonly public static Button MINIMIZE = new Button();
	    readonly public static Button QUIT = new Button();

	    readonly public static Label TITLE = new Label();
	}

	private void InitializeMenuBar()
	{
	    try
	    {
		var CONTAINER_SIZE = new Size(Width, 28);
		var CONTAINER_LOCA = new Point(0, 0);
		var CONTAINER_BCOL = Color.FromArgb(8, 8, 8);

		CONTROL.Image(this, MENUBAR.CONTAINER, CONTAINER_SIZE, CONTAINER_LOCA, null, CONTAINER_BCOL);

		var BORDER_SIZE = new Size(Width - 2, Height - 2);
		var BORDER_LOCA = new Point(1, 0);
		var BORDER_BCOL = CONTAINER_BCOL;

		TOOL.PaintRectangle(this, 2, BORDER_SIZE, BORDER_LOCA, BORDER_BCOL);
		TOOL.Interactive(MENUBAR.CONTAINER, this);

		void Interactor()
		{
		    foreach (Control C in MENUBAR.CONTAINER.Controls)
		    {
			if (!(C is Button))
			{
			    TOOL.Interactive(C, this);
			};
		    };

		    return;
		};

		var LOGO_SIZE = new Size(28, 24);
		var LOGO_LOCA = new Point(5, (CONTAINER_SIZE.Height - LOGO_SIZE.Height) / 2);
		var LOGO_IMAG = Properties.Resources.ICON_PNG;
		var LOGO_BCOL = CONTAINER_BCOL;

		CONTROL.Image(MENUBAR.CONTAINER, MENUBAR.ICON, LOGO_SIZE, LOGO_LOCA, LOGO_IMAG, LOGO_BCOL);

		var TITLE_SIZE = Size.Empty;
		var TITLE_LOCA = new Point(50, -1);
		var TITLE_BCOL = CONTAINER_BCOL;
		var TITLE_FCOL = Color.White;
		var TITLE_TEXT = "DStream 1.0";

		CONTROL.Label(MENUBAR.CONTAINER, MENUBAR.TITLE, TITLE_SIZE, TITLE_LOCA, TITLE_FCOL, TITLE_BCOL, 1, 8, TITLE_TEXT);

		var BUTTON_SIZE = new Size(65, CONTAINER_SIZE.Height - 1);
		var BUTTON_LOCA = new Point(CONTAINER_SIZE.Width - BUTTON_SIZE.Width - 1, 1);
		var BUTTON_BCOL = CONTAINER_BCOL;
		var BUTTON_FCOL = Color.White;
		var BUTTON_TEXT = "X";

		CONTROL.Button(MENUBAR.CONTAINER, MENUBAR.QUIT, BUTTON_SIZE, BUTTON_LOCA, BUTTON_BCOL, BUTTON_FCOL, 1, 10, BUTTON_TEXT, Color.Empty);

		MENUBAR.QUIT.Click += (s, e) =>
		    Close();

		BUTTON_LOCA.X -= BUTTON_SIZE.Width;
		BUTTON_TEXT = "-";

		CONTROL.Button(MENUBAR.CONTAINER, MENUBAR.MINIMIZE, BUTTON_SIZE, BUTTON_LOCA, BUTTON_BCOL, BUTTON_FCOL, 1, 10, BUTTON_TEXT, Color.Empty);

		MENUBAR.MINIMIZE.Click += (s, e) =>
		    SendToBack();

		Interactor();
	    }

	    catch
	    {
		throw new Exception("--InitializeMenuBar() in DStream.cs <----");
	    };
	}

	private class FILECONTAINER
	{
	    readonly public static PictureBox FILE_CONTAINER = new PictureBox();
	    readonly public static PictureBox CONTAINER = new PictureBox();

	    readonly public static TextBox FILE_BOX = new TextBox();

	    readonly public static Button OPEN = new Button();
	}

	private void InitializeFileCon()
	{
	    try
	    {
		var CONTAINER_SIZE = new Size(Width - 22, 28);
		var CONTAINER_LOCA = new Point((Width - CONTAINER_SIZE.Width) / 2 - 1, MENUBAR.CONTAINER.Height + 10);
		var CONTAINER_BCOL = BackColor;

		CONTROL.Image(this, FILECONTAINER.CONTAINER, CONTAINER_SIZE, CONTAINER_LOCA, null, CONTAINER_BCOL);

		var FILE_SIZE = new Size(CONTAINER_SIZE.Width - 90, CONTAINER_SIZE.Height - 4); // --OPEN-WIDTH=80 | --SEPA-WIDTH=10, 10 
		var FILE_LOCA = new Point(2, 2);
		var FILE_BCOL = Color.FromArgb(20, 20, 20);
		var FILE_FCOL = Color.LightGray;

		CONTROL.TextBox(FILECONTAINER.CONTAINER, FILECONTAINER.FILE_BOX, FILE_SIZE, FILE_LOCA, FILE_BCOL, FILE_FCOL, 1, 10, Color.Empty, FIXEDSIZE:true, READONLY:true);
		TOOL.Resize(FILECONTAINER.FILE_BOX, new Size(FILECONTAINER.FILE_BOX.Width - 3, FILECONTAINER.FILE_BOX.Height));

		FILECONTAINER.FILE_BOX.Text = @"C:\Windows\System32\cmd.exe";

		var BORDER_MAST = FILECONTAINER.CONTAINER.Controls[FILECONTAINER.CONTAINER.Controls.Count - 1];
		var BORDER_SIZE = new Size(FILE_SIZE.Width - 1, CONTAINER_SIZE.Height - 5);
		var BORDER_LOCA = new Point(0, 0);
		var BORDER_BCOL = MENUBAR.CONTAINER.BackColor;

		TOOL.PaintRectangle(BORDER_MAST, 2, BORDER_SIZE, BORDER_LOCA, BORDER_BCOL);

		var OPEN_SIZE = new Size(80, CONTAINER_SIZE.Height - 4);
		var OPEN_LOCA = new Point(FILE_SIZE.Width + 10, 2);
		var OPEN_BCOL = Color.FromArgb(16, 16, 16);
		var OPEN_FCOL = Color.White;

		CONTROL.Button(FILECONTAINER.CONTAINER, FILECONTAINER.OPEN, OPEN_SIZE, OPEN_LOCA, OPEN_BCOL, OPEN_FCOL, 1, 10, "Open File", Color.Empty);
		//TOOL.Round(FILECONTAINER.OPEN, 8);

		FILECONTAINER.OPEN.Click += (s, e) =>
		{
		    using (var diag = new OpenFileDialog())
		    {
			new Thread(() =>
			{
			    diag.CheckFileExists = true;
			    diag.CheckPathExists = true;

			    diag.Filter = "Any File|*.*";
			    diag.Title = "Select File";

			    switch (diag.ShowDialog())
			    {
				case DialogResult.OK:
				    break;

				default:
				{
				    MessageBox.Show("F.Y.I: No files have been selected.\r\nPress OK to continue.", "Hey there!", MessageBoxButtons.OK, MessageBoxIcon.Information);
				    return;
				};
			    };

			    MessageBox.Show($"F.Y.I: You have now selected:\r\n\"{diag.FileName}\"!\r\nPress OK to continue.", "Hey there!", MessageBoxButtons.OK, MessageBoxIcon.Information);

			    FILECONTAINER.FILE_BOX.Text = diag.FileName;
			    DETAILCONTAINER.UPDATE_DATA();
			})

			{ IsBackground = true }.Start();
		    };
		};
	    }

	    catch
	    {
		throw new Exception("--InitializeFileCon() in DStream.cs <----");
	    };
	}

	private class DETAILCONTAINER
	{
	    readonly public static Form CONTAINER = new Form();

	    public static void SETUPCONTAINER(Form M)
	    {
		CONTAINER.Size = new Size(M.Width - 20, M.Height - (MENUBAR.CONTAINER.Height + MENUBAR.CONTAINER.Top + 62));
		CONTAINER.Location = new Point(10, MENUBAR.CONTAINER.Top + MENUBAR.CONTAINER.Height + 50);

		CONTAINER.FormBorderStyle = FormBorderStyle.None;
		CONTAINER.BackColor = Color.FromArgb(20, 20, 20);

		CONTAINER.HorizontalScroll.Enabled = true;
		CONTAINER.VerticalScroll.Enabled = true;

		CONTAINER.HorizontalScroll.Visible = true;
		CONTAINER.VerticalScroll.Visible = true;

		CONTAINER.AutoScroll = true;
		CONTAINER.TopLevel = false;

		M.Controls.Add(CONTAINER);

		CONTAINER.Show();

		var BORDER_SIZE = new Size(CONTAINER.Size.Width + 2, CONTAINER.Size.Height + 2);
		var BORDER_LOCA = new Point(CONTAINER.Location.X - 1, CONTAINER.Location.Y - 1);
		var BORDER_COLA = MENUBAR.CONTAINER.BackColor;

		TOOL.PaintRectangle(M, 2, BORDER_SIZE, BORDER_LOCA, BORDER_COLA);
		TOOL.Round(M, 6);
	    }

	    readonly public static Label FILEPATH = new Label() { Text = "Full Path:" };
	    readonly public static Label FILENAME = new Label() { Text = "Name:" };
	    readonly public static Label ORIGNAME = new Label() { Text = "Original Name:" };
	    readonly public static Label PRODNAME = new Label() { Text = "Product Name:" };
	    readonly public static Label INTENAME = new Label() { Text = "Internal Name:" };
	    readonly public static Label FILEEXTE = new Label() { Text = "Extension:" };
	    readonly public static Label FILEDESC = new Label() { Text = "Description:" };
	    readonly public static Label FILEVERS = new Label() { Text = "Version:" };
	    readonly public static Label FILECOPY = new Label() { Text = "Legal Copyright:" };
	    readonly public static Label FILECOMP = new Label() { Text = "Company:" };
	    readonly public static Label FILECOMM = new Label() { Text = "Comment(s):" };
	    readonly public static Label FILECREA = new Label() { Text = "Creation Date:" };
	    readonly public static Label FILEMODI = new Label() { Text = "Last Modified Date:" };
	    readonly public static Label FILELAST = new Label() { Text = "Last Access Date:" };

	    readonly public static List<Label> LABELS = new List<Label>() { FILEPATH, FILENAME, ORIGNAME, PRODNAME, INTENAME, FILEEXTE, FILEDESC, FILEVERS, FILECOPY, FILECOMP, FILECOMM, FILECREA, FILEMODI, FILELAST };

	    readonly public static TextBox FFILEPATH = new TextBox();
	    readonly public static TextBox FFILENAME = new TextBox();
	    readonly public static TextBox OORIGNAME = new TextBox();
	    readonly public static TextBox PPRODNAME = new TextBox();
	    readonly public static TextBox IINTENAME = new TextBox();
	    readonly public static TextBox FFILEEXTE = new TextBox();
	    readonly public static TextBox FFILEDESC = new TextBox();
	    readonly public static TextBox FFILEVERS = new TextBox();
	    readonly public static TextBox FFILECOPY = new TextBox();
	    readonly public static TextBox FFILECOMP = new TextBox();
	    readonly public static TextBox FFILECOMM = new TextBox();
	    readonly public static TextBox FFILECREA = new TextBox();
	    readonly public static TextBox FFILEMODI = new TextBox();
	    readonly public static TextBox FFILELAST = new TextBox();

	    readonly public static List<TextBox> DATA = new List<TextBox>() { FFILEPATH, FFILENAME, OORIGNAME, PPRODNAME, IINTENAME, FFILEEXTE, FFILEDESC, FFILEVERS, FFILECOPY, FFILECOMP, FFILECOMM, FFILECREA, FFILEMODI, FFILELAST };

	    public static void UPDATE_DATA()
	    {
		var ps = new List<ATTMAN.Properties>();

		foreach ( ATTMAN.Properties p in Enum.GetValues( typeof ( ATTMAN.Properties ) ) )
		    ps.Add(p);

		var v = "";

		for (int k = 0; k < ps.Count - 1; k += 1)
		{
		    var s = ATTMAN.GetProperties(FILECONTAINER.FILE_BOX.Text, true);
		    v = ATTMAN.GetRawValue(s, prop:ps[k]);

		    if (v.Length < 1)
			v = "None";

		    var l = TextRenderer.MeasureText(v, TOOL.GetFont(1, 10));
		    var i = (LABELS.Count + k);

		    TOOL.Resize(CONTAINER.Controls[i], new Size(l.Width, 20));
		    CONTAINER.Controls[i].Text = v;
		};
	    }
	}

	private void InitializeDetaCon()
	{
	    try
	    {
		DETAILCONTAINER.SETUPCONTAINER(this);
		
		var LABEL_SIZE = new Size(0, 20);
		var LABEL_LOCA = new Point(0, 0);
		var LABEL_BCOL = DETAILCONTAINER.CONTAINER.BackColor;
		var LABEL_FCOL = Color.LightGray;

		for (int k = 0; k < DETAILCONTAINER.LABELS.Count; LABEL_LOCA.Y += LABEL_SIZE.Height, k += 1)
		{
		    var LS = TextRenderer.MeasureText(DETAILCONTAINER.LABELS[k].Text, TOOL.GetFont(1, 10));

		    LABEL_SIZE.Width = LS.Width;

		    CONTROL.Label(DETAILCONTAINER.CONTAINER, DETAILCONTAINER.LABELS[k], LABEL_SIZE, LABEL_LOCA, LABEL_FCOL, LABEL_BCOL, 1, 10, DETAILCONTAINER.LABELS[k].Text);
		};

		var DATA_BCOL = DETAILCONTAINER.CONTAINER.BackColor;
		var DATA_FCOL = Color.LightBlue;

		for (int k = 0; k < DETAILCONTAINER.DATA.Count; k += 1)
		{
		    var DATA_SIZE = new Size(DETAILCONTAINER.CONTAINER.Width - DETAILCONTAINER.LABELS[k].Width - 17, 20);
		    var DATA_LOCA = new Point(DETAILCONTAINER.LABELS[k].Width, DETAILCONTAINER.LABELS[k].Top);

		    CONTROL.TextBox(DETAILCONTAINER.CONTAINER, DETAILCONTAINER.DATA[k], DATA_SIZE, DATA_LOCA, DATA_BCOL, DATA_FCOL, 1, 10, Color.Empty, FIXEDSIZE: false);

		    DETAILCONTAINER.DATA[k].WordWrap = false;
		};

		DETAILCONTAINER.UPDATE_DATA();
	    }

	    catch
	    {
		throw new Exception("--InitializeDetaCon() in DStream.cs <----");
	    };
	}

	public DStream()
	{
	    InitializeComponent();

	    try
	    {
		Hide();

		APP = this;

		InitializeMainGUI();
		InitializeMenuBar();
		InitializeFileCon();
		InitializeDetaCon();

		Show();
	    }

	    catch (Exception e)
	    {
		var m = MessageBox.Show(ERROR_FORMAT.Replace("%m%", e.Message), "Oh No!", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);

		switch (m)
		{
		    case DialogResult.OK:
		    {
			Application.Restart();
			break;
		    };
		};

		Close();
	    };
	}
    }
}
