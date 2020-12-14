
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace DashBook
{
    public class TabRichTextBox : RichTextBox
    {
	protected override bool ProcessCmdKey(ref Message m, Keys keyData)
	{
	    if (keyData == Keys.Tab)
	    {
		SelectedText += "     ";
		return true;
	    }

	    else if (keyData == Keys.Enter)
	    {
		SelectedText += "\n";
		return true;
	    };

	    return base.ProcessCmdKey(ref m, keyData);
	}
    }
}
