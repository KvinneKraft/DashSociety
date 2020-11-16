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
	    readonly public static TextBox URL_BOX = new TextBox();
	}
	
	public void Initialize(Form CON)
	{
	    var TEXT_SIZE = new Size(250, /*BUTTON HEIGHT*/ (28 * 3) + /*BUTTON SPACING*/ (8 * 3) + /*AUTHOR LABEL*/ (36));
	    var TEXT_LOCA = new Point(0, 0);
	    var TEXT_BCOL = Color.FromArgb(12, 12, 12);
	    var TEXT_FCOL = Color.White;

	    CONTROL.TextBox(DashDownloader.TOP_CONTAINER, URL_OBJECTS.URL_BOX, TEXT_SIZE, TEXT_LOCA, TEXT_BCOL, TEXT_FCOL, 1, 10, Color.Empty, SCROLLBAR:true, MULTILINE:true, FIXEDSIZE:false);

	    TOOL.Round(URL_OBJECTS.URL_BOX, 6);

	    URL_OBJECTS.URL_BOX.WordWrap = false;
	    URL_OBJECTS.URL_BOX.Text += ("https://www.youtube.com/watch?v=yFH79aY17rY\r\nhttps://www.youtube.com/watch?v=8MJCDW5KTRA\r\nhttps://static.wikia.nocookie.net/mlp/images/a/aa/FANMADE_Princess_Luna_Walking.gif/revision/latest/");
	}
    }
}