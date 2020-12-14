
/* (c) All Rights Reserved, Dashies Software Inc. */

// Advanced Options dialog
// Will most likely be improved in the near future.

using System;
using Microsoft.Win32;
using System.Drawing;
using System.Data;
using System.Data.Sql;
using System.Net.NetworkInformation;
using System.Security.Principal;
using System.Text;
using System.Linq;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Resources;
using System.Reflection;
using System.Diagnostics;
using System.Windows.Forms;
using System.Management;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Web;

namespace Dash_Server {
    public partial class Moar_Options : Form {
        Class Get = new Class();

        public Boolean useAdvanced = false, 
               EncryptMethod = false,
               AutoLoad = false,
               AutoStart = false,
               SafeBoot = false,
               CleanScreen = false;

        Boolean[] AdvancedOptions = new bool[5];

        CheckBox autoLoad = new CheckBox(), 
                 autoStart = new CheckBox(), 
                 encryptMethod = new CheckBox(), 
                 safeBoot = new CheckBox(),
                 cleanScreen = new CheckBox();

        Label autoload = new Label(), 
              autostart = new Label(),
              encryptmethod = new Label(),
              safeboot = new Label(),
              cleanscreen = new Label();

        public Moar_Options() {
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.CenterParent;

            this.BackColor = Color.FromArgb(2, 2, 2);
            this.Text = (string)("Dash Server 1.0");

            ResourceManager Resource_Loader = new ResourceManager((Get.Resource_ID), Assembly.GetExecutingAssembly());
            this.Icon = (Icon)Resource_Loader.GetObject("profile");
            Get.SetAnimatedCursor(this, (Byte[])Resource_Loader.GetObject("Cursor"));

            this.Width = 350;
            this.Height = 185;

            this.MaximumSize = new Size(350, 185);
            this.MinimumSize = new Size(350, 185);

            Button Quit = new Button();

            Get.InjectCheckBox(this, autoLoad, 10, 60, 12, 12, 20, 20, 20);
            Get.InjectCheckBox(this, autoStart, 10, 85, 12, 12, 20, 20, 20);
            Get.InjectCheckBox(this, encryptMethod, 10, 110, 12, 12, 20, 20, 20);
            Get.InjectCheckBox(this, safeBoot, 10, 135, 12, 12, 20, 20, 20);
            Get.InjectCheckBox(this, cleanScreen, 10, 160, 12, 12, 20, 20, 20);

            Get.InjectLabel(this, autoload, "auto load new files upon applying.", true, "Cursor", 9, 0, 0, 30, 58, 255, 255, 255, 2, 2, 2);
            Get.InjectLabel(this, autostart, "auto load the webpage upon boot. ", true, "Cursor", 9, 0, 0, 30, 83, 255, 255, 255, 2, 2, 2);
            Get.InjectLabel(this, encryptmethod, "auto encrypt selected files.", true, "Cursor", 9, 0, 0, 30, 108, 255, 255, 255, 2, 2, 2);
            Get.InjectLabel(this, safeboot, "safely boot, I guess?", true, "Cursor", 9, 0, 0, 30, 132, 255, 255, 255, 2, 2, 2);
            Get.InjectLabel(this, cleanscreen, "reset activity log after termination.", true, "Cursor", 9, 0, 0, 30, 156, 255, 255, 255, 2, 2, 2);

            Get.InjectButton(this, Quit, "X", 10, false, this.Width-100, -2, 100, 22, 255, 255, 255, 2, 2, 2, 40, 40, 40);

            // Will be fixed in the next update ^-^

            encryptMethod.Enabled = false;
            autoLoad.Enabled = false;
            
            if((encryptMethod.Enabled == false) && (autoLoad.Enabled == false)) {
                //encryptMethod.BackColor = Color.FromArgb();
                //autoLoad.BackColor = Color.FromArgb();
            }

            //

            if(AutoLoad == true) autoLoad.CheckState = CheckState.Checked;
            if(AutoStart == true) autoStart.CheckState = CheckState.Checked;
            if(EncryptMethod == true) encryptMethod.CheckState = CheckState.Checked;
            if(SafeBoot == true) safeBoot.CheckState = CheckState.Checked;
            if(CleanScreen == true) cleanScreen.CheckState = CheckState.Checked;

            autoLoad.CheckedChanged += (sender, argumentation) => {
                if (AutoLoad != true) AutoLoad = true;
                else AutoLoad = false;
            };

            safeBoot.CheckedChanged += (sender, argumentation) => {
                if (SafeBoot != true) SafeBoot = true;
                else SafeBoot = false;
            };

            autoStart.CheckedChanged += (sender, argumentation) => {
                if (AutoStart != true) AutoStart = true;
                else AutoStart = false;
            };

            encryptMethod.CheckedChanged += (sender, argumentation) => {
                if (EncryptMethod != true) EncryptMethod = true;
                else EncryptMethod = false;
            };

            cleanScreen.CheckedChanged += (sender, argumentation) => {
                if (CleanScreen != true) CleanScreen = true;
                else CleanScreen = false;
            };

            Quit.Click += (sender, argumentation) => {
                this.Hide();
            };
        }
    }
}
