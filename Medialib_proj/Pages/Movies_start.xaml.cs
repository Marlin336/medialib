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
	/// Логика взаимодействия для Movies_start.xaml
	/// </summary>
	public partial class Movies_start : Page
    {
		public Movies_start()
		{
			InitializeComponent();
			_Genres.ItemsSource = Genre_list;
			FillGenreList();
			FillMovieList();
			if (!Shared_data.perm_list.Exists(x => x == "admin") && !Shared_data.perm_list.Exists(x => x == "film manager"))
				manager_panel.Visibility = Visibility.Collapsed;
		}
		/// <summary>
		/// Конструктор страницы со сторокой поиска
		/// </summary>
		/// <param name="search_str">Параметр строки поиска</param>
		public Movies_start(string search_str)
		{
			InitializeComponent();
			tb_search.Foreground = new SolidColorBrush(Colors.Black);
			_Genres.ItemsSource = Genre_list;
			tb_search.Text = search_str;
			IsSearch = true;
			Search();
		}
		public bool IsSearch { get; private set; } = false;
		public string SearchString { get; private set; } = string.Empty;
		public List<Genre_item> Genre_list = new List<Genre_item>();

		#region functions and procedures
		private void FillGenreList()
		{
			Genre_list.Clear();
			NpgsqlCommand comm = new NpgsqlCommand("SELECT * FROM f_genre", Shared_data.conn);
			Shared_data.conn.Open();
			NpgsqlDataReader r = comm.ExecuteReader();
			while (r.Read())
				Genre_list.Add(new Genre_item(new Reference(r.GetInt32(0), r.GetString(1))));
			Shared_data.conn.Close();
		}
		private void FillMovieList()
		{
			grid_movies.Items.Clear();
			Genre_item[] genre_arr = Genre_list.FindAll(x => x.IsChecked).ToArray();
			string[] g = new string[genre_arr.Length];
			for (int i = 0; i < g.Length; i++)
				g[i] = genre_arr[i].item.id.ToString();
			string sqlQu = string.Join(",", g);
			NpgsqlCommand comm = new NpgsqlCommand("SELECT id, orig_name, get_local_film(id, " + Shared_data.lang_id + "), year, duration, rating FROM find_film($$" + SearchString + "$$) INTERSECT SELECT id, orig_name, get_local_film(id, " + Shared_data.lang_id + "), year, duration, rating FROM public.find_film_genre(ARRAY[" + sqlQu + "]::int[])", Shared_data.conn);
			Shared_data.conn.Open();
			NpgsqlDataReader r = comm.ExecuteReader();
			while (r.Read())
			{
				grid_movies.Items.Add(new Movie_item(
					r.GetInt32(0),
					r.GetString(1),
					r.GetValue(2).ToString(),
					r.GetInt32(3),
					r.GetTimeSpan(4).ToString(),
					Math.Round(r.GetDouble(5),1)));
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
			FillMovieList();
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
		public class Movie_item
		{
			public int id { get; }
			public string orig_name { get; }
			public string local_name { get; }
			public int year { get; }
			public string duration { get; }
			public double rating { get; }
			public Movie_item(int id, string orig_name, string local_name, int year, string duration, double rating)
			{
				this.id = id;
				this.orig_name = orig_name;
				this.local_name = local_name;
				this.year = year;
				this.duration = duration;
				this.rating = rating;
			}
		}
		#endregion
		#region events
		private void DataGridRow_MouseDoubleClick(object sender, MouseButtonEventArgs e)
		{
			Movie_item row = grid_movies.SelectedItem as Movie_item;
			if (row == null) return;
			int id = row.id;
			Page page = new Movies_view(id);
			NavigationService.Navigate(page);
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

		private void Tb_search_GotFocus(object sender, RoutedEventArgs e)
		{
			if (!IsSearch)
			{
				tb_search.Text = string.Empty;
				tb_search.Foreground = new SolidColorBrush(Colors.Black);
			}
		}

		private void B_search_Click(object sender, RoutedEventArgs e)
		{
			Search();
		}

		private void B_all_Click(object sender, RoutedEventArgs e)
		{
			Genre_list.ForEach(item => item.IsChecked = true);
			_Genres.Items.Refresh();
			FillMovieList();
		}

		private void B_none_Click(object sender, RoutedEventArgs e)
		{
			Genre_list.ForEach(item => item.IsChecked = false);
			_Genres.Items.Refresh();
			FillMovieList();
		}

		private void B_add_Click(object sender, RoutedEventArgs e)
		{
			Add_movie window = new Add_movie();
			window.ShowDialog();
			FillGenreList();
			FillMovieList();
		}

		private void B_del_Click(object sender, RoutedEventArgs e)
		{
			Movie_item item = grid_movies.SelectedItem as Movie_item;
			if (MessageBox.Show("Do you want to delete record \"" + item.orig_name + "\"?", "Delete record", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
			{
				try
				{
					NpgsqlCommand comm = new NpgsqlCommand("DELETE FROM media_film WHERE id = " + item.id, Shared_data.conn);
					Shared_data.conn.Open();
					comm.ExecuteNonQuery();
					Shared_data.conn.Close();
					grid_movies.Items.Remove(item);
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message);
				}
			}
		}

		private void B_edit_Click(object sender, RoutedEventArgs e)
		{
			Movie_item item = grid_movies.SelectedItem as Movie_item;
			Add_movie window = new Add_movie(item.id);
			window.ShowDialog();
			FillMovieList();
			FillGenreList();
		}

		private void Grid_movies_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			b_del.IsEnabled = b_edit.IsEnabled = b_lang.IsEnabled = grid_movies.SelectedIndex != -1;
		}

		private void B_lang_Click(object sender, RoutedEventArgs e)
		{
			Movie_item item = grid_movies.SelectedItem as Movie_item;
			Window win = new Add_lang(item.id);
			win.ShowDialog();
			FillMovieList();
		}

		private void CheckBox_Checked(object sender, RoutedEventArgs e)
		{
			FillMovieList();
		}
		#endregion
	}
}
