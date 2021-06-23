using Npgsql;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Medialib_proj
{
	/// <summary>
	/// Логика взаимодействия для Add_lang.xaml
	/// </summary>
	public partial class Add_lang : Window
	{
		int id { get; }
		List<Lang_item> languages = new List<Lang_item>();
		public Add_lang(int id)
		{
			InitializeComponent();
			this.id = id;
			_Languages.ItemsSource = languages;

			NpgsqlCommand comm = new NpgsqlCommand("SELECT lang.* FROM f_language as lang LEFT JOIN media_film as film ON film.origlang_id <> lang.id WHERE film.id = " + id, Shared_data.conn);
			Shared_data.conn.Open();
			NpgsqlDataReader r = comm.ExecuteReader();
			while (r.Read())
				languages.Add(new Lang_item(r.GetInt32(0), r.GetString(1) + "(" + r.GetString(2) + ")"));
			Shared_data.conn.Close();

			comm = new NpgsqlCommand("SELECT orig_name FROM view_film WHERE id = " + id, Shared_data.conn);
			Shared_data.conn.Open();
			r = comm.ExecuteReader();
			r.Read();
			l_orig_name.Content = r.GetString(0);
			Shared_data.conn.Close();
		}

		class Lang_item
		{
			public int id { get; }
			public string lang { get; set; }
			public Lang_item(int id, string lang)
			{
				this.id = id;
				this.lang = lang;
			}
		}

		private void B_add_Click(object sender, RoutedEventArgs e)
		{
			Lang_item item = _Languages.SelectedItem as Lang_item;
			if (tb_value.Text.Trim() == string.Empty)
			{
				NpgsqlCommand comm = new NpgsqlCommand("DELETE FROM _lang_film WHERE film_id = " + id + " AND lang_id = " + item.id, Shared_data.conn);
				Shared_data.conn.Open();
				comm.ExecuteNonQuery();
				Shared_data.conn.Close();
				MessageBox.Show("Local name deleted", "", MessageBoxButton.OK, MessageBoxImage.Information);
			}
			else
			{
				NpgsqlCommand comm = new NpgsqlCommand("SELECT EXISTS (SELECT * FROM _lang_film WHERE film_id = " + id + " AND lang_id = " + item.id+")", Shared_data.conn);
				Shared_data.conn.Open();
				bool exists = (bool)comm.ExecuteScalar();
				Shared_data.conn.Close();
				if (exists)
				{
					comm = new NpgsqlCommand("UPDATE _lang_film SET value = $$" + tb_value.Text + "$$ WHERE film_id = " + id + " AND lang_id = " + item.id, Shared_data.conn);
					Shared_data.conn.Open();
					comm.ExecuteNonQuery();
					Shared_data.conn.Close();
					MessageBox.Show("Local name changed", "", MessageBoxButton.OK, MessageBoxImage.Information);
				}
				else
				{
					comm = new NpgsqlCommand("INSERT INTO _lang_film(lang_id, film_id, value) VALUES(" + item.id + ", " + id + ", $$" + tb_value.Text + "$$)", Shared_data.conn);
					Shared_data.conn.Open();
					comm.ExecuteNonQuery();
					Shared_data.conn.Close();
					MessageBox.Show("Local name added", "", MessageBoxButton.OK, MessageBoxImage.Information);
				}
			}
		}

		private void _Languages_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (_Languages.SelectedIndex != -1)
			{
				Lang_item item = _Languages.SelectedItem as Lang_item;
				NpgsqlCommand comm = new NpgsqlCommand("SELECT value FROM _lang_film WHERE film_id = " + id + " AND lang_id = " + item.id, Shared_data.conn);
				Shared_data.conn.Open();
				try
				{
					tb_value.Text = comm.ExecuteScalar().ToString();
				}
				catch (Exception ex)
				{
					tb_value.Text = string.Empty;
				}
				finally
				{
					Shared_data.conn.Close();
				}
			}
		}
	}
}
