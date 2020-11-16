using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dasher
{
    public partial class IDasher : Form
    {
	readonly PictureBox MENU_BAR = new PictureBox();
	readonly Label MENU_BAR_TITLE = new Label();

	private void IInterface()
	{
	    var BACK_COLOR = Color.FromArgb(24, 15, 41);
	    var FORE_COLOR = Color.White;

	    Add.MenuBar(this, MENU_BAR, BACK_COLOR, MENU_BAR_TITLE, "Dasher 1.0", 1, 10, FORE_COLOR, _border:true, _borderColor:BACK_COLOR, _quit:true, _draggable:true);
	}

	public IDasher()
	{
	    InitializeComponent();

	    try
	    {
		IInterface();
	    }

	    catch (Exception e)
	    {

	    };


	}
    };
};
