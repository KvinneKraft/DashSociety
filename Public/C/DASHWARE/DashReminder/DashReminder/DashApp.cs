using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace DashReminder
{
    public partial class DashApp : Form
    {
	readonly PictureBox _mbar = new PictureBox();

	public DashApp()
	{
	    Hide(); Icon = Properties.Resources.icon;
	    InitializeComponent();

	    Add.MenuBar(this, _mbar, Color.Indigo, new Label(), "Dash Reminder 1.0", Get.Font.NORMAL, 10, Color.White, _border:true, _borderColor:Color.Indigo, _quit:true, _minimize:false, _draggable:true);

	    InitializeBottomContainer();
	}

	readonly PictureBox BottomContainer = new PictureBox();

	private void InitializeBottomContainer()
	{
	    Point GetBottomContainerLocation() => new Point(1, Height - 276);
	    Size GetBottomContainerSize() => new Size(Width - 2, 275);

	    Add.PictureBox(this, BottomContainer, null, GetBottomContainerSize(), GetBottomContainerLocation(), _mbar.BackColor);
	}
    };
};
