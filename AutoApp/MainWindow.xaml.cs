using System;
//using MySql.Data.MySqlClient;
using System.Data.SqlClient;
using System.IO;
using System.Windows;
using System.Windows.Navigation;
using System.Windows.Threading;

namespace AutoApp
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private SqlConnection connection = new SqlConnection(@"server=192.168.2.4\SQLEXPRESS; database=user1_db; user=user1; password=intel");

        private NavigationService navigationService;

        private DispatcherTimer blockingTimer = new DispatcherTimer();
        private int loginCount = 0;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void ResetBlcok(object sender, EventArgs e)
        {
            loginCount = 0;
            if (File.Exists("block")) File.Delete("block");
            blockingTimer.Stop();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            if (loginCount >= 3)
            {
                MessageBox.Show("Превышено кол-во попыток");
                return;
            }
            SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM users WHERE (username = @username AND password = @password)", connection);

            cmd.Parameters.AddWithValue("@username", username.Text);
            cmd.Parameters.AddWithValue("@password", password.Text);

            int count = (int)cmd.ExecuteScalar();
            if (count == 1)
            {
                Drivers drivers = new Drivers();
                drivers.Owner = this;
                Hide();
                drivers.Show();
            }
            else
            {
                loginCount++;
                if (loginCount >= 3)
                {
                    File.Create("block");
                    blockingTimer.Start();
                }
                MessageBox.Show("Неправильный пароль");
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            navigationService = NavigationService.GetNavigationService(this);

            blockingTimer.Interval = new TimeSpan(0, 1, 0);
            blockingTimer.Tick += ResetBlcok;

            if (File.Exists("block"))
            {
                loginCount = 3;
                blockingTimer.Start();
            }

            try
            {
                connection.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
