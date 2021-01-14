namespace DashCurl
{
    partial class AboutDialog
    {
	/// <summary>
	/// Required designer variable.
	/// </summary>
	private System.ComponentModel.IContainer components = null;

	/// <summary>
	/// Clean up any resources being used.
	/// </summary>
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
	    System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AboutDialog));
	    this.Okay = new System.Windows.Forms.Button();
	    this.Github = new System.Windows.Forms.Label();
	    this.linkLabel1 = new System.Windows.Forms.LinkLabel();
	    this.textBox1 = new System.Windows.Forms.TextBox();
	    this.SuspendLayout();
	    // 
	    // Okay
	    // 
	    this.Okay.Location = new System.Drawing.Point(12, 167);
	    this.Okay.Name = "Okay";
	    this.Okay.Size = new System.Drawing.Size(197, 26);
	    this.Okay.TabIndex = 0;
	    this.Okay.Text = "Okay";
	    this.Okay.UseVisualStyleBackColor = false;
	    this.Okay.Click += new System.EventHandler(this.button1_Click);
	    // 
	    // Github
	    // 
	    this.Github.AutoSize = true;
	    this.Github.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F);
	    this.Github.ForeColor = System.Drawing.SystemColors.ControlText;
	    this.Github.Location = new System.Drawing.Point(12, 143);
	    this.Github.Name = "Github";
	    this.Github.Size = new System.Drawing.Size(49, 16);
	    this.Github.TabIndex = 2;
	    this.Github.Text = "Github:";
	    this.Github.Click += new System.EventHandler(this.Github_Click);
	    // 
	    // linkLabel1
	    // 
	    this.linkLabel1.AutoSize = true;
	    this.linkLabel1.Location = new System.Drawing.Point(54, 144);
	    this.linkLabel1.Name = "linkLabel1";
	    this.linkLabel1.Size = new System.Drawing.Size(155, 13);
	    this.linkLabel1.TabIndex = 3;
	    this.linkLabel1.TabStop = true;
	    this.linkLabel1.Text = "https://github.com/KvinneKraft";
	    this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
	    // 
	    // textBox1
	    // 
	    this.textBox1.Location = new System.Drawing.Point(5, 6);
	    this.textBox1.Multiline = true;
	    this.textBox1.Name = "textBox1";
	    this.textBox1.ReadOnly = true;
	    this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
	    this.textBox1.Size = new System.Drawing.Size(211, 128);
	    this.textBox1.TabIndex = 4;
	    this.textBox1.Text = resources.GetString("textBox1.Text");
	    // 
	    // AboutDialog
	    // 
	    this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
	    this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
	    this.ClientSize = new System.Drawing.Size(221, 201);
	    this.Controls.Add(this.textBox1);
	    this.Controls.Add(this.linkLabel1);
	    this.Controls.Add(this.Github);
	    this.Controls.Add(this.Okay);
	    this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
	    this.MaximizeBox = false;
	    this.MinimizeBox = false;
	    this.Name = "AboutDialog";
	    this.Padding = new System.Windows.Forms.Padding(9);
	    this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
	    this.Text = "AboutDialog";
	    this.ResumeLayout(false);
	    this.PerformLayout();

	}

	#endregion

	private System.Windows.Forms.Button Okay;
	private System.Windows.Forms.Label Github;
	private System.Windows.Forms.LinkLabel linkLabel1;
	private System.Windows.Forms.TextBox textBox1;
    }
}
