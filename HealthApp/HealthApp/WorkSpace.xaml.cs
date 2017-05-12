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
    /// Interaction logic for WorkSpace.xaml
    /// </summary>
    public partial class WorkSpace : Window
    {
        SqlConnection conn;
        User user;

        public WorkSpace(User user)
        {
            InitializeComponent();
            this.user = user;
            MessageBox.Show(user.getName());
            namelable.Content = user.getName();
            getCal();

        }

        private async void getCal()
        {
            string connectionString = @"Data Source=(LocalDB)\v11.0;AttachDbFilename=C:\Users\nick\Source\Repos\HealthApp\HealthApp\HealthApp\Database1.mdf;Integrated Security=True";
            conn = new SqlConnection(connectionString);

            await conn.OpenAsync();

            SqlCommand cmd = new SqlCommand("SELECT * FROM [Food] WHERE user_id = @id ORDER BY Eating_time ASC", conn);

            cmd.Parameters.Add("@id", SqlDbType.Int).Value = user.getId();

            await cmd.ExecuteNonQueryAsync();
            SqlDataReader reader = await cmd.ExecuteReaderAsync();
            int cal = 0;

            if (reader.HasRows)
            {

                while (reader.Read())
                {
                    foodbox.Items.Add(Convert.ToString(reader["Food_title"]) + " " + Convert.ToString(reader["Calories"]) + " " + Convert.ToString(reader["Eating_time"]));
                    cal = Convert.ToInt16(reader["Calories"]) + cal;
                }
            }
            else
            {

            }

            totalcal.Text = Convert.ToString(cal);
        }
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private async void addfood_Click(object sender, RoutedEventArgs e)
        {
            string connectionString = @"Data Source=(LocalDB)\v11.0;AttachDbFilename=C:\Users\nick\Source\Repos\HealthApp\HealthApp\HealthApp\Database1.mdf;Integrated Security=True";
            conn = new SqlConnection(connectionString);

            await conn.OpenAsync();

            SqlCommand cmd = new SqlCommand("INSERT INTO [Food](Food_title, Calories, Eating_time, user_id)VALUES(@title, @cal, @time, @id)", conn);
            try {
            cmd.Parameters.Add("@title", SqlDbType.NVarChar, 50).Value = foodtitle.Text;
            cmd.Parameters.Add("@cal", SqlDbType.Int).Value = Convert.ToInt32(calories.Text);

            if (breakfast.IsChecked == true) {
            cmd.Parameters.Add("@time", SqlDbType.NVarChar, 50).Value = "Завтрак";
            }
            if (lunch.IsChecked == true)
            {
                cmd.Parameters.Add("@time", SqlDbType.NVarChar, 50).Value = "Обед";
            }
            if (dinner.IsChecked == true)
            {
                cmd.Parameters.Add("@time", SqlDbType.NVarChar, 50).Value = "Ужин";
            }

            cmd.Parameters.Add("@id", SqlDbType.Int).Value = user.getId();

            await cmd.ExecuteNonQueryAsync();

            conn.Close();
            foodtitle.Clear();
            calories.Clear();
            breakfast.IsChecked = false;
            lunch.IsChecked = false;
            dinner.IsChecked = false;
            
            }
            catch {
                MessageBox.Show("Некорректные данные ввода");
            }
        
        }

        private async void updatefood_Click(object sender, RoutedEventArgs e)
        {
            foodbox.Items.Clear();
            string connectionString = @"Data Source=(LocalDB)\v11.0;AttachDbFilename=C:\Users\nick\Source\Repos\HealthApp\HealthApp\HealthApp\Database1.mdf;Integrated Security=True";
            conn = new SqlConnection(connectionString);

            await conn.OpenAsync();

            SqlCommand cmd = new SqlCommand("SELECT * FROM [Food] WHERE user_id = @id ORDER BY Eating_time ASC", conn);

            cmd.Parameters.Add("@id", SqlDbType.Int).Value = user.getId();

            await cmd.ExecuteNonQueryAsync();
            SqlDataReader reader = await cmd.ExecuteReaderAsync();
            int cal = 0;

            if (reader.HasRows)
            {

                while (reader.Read())
                {
                    foodbox.Items.Add(Convert.ToString(reader["Food_title"]) + " " + Convert.ToString(reader["Calories"]) + " " + Convert.ToString(reader["Eating_time"]));
                    cal = Convert.ToInt16(reader["Calories"]) + cal;
                }
            }
            else
            {
                
            }

            totalcal.Text = Convert.ToString(cal);
        }

        //public WorkSpace()
        //{
        //    InitializeComponent();
        //}
    }
}
