using System;
using System.Drawing;
using System.Windows.Forms;

namespace DashDownloader
{
    public class OptionSection
    {
	readonly private DashControls CONTROL = new DashControls();
	readonly private DashTools TOOL = new DashTools();

	public static class OPTION_OBJECTS
	{
	    readonly public static PictureBox CONTAINER = new PictureBox();
	    readonly public static PictureBox LUNA = new PictureBox();

	    readonly public static Button CHECK_AVAILABLE = new Button();
	    readonly public static Button DOWNLOAD = new Button();
	    readonly public static Button OPTIONS = new Button();
	}

	public void Initialize(Form CON)
	{
	    try
	    {
		var CONTAINER_SIZE = new Size(123, UrlSection.URL_OBJECTS.INPUT.Height + 8);
		var CONTAINER_LOCA = new Point(UrlSection.URL_OBJECTS.INPUT.Width + UrlSection.URL_OBJECTS.INPUT.Left + 6, 0);
		var CONTAINER_COLA = CON.BackColor;

		CONTROL.Image(DashDownloader.TOP_CONTAINER, OPTION_OBJECTS.CONTAINER, CONTAINER_SIZE, CONTAINER_LOCA, null, CONTAINER_COLA);

		var BUTTON_SIZE = new Size(CONTAINER_SIZE.Width, 28);
		var BUTTON_LOCA = new Point(0, CONTAINER_SIZE.Height - 28);
		var BUTTON_BCOL = Color.FromArgb(4, 4, 4);
		var BUTTON_FCOL = Color.White;

		CONTROL.Button(OPTION_OBJECTS.CONTAINER, OPTION_OBJECTS.OPTIONS, BUTTON_SIZE, BUTTON_LOCA, BUTTON_BCOL, BUTTON_FCOL, 1, 10, "Options", Color.Empty);
		TOOL.Round(OPTION_OBJECTS.OPTIONS, 6);

		BUTTON_LOCA.Y -= (28 + 5);

		CONTROL.Button(OPTION_OBJECTS.CONTAINER, OPTION_OBJECTS.CHECK_AVAILABLE, BUTTON_SIZE, BUTTON_LOCA, BUTTON_BCOL, BUTTON_FCOL, 1, 10, "Check Available", Color.Empty);
		TOOL.Round(OPTION_OBJECTS.CHECK_AVAILABLE, 6);

		BUTTON_LOCA.Y -= (28 + 5);

		CONTROL.Button(OPTION_OBJECTS.CONTAINER, OPTION_OBJECTS.DOWNLOAD, BUTTON_SIZE, BUTTON_LOCA, BUTTON_BCOL, BUTTON_FCOL, 1, 10, "Download", Color.Empty);
		TOOL.Round(OPTION_OBJECTS.DOWNLOAD, 6);

		var LUNA_LOCA = new Point(15, 0);
		var LUNA_IMAG = (Image)Properties.Resources.BANNER_GIF;
		var LUNA_SIZE = LUNA_IMAG.Size;
		var LUNA_COLA = CONTAINER_COLA;

		CONTROL.Image(OPTION_OBJECTS.CONTAINER, OPTION_OBJECTS.LUNA, LUNA_SIZE, LUNA_LOCA, LUNA_IMAG, LUNA_COLA);
		// Setup CLICK Event for LUNA
	    }

	    catch
	    {
		throw new Exception("OptionSection.cs");
	    };
	}
    }
}