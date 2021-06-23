using Npgsql;
using System;
using System.Windows;

namespace Medialib_proj
{
	/// <summary>
	/// Логика взаимодействия для Logwin.xaml
	/// </summary>
	public partial class Logwin : Window
    {
		public NpgsqlConnection conn;
		public NpgsqlCommand comm;

		public Logwin()
        {
            InitializeComponent();
        }

		private void B_sigup_Click(object sender, RoutedEventArgs e)
		{
			SignUpWindow signUp = new SignUpWindow();
			signUp.ShowDialog();
		}

		private void B_sigin_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				string conn_param = "Server=127.0.0.1;Port=5432;User Id=medialib_login;Password=0000;Database=medialib";
				string sql = "select exists(select id from lib_user where login = '" + tb_login.Text + "')";
				conn = new NpgsqlConnection(conn_param);
				comm = new NpgsqlCommand(sql, conn);
				conn.Open();
				bool result = (bool)comm.ExecuteScalar();
				conn.Close();
				if (!result)
				{
					MessageBox.Show("Invalid login. Please, make sure you wrote it correctly", "Login error", MessageBoxButton.OK, MessageBoxImage.Asterisk, MessageBoxResult.OK);
					return;
				}
				conn_param = "Server=127.0.0.1;Port=5432;User Id=" + tb_login.Text + ";Password=" + tb_pass.Password + ";Database=medialib";
				conn = new NpgsqlConnection(conn_param);
				conn.Open();
				conn.Close();
				MainWindow main = new MainWindow(this, tb_login.Text, conn_param);
				main.Show();
				Hide();
			}
			catch (NpgsqlException sqlEx)
			{
				MessageBox.Show(sqlEx.Message+"\nCode: "+sqlEx.ErrorCode);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
	}
}
