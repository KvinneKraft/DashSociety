

// Author: Dashie
// Version: 1.0


using System.Runtime.InteropServices;
using System.Net.NetworkInformation;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Drawing.Text;
using System.Management;
using System.Threading;
using System.Drawing;
using System.Linq;
using System;


namespace DNSChanger
{
    public class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Interface());
        }
    };


    public partial class Interface : Form
    {
        private readonly static Moon mon = new Moon();

        public class init
        {
            public static Size client_size = new Size(320, 135);

            private readonly PictureBox menu_bar = new PictureBox();

            private readonly Button quit = new Button();

            private readonly PictureBox logo = new PictureBox();

            private readonly Label title = new Label();

            public void layout(Form obj)
            {
                obj.FormBorderStyle = FormBorderStyle.None;
                obj.StartPosition = FormStartPosition.CenterScreen;

                obj.Size = client_size;
                obj.MinimumSize = client_size;
                obj.MaximumSize = client_size;

                obj.MinimizeBox = false;
                obj.MaximizeBox = false;

                obj.BackColor = Color.FromArgb(22, 22, 22);
                obj.Text = ("Dash DNS Spoofer 3.0");

                obj.Icon = Properties.Resources.icon;

                obj.Paint += (s, e) =>
                {
                    mon.paint_border(e, Color.FromArgb(10, 10, 10), 2, client_size, Point.Empty);
                };

                mon.Image(obj, menu_bar, null, new Size(obj.Width - 2, 26), new Point(1, 0));
                mon.Image(menu_bar, logo, Properties.Resources.icon1, new Size(54, 26), new Point(1, 0));

                mon.drag_material(menu_bar, obj);
                mon.drag_material(title, obj);
                mon.drag_material(logo, obj);

                mon.Button(menu_bar, quit, "X", 11, new Size(55, 24), new Point(menu_bar.Width - 55, 1), menu_bar.BackColor, Color.FromArgb(255, 255, 255), 0);

                quit.MouseEnter += (s, e) => quit.BackColor = Color.FromArgb(16, 16, 16);
                quit.MouseLeave += (s, e) => quit.BackColor = menu_bar.BackColor;
                quit.Click += (s, e) => Environment.Exit(-1);

                mon.Label(menu_bar, title, "Dash DNS Spoofer 3.0", 9, new Size(0, 0), Point.Empty, Color.FromArgb(255, 255, 255));

                title.Size = title.PreferredSize;
                title.MinimumSize = title.PreferredSize;
                title.MaximumSize = title.PreferredSize;

                title.Location = new Point((menu_bar.Width - title.Width) / 2, (menu_bar.Height - title.Height) / 2);

                menu_bar.BackColor = Color.FromArgb(10, 10, 10);
            }

            private readonly Label water_mark = new Label();

            private readonly Label ip1t = new Label();
            private readonly Label ip2t = new Label();

            private readonly TextBox ip1i = new TextBox();
            private readonly TextBox ip2i = new TextBox();

            private readonly Button spoof = new Button();
            private readonly Button tools = new Button();

            private readonly PictureBox container = new PictureBox();

            public void controls(Form obj)
            {
                mon.Image(obj, container, null, new Size(300, 88), new Point((obj.Width - 300) / 2, 36));

                container.BackColor = Color.FromArgb(16, 16, 16);

                container.Paint += (s, e) =>
                {
                    mon.paint_border(e, Color.FromArgb(8, 8, 8), 2, ip1i.Size, ip1i.Location);
                    mon.paint_border(e, Color.FromArgb(8, 8, 8), 2, ip2i.Size, ip2i.Location);
                };

                obj.Paint += (s, e) => 
                { 
                    mon.paint_border(e, Color.FromArgb(8, 8, 8), 2, container.Size, container.Location);
                };

                mon.Label(container, ip1t, "IPv4 (1):", 11, new Size(0, 0), Point.Empty, Color.FromArgb(255, 255, 255));

                ip1t.Size = ip1t.PreferredSize;
                ip1t.MinimumSize = ip1t.PreferredSize;
                ip1t.MaximumSize = ip1t.PreferredSize;

                ip1t.Location = new Point(8, 8);

                mon.TextBox(container, ip1i, "1.1.1.1", 12, new Size(120, 20), new Point(ip1t.Left + ip1t.Width + 5, ip1t.Top + 1), Color.FromArgb(22, 22, 22), Color.FromArgb(200, 200, 200));

                ip1i.TextAlign = HorizontalAlignment.Center;

                mon.Label(container, ip2t, "IPv4 (2):", 11, new Size(0, 0), Point.Empty, Color.FromArgb(255, 255, 255));

                ip2t.Size = ip2t.PreferredSize;
                ip2t.MinimumSize = ip2t.PreferredSize;
                ip2t.MaximumSize = ip2t.PreferredSize;

                ip2t.Location = new Point(8, ip1t.Height + ip1t.Top + 12);

                mon.TextBox(container, ip2i, "1.0.0.1", 12, ip1i.Size, new Point(ip2t.Left + ip2t.Width + 5, ip2t.Top + 1), ip1i.BackColor, ip1i.ForeColor);

                ip2i.TextAlign = HorizontalAlignment.Center;

                mon.Button(container, spoof, "Spoof It", 9, new Size(90, 24), new Point(ip1i.Left + ip1i.Width + 7, ip1i.Top - 2), Color.FromArgb(40, 43, 69), Color.FromArgb(255, 255, 255), 8);

                spoof.TextAlign = ContentAlignment.MiddleCenter;

                spoof.MouseLeave += (s, e) => spoof.BackColor = Color.FromArgb(40, 43, 69);
                spoof.MouseEnter += (s, e) => spoof.BackColor = Color.FromArgb(50, 54, 87);

                mon.Button(container, tools, "Dash Tools", 9, spoof.Size, new Point(ip2i.Left + ip2i.Width + 7, ip2i.Top - 2), spoof.BackColor, spoof.ForeColor, 8);

                tools.TextAlign = ContentAlignment.MiddleCenter;

                tools.MouseLeave += (s, e) => tools.BackColor = Color.FromArgb(40, 43, 69);
                tools.MouseEnter += (s, e) => tools.BackColor = Color.FromArgb(50, 54, 87);

                mon.Label(container, water_mark, ("Dashies ☽⛤☾ Softwaries"), 6, new Size(0, 0), Point.Empty, Color.FromArgb(255, 255, 255));

                water_mark.Size = water_mark.PreferredSize;
                water_mark.MinimumSize = water_mark.PreferredSize;
                water_mark.MaximumSize = water_mark.PreferredSize;

                water_mark.Location = new Point(((container.Width - water_mark.Width) / 2), container.Height - water_mark.Height - 6);
            }

            public static class dns
            {
                public static NetworkInterface GetAdapter()
                {
                    var Nic = NetworkInterface.GetAllNetworkInterfaces().FirstOrDefault
                    (
                        a => a.OperationalStatus == OperationalStatus.Up && 
                            (
                                a.NetworkInterfaceType == NetworkInterfaceType.Wireless80211 ||
                                a.NetworkInterfaceType == NetworkInterfaceType.Ethernet
                            ) 
                            
                        && a.GetIPProperties().GatewayAddresses.Any(g => g.Address.AddressFamily.ToString().ToLower() == "internetwork")
                    );

                    return Nic;
                }

                public static bool ChangeDNS(string ip1, string ip2)
                {
                    ManagementClass MC = new ManagementClass("Win32_NetworkAdapterConfiguration");
                    ManagementObjectCollection MOC = MC.GetInstances();

                    if(MOC.Count < 1)
                    {
                        return false;
                    };

                    foreach ( ManagementObject obj in MOC )
                    {
                        if ( ( bool ) obj["IPEnabled"] )
                        {
                            ManagementBaseObject dns_obj = obj.GetMethodParameters("SetDNSServerSearchOrder");

                            if ( dns_obj != null )
                            {
                                dns_obj["DNSServerSearchOrder"] = new string[] { ip1, ip2 };
                                obj.InvokeMethod("SetDNSServerSearchOrder", dns_obj, null);
                            }

                            else
                            {
                                return false;
                            };
                        };
                    };

                    return true;
                }
            };

            public bool isIP(String ip)
            {
                try
                {
                    System.Net.IPAddress.Parse(ip);
                    return true;
                }

                catch
                {
                    return false;
                };
            }

            ToolsDialog tools_dialog = new ToolsDialog();

            public void events()
            {
                spoof.Click += (s, e) =>
                {
                    if (spoof.Text.ToLower() != "Working ....")
                    {
                        if ((!isIP(ip1i.Text) || !isIP(ip2i.Text)) || (ip1i.Text.Length > 15 || ip2i.Text.Length > 15) || (ip1i.Text.Length < 7 && ip2i.Text.Length < 7))
                        {
                            MessageBox.Show("It appears that you have given us an invalid IP Address in either the IPv4 1 input box or the IPv4 2 input box. \r\n\r\nPlease recheck your input and retry, if this problem persists, contact us at KvinneKraft@protonmail.com", "DNS Spoofer", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        };

                        new Thread(() =>
                        {
                            spoof.Text = "Working ....";

                            if (dns.ChangeDNS(ip1i.Text, ip2i.Text))
                            {
                                MessageBox.Show("Your DNS Servers have been changed, you may have to restart your system if none of the changes were applied yet.", "Dash DNS Spoofer", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }

                            else
                            {
                                MessageBox.Show("There was an issue while attempting to change your DNS servers.\r\n\r\nPlease retry, if the issue persists, perhaps try to contact us at KvinneKraft@protonmail.com", "Dash DNS Spoofer", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            };

                            spoof.Text = "Spoof It";
                        })

                        { IsBackground = false }.Start();

                        return;
                    };
                };

                tools.Click += (s, e) =>
                {
                    if(!tools_dialog.Visible)
                    {

                    };
                };
            }
        };

	public Interface()
	{
            if(!mon.isAdministrator())
            {
                MessageBox.Show($"It has got to me that I do not have the rights to access your network devices. \r\n\r\nPlease restart this application as an administrator, it may just solve ye issue {Environment.UserName}!", "Dash DNS Spoofer", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(-1);
            };

            init inity = new init();

            inity.layout(this);
            inity.controls(this);
            inity.events();
	}
    }


    public class ToolsDialog : Form
    {
        public ToolsDialog()
        {

        }
    };


    public class Moon : Form
    {
        public bool isAdministrator()
        {
            using (System.Security.Principal.WindowsIdentity identity = System.Security.Principal.WindowsIdentity.GetCurrent())
            {
                System.Security.Principal.WindowsPrincipal principal = new System.Security.Principal.WindowsPrincipal(identity);

                if(!principal.IsInRole(System.Security.Principal.WindowsBuiltInRole.Administrator))
                {
                    return false;
                };
            };

            return true;
        }

        public void Image(Control obj, PictureBox img, Image image, Size size, Point location)
        {
            img.Size = size;
            img.MinimumSize = size;
            img.MaximumSize = size;

            img.BorderStyle = BorderStyle.None;
            img.BackColor = Color.FromArgb(0, 0, 0, 255);

            if (location.X < 0)
            {
                location.X = (obj.Width - size.Width) / 2;
            };

            if (location.Y < 0)
            {
                location.Y = (obj.Height - size.Height) / 2;
            };

            img.Location = location;

            if (image != null) img.Image = image;

            obj.Controls.Add(img);
        }

        public void TextBox(Control obj, TextBox box, string text, int points, Size size, Point location, Color bgcolor, Color frcolor)
        {
            box.Size = size;
            box.MinimumSize = size;
            box.MaximumSize = size;

            if (location.X < 0)
            {
                location.X = (obj.Width - size.Width) / 2;
            };

            if (location.Y < 0)
            {
                location.Y = (obj.Height - size.Height) / 2;
            };

            box.Location = location;

            box.Font = FlubbyFont(points, false); //new Font("Modern", points);
            box.Text = text;

            box.BackColor = bgcolor;
            box.ForeColor = frcolor;

            box.BorderStyle = BorderStyle.None;
            box.ReadOnly = false;

            obj.Controls.Add(box);
        }

        public void Label(Control obj, Label label, string text, int points, Size size, Point location, Color color)
        {
            label.Size = size;
            label.MinimumSize = size;
            label.MaximumSize = size;

            if (location.X < 0)
            {
                location.X = (obj.Width - size.Width) / 2;
            };

            if (location.Y < 0)
            {
                location.Y = (obj.Height - size.Height) / 2;
            };

            label.Location = location;

            label.Font = FlubbyFont(points, false); //new Font("Modern", points);
            label.Text = text;

            label.BackColor = Color.FromArgb(0, 0, 0, 255);
            label.ForeColor = color;

            obj.Controls.Add(label);
        }

        public void Button(Control obj, Button button, string text, int points, Size size, Point location, Color bgcolor, Color frcolor, int border_radius)
        {
            button.Size = size;
            button.MinimumSize = size;
            button.MaximumSize = size;

            if (location.X < 0)
            {
                location.X = (obj.Width - size.Width) / 2;
            };

            if (location.Y < 0)
            {
                location.Y = (obj.Height - size.Height) / 2;
            };

            button.Location = location;

            button.Font = FlubbyFont(points, false); //new Font("Modern", points);
            button.Text = text;

            button.BackColor = bgcolor;
            button.ForeColor = frcolor;

            button.FlatAppearance.BorderColor = Color.FromArgb(0, 0, 0, 255);
            button.FlatAppearance.BorderSize = 0;

            button.TextAlign = ContentAlignment.MiddleCenter;
            button.FlatStyle = FlatStyle.Flat;

            if (border_radius > 0)
            {
                button.Paint += (s, e) =>
                {
                    try
                    {
                        e.Graphics.SmoothingMode = SmoothingMode.HighQuality;

                        base.OnPaint(e);

                        Rectangle rectum = new Rectangle(0, 0, button.Width, button.Height);

                        GraphicsPath graphics_path = new GraphicsPath();

                        int radius = (border_radius) * 2;

                        graphics_path.AddArc(rectum.X, rectum.Y, radius, radius, 170, 90);
                        graphics_path.AddArc((rectum.X + rectum.Width - radius), rectum.Y, radius, radius, 270, 90);
                        graphics_path.AddArc((rectum.X + rectum.Width - radius), (rectum.Y + rectum.Height - radius), radius, radius, 0, 90);
                        graphics_path.AddArc(rectum.X, (rectum.Y + rectum.Height - radius), radius, radius, 90, 90);

                        Region reg = new Region(graphics_path);
                        button.Region = reg;
                    }

                    catch { };
                };
            };

            obj.Controls.Add(button);
        }

        public void drag_material(Control t, Control d)
        {
            Point point = Point.Empty;

            t.MouseDown += (s, e) =>
                point = new Point(e.X, e.Y);

            t.MouseUp += (s, e) =>
                point = Point.Empty;

            t.MouseMove += (s, e) =>
            {
                if (point.IsEmpty)
                {
                    return;
                };

                d.Location = new Point(d.Location.X + (e.X - point.X), d.Location.Y + (e.Y - point.Y));
            };
        }

        public void paint_border(PaintEventArgs e, Color color, float width, Size size, Point point)
        {
            Graphics graphics = e.Graphics;

            using (Pen pen = new Pen(color, width))
            {
                graphics.DrawRectangle(pen, new Rectangle(point, size));
            };
        }

        private List<ContainerControl> containers = new List<ContainerControl>();

        public void setup_container()
        {
            Color container_color = Color.FromArgb(16, 16, 16);
            Point container_point = new Point(5, 5);

            Size container_size = new Size(Interface.ActiveForm.Width - 10, Interface.ActiveForm.Height - 10);

            containers.Add(
                new ContainerControl()
                {
                    Location = container_point,
                    Size = container_size,
                    BackColor = container_color,
                }
            );

            int container_key = containers.Count - 1;

            containers[container_key].Paint += (s, e) =>
                paint_border(e, Color.FromArgb(1, 1, 1), 2, containers[container_key].Size, Point.Empty);

            Interface.
                ActiveForm.
                    Controls.
                        Add(containers[container_key]);
        }

        public void border_control(Control ctrl)
        {
            ctrl.Paint += (s, e) =>
            {
                GraphicsPath graphics_path = new GraphicsPath();
                Rectangle rectangle = ctrl.ClientRectangle;

                rectangle.Inflate(10, 8); // 10 - 8

                e.Graphics.DrawEllipse(Pens.Transparent, rectangle);

                rectangle.Inflate(-1, -1);
                graphics_path.AddEllipse(rectangle);

                ctrl.Region = new Region(graphics_path);
            };
        }

        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        private static extern IntPtr AddFontMemResourceEx(IntPtr pbFont, uint cbFont, IntPtr pdv, [System.Runtime.InteropServices.In] ref uint pcFonts);

        private static readonly byte[] font_data = (byte[])Properties.Resources.font;

        private static PrivateFontCollection PermFontCollection = new PrivateFontCollection();

        public static void InitFont()
        {
            IntPtr font_ptr = Marshal.AllocCoTaskMem(font_data.Length);
            Marshal.Copy(font_data, 0, font_ptr, font_data.Length);
            uint fluff = 0;
            AddFontMemResourceEx(font_ptr, (uint)font_data.Length, IntPtr.Zero, ref fluff);
            PermFontCollection.AddMemoryFont(font_ptr, font_data.Length);
        }

        public static Font FlubbyFont(int pt, bool bold)
        {
            if (PermFontCollection.Families.Length < 1)
                InitFont();

            FontStyle fontStyle = FontStyle.Bold;

            if (!bold)
                fontStyle = FontStyle.Regular;

            return new Font(PermFontCollection.Families[0], pt, fontStyle);
        }
    };
}
