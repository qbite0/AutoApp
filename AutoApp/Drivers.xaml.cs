using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Threading;

namespace AutoApp
{
    /// <summary>
    /// Логика взаимодействия для Drivers.xaml
    /// </summary>
    public partial class Drivers : Window
    {
        DispatcherTimer logout_timer = new DispatcherTimer();
        SqlConnection connection;

        public Drivers(SqlConnection conn)
        {
            connection = conn;
            logout_timer.Interval = new TimeSpan(0, 0, 10);
            logout_timer.Tick += Logout;
            InitializeComponent();

        }

        private void Logout(object sender, EventArgs e)
        {
            Owner.Show();
            Hide();
        }

        private void Window_Deactivated(object sender, EventArgs e)
        {
            logout_timer.Start();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM drivers", connection);

            List<Driver> list = new List<Driver>();

            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    var driver = new Driver
                    {
                        Имя = reader.GetString(1),
                        Фамилия = reader.GetString(2),
                        Отчество = reader.GetString(3),
                        СерияПаспорта = reader.GetInt32(4),
                        НомерПаспорта = reader.GetInt32(5),
                        ПочтовыйИндекс = reader.GetInt32(6),
                        Адрес = reader.GetString(7),
                        Компания = reader.GetString(9),
                        Работа = reader.GetString(10),
                        Телефон = reader.GetString(11),
                        Почта = reader.GetString(12),
                        //Фото = reader.GetString(13)
                    };

                    list.Add(driver);
                }
                driversList.ItemsSource = list;
            }
        }
    }
}
