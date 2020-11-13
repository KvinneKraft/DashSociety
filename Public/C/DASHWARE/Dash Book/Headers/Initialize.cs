
using System;
using System.IO;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using System.Collections.Generic;
using System.Text.RegularExpressions;

using DashBook.Properties;
using System.IO.MemoryMappedFiles;

namespace DashBook
{
    public class Initialize
    {
	readonly DashControls CONTROL = new DashControls();
	readonly DashTools TOOL = new DashTools();

	public void Splash()
	{
	    // CODE
	}

	public class MENU_OBJECTS
	{
	    readonly public static PictureBox MENU_ICON = new PictureBox();
	    readonly public static PictureBox MENU_BAR = new PictureBox();

	    readonly public static Button MENU_CLOSE = new Button();
	    readonly public static Button MENU_MINIM = new Button();

	    readonly public static Label MENU_TITLE = new Label();

	    public static void SETUP_EVENTS(Control MASTER)
	    {
		MENU_CLOSE.Click += (s, e) =>
		    Environment.Exit(-1);

		MENU_MINIM.Click += (s, e) =>
		{
		    if (MASTER is Form)
			((Form)MASTER).WindowState = FormWindowState.Minimized;

		    else
			MASTER.SendToBack();
		};
	    }
	}

	public void MenuBar(Form CON)
	{
	    var MENUBAR_SIZE = new Size(CON.Width - 2, 26);
	    var MENUBAR_LOCA = new Point(1, 1);
	    var MENUBAR_COLA = Color.FromArgb(16, 16, 16);

	    CONTROL.Image(CON, MENU_OBJECTS.MENU_BAR, MENUBAR_SIZE, MENUBAR_LOCA, null, MENUBAR_COLA);
	    
	    var MENUICON = (Image) Properties.Resources.ICON_PNG;
	    var MENUICON_SIZE = new Size(MENUICON.Width, MENUBAR_SIZE.Height);
	    var MENUICON_LOCA = new Point(0, -1);

	    CONTROL.Image(MENU_OBJECTS.MENU_BAR, MENU_OBJECTS.MENU_ICON, MENUICON_SIZE, MENUICON_LOCA, MENUICON, MENUBAR_COLA);

	    var MENUTITLE_LOCA = TOOL.GetCenter(MENU_OBJECTS.MENU_BAR, MENU_OBJECTS.MENU_TITLE, new Point(MENUICON_LOCA.X + 35, -1));
	    var MENUTITLE_FCOL = Color.White;

	    CONTROL.Label(MENU_OBJECTS.MENU_BAR, MENU_OBJECTS.MENU_TITLE, Size.Empty, MENUTITLE_LOCA, MENUTITLE_FCOL, MENUBAR_COLA, 1, 9, $"Dash Book");

	    var BORDER_SIZE = new Size(CON.Width - 1, CON.Height - 1);
	    var BORDER_LOCA = new Point(0, 0);
	    var BORDER_COLA = Color.FromArgb(1, 1, 1);

	    TOOL.PaintRectangle(CON, 2, BORDER_SIZE, BORDER_LOCA, BORDER_COLA);

	    var CLOSE_SIZE = new Size(65, MENUBAR_SIZE.Height);
	    var CLOSE_LOCA = new Point(MENUBAR_SIZE.Width - CLOSE_SIZE.Width, 0);
	    var CLOSE_FCOL = Color.White;
	    var CLOSE_BCOL = MENUBAR_COLA;

	    CONTROL.Button(MENU_OBJECTS.MENU_BAR, MENU_OBJECTS.MENU_CLOSE, CLOSE_SIZE, CLOSE_LOCA, CLOSE_BCOL, CLOSE_FCOL, 1, 12, "X", Color.Empty);

	    var MINIM_LOCA = new Point(CLOSE_LOCA.X - CLOSE_SIZE.Width, 0);

	    CONTROL.Button(MENU_OBJECTS.MENU_BAR, MENU_OBJECTS.MENU_MINIM, CLOSE_SIZE, MINIM_LOCA, CLOSE_BCOL, CLOSE_FCOL, 1, 12, "-", Color.Empty);

	    MENU_OBJECTS.SETUP_EVENTS(CON);

	    TOOL.Interactive(MENU_OBJECTS.MENU_BAR, CON);
	}

	public class TOOLBAR_OBJECTS
	{
	    readonly public static PictureBox OPTION_CONTAINER = new PictureBox();
	    readonly public static PictureBox TOOL_BAR = new PictureBox();
	    readonly public static PictureBox HELPER = new PictureBox();

