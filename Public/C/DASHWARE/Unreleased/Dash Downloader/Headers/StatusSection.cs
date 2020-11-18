using System;
using System.Drawing;
using System.Windows.Forms;

namespace DashDownloader
{
    public class StatusSection
    {
	readonly private DashControls CONTROL = new DashControls();
	readonly private DashTools TOOL = new DashTools();

	public class STATUS_OBJECTS
	{
	    readonly public static PictureBox CONTAINER = new PictureBox();
	    readonly public static RichTextBox STATUS = new RichTextBox();
	}

	public void Initialize(Form CON)
	{
	    try
	    {
		var CONTAINER_SIZE = new Size(CON.Width - 16, 150);
		var CONTAINER_LOCA = new Point(8, MenuBar.MENU_OBJECTS.BAR.Height + UrlSection.URL_OBJECTS.INPUT.Height + 26);
		var CONTAINER_COLA = UrlSection.URL_OBJECTS.INPUT.BackColor;

		CONTROL.Image(CON, STATUS_OBJECTS.CONTAINER, CONTAINER_SIZE, CONTAINER_LOCA, null, CONTAINER_COLA);
		TOOL.Round(STATUS_OBJECTS.CONTAINER, 6);

		var STATUS_SIZE = new Size(CONTAINER_SIZE.Width - 5, CONTAINER_SIZE.Height - 10);
		var STATUS_LOCA = new Point(5, 5);
		var STATUS_BCOL = Color.FromArgb(24, 24, 24);
		var STATUS_FCOL = Color.FromArgb(255, 255, 255);

		CONTROL.RichTextBox(STATUS_OBJECTS.CONTAINER, STATUS_OBJECTS.STATUS, STATUS_SIZE, STATUS_LOCA, STATUS_FCOL, STATUS_BCOL, 1, 10, "[STATUS]: Waiting ....");

		STATUS_OBJECTS.STATUS.ScrollBars = RichTextBoxScrollBars.ForcedVertical;
	    }

	    catch
	    {
		throw new Exception("StatusSection.cs");
	    };
	}
    }
}