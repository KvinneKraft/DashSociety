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
    public partial class Network_Information : Form {
      core DashCore = new core();
      Label text = new Label();

      ListView network_information = new ListView();

        public Network_Information(Form Parent) {
            InitializeComponent();
            DashCore.SetWindowProperties(this, 0, 18, Parent.Width+16, Parent.Height+20, "Sub_Icon", 40, 40, 40);

            network_information.View = View.Details;
            network_information.GridLines = true;
            network_information.FullRowSelect = true;

            network_information.Columns.Add("Type", 172);
            network_information.Columns.Add("Output", this.Width-30);

            network_information.Width = this.Width;
            network_information.Height = this.Height;
            network_information.Font = new Font(DashCore.DEFAULT_FONT_TYPE, 8);
            network_information.ForeColor = Color.FromArgb(198, 220, 255);
            network_information.BackColor = Color.FromArgb(0, 28, 73);
            network_information.BorderStyle = BorderStyle.None;
            
            this.Controls.Add(network_information);

            DashCore.AddListItem(network_information, "Private IPv4 Address", DashCore.GetMachineIP());
            //Make dis work
            //DashCore.AddListItem(network_information, "Public IPv4 Address", DashCore.GetNetworkIP());

            Task GetMACId = Task.Run(() => DashCore.AddListItem(network_information, "Private Mac Address", DashCore.GetMacID()));

            DashCore.WriteText(this, text, "Network Information", DashCore.DEFAULT_FONT_TYPE, 14, DashCore.DEFAULT_FONT_WEIGHT, 0, 0, true, 0, 50, 40, 40, 40, 255, 255, 255);
        }
    }
}
