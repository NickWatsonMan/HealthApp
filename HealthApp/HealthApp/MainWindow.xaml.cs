using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data;
using System.Data.SqlClient;

namespace HealthApp
{
  
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        User user;
        SqlConnection conn;
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void startwin_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        private async void signup_Click(object sender, RoutedEventArgs e)
        {
            SignUp sgn = new SignUp();
            sgn.Show();

        }

        private async void enter_Click(object sender, RoutedEventArgs e)
        {
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Database1.mdf;Integrated Security=True";
            conn = new SqlConnection(connectionString);

            await conn.OpenAsync();

            SqlCommand cmd = new SqlCommand("SELECT id FROM [User] WHERE login = @login AND password = @password", conn);

            cmd.Parameters.Add("@login", SqlDbType.VarChar, 50).Value = loginbox.Text;
            cmd.Parameters.Add("@password", SqlDbType.VarChar, 50).Value = passwordbox.Password.ToString();
            await cmd.ExecuteNonQueryAsync();
            SqlDataReader reader = await cmd.ExecuteReaderAsync();

            if (reader.HasRows)
            {

                while (reader.Read())
                {
                    int id = Convert.ToInt16(reader["Id"]);
                    user = new User(loginbox.Text, id);
                    WorkSpace ws = new WorkSpace(user);
                    ws.Show();
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("Вы ввели не верные данные!");
            }
         

            conn.Close();
        }
    }
}
