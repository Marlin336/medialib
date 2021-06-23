using Npgsql;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Medialib_proj.Pages
{
	/// <summary>
	/// Логика взаимодействия для Userlist_view.xaml
	/// </summary>
	public partial class Userlist_view : Page
    {
		
        public Userlist_view()
        {
            InitializeComponent();
			_Privileges.ItemsSource = Privilege_list;
			FillPivilegeList();
			FillUserList();
        }
		public bool IsSearch { get; private set; } = false;
		public string SearchString { get; private set; } = string.Empty;
		private List<Privilege_item> Privilege_list = new List<Privilege_item>();

		private void FillPivilegeList()
		{
			Privilege_list.Clear();
			NpgsqlCommand comm = new NpgsqlCommand("SELECT * FROM lib_role", Shared_data.conn);
			//Заполняем список в логике
			Shared_data.conn.Open();
			NpgsqlDataReader r = comm.ExecuteReader();
			while (r.Read())
				Privilege_list.Add(new Privilege_item(r.GetString(1)));
			Shared_data.conn.Close();
		}
		private void FillUserList()
		{
			grid_users.Items.Clear();
			Privilege_item[] priv_arr = Privilege_list.FindAll(x => x.IsChecked).ToArray();
			string[] p = new string[priv_arr.Length];
			for (int i = 0; i < p.Length; i++)
				p[i] = priv_arr[i].Privilege;
			string sqlQu = string.Join("', '", p);
			if (sqlQu.Length != 0)
			{
				sqlQu = sqlQu.Insert(0, "'");
				sqlQu = sqlQu.Insert(sqlQu.Length, "'");
			}
			NpgsqlCommand comm = new NpgsqlCommand("SELECT * FROM find_user($$"+SearchString+"$$) INTERSECT SELECT * FROM find_user_role(ARRAY["+sqlQu+"]::text[])", Shared_data.conn);
			Shared_data.conn.Open();
			NpgsqlDataReader r = comm.ExecuteReader();
			while (r.Read())
			{
				grid_users.Items.Add(new User_item(int.Parse(r.GetValue(0).ToString()), r.GetString(1), r.GetBoolean(3), r.GetBoolean(4), r.GetBoolean(5), r.GetBoolean(6), r.GetBoolean(7), r.GetBoolean(8)));
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
			FillUserList();
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
		private class User_item
		{
			public int id { get; }
			public string login { get; }
			public bool moderor { get; set; }
			public bool admin { get; set; }
			public bool film_manager { get; set; }
			public bool music_manager { get; set; }
			public bool picture_manager { get; set; }
			public bool text_manager { get; set; }
			public User_item(int id, string login, bool a, bool mod, bool f, bool m, bool p, bool t)
			{
				this.id = id;
				this.login = login;
				admin = a;
				moderor = mod;
				film_manager = f;
				music_manager = m;
				picture_manager = p;
				text_manager = t;
			}
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
			Privilege_list.ForEach(item => item.IsChecked = true);
			_Privileges.Items.Refresh();
			FillUserList();
		}

		private void B_none_Click(object sender, RoutedEventArgs e)
		{
			Privilege_list.ForEach(item => item.IsChecked = false);
			_Privileges.Items.Refresh();
			FillUserList();
		}

		private void DataGridRow_MouseDoubleClick(object sender, MouseButtonEventArgs e)
		{
			User_item item = grid_users.SelectedItem as User_item;
			Edit_privileges win = new Edit_privileges(item.id);
			win.ShowDialog();
			FillUserList();
		}

		private void CheckBox_Checked(object sender, RoutedEventArgs e)
		{
			FillUserList();
		}
	}
}
