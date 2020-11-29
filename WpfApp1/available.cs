using SimpleWifi;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WpfApp1
{
    public partial class available : Form
    {
        private static Wifi wifi;
        public available()
        {
            InitializeComponent();
        }

        private void available_Load(object sender, EventArgs e)
        {
            wifi = new Wifi();
            List<AccessPoint> aps = wifi.GetAccessPoints();
            foreach (AccessPoint ap in aps)
            {
                ListViewItem lvItem = new ListViewItem(ap.Name);
                lvItem.SubItems.Add((ap.SignalStrength + "%"));
                lvItem.Tag = ap;

                listView1.Items.Add(lvItem);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(listView1.SelectedItems.Count > 0 )
            {
                ListViewItem selectedItem = listView1.SelectedItems[0];

                string ssid = listView1.SelectedItems[0].Text;
                Trace.WriteLine("SSID 123: ", ssid);

                SqlConnection con = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = D:\OHIO SU\Subjects\Bitcoin and Networking\WpfApp1\WpfApp1\Database_All.mdf; Integrated Security = True;");
                con.Open();

                string qu1 = "SELECT WifiPassword FROM [dbo].[Credentials] WHERE WifiSsid = @un;";
                SqlCommand cmd1 = new SqlCommand(qu1, con);
                cmd1.Parameters.Add("@un", SqlDbType.NVarChar).Value = ssid;
                string password = (string)cmd1.ExecuteScalar();

                Trace.WriteLine("Pass 123: ", password);


                AccessPoint ap = (AccessPoint)selectedItem.Tag;
                if (connectToWifi(ap, password))
                    lbl_status.Text = "You connected Successfully";
                else
                    lbl_status.Text = "Connection Failed";
            }
        }

        private bool connectToWifi(AccessPoint ap, string password)
        {
            AuthRequest authRequest = new AuthRequest(ap);
            authRequest.Password = password;
            return ap.Connect(authRequest);
        }
    }
}
