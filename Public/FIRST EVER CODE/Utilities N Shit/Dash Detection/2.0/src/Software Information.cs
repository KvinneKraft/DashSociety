using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace src {
    public partial class Software_Information : Form {
      core DashCore = new core();
      Label text = new Label();

      ListView software_information = new ListView();

        public Software_Information(Form Parent) {
            InitializeComponent();
            DashCore.SetWindowProperties(this, 0, 18, Parent.Width+16, Parent.Height+20, "Sub_Icon", 40, 40, 40);

            software_information.View = View.Details;
            software_information.GridLines = true;
            software_information.FullRowSelect = true;

            software_information.Columns.Add("Type", 172);
            software_information.Columns.Add("Output", this.Width-30);

            software_information.Width = this.Width;
            software_information.Height = this.Height;
            software_information.Font = new Font(DashCore.DEFAULT_FONT_TYPE, 8);
            software_information.ForeColor = Color.FromArgb(198, 220, 255);
            software_information.BackColor = Color.FromArgb(0, 28, 73);
            software_information.BorderStyle = BorderStyle.None;

            this.Controls.Add(software_information);

            DashCore.AddListItem(software_information, "Windows Edition", DashCore.GetWindowsEdition());
            DashCore.AddListItem(software_information, "Machine\'s Description", DashCore.GetWindowsComputerDescription());
            DashCore.AddListItem(software_information, "Machine\'s Name", DashCore.GetNames(2));
            DashCore.AddListItem(software_information, "Currently Logged In User", DashCore.GetNames(1));

            DashCore.WriteText(this, text, "Software Information", DashCore.DEFAULT_FONT_TYPE, 14, DashCore.DEFAULT_FONT_WEIGHT, 0, 0, true, 0, 50, 40, 40, 40, 255, 255, 255);
        }
    }
}
