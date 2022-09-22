using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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

namespace AutoApp
{
    /// <summary>
    /// Логика взаимодействия для DriversAdd.xaml
    /// </summary>
    public partial class DriversAdd : Window
    {
        SqlConnection connection = new SqlConnection();
        public DriversAdd(SqlConnection conn)
        {
            connection = conn;
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SqlCommand cmd = new SqlCommand("INSERT INTO drivers(" +
                "id," +
                "lastname," +
                "name," +
                "middlename," +
                "jobname," +
                "phone," +
                "email" +
            ")" +
            "VALUES(" +
                "@id," +
                "@lastname," +
                "@name," +
                "@middlename," +
                "@jobname," +
                "@phone," +
                "@email" +
            ")", connection);

            cmd.Parameters.AddWithValue("@id", id.Text);
            cmd.Parameters.AddWithValue("@lastname", lastname.Text);
            cmd.Parameters.AddWithValue("@name", name.Text);
            cmd.Parameters.AddWithValue("@middlename", middlename.Text);
            cmd.Parameters.AddWithValue("@jobname", jobname.Text);
            cmd.Parameters.AddWithValue("@phone", phone.Text);
            cmd.Parameters.AddWithValue("@email", email.Text);

            cmd.ExecuteNonQuery();
            Close();
        }
    }
}
