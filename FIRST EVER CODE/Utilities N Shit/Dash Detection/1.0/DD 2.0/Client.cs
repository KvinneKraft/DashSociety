
/* (c) All Rights Reserved, Dashies Software Inc. */



/* 
 
this project has been created on 13-11-2018 at 8:59 in the morning.

--------------
--------------
--------------

this application will allow you to determine specific information about 
either YOU or your beautiful System Setup!

Hardware and Software related detection technologies are implemented in
order for us to give you the best experience possible.

(c) Dashies Software 2018
 
*/



using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Resources;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Windows.Forms;



namespace DD_2._0 {
    public partial class Client : Form {
        private Dash_Library get = new Dash_Library();
        
        struct Debug {
            public Boolean status;
        };

        Debug debug;

        public Client() {
            InitializeComponent();

            ResourceManager getResource = new ResourceManager((get.resourceId), Assembly.GetExecutingAssembly());

            this.Icon = (Icon)getResource.GetObject("Main");
            this.FormBorderStyle = FormBorderStyle.None;

            debug.status = get.CreateMenu(this, 0, 0, this.Width, 24, (""), 24, 24, 1, 8, 8, 8);
            if(debug.status != true) { /* Error Bullshit here */ }

        }
    }
}
