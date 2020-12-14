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
    public partial class About : Form {
      core DashCore = new core();
      Label text = new Label();
        public About(Form Parent) {
            InitializeComponent();
            DashCore.SetWindowProperties(this, 0, 18, Parent.Width+16, Parent.Height+20, "Sub_Icon", 40, 40, 40);

            DashCore.WriteText(this, text, "About", DashCore.DEFAULT_FONT_TYPE, 14, DashCore.DEFAULT_FONT_WEIGHT, 0, 0, true, 0, 50, 40, 40, 40, 255, 255, 255);
        }
    }
}
