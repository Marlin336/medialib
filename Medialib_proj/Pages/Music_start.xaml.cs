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
	/// Логика взаимодействия для Music_start.xaml
	/// </summary>
	public partial class Music_start : Page
    {
		public Music_start()
		{
			InitializeComponent();
			_Genres.ItemsSource = Genre_list;
			FillGenreList();
			FillMusicList();
			if (!Shared_data.perm_list.Exists(x => x == "admin") && !Shared_data.perm_list.Exists(x => x == "music manager"))
				manager_panel.Visibility = Visibility.Collapsed;
		}
		/// <summary>
		/// Конструктор страницы со сторокой поиска
		/// </summary>
		/// <param name="search_str">Параметр строки поиска</param>
		public Music_start(string search_str)
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
		/// <summary>
		/// Заполнение списка жанров
		/// </summary>
		private void FillGenreList()
		{
			Genre_list.Clear();
			NpgsqlCommand comm = new NpgsqlCommand("SELECT * FROM m_genre", Shared_data.conn);
			Shared_data.conn.Open();
			NpgsqlDataReader r = comm.ExecuteReader();
			while (r.Read())
				Genre_list.Add(new Genre_item(new Reference(r.GetInt32(0), r.GetString(1))));
			Shared_data.conn.Close();
		}
		/// <summary>
		/// Заполнение списка музыки
		/// </summary>
		private void FillMusicList()
		{
			grid_music.Items.Clear();
			Genre_item[] genre_arr = Genre_list.FindAll(x => x.IsChecked).ToArray();
			string[] g = new string[genre_arr.Length];
			for (int i = 0; i < g.Length; i++)
				g[i] = genre_arr[i].item.id.ToString();
			string sqlQu = string.Join(",", g);
			NpgsqlCommand comm = new NpgsqlCommand("SELECT vm.*, band.name FROM find_music($$" + SearchString + "$$) as vm " +
			"LEFT JOIN m_album as ma ON ma.id = vm.album_id " +
			"LEFT JOIN m_band as band ON ma.band_id = band.id " +
			"INTERSECT " +
			"SELECT vm.*, band.name FROM find_music_genre(ARRAY[" + sqlQu + "]::int[]) as vm " +
			"LEFT JOIN m_album as ma ON ma.id = vm.album_id " +
			"LEFT JOIN m_band as band ON ma.band_id = band.id", Shared_data.conn);
			Shared_data.conn.Open();
			NpgsqlDataReader r = comm.ExecuteReader();
			while (r.Read())
			{
				grid_music.Items.Add(new Music_item(
					r.GetInt32(0),
					r.GetValue(16).ToString(),
					r.GetValue(3).ToString(),
					r.GetInt32(5),
					r.GetInt32(4),
					r.GetString(1),
					r.GetTimeSpan(6).ToString(),
					r.GetDouble(14)));
			}
			Shared_data.conn.Close();
		}
		/// <summary>
		/// Поиск в таблице
		/// </summary>
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
			FillMusicList();
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
		public class Music_item
		{
			public int id { get; }
			public string musician { get; }
			public string album { get; }
			public int year { get; }
			public int number { get; }
			public string name { get; }
			public string duration { get; }
			public double rating { get; }
			public Music_item(int id, string musician, string album, int year, int number, string name, string duration, double rating)
			{
				this.id = id;
				this.musician = musician;
				this.album = album;
				this.year = year;
				this.number = number;
				this.name = name;
				this.duration = duration;
				this.rating = rating;
			}
		}
		#endregion
		#region events
		private void DataGridRow_MouseDoubleClick(object sender, MouseButtonEventArgs e)
		{
			Music_item row = grid_music.SelectedItem as Music_item;
			if (row == null) return;
			int id = row.id;
			Page page = new Music_view(id);
			NavigationService.Navigate(page);
		}

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

		private void B_all_Click(object sender, RoutedEventArgs e)
		{
			Genre_list.ForEach(item => item.IsChecked = true);
			_Genres.Items.Refresh();
			FillMusicList();
		}

		private void B_none_Click(object sender, RoutedEventArgs e)
		{
			Genre_list.ForEach(item => item.IsChecked = false);
			_Genres.Items.Refresh();
			FillMusicList();
		}

		private void B_add_Click(object sender, RoutedEventArgs e)
		{
			Add_music window = new Add_music();
			window.ShowDialog();
			FillMusicList();
			FillGenreList();
		}

		private void B_del_Click(object sender, RoutedEventArgs e)
		{
			Music_item item = grid_music.SelectedItem as Music_item;
			if (MessageBox.Show("Do you want to delete record \"" + item.name + "\"?", "Delete record", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
			{
				try
				{
					NpgsqlCommand comm = new NpgsqlCommand("DELETE FROM media_music WHERE id = " + item.id, Shared_data.conn);
					Shared_data.conn.Open();
					comm.ExecuteNonQuery();
					Shared_data.conn.Close();
					grid_music.Items.Remove(item);
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message);
				}
			}
		}

		private void B_edit_Click(object sender, RoutedEventArgs e)
		{
			Music_item item = grid_music.SelectedItem as Music_item;
			Add_music window = new Add_music(item.id);
			window.ShowDialog();
			FillMusicList();
			FillGenreList();
		}

		private void Grid_music_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			b_del.IsEnabled = b_edit.IsEnabled = grid_music.SelectedIndex != -1;
		}

		private void CheckBox_Checked(object sender, RoutedEventArgs e)
		{
			FillMusicList();
		}
		#endregion
	}
}
