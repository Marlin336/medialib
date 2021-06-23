using Npgsql;
using System;
using System.Collections.Generic;
using System.Windows;

namespace Medialib_proj
{
	/// <summary>
	/// Логика взаимодействия для Edit_privileges.xaml
	/// </summary>
	public partial class Edit_privileges : Window
	{
		int id { get; }
		List<Privilege_item> priv_list = new List<Privilege_item>();
		public Edit_privileges(int id)
		{
			InitializeComponent();
			this.id = id;
			_Privileges.ItemsSource = priv_list;

			NpgsqlCommand comm = new NpgsqlCommand("SELECT display_name FROM lib_role", Shared_data.conn);
			Shared_data.conn.Open();
			NpgsqlDataReader r = comm.ExecuteReader();
			while (r.Read())
			{
				priv_list.Add(new Privilege_item(r.GetString(0)));
			}
			Shared_data.conn.Close();

			comm = new NpgsqlCommand("SELECT * FROM view_user WHERE id = " + id, Shared_data.conn);
			Shared_data.conn.Open();
			r = comm.ExecuteReader();
			r.Read();
			l_log.Text = r.GetString(1);
			priv_list.Find(x => x.Privilege == "admin").IsChecked = r.GetBoolean(3);
			priv_list.Find(x => x.Privilege == "moderor").IsChecked = r.GetBoolean(4);
			priv_list.Find(x => x.Privilege == "film manager").IsChecked = r.GetBoolean(5);
			priv_list.Find(x => x.Privilege == "music manager").IsChecked = r.GetBoolean(6);
			priv_list.Find(x => x.Privilege == "picture manager").IsChecked = r.GetBoolean(7);
			priv_list.Find(x => x.Privilege == "text manager").IsChecked = r.GetBoolean(8);
			Shared_data.conn.Close();
		}

		private class Privilege_item
		{
			public bool IsChecked { get; set; } = false;
			public string Privilege { get; }
			public Privilege_item(string privilege)
			{
				Privilege = privilege;
			}
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			if (MessageBox.Show("Are you sure to change privileges for user \""+l_log.Text+"\"", "Save privileges", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
			{
				try
				{
					Dictionary<string, string> diction = new Dictionary<string, string>();
					NpgsqlCommand comm = new NpgsqlCommand("SELECT * FROM lib_role", Shared_data.conn);
					Shared_data.conn.Open();
					NpgsqlDataReader r = comm.ExecuteReader();
					while (r.Read())
						diction.Add(r.GetString(1), r.GetString(0));
					Shared_data.conn.Close();

					List<Privilege_item> check = priv_list.FindAll(x => x.IsChecked);
					List<Privilege_item> uncheck = priv_list.FindAll(x => !x.IsChecked);
					List<string> grant = new List<string>();
					List<string> revoke = new List<string>();
					for (int i = 0; i < check.Count; i++)
						grant.Add(diction[check[i].Privilege]);
					for (int i = 0; i < uncheck.Count; i++)
						revoke.Add(diction[uncheck[i].Privilege]);
					string grant_str = string.Join(", ", grant);
					string revoke_str = string.Join(", ", revoke);
					string sql = string.Empty;
					sql += !string.IsNullOrEmpty(grant_str) ? "GRANT " + grant_str + " TO \"" + l_log.Text + "\"; " :string.Empty;
					sql += !string.IsNullOrEmpty(revoke_str) ? "REVOKE " + revoke_str + " FROM \"" + l_log.Text + "\";" : string.Empty;
					comm = new NpgsqlCommand(sql, Shared_data.conn);
					Shared_data.conn.Open();
					comm.ExecuteNonQuery();
					Shared_data.conn.Close();

					if (check.Exists(x=>x.Privilege == "admin"))
						comm = new NpgsqlCommand("ALTER ROLE \"" + l_log.Text + "\" SUPERUSER CREATEROLE;", Shared_data.conn);
					else
						comm = new NpgsqlCommand("ALTER ROLE \"" + l_log.Text + "\" NOSUPERUSER NOCREATEROLE;", Shared_data.conn);
					Shared_data.conn.Open();
					comm.ExecuteNonQuery();
					Shared_data.conn.Close();

					Close();
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message);
					Shared_data.conn.Close();
				}
			}
		}
	}
}
