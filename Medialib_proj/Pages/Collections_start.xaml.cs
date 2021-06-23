using Npgsql;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Navigation;

namespace Medialib_proj.Pages
{
	/// <summary>
	/// Логика взаимодействия для Searchable.xaml
	/// </summary>
	public partial class Collections_start : Page
    {
        public Collections_start()
        {
            InitializeComponent();
			FillCollectionList();
			if (!Shared_data.perm_list.Exists(x => x == "admin") && !Shared_data.perm_list.Exists(x => x == "film manager")
				&& !Shared_data.perm_list.Exists(x => x == "music manager") && !Shared_data.perm_list.Exists(x => x == "picture manager")
				&& !Shared_data.perm_list.Exists(x => x == "text manager"))
				manager_panel.Visibility = Visibility.Collapsed;
		}
		public Collections_start(string search_str)
		{
			InitializeComponent();
			if (!Shared_data.perm_list.Exists(x => x == "admin") && !Shared_data.perm_list.Exists(x => x == "film manager")
				&& !Shared_data.perm_list.Exists(x => x == "music manager") && !Shared_data.perm_list.Exists(x => x == "picture manager")
				&& !Shared_data.perm_list.Exists(x => x == "text manager"))
				manager_panel.Visibility = Visibility.Collapsed;
			tb_search.Foreground = new SolidColorBrush(Colors.Black);
			tb_search.Text = search_str;
			IsSearch = true;
			Search();
		}

		public bool IsSearch { get; private set; } = false;
		public string SearchString { get; private set; } = string.Empty;

		#region functions and procedures
		public void Search()
		{
			if (IsSearch)
			{
				SearchString = tb_search.Text;
			}
			else
			{
				SearchString = string.Empty;
			}
			grid_collections.Items.Clear();
			NpgsqlCommand comm = new NpgsqlCommand("SELECT * FROM find_collection('" + SearchString + "') ORDER BY name", Shared_data.conn);
			Shared_data.conn.Open();
			NpgsqlDataReader r = comm.ExecuteReader();
			while (r.Read())
			{
				grid_collections.Items.Add(new Collection_item(r.GetInt32(0), r.GetString(1), r.GetInt32(2), r.GetInt32(3), r.GetInt32(4), r.GetInt32(5)));
			}
			Shared_data.conn.Close();
		}
		public void FillCollectionList()
		{
			grid_collections.Items.Clear();
			NpgsqlCommand comm = new NpgsqlCommand("SELECT * FROM view_collection ORDER BY name", Shared_data.conn);
			Shared_data.conn.Open();
			NpgsqlDataReader r = comm.ExecuteReader();
			while(r.Read())
				grid_collections.Items.Add(new Collection_item(r.GetInt32(0), r.GetString(1), r.GetInt32(2), r.GetInt32(3), r.GetInt32(4), r.GetInt32(5)));
			Shared_data.conn.Close();
		}
		#endregion
		#region classes
		private class Collection_item
		{
			public int id { get; }
			public string name { get; }
			public int movie { get; }
			public int music { get; }
			public int picture { get; }
			public int text { get; }
			public Collection_item(int id, string name, int movie, int music, int picture, int text)
			{
				this.id = id;
				this.name = name;
				this.movie = movie;
				this.music = music;
				this.picture = picture;
				this.text = text;
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

		private void Grid_collection_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			b_del.IsEnabled = b_edit.IsEnabled = grid_collections.SelectedIndex != -1;
		}

		private void B_add_Click(object sender, RoutedEventArgs e)
		{
			Add_collection window = new Add_collection();
			window.ShowDialog();
			FillCollectionList();
		}

		private void B_del_Click(object sender, RoutedEventArgs e)
		{
			Collection_item item = grid_collections.SelectedItem as Collection_item;
			if (MessageBox.Show("Do you want to delete record \"" + item.name + "\"?", "Delete record", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
			{
				NpgsqlCommand comm = new NpgsqlCommand("DELETE FROM collection WHERE id = " + item.id, Shared_data.conn);
				Shared_data.conn.Open();
				comm.ExecuteNonQuery();
				Shared_data.conn.Close();
			}
		}

		private void B_edit_Click(object sender, RoutedEventArgs e)
		{
			Collection_item item = grid_collections.SelectedItem as Collection_item;
			Add_collection window = new Add_collection(item.id);
			window.ShowDialog();
			FillCollectionList();
		}

		private void DataGridRow_MouseDoubleClick(object sender, MouseButtonEventArgs e)
		{
			Collection_item row = grid_collections.SelectedItem as Collection_item;
			if (row == null) return;
			Page page = new Collections_view(row.id);
			NavigationService.Navigate(page);
		}
		#endregion
	}
}
