
using System;
using System.Windows.Forms;

namespace DashBook
{ 
    public partial class DashBook : Form
    {
	readonly DashTools TOOL = new DashTools();

	public DashBook()
	{
	    InitializeComponent();

	    try
	    {
		var _Init = new Initialize();

		_Init.Splash(); // <--- SHOULD SHOW

		_Init.MenuBar(this);
		_Init.ToolBar(this);
		_Init.MainGUI(this);

		_Init.EditContainer(this);
	    }

	    catch (Exception e)
	    {
		MessageBox.Show(TOOL.GetErrorFormat(e), "[(Dash Books)]");
		Environment.Exit(-1);
	    };
	}
    }
}
