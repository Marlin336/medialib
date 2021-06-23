using Medialib_proj.Pages;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Windows;

namespace Medialib_proj
{
	/// <summary>
	/// Логика взаимодействия для MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private Logwin Log_win { get; }
		private bool logout = false;
		public NpgsqlCommand comm;
		public string Lang { get; }		

		public MainWindow(Logwin parrent, string user, string conn_param)
		{
			InitializeComponent();
			Shared_data.conn = new NpgsqlConnection(conn_param);
			Shared_data.user_name = user;
			Shared_data.perm_list = new List<string>();
			Log_win = parrent;
			Title = Shared_data.user_name;
			frame.NavigationService.Navigate(new HomePage());
			comm = new NpgsqlCommand("SELECT lang_id FROM lib_user WHERE login = current_user", Shared_data.conn);
			Shared_data.conn.Open();
			Shared_data.lang_id = (int)comm.ExecuteScalar();
			Shared_data.conn.Close();
			comm = new NpgsqlCommand("select get_role_list('" + user + "')", Shared_data.conn);
			Shared_data.conn.Open();
			NpgsqlDataReader reader = comm.ExecuteReader();
			while(reader.Read())
				Shared_data.perm_list.Add(reader.GetString(0));
			if (!Shared_data.perm_list.Exists(x => x == "admin"))
				b_user_list.Visibility = Visibility.Collapsed;
			Shared_data.conn.Close();
		}

		private void B_logout_Click(object sender, RoutedEventArgs e)
		{
			if(MessageBox.Show("Do you want to logout?", "Logout", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
			{
				logout = true;
				Log_win.tb_pass.Password = Log_win.tb_login.Text = string.Empty;
				Shared_data.conn.Close();
				Close();
			}
		}

		private void B_prof_Click(object sender, RoutedEventArgs e)
		{
			ProfWindow prof = new ProfWindow(this);
			int len = Shared_data.user_name.Length * 7 + 80;
			prof.Width = Math.Max(len, prof.MinWidth);
			prof.Show();
		}

		private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if (logout)
				Log_win.Show();
			else
			{
				if (MessageBox.Show("Do you want to close the application?", "Closing", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
				{
					Shared_data.conn.Close();
					Application.Current.Shutdown();
				}
				else
					e.Cancel = true;
			}
		}

		private void B_user_list_Click(object sender, RoutedEventArgs e)
		{
			frame.NavigationService.Navigate(new Userlist_view());
		}
	}
}
