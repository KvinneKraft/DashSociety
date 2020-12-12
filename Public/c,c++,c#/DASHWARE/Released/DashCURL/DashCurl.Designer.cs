namespace DashCurl
{
    partial class DashCurl
    {
	/// <summary>
	/// Required designer variable.
	/// </summary>
	private System.ComponentModel.IContainer components = null;

	/// <summary>
	/// Clean up any resources being used.
	/// </summary>
	/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
	protected override void Dispose(bool disposing)
	{
	    if (disposing && (components != null))
	    {
		components.Dispose();
	    }
	    base.Dispose(disposing);
	}

	#region Windows Form Designer generated code

	/// <summary>
	/// Required method for Designer support - do not modify
	/// the contents of this method with the code editor.
	/// </summary>
	private void InitializeComponent()
	{
	    this.Download = new System.Windows.Forms.Button();
	    this.About = new System.Windows.Forms.Button();
	    this.label1 = new System.Windows.Forms.Label();
	    this.Url = new System.Windows.Forms.TextBox();
	    this.label2 = new System.Windows.Forms.Label();
	    this.SaveLocation = new System.Windows.Forms.TextBox();
	    this.SuspendLayout();
	    // 
	    // Download
	    // 
	    this.Download.ForeColor = System.Drawing.SystemColors.ControlText;
	    this.Download.Location = new System.Drawing.Point(12, 73);
	    this.Download.Name = "Download";
	    this.Download.Size = new System.Drawing.Size(165, 23);
	    this.Download.TabIndex = 0;
	    this.Download.Text = "Download";
	    this.Download.UseVisualStyleBackColor = true;
	    this.Download.Click += new System.EventHandler(this.onDownload);
	    // 
	    // About
	    // 
	    this.About.ForeColor = System.Drawing.SystemColors.ControlText;
	    this.About.Location = new System.Drawing.Point(183, 73);
	    this.About.Name = "About";
	    this.About.Size = new System.Drawing.Size(146, 23);
	    this.About.TabIndex = 3;
	    this.About.Text = "About";
	    this.About.UseVisualStyleBackColor = true;
	    this.About.Click += new System.EventHandler(this.onAbout);
	    // 
	    // label1
	    // 
	    this.label1.AutoSize = true;
	    this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
	    this.label1.ForeColor = System.Drawing.SystemColors.InfoText;
	    this.label1.Location = new System.Drawing.Point(7, 9);
	    this.label1.Name = "label1";
	    this.label1.Size = new System.Drawing.Size(39, 25);
	    this.label1.TabIndex = 4;
	    this.label1.Text = "Url";
	    // 
	    // Url
	    // 
	    this.Url.Location = new System.Drawing.Point(52, 12);
	    this.Url.Name = "Url";
	    this.Url.Size = new System.Drawing.Size(277, 20);
	    this.Url.TabIndex = 5;
	    this.Url.Text = "https://web.io/downloads/pack.zip";
	    // 
	    // label2
	    // 
	    this.label2.AutoSize = true;
	    this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
	    this.label2.ForeColor = System.Drawing.SystemColors.InfoText;
	    this.label2.Location = new System.Drawing.Point(7, 34);
	    this.label2.Name = "label2";
	    this.label2.Size = new System.Drawing.Size(93, 25);
	    this.label2.TabIndex = 6;
	    this.label2.Text = "Save-To";
	    // 
	    // SaveLocation
	    // 
	    this.SaveLocation.Location = new System.Drawing.Point(106, 38);
	    this.SaveLocation.Name = "SaveLocation";
	    this.SaveLocation.Size = new System.Drawing.Size(223, 20);
	    this.SaveLocation.TabIndex = 7;
	    this.SaveLocation.Text = "C:\\Users\\{user}\\Downloads\\{filename}";
	    this.SaveLocation.Click += new System.EventHandler(this.onModifySaveTo);
	    // 
	    // DashCurl
	    // 
	    this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
	    this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
	    this.ClientSize = new System.Drawing.Size(346, 107);
	    this.Controls.Add(this.SaveLocation);
	    this.Controls.Add(this.label2);
	    this.Controls.Add(this.Url);
	    this.Controls.Add(this.label1);
	    this.Controls.Add(this.About);
	    this.Controls.Add(this.Download);
	    this.ForeColor = System.Drawing.SystemColors.Control;
	    this.Name = "DashCurl";
	    this.Text = "DashCurl";
	    this.ResumeLayout(false);
	    this.PerformLayout();

	}

	#endregion

	private System.Windows.Forms.Button Download;
	private System.Windows.Forms.Button About;
	private System.Windows.Forms.Label label1;
	private System.Windows.Forms.TextBox Url;
	private System.Windows.Forms.Label label2;
	private System.Windows.Forms.TextBox SaveLocation;
    }
}

