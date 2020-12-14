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
    public partial class Hardware_Information : Form {
      core DashCore = new core();
      Label text = new Label();
      
      String Content = String.Empty;
      ListView hardware_information = new ListView();

        public Hardware_Information(Form Parent) {
            InitializeComponent();
            DashCore.SetWindowProperties(this, 0, 18, Parent.Width+16, Parent.Height+20, "Sub_Icon", 40, 40, 40);

            hardware_information.View = View.Details;
            hardware_information.GridLines = true;
            hardware_information.FullRowSelect = true;

            hardware_information.Columns.Add("Type", 172);
            hardware_information.Columns.Add("Output", this.Width-30);

            hardware_information.Width = this.Width;
            hardware_information.Height = this.Height;
            hardware_information.Font = new Font(DashCore.DEFAULT_FONT_TYPE, 8);
            hardware_information.ForeColor = Color.FromArgb(198, 220, 255);
            hardware_information.BackColor = Color.FromArgb(0, 28, 73);
            hardware_information.BorderStyle = BorderStyle.None;

            this.Controls.Add(hardware_information);

            Task GetRAM = Task.Run(() => DashCore.AddListItem(hardware_information, "Random Access Memory Amount", (Convert.ToInt32(DashCore.GetRAM(1))/1000).ToString() + " GB / " + DashCore.GetRAM(1) + " MB / " + (Convert.ToInt32(DashCore.GetRAM(1))*1024).ToString() + " KB"));
            //DashCore.AddListItem(hardware_information, "Random Access Memory Type", DashCore.GetRAM(3));
            Task GetGPU = Task.Run(() => DashCore.AddListItem(hardware_information, "Graphics Processing Unit", DashCore.GetSystemGPU()));
            Task GetCPU = Task.Run(() => DashCore.AddListItem(hardware_information, "Central Processing Unit", DashCore.GetSystemCPU()));      
        }
    }
}