	    readonly public static Button OPTION_1 = new Button();

	    static void SaveFile()// Perhaps implement asynchronization ?
	    {
		using (var diag = new SaveFileDialog())
		{
		    diag.InitialDirectory = Environment.CurrentDirectory;
		    diag.Filter = "Any File|*.*";

		    diag.CheckPathExists = true;
		    diag.CheckFileExists = false;

		RELOCATE:

		    var resu = diag.ShowDialog();

		    if (resu != DialogResult.OK)
			return;

		    var fnam = diag.FileName;

		    if (File.Exists(fnam))
		    {
			var resp = MessageBox.Show("The file already exists, would you like me to overwrite it anyway?", "Eh", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

			if (resp != DialogResult.Yes)
			{
			    return;
			};
		    };

		    using (var file = File.Create(fnam))
		    {
			var data = System.Text.Encoding.UTF8.GetBytes(EDITCONTAINER_OBJECTS.EDITOR_EDITAREA.Text);
			var leng = data.Length;

			file.Write(data, 0, leng);
			file.Close();

			if (!File.Exists(fnam))
			{
			    var resp = MessageBox.Show("I was unable to save the your file to the given location, would you like to relocate its destination?", "Oh no", MessageBoxButtons.YesNo, MessageBoxIcon.Error);

			    if (resp == DialogResult.Yes)
			    {
				goto RELOCATE;
			    };

			    return;
			};

			MessageBox.Show("The file has been saved!", "Yay", MessageBoxButtons.OK, MessageBoxIcon.Information);
		    };
		};
	    }

	    readonly public static Button OPTION_2 = new Button();

	    static void OpenFile()
	    {
		using (var diag = new OpenFileDialog())
		{
		    diag.InitialDirectory = Environment.CurrentDirectory;
		    diag.Filter = "Any File|*.*";

		    diag.CheckFileExists = true;
		    diag.CheckPathExists = true;

		RELOCATE:

		    var resu = diag.ShowDialog();

		    if (resu != DialogResult.OK)
			return;

		    var fdia = diag.FileName;

		    if (!File.Exists(fdia))
		    {
			var resp = MessageBox.Show("I was unable to open this file, would you like to retry?", "Oh no", MessageBoxButtons.YesNo, MessageBoxIcon.Error);

			if (resp == DialogResult.Yes)
			{
			    goto RELOCATE;
			};

			return;
		    };
		    
		    string outcome = "";

		    using (MemoryMappedFile file = MemoryMappedFile.OpenExisting(fdia))
		    {
			using (MemoryMappedViewStream stre = file.CreateViewStream())
			{
			    while ( true )
			    {// Make it work better <---
				int inte = stre.ReadByte();

				if (inte == 0)
				    break;

				outcome += 
			    };
			};
		    };


		    EDITCONTAINER_OBJECTS.EDITOR_EDITAREA.Text = outcome;

		    MessageBox.Show("The file has been loaded!", "Yay", MessageBoxButtons.OK, MessageBoxIcon.Information);
		};
	    }

	    readonly public static Button OPTION_3 = new Button();

	    static void NewFile()
	    {

	    }

	    readonly public static Button OPTION_4 = new Button();

	    static void ClearEditor()
	    {

	    }

	    readonly public static Button OPTION_5 = new Button();

	    static void OptionsDialog()
	    {

	    }

	    readonly public static List<string> OPTION_TEXTS = new List<string>() { "Save", "Open", "New", "Clear", "Options", };

	    public static void SETUP_EVENTS()
	    {
		OPTION_1.Click += (s, e) =>
		    new Thread(() => SaveFile())
			{ IsBackground = true }.Start();

		OPTION_2.Click += (s, e) =>
		    new Thread(() => OpenFile())
			{ IsBackground = true }.Start();

		OPTION_3.Click += (s, e) =>
		    NewFile();

		OPTION_4.Click += (s, e) =>
		    ClearEditor();

		OPTION_5.Click += (s, e) =>
		    OptionsDialog();
	    }
	}

