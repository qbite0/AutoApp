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
using System.Windows.Threading;

namespace AutoApp
{
    /// <summary>
    /// Логика взаимодействия для Drivers.xaml
    /// </summary>
    public partial class Drivers : Window
    {
        DispatcherTimer logout_timer = new DispatcherTimer();

        public Drivers()
        {
            logout_timer.Interval = new TimeSpan(0, 0, 10);
            logout_timer.Tick += Logout;
            InitializeComponent();

        }

        private void Logout(object sender, EventArgs e)
        {

        }

        private void Window_Deactivated(object sender, EventArgs e)
        {
            logout_timer.Start();
        }
    }
}
