
// TEST APPLICATION #1 - The functional side is what matters the most to me. || Disclaimer:
//
// This application along with many others are made purely for experimentational purposes only, which means
// that the codes are not clean most of the time.  It is only conceptual in the sense of the application being
// an actual application meant for marketing ends.

// Author: Dashie
// Version: 1.0

using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows.Forms;

namespace DashCurl
{
    public partial class DashCurl : Form
    {
	public DashCurl()
	{
	    InitializeComponent();

	    MaximizeBox = false;
	    MinimizeBox = false;

	    MinimumSize = Size;
	    MaximumSize = Size;

	    Icon = Properties.Resources.blue;
	}

	private bool isUri(string Url)
	{
	    Uri uriResult;

	    return Uri.TryCreate(Url, UriKind.Absolute, out uriResult)
		&& (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
	}

	private void onDownload(object sender, EventArgs e)
	{
	    if (!isUri(Url.Text))
	    {
		MessageBox.Show("It appears that the specified U.R.L. is not a valid U.R.L.\n\nPlease make sure that the U.R.L. was typed correctly and also make sure that the U.R.L. is reachable.", "Dash Curl", MessageBoxButtons.OK, MessageBoxIcon.Error);
		return;
	    };

	    if (SaveLocation.Text.Any(Path.GetInvalidPathChars().Contains))
	    {
		MessageBox.Show("It appears that the specified save location is unavailable.\n\nPlease make sure that the path was typed correctly and that you have the required rights to the file system.\n\nPress OK to close this dialog.", "Dash Curl", MessageBoxButtons.OK, MessageBoxIcon.Error);
		return;
	    };

	    File.Delete(SaveLocation.Text);

	    try
	    {
		using (WebClient client = new WebClient())
		{
		    client.DownloadFile(Url.Text, SaveLocation.Text);
		};
	    }

	    catch (Exception exp)
	    {
		MessageBox.Show($"The file could not be retrieved.\n\nStack-trace:\n\n{exp.StackTrace}\n\nPress OK to close this dialog.", "Dash Curl", MessageBoxButtons.OK, MessageBoxIcon.Error);
	    };
	}

	readonly AboutDialog about = new AboutDialog();

	private void onAbout(object sender, EventArgs e)
	{
	    about.ShowDialog();
	}

	private void onModifySaveTo(object sender, EventArgs e)
	{
	    using (SaveFileDialog dialog = new SaveFileDialog())
	    {
		dialog.CheckFileExists = false;
		dialog.CheckPathExists = true;

		dialog.Filter = "Any File|*.*";
		dialog.Title = "Output Location";

		dialog.ShowDialog();

		if (dialog.FileName.Length > 0)
		{
		    MessageBox.Show(null, $"The file will be saved to- \n\n'{dialog.FileName}'\n\n-when it is done downloading!\n\nPress OK to close this dialog.", "Dash Curl", MessageBoxButtons.OK, MessageBoxIcon.Information);
		    SaveLocation.Text = $"{dialog.FileName}";
		};
	    };
	}
    }

    static class Program
    {
	[STAThread]
	static void Main()
	{
	    Application.EnableVisualStyles();
	    Application.SetCompatibleTextRenderingDefault(false);

	    DashCurl application = new DashCurl();

	    Application.Run(application);
	}
    }
}
