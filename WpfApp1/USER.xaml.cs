using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for USER.xaml
    /// </summary>
    /// 
    public partial class USER : Window
    {
        string username;

        public USER(string loginname)
        {
            string ssid, pass, speed;
            InitializeComponent();
            textBox.Text = "Hello " + loginname ;
            username = loginname;
            textBox1.Text = username;
            SqlConnection con = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = D:\OHIO SU\Subjects\Bitcoin and Networking\WpfApp1\WpfApp1\Database_All.mdf; Integrated Security = True;");
            con.Open();

            string qu = "SELECT WifiSsid FROM [dbo].[Credentials] WHERE Username = @un;";
            SqlCommand cmd = new SqlCommand(qu, con);
            cmd.Parameters.Add("@un", SqlDbType.NVarChar).Value = username;

            string qu1 = "SELECT WifiPassword FROM [dbo].[Credentials] WHERE Username = @un;";
            SqlCommand cmd1 = new SqlCommand(qu1, con);
            cmd1.Parameters.Add("@un", SqlDbType.NVarChar).Value = username;

            string qu2 = "SELECT WifiSpeed FROM [dbo].[Credentials] WHERE Username = @un;";
            SqlCommand cmd2 = new SqlCommand(qu2, con);
            cmd2.Parameters.Add("@un", SqlDbType.NVarChar).Value = username;

            ssid = (string)cmd.ExecuteScalar();
            pass = (string)cmd1.ExecuteScalar();
            speed = (string)cmd2.ExecuteScalar();

            textBox2.Text = ssid;
            textBox3.Text = pass;
            textBox4.Text = speed;
            Trace.WriteLine(ssid);
            Trace.WriteLine(pass);
            Trace.WriteLine(speed);

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            //textBox.Text = username;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = D:\OHIO SU\Subjects\Bitcoin and Networking\WpfApp1\WpfApp1\Database_All.mdf; Integrated Security = True;");
            con.Open();
            Trace.WriteLine(username);
            string ssid = textBox2.Text;
            string password = textBox3.Text;
            string speed = textBox4.Text;
            var hasher = new SHA256Managed();
            var unhashed = System.Text.Encoding.Unicode.GetBytes(password);
            var hashed = hasher.ComputeHash(unhashed);
            var hashedPassword = Convert.ToBase64String(hashed);
            String query = "INSERT INTO [dbo].[Credentials] (Username, WifiSsid, WifiPassword, WifiSpeed) VALUES (@un, @wsid, @wpass, @wspeed)";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.Add("@wsid", SqlDbType.NVarChar).Value = ssid;
            cmd.Parameters.Add("@wpass", SqlDbType.NVarChar).Value = password; //hashedPassword; -> Use this for hashing the password (remove password).
            cmd.Parameters.Add("@wspeed", SqlDbType.NVarChar).Value = speed;
            cmd.Parameters.Add("@un", SqlDbType.NVarChar).Value = username;
            cmd.ExecuteNonQuery();
        }

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            available win2 = new available();
            win2.Show();
        }

        private void Button_Copy_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = D:\OHIO SU\Subjects\Bitcoin and Networking\WpfApp1\WpfApp1\Database_All.mdf; Integrated Security = True;");
            con.Open();
            string ssid = textBox2.Text;
            string password = textBox3.Text;
            string speed = textBox4.Text;
            String query = "UPDATE [dbo].[Credentials] SET WifiSsid=@wsid, WifiPassword=@wpass, WifiSpeed=@wspeed WHERE Username=@un;";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.Add("@wsid", SqlDbType.NVarChar).Value = ssid;
            cmd.Parameters.Add("@wpass", SqlDbType.NVarChar).Value = password;
            cmd.Parameters.Add("@wspeed", SqlDbType.NVarChar).Value = speed;
            cmd.Parameters.Add("@un", SqlDbType.NVarChar).Value = username;
            cmd.ExecuteNonQuery();
        }
    }
}
