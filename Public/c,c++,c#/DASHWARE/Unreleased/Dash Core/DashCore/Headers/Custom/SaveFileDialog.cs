
// Author: Dashie
// Version: 1.0

using System;
using System.Windows.Forms;
using System.Collections.Generic;

namespace DashCore
{
    public class SaveFileDialog : Form
    {
	public List<string> FILE_NAMES = new List<string>(); // <--- Select

	public string DIALOG_TITLE = "Save File";

	public bool CREATE_MISSING_DIRECTORIES = false;
	public bool CHECK_FILE_EXISTS = false;
	public bool CHECK_PATH_EXISTS = false;

	public SaveFileDialog()
	{

	}

	~SaveFileDialog() { /*Disposal*/ }
    }
}
