using Npgsql;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Navigation;

namespace Medialib_proj.Pages
{
	/// <summary>
	/// Логика взаимодействия для Texts_start.xaml
	/// </summary>
	public partial class Texts_start : Page
    {
		public Texts_start()
		{
			InitializeComponent();
			_Genres.ItemsSource = Genre_list;
			FillGenreList();
			FillTextList();
			if (!Shared_data.perm_list.Exists(x => x == "admin") && !Shared_data.perm_list.Exists(x => x == "text manager"))
				manager_panel.Visibility = Visibility.Collapsed;
		}
		public Texts_start(string search_str)
		{
			InitializeComponent();
			tb_search.Foreground = new SolidColorBrush(Colors.Black);
			_Genres.ItemsSource = Genre_list;
			tb_search.Text = search_str;
			IsSearch = true;
			Search();
		}
		public bool IsSearch { get; private set; }
		public string SearchString { get; private set; }
		public List<Genre_item> Genre_list = new List<Genre_item>();

		#region functions and procedures
		private void FillGenreList()
		{
			Genre_list.Clear();
			NpgsqlCommand comm = new NpgsqlCommand("SELECT * FROM t_genre", Shared_data.conn);
			Shared_data.conn.Open();
			NpgsqlDataReader r = comm.ExecuteReader();
			while (r.Read())
				Genre_list.Add(new Genre_item(new Reference(r.GetInt32(0), r.GetString(1))));
			Shared_data.conn.Close();
		}
		private void FillTextList()
		{
			grid_texts.Items.Clear();
			Genre_item[] genre_arr = Genre_list.FindAll(x => x.IsChecked).ToArray();
			string[] g = new string[genre_arr.Length];
			for (int i = 0; i < g.Length; i++)
				g[i] = genre_arr[i].item.id.ToString();
			string sqlQu = string.Join(",", g);
			NpgsqlCommand comm = new NpgsqlCommand("SELECT id, name, year, rating FROM find_text($$" + SearchString + "$$) INTERSECT SELECT id, name, year, rating FROM find_text_genre(ARRAY[" + sqlQu + "]::int[])", Shared_data.conn);
			Shared_data.conn.Open();
			NpgsqlDataReader r = comm.ExecuteReader();
			while (r.Read())
			{
				grid_texts.Items.Add(new Text_item(
					r.GetInt32(0),
					r.GetString(1),
					r.GetInt32(2),
					r.GetDouble(3)));
			}
			Shared_data.conn.Close();
		}
		private void Search()
		{
			if (IsSearch)
			{
				SearchString = tb_search.Text;
			}
			else
			{
				SearchString = string.Empty;
			}
			FillTextList();
		}
		#endregion
		#region classes
		public class Genre_item
		{
			public Reference item { get; }
			public bool IsChecked { get; set; } = false;

			public Genre_item(Reference @ref)
			{
				item = @ref;
			}
		}
		public class Text_item
		{
			public int id { get; }
			public string name { get; }
			public int year { get; }
			public double rating { get; }
			public Text_item(int id, string name, int year, double rating)
			{
				this.id = id;
				this.name = name;
				this.year = year;
				this.rating = rating;
			}
		}
		#endregion
		#region events
		private void B_search_Click(object sender, RoutedEventArgs e)
		{
			Search();
		}

		private void Tb_search_GotFocus(object sender, RoutedEventArgs e)
		{
			if (!IsSearch)
			{
				tb_search.Text = string.Empty;
				tb_search.Foreground = new SolidColorBrush(Colors.Black);
			}
		}

		private void Tb_search_LostFocus(object sender, RoutedEventArgs e)
		{
			IsSearch = tb_search.Text.Length != 0;
			if (!IsSearch)
			{
				tb_search.Text = "Search...";
				tb_search.Foreground = new SolidColorBrush(Colors.Gray);
			}
		}


		private void DataGridRow_MouseDoubleClick(object sender, MouseButtonEventArgs e)
		{
			Text_item row = grid_texts.SelectedItem as Text_item;
			if (row == null) return;
			int id = row.id;
			Page page = new Texts_view(id);
			NavigationService.Navigate(page);
		}

		private void B_all_Click(object sender, RoutedEventArgs e)
		{
			Genre_list.ForEach(item => item.IsChecked = true);
			_Genres.Items.Refresh();
			FillTextList();
		}

		private void B_none_Click(object sender, RoutedEventArgs e)
		{
			Genre_list.ForEach(item => item.IsChecked = false);
			_Genres.Items.Refresh();
			FillTextList();
		}
		#endregion

		private void B_add_Click(object sender, RoutedEventArgs e)
		{
			Add_text windows = new Add_text();
			windows.ShowDialog();
			FillTextList();
			FillGenreList();
		}

		private void B_del_Click(object sender, RoutedEventArgs e)
		{
			Text_item item = grid_texts.SelectedItem as Text_item;
			if (MessageBox.Show("Do you want to delete record \"" + item.name + "\"?", "Delete record", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
			{
				try
				{
					NpgsqlCommand comm = new NpgsqlCommand("DELETE FROM media_text WHERE id = " + item.id, Shared_data.conn);
					Shared_data.conn.Open();
					comm.ExecuteNonQuery();
					Shared_data.conn.Close();
					grid_texts.Items.Remove(item);
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message);
				}
			}
		}

		private void B_edit_Click(object sender, RoutedEventArgs e)
		{
			Text_item item = grid_texts.SelectedItem as Text_item;
			Add_text window = new Add_text(item.id);
			window.ShowDialog();
		}

		private void Grid_texts_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			b_del.IsEnabled = b_edit.IsEnabled = grid_texts.SelectedIndex != -1;
		}

		private void CheckBox_Checked(object sender, RoutedEventArgs e)
		{
			FillTextList();
		}
	}
}
