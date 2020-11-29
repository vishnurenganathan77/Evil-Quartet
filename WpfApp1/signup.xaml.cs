using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
    /// Interaction logic for signup.xaml
    /// </summary>
    public partial class signup : Window
    {
        public signup()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = D:\OHIO SU\Subjects\Bitcoin and Networking\WpfApp1\WpfApp1\Database_All.mdf; Integrated Security = True;");
            con.Open();
            string name = textBox1.Text;
            string address = textBox2.Text;
            string latitude = textBox3.Text;
            string longitude = textBox4.Text;
            string username = textBox5.Text;
            string password = textBox6.Text;
            String query = "INSERT INTO [dbo].[UserName] (Name, Address, Latitude, Longitude, Username, Password) VALUES (@na, @add, @lat, @lon, @un, @pass)";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.Add("@na", SqlDbType.VarChar).Value = name;
            cmd.Parameters.Add("@add", SqlDbType.VarChar).Value = address;
            cmd.Parameters.Add("@lat", SqlDbType.VarChar).Value = latitude;
            cmd.Parameters.Add("@lon", SqlDbType.VarChar).Value = longitude;
            cmd.Parameters.Add("@un", SqlDbType.VarChar).Value = username;
            cmd.Parameters.Add("@pass", SqlDbType.VarChar).Value = password;
            
            cmd.ExecuteNonQuery();

            this.Close();
        }
    }
}
