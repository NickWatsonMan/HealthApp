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

namespace HealthApp
{
    /// <summary>
    /// Interaction logic for WorkSpace.xaml
    /// </summary>
    public partial class WorkSpace : Window
    {

        User user;

        public WorkSpace(User user)
        {
            InitializeComponent();
            this.user = user;
            MessageBox.Show(user.getName());
            namelable.Content = user.getName();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void addfood_Click(object sender, RoutedEventArgs e)
        {

        }

        //public WorkSpace()
        //{
        //    InitializeComponent();
        //}
    }
}
