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
        SqlConnection conn1;
        User user;

        public WorkSpace(User user)
        {
            InitializeComponent();
            this.user = user;
            namelable.Content = user.getName();
            getCal();
            getSport();

        }

        private async void getCal()
        {

            foodbox.Items.Clear();
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Database1.mdf;Integrated Security=True";
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
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Database1.mdf;Integrated Security=True";
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
           
            getCal();
            
            }
            catch {
                MessageBox.Show("Некорректные данные ввода");
            }
           
        
        }

        private void updatefood_Click(object sender, RoutedEventArgs e)
        {
            getCal();
        }

        private async void clearfood_Click(object sender, RoutedEventArgs e)
        {
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Database1.mdf;Integrated Security=True";
            conn = new SqlConnection(connectionString);

            await conn.OpenAsync();

            SqlCommand cmd = new SqlCommand("DELETE FROM [Food] WHERE user_id = @id", conn);

            cmd.Parameters.Add("@id", SqlDbType.Int).Value = user.getId();

            await cmd.ExecuteNonQueryAsync();

            conn.Close();
            getCal();
        }

        //Sports part
        private async void addsport_Click(object sender, RoutedEventArgs e)
        {
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Database1.mdf;Integrated Security=True";
            conn = new SqlConnection(connectionString);

            await conn.OpenAsync();

            SqlCommand cmd = new SqlCommand("INSERT INTO [Sport](Sport_title, Repetitions, user_id, sets)VALUES(@title, @reps, @id, @sets)", conn);
            try
            {
                cmd.Parameters.Add("@title", SqlDbType.NVarChar, 50).Value = sportTitle.Text;
                cmd.Parameters.Add("@reps", SqlDbType.Int).Value = Convert.ToInt32(repetitions.Text);
                cmd.Parameters.Add("@sets", SqlDbType.Int).Value = Convert.ToInt32(sets.Text);
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = user.getId();

                await cmd.ExecuteNonQueryAsync();

                conn.Close();

                sportTitle.Clear();
                repetitions.Clear();
                sets.Clear();
                getSport();

            }
            catch
            {
                MessageBox.Show("Некорректные данные ввода");
            }
        }

        private async void getSport()
        {

            sportbox.Items.Clear();
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Database1.mdf;Integrated Security=True";
            conn1 = new SqlConnection(connectionString);

            await conn1.OpenAsync();

            SqlCommand cmd1 = new SqlCommand("SELECT * FROM [Sport] WHERE user_id = @id", conn1);

            cmd1.Parameters.Add("@id", SqlDbType.Int).Value = user.getId();

            await cmd1.ExecuteNonQueryAsync();
            SqlDataReader reader = await cmd1.ExecuteReaderAsync();
            int totalex = 0;

            if (reader.HasRows)
            {

                while (reader.Read())
                {
                    sportbox.Items.Add(Convert.ToString(reader["Sport_title"]) + " повторений: " + Convert.ToString(reader["Repetitions"]) + " подходов: " + Convert.ToString(reader["Sets"]) );
                    totalex = totalex + 1;
                }
            }
            else
            {

            }
            conn1.Close();

            totalsport.Text = Convert.ToString(totalex);
        }

        private async void clearsport_Click(object sender, RoutedEventArgs e)
        {
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Database1.mdf;Integrated Security=True";
            conn = new SqlConnection(connectionString);

            await conn.OpenAsync();

            SqlCommand cmd = new SqlCommand("DELETE FROM [Sport] WHERE user_id = @id", conn);

            cmd.Parameters.Add("@id", SqlDbType.Int).Value = user.getId();

            await cmd.ExecuteNonQueryAsync();

            conn.Close();
            getSport();
        }

        private void updatesport_Click(object sender, RoutedEventArgs e)
        {
            getSport();
        }

        //public WorkSpace()
        //{
        //    InitializeComponent();
        //}
    }
}
