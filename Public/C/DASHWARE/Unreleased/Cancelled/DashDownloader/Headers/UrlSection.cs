using System;
using System.Drawing;
using System.Windows.Forms;

namespace DashDownloader
{
    public class UrlSection
    {
	readonly private DashControls CONTROL = new DashControls();
	readonly private DashTools TOOL = new DashTools();
	
	public static class URL_OBJECTS
	{
	    readonly public static PictureBox CONTAINER = new PictureBox();
	    readonly public static TextBox INPUT = new TextBox();
	}

	public static int ContainerHeight() => /*BUTTON HEIGHT*/ (28 * 3) + /*BUTTON SPACING*/ (8 * 3) + 35;

	public void Initialize(Form CON)
	{
	    try
	    {
		var CONTAINER_SIZE = new Size(255, ContainerHeight());
		var CONTAINER_LOCA = new Point(0, 0);
		var CONTAINER_BCOL = Color.FromArgb(24, 24, 24);

		CONTROL.Image(DashDownloader.TOP_CONTAINER, URL_OBJECTS.CONTAINER, CONTAINER_SIZE, CONTAINER_LOCA, null, CONTAINER_BCOL);
		TOOL.Round(URL_OBJECTS.CONTAINER, 6);

		var INPUT_SIZE = new Size(CONTAINER_SIZE.Width - 5, CONTAINER_SIZE.Height - 10);
		var INPUT_LOCA = new Point(5, 5);
		var INPUT_FCOL = Color.White;

		CONTROL.TextBox(URL_OBJECTS.CONTAINER, URL_OBJECTS.INPUT, INPUT_SIZE, INPUT_LOCA, CONTAINER_BCOL, INPUT_FCOL, 1, 10, Color.Empty, SCROLLBAR: true, MULTILINE: true, FIXEDSIZE: false);

		URL_OBJECTS.INPUT.WordWrap = false;
		URL_OBJECTS.INPUT.Text += ("https://www.youtube.com/watch?v=yFH79aY17rY\r\nhttps://www.youtube.com/watch?v=8MJCDW5KTRA\r\nhttps://static.wikia.nocookie.net/mlp/images/a/aa/FANMADE_Princess_Luna_Walking.gif/revision/latest/");
	    }

	    catch
	    {
		throw new Exception("UrlSection.cs");
	    };
	}
    }
}