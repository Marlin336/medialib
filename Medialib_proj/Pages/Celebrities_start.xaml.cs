using Npgsql;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Navigation;

namespace Medialib_proj.Pages
{
	/// <summary>
	/// Логика взаимодействия для Celebrities_start.xaml
	/// </summary>
	public partial class Celebrities_start : Page
    {
		public Celebrities_start()
		{
			InitializeComponent();
			FillSelebritieList();
			if (!Shared_data.perm_list.Exists(x => x == "admin") && !Shared_data.perm_list.Exists(x => x == "film manager")
				&& !Shared_data.perm_list.Exists(x => x == "music manager") && !Shared_data.perm_list.Exists(x => x == "picture manager")
				&& !Shared_data.perm_list.Exists(x => x == "text manager"))
				manager_panel.Visibility = Visibility.Collapsed;
		}
		public Celebrities_start(string search_str)
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
		public void FillSelebritieList()
		{
			grid_celebrities.Items.Clear();
			NpgsqlCommand comm = new NpgsqlCommand("SELECT * FROM view_person ORDER BY name", Shared_data.conn);
			Shared_data.conn.Open();
			NpgsqlDataReader r = comm.ExecuteReader();
			while (r.Read())
			{
				grid_celebrities.Items.Add(new Celebritie_item(r.GetInt32(0), r.GetString(1), r.GetDate(3).ToString(), r.GetInt32(4)));
			}
			Shared_data.conn.Close();
		}

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
			grid_celebrities.Items.Clear();
			NpgsqlCommand comm = new NpgsqlCommand("SELECT * FROM find_person('" + SearchString + "') ORDER BY name", Shared_data.conn);
			Shared_data.conn.Open();
			NpgsqlDataReader r = comm.ExecuteReader();
			while (r.Read())
			{
				grid_celebrities.Items.Add(new Celebritie_item(r.GetInt32(0), r.GetString(1), r.GetDate(3).ToString(), r.GetInt32(4)));
			}
			Shared_data.conn.Close();
		}
		#endregion
		#region classes
		private class Celebritie_item
		{
			public int id { get; }
			public string name { get; }
			public string birthday { get; }
			public int age { get; }
			public Celebritie_item(int id, string name, string birthday, int age)
			{
				this.id = id;
				this.name = name;
				this.birthday = birthday;
				this.age = age;
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
			b_del.IsEnabled = b_edit.IsEnabled = grid_celebrities.SelectedIndex != -1;
		}

		private void B_add_Click(object sender, RoutedEventArgs e)
		{
			Add_celebritie window = new Add_celebritie();
			window.ShowDialog();
			FillSelebritieList();
		}

		private void B_del_Click(object sender, RoutedEventArgs e)
		{
			Celebritie_item item = grid_celebrities.SelectedItem as Celebritie_item;
			if (MessageBox.Show("Do you want to delete record \"" + item.name + "\"?", "Delete record", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
			{
				NpgsqlCommand comm = new NpgsqlCommand("DELETE FROM person WHERE id = " + item.id, Shared_data.conn);
				Shared_data.conn.Open();
				comm.ExecuteNonQuery();
				Shared_data.conn.Close();
			}
		}

		private void B_edit_Click(object sender, RoutedEventArgs e)
		{
			Celebritie_item item = grid_celebrities.SelectedItem as Celebritie_item;
			Add_celebritie window = new Add_celebritie(item.id);
			window.ShowDialog();
			FillSelebritieList();
		}

		private void DataGridRow_MouseDoubleClick(object sender, MouseButtonEventArgs e)
		{
			Celebritie_item row = grid_celebrities.SelectedItem as Celebritie_item;
			if (row == null) return;
			Page page = new Celebrities_view(row.id);
			NavigationService.Navigate(page);
			FillSelebritieList();
		}
		#endregion
	}
}