	public void ToolBar(Form CON)
	{
	    var TOOLBAR_SIZE = new Size(CON.Width - 2, 45);
	    var TOOLBAR_LOCA = new Point(1, CON.Height - TOOLBAR_SIZE.Height - 1);
	    var TOOLBAR_COLA = Color.FromArgb(16, 16, 16);

	    CONTROL.Image(CON, TOOLBAR_OBJECTS.TOOL_BAR, TOOLBAR_SIZE, TOOLBAR_LOCA, null, TOOLBAR_COLA);

	    var OPTION_SIZE = new Size(75, 36);

	    var OPTIONCONTAINER_SIZE = new Size(OPTION_SIZE.Width * 5 + 8 * 4, OPTION_SIZE.Height);
	    var OPTIONCONTAINER_LOCA = new Point(5, (TOOLBAR_OBJECTS.TOOL_BAR.Height - OPTION_SIZE.Height) / 2 + 1);
	    var OPTIONCONTAINER_COLA = TOOLBAR_COLA;

	    CONTROL.Image(TOOLBAR_OBJECTS.TOOL_BAR, TOOLBAR_OBJECTS.OPTION_CONTAINER, OPTIONCONTAINER_SIZE, OPTIONCONTAINER_LOCA, null, OPTIONCONTAINER_COLA); //Add Buttons AUTOMATICALLY and AUTO SIZE THAT SHIT

	    var OPTION_LOCA = new Point(0, 0);
	    var OPTION_BCOLA = TOOLBAR_OBJECTS.OPTION_CONTAINER.BackColor;
	    var OPTION_FCOLA = Color.White;

	    string OPTION_TEXT(int id) => TOOLBAR_OBJECTS.OPTION_TEXTS[id];

	    CONTROL.Button(TOOLBAR_OBJECTS.OPTION_CONTAINER, TOOLBAR_OBJECTS.OPTION_1, OPTION_SIZE, OPTION_LOCA, OPTION_BCOLA, OPTION_FCOLA, 1, 12, OPTION_TEXT(0), Color.Empty); OPTION_LOCA.X += OPTION_SIZE.Width + 8;
	    CONTROL.Button(TOOLBAR_OBJECTS.OPTION_CONTAINER, TOOLBAR_OBJECTS.OPTION_2, OPTION_SIZE, OPTION_LOCA, OPTION_BCOLA, OPTION_FCOLA, 1, 12, OPTION_TEXT(1), Color.Empty); OPTION_LOCA.X += OPTION_SIZE.Width + 8;
	    CONTROL.Button(TOOLBAR_OBJECTS.OPTION_CONTAINER, TOOLBAR_OBJECTS.OPTION_3, OPTION_SIZE, OPTION_LOCA, OPTION_BCOLA, OPTION_FCOLA, 1, 12, OPTION_TEXT(2), Color.Empty); OPTION_LOCA.X += OPTION_SIZE.Width + 8;
	    CONTROL.Button(TOOLBAR_OBJECTS.OPTION_CONTAINER, TOOLBAR_OBJECTS.OPTION_4, OPTION_SIZE, OPTION_LOCA, OPTION_BCOLA, OPTION_FCOLA, 1, 12, OPTION_TEXT(3), Color.Empty); OPTION_LOCA.X += OPTION_SIZE.Width + 8;
	    CONTROL.Button(TOOLBAR_OBJECTS.OPTION_CONTAINER, TOOLBAR_OBJECTS.OPTION_5, OPTION_SIZE, OPTION_LOCA, OPTION_BCOLA, OPTION_FCOLA, 1, 12, OPTION_TEXT(4), Color.Empty);

	    var HELPER_IMAGE = (Image)Properties.Resources.HELPER_PNG;
	    var HELPER_SIZE = HELPER_IMAGE.Size;
	    var HELPER_LOCA = new Point(TOOLBAR_OBJECTS.TOOL_BAR.Width - HELPER_SIZE.Height, (TOOLBAR_OBJECTS.TOOL_BAR.Height - HELPER_SIZE.Height) / 2);

	    CONTROL.Image(TOOLBAR_OBJECTS.TOOL_BAR, TOOLBAR_OBJECTS.HELPER, HELPER_SIZE, HELPER_LOCA, HELPER_IMAGE, OPTION_BCOLA);

	    var BORDER_LOC1 = new Point(0, 0);
	    var BORDER_LOC2 = new Point(TOOLBAR_SIZE.Width, 0);
	    var BORDER_COLA = Color.FromArgb(1, 1, 1);

	    TOOL.PaintLine(TOOLBAR_OBJECTS.TOOL_BAR, BORDER_COLA, 1, BORDER_LOC1, BORDER_LOC2);

	    TOOLBAR_OBJECTS.SETUP_EVENTS();
	}

	public class MAINGUI_OBJECTS
	{
	    readonly public static PictureBox MAIN_CONTAINER = new PictureBox();
	}

