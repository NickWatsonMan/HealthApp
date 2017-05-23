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
using System.Windows.Shapes;
using System.Data;
using System.Data.SqlClient;

namespace HealthApp
{
    /// <summary>
    /// Interaction logic for SignUp.xaml
    /// </summary>
    public partial class SignUp : Window
    {
        SqlConnection conn;
        public SignUp()
        {
            InitializeComponent();
        }

        private async void dosignup_Click(object sender, RoutedEventArgs e)
        {
            string connectionString = @"Data Source=(LocalDB)\v11.0;AttachDbFilename=|DataDirectory|\Database1.mdf;Integrated Security=True";
            conn = new SqlConnection(connectionString);
     
            await conn.OpenAsync();

            SqlCommand cmd = new SqlCommand("INSERT INTO [User](login, password)VALUES(@login, @password)", conn);
            try
            {
                if (namebox.Text.Length != 0)
                {

                    cmd.Parameters.Add("@login", SqlDbType.NVarChar).Value = namebox.Text;

                    if ((pwdbox1.Password.ToString() == pwdbox2.Password.ToString()) && (pwdbox1.Password.ToString().Length != 0))
                    {

                    
                        cmd.Parameters.AddWithValue("password", pwdbox1.Password.ToString());

                        await cmd.ExecuteNonQueryAsync();
        
                        
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Пароли не совпадают");
                    }
                }
                else
                {
                    MessageBox.Show("Укажите логин");
                }
            }
            catch
            {
                MessageBox.Show("Такой логин уже занят. Укажите другой!");
            }

            conn.Close();
            }
            

            
        }
    }
