using Npgsql;
using System;
using System.Collections.Generic;
using System.Windows;

namespace Medialib_proj
{
	/// <summary>
	/// Логика взаимодействия для SignUpWindow.xaml
	/// </summary>
	public partial class SignUpWindow : Window
    {
		private List<Lang_item> languages = new List<Lang_item>();
		private string conn_param = "Server=127.0.0.1;Port=5432;User Id=medialib_login;Password=0000;Database=medialib";
		NpgsqlConnection conn;

		public SignUpWindow()
        {
            InitializeComponent();
			cb_lang.ItemsSource = languages;
			conn = new NpgsqlConnection(conn_param);
			NpgsqlCommand comm = new NpgsqlCommand("SELECT * FROM f_language ORDER BY name", conn);
			conn.Open();
			NpgsqlDataReader reader = comm.ExecuteReader();
			while(reader.Read())
				languages.Add(new Lang_item(reader.GetInt32(0), reader.GetString(1) + "(" + reader.GetString(2) + ")"));
			conn.Close();
        }

		private class Lang_item
		{
			public int id { get; }
			public string lang { get; }
			public Lang_item(int id, string lang)
			{
				this.id = id;
				this.lang = lang;
			}
		}

		private void B_ok_Click(object sender, RoutedEventArgs e)
		{
			if (tb_login.Text == string.Empty)
			{
				MessageBox.Show("The login field cannot be empty", "Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
				return;
			}
			if (tb_pass.Password == string.Empty)
			{
				MessageBox.Show("The password field cannot be empty", "Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
				return;
			}
			else
			{
				if (tb_pass.Password != tb_confpass.Password)
				{
					MessageBox.Show("Password confirmation failed!", "Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
					tb_confpass.Password = tb_pass.Password = string.Empty;
					return;
				}
			}
			if (cb_lang.SelectedIndex == -1)
			{
				MessageBox.Show("You must make a choice of language", "Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
				return;
			}
			Lang_item row = cb_lang.SelectedValue as Lang_item;
			try
			{
				NpgsqlCommand comm = new NpgsqlCommand("CALL add_user('" + tb_login.Text.Trim() + "','" + tb_pass.Password + "', " + row.id + ")", conn);
				conn.Open();
				comm.ExecuteNonQuery();
				MessageBox.Show("Account created successfully", "Welcome!", MessageBoxButton.OK, MessageBoxImage.Asterisk);
				Close();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Exception", MessageBoxButton.OK, MessageBoxImage.Error);
			}
			finally { conn.Close(); }
		}

		private void B_cancel_Click(object sender, RoutedEventArgs e)
		{
			if (MessageBox.Show("Cancel signing up?", "Cancel", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
				Close();
		}
	}
}
