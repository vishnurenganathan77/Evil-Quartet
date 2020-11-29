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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            signup win2 = new signup();
            win2.Show();
        }

        private void Button1_Copy_Click(object sender, RoutedEventArgs e)
        {
            signup win2 = new signup();
            win2.Show();
        }

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            string username = textBox.Text;
            string password = textBox_Copy.Text;
            SqlConnection con = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = D:\OHIO SU\Subjects\Bitcoin and Networking\WpfApp1\WpfApp1\Database_All.mdf; Integrated Security = True;");
            con.Open();
            string qu = "SELECT Password FROM [dbo].[UserName] WHERE Username = @un;";
            SqlCommand cmd = new SqlCommand(qu, con);
            cmd.Parameters.Add("@un", SqlDbType.NVarChar).Value = username;
            string pass = (string)cmd.ExecuteScalar();
            Trace.WriteLine("Password: ", password);
            Trace.WriteLine("Pass: ", pass);
            if (pass == password)
            {
                USER win2 = new USER(username);
                win2.Show();
            }
            else
            {
                MessageBox.Show("Incorrect Password or Username");
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void TextBox_Copy_TextChanged(object sender, TextChangedEventArgs e)
        {
            
        }
    }
}