	public void MainGUI(Form CON)
	{
	    var GUI_ICON = (Icon)Properties.Resources.ICON_ICO;
	    var GUI_COLA = Color.FromArgb(1, 1, 1);

	    CON.Icon = GUI_ICON;
	    CON.BackColor = GUI_COLA;

	    var CONTAINER_SIZE = new Size(CON.Width - 2, CON.Height - (MENU_OBJECTS.MENU_BAR.Height + TOOLBAR_OBJECTS.TOOL_BAR.Height + 3));
	    var CONTAINER_LOCA = new Point(1, MENU_OBJECTS.MENU_BAR.Height + MENU_OBJECTS.MENU_BAR.Top + 1);
	    var CONTAINER_COLA = Color.FromArgb(12, 12, 12);

	    CONTROL.Image(CON, MAINGUI_OBJECTS.MAIN_CONTAINER, CONTAINER_SIZE, Point.Empty, null, CONTAINER_COLA);

	    MAINGUI_OBJECTS.MAIN_CONTAINER.Location = CONTAINER_LOCA;
	}

	public class EDITCONTAINER_OBJECTS
	{
	    readonly public static TabRichTextBox EDITOR_EDITAREA = new TabRichTextBox();

	    readonly public static RichTextBox EDITOR_LINENUMBER = new RichTextBox();

	    readonly public static PictureBox EDITOR_SCROLLCONTAINER = new PictureBox();
	    readonly public static PictureBox EDITOR_SCROLLBAR = new PictureBox();
	    readonly public static PictureBox EDITOR_CONTAINER = new PictureBox();

	    /*readonly private static List<string> CODE_KEYWORDS = new List<string>()
	    {
		"public", "private", "void", "using", "static", "class", "readonly", "protected", "override",
		"bool", "float", "int"
	    };*/

	    public static void SETUP_EVENTS()
	    {
		var OLines = 1;

		EDITOR_EDITAREA.TextChanged += (s, e) =>
		{
		    var Lines = EDITOR_EDITAREA.Lines.Length;

		    if (Lines > OLines)
		    {
			EDITOR_LINENUMBER.Text += $"{EDITOR_LINENUMBER.Lines.Length}\r\n";

			CenterLineNumbers();

			OLines = Lines;
		    };

		    /* //Syntax Highlight Ideology
		    foreach (Match MATCH in Regex.Matches(EDITOR_EDITAREA.Text.ToLower(), "static"))
		    {
			EDITOR_EDITAREA.SelectionColor = Color.Cyan;
			EDITOR_EDITAREA.Select(MATCH.Index, MATCH.Length);
		    };
		    */
		};
	    }
	}

	public static void CenterLineNumbers()
	{
	    EDITCONTAINER_OBJECTS.EDITOR_LINENUMBER.SelectAll();
	    EDITCONTAINER_OBJECTS.EDITOR_LINENUMBER.SelectionAlignment = HorizontalAlignment.Center;
	}

