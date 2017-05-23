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
            string connectionString = @"Data Source=(LocalDB)\v11.0;AttachDbFilename=C:\Users\nick\HealthApp\HealthApp\HealthApp\Database1.mdf;Integrated Security=True";
            conn = new SqlConnection(connectionString);

            await conn.OpenAsync();

            SqlCommand cmd = new SqlCommand("SELECT * FROM [User]", conn);
         
          
         //   cmd.Parameters.AddWithValue("login", loginbox.Text);
         //   cmd.Parameters.AddWithValue("password", passwordbox.Password.ToString());
          
            await cmd.ExecuteNonQueryAsync();
            SqlDataReader reader = await cmd.ExecuteReaderAsync();


            
                while (reader.Read())
                {
                    Console.Write(Convert.ToString(reader.GetSqlString(2)).TrimEnd() + " = " + Convert.ToString(passwordbox.Password.ToString()) + "; ");

                    if ((Convert.ToString(loginbox.Text.TrimEnd()) == Convert.ToString(reader.GetSqlString(1)).TrimEnd()) && ((Convert.ToString(passwordbox.Password.ToString()) == Convert.ToString(reader.GetSqlString(2)).TrimEnd())))
                    {
                        int id = Convert.ToInt32(reader["Id"]);
                        user = new User(loginbox.Text, id);
                        WorkSpace ws = new WorkSpace(user);
                        ws.Show();
                      
                        this.Close();
                    }

                }

                reader.Close();
         

            conn.Close();
        }
    }
}