	public void EditContainer(Form CON)
	{
	    var EDITCONTAINER_SIZE = new Size(MAINGUI_OBJECTS.MAIN_CONTAINER.Width - 10, MAINGUI_OBJECTS.MAIN_CONTAINER.Height - 20);
	    var EDITCONTAINER_LOCA = new Point(10, 10);
	    var EDITCONTAINER_COLA = Color.FromArgb(16, 16, 16);

	    CONTROL.Image(MAINGUI_OBJECTS.MAIN_CONTAINER, EDITCONTAINER_OBJECTS.EDITOR_CONTAINER, EDITCONTAINER_SIZE, EDITCONTAINER_LOCA, null, EDITCONTAINER_COLA);

	    var EDITLINE_SIZE = new Size(32, MAINGUI_OBJECTS.MAIN_CONTAINER.Height);
	    var EDITLINE_LOCA = new Point(0, 0);
	    var EDITLINE_FCOL = Color.FromArgb(255, 255, 255);
	    var EDITLINE_BCOL = Color.FromArgb(39, 28, 59);//26, 26, 26);

	    CONTROL.RichTextBox(EDITCONTAINER_OBJECTS.EDITOR_CONTAINER, EDITCONTAINER_OBJECTS.EDITOR_LINENUMBER, EDITLINE_SIZE, EDITLINE_LOCA, EDITLINE_FCOL, EDITLINE_BCOL, 1, 10, "");

	    EDITCONTAINER_OBJECTS.EDITOR_LINENUMBER.ScrollBars = RichTextBoxScrollBars.None;
	    EDITCONTAINER_OBJECTS.EDITOR_LINENUMBER.Multiline = true;
	    EDITCONTAINER_OBJECTS.EDITOR_LINENUMBER.ReadOnly = true;

	    CenterLineNumbers();

	    var EDITOR_SIZE = new Size(EDITCONTAINER_OBJECTS.EDITOR_CONTAINER.Width - EDITCONTAINER_OBJECTS.EDITOR_LINENUMBER.Width - 5/*REMOVE SCROLL BAR WIDTH*/, EDITCONTAINER_OBJECTS.EDITOR_CONTAINER.Height);
	    var EDITOR_LOCA = new Point(EDITCONTAINER_OBJECTS.EDITOR_LINENUMBER.Width + 5, 0);
	    var EDITOR_FCOL = Color.FromArgb(195, 195, 195);
	    var EDITOR_BCOL = Color.FromArgb(20, 20, 20);

	    CONTROL.RichTextBox(EDITCONTAINER_OBJECTS.EDITOR_CONTAINER, EDITCONTAINER_OBJECTS.EDITOR_EDITAREA, EDITOR_SIZE, EDITOR_LOCA, EDITOR_FCOL, EDITOR_BCOL, 1, 10, "I am text.");

	    // The goal is to move the Line number container along with the scroll bar based off of
	    // the current and upcoming X and Y values.

	    EDITCONTAINER_OBJECTS.EDITOR_EDITAREA.ScrollBars = RichTextBoxScrollBars.None;
	    EDITCONTAINER_OBJECTS.EDITOR_EDITAREA.AcceptsTab = true;
	    EDITCONTAINER_OBJECTS.EDITOR_EDITAREA.Text = Resources.START_TEXT_CPP;

	    for (int l = 0; l < EDITCONTAINER_OBJECTS.EDITOR_EDITAREA.Lines.Length; l += 1)
	    {
		EDITCONTAINER_OBJECTS.EDITOR_LINENUMBER.Text += $"{l+1}\r\n";
	    };

	    var SCROLLBAR_SIZE = new Size(24, EDITOR_SIZE.Height);
	    var SCROLLBAR_LOCA = new Point(EDITCONTAINER_OBJECTS.EDITOR_EDITAREA.Width - SCROLLBAR_SIZE.Width, 0);
	    var SCROLLBAR_COL1 = EDITOR_BCOL;
	    var SCROLLBAR_COL2 = Color.FromArgb(90, 118, 87, 150);

	    CONTROL.ScrollBar(EDITCONTAINER_OBJECTS.EDITOR_EDITAREA, EDITCONTAINER_OBJECTS.EDITOR_SCROLLCONTAINER, SCROLLBAR_COL1, EDITCONTAINER_OBJECTS.EDITOR_SCROLLBAR, SCROLLBAR_COL2, SCROLLBAR_SIZE, SCROLLBAR_LOCA);
	    
	    TOOL.Round(EDITCONTAINER_OBJECTS.EDITOR_SCROLLBAR, 4);

	    var LEFTSPACING_OBJE = new PictureBox();
	    var LEFTSPACING_SIZE = new Size(5, EDITOR_SIZE.Height);
	    var LEFTSPACING_LOCA = new Point(EDITOR_LOCA.X - 5, EDITOR_LOCA.Y);
	    var LEFTSPACING_COLA = EDITOR_BCOL;

	    CONTROL.Image(EDITCONTAINER_OBJECTS.EDITOR_CONTAINER, LEFTSPACING_OBJE, LEFTSPACING_SIZE, LEFTSPACING_LOCA, null, LEFTSPACING_COLA);

	    var BORDER_SIZE = new Size(EDITCONTAINER_SIZE.Width, EDITCONTAINER_SIZE.Height + 1);
	    var BORDER_LOCA = new Point(EDITCONTAINER_LOCA.X - 1, EDITCONTAINER_LOCA.Y - 1);
	    var BORDER_COLA = Color.FromArgb(1, 1, 1);

	    TOOL.PaintRectangle(MAINGUI_OBJECTS.MAIN_CONTAINER, 1, BORDER_SIZE, BORDER_LOCA, BORDER_COLA);

	    var LINE_LOC1 = new Point(0, 0);//new Point(EDITLINE_LOCA.X + EDITLINE_SIZE.Width + 1, 0);
	    var LINE_LOC2 = new Point(0, EDITLINE_SIZE.Height); //new Point(LINE_LOC1.X, EDITLINE_SIZE.Height);
	    var LINE_COLA = BORDER_COLA;

	    TOOL.PaintLine(LEFTSPACING_OBJE, LINE_COLA, 1, LINE_LOC1, LINE_LOC2);

	    EDITCONTAINER_OBJECTS.SETUP_EVENTS();
	}
    }
}