using Npgsql;
using System.Collections.Generic;
using System.Windows;

namespace Medialib_proj
{
	/// <summary>
	/// Логика взаимодействия для ProfWindow.xaml
	/// </summary>
	public partial class ProfWindow : Window
    {
		private MainWindow main { get; }
		private List<Privilage_item> privilages { get; } = new List<Privilage_item>();
		private List<Lang_item> languages = new List<Lang_item>();
		private int st_lang_id;
        public ProfWindow(MainWindow parrent)
        {
			main = parrent;
            InitializeComponent();
			l_log.Text = Shared_data.user_name;
			_Privileges.ItemsSource = privilages;
			_Languages.ItemsSource = languages;
			NpgsqlCommand comm =  new NpgsqlCommand("SELECT display_name FROM public.lib_role", Shared_data.conn);
			Shared_data.conn.Open();
			NpgsqlDataReader reader = comm.ExecuteReader();
			for (; reader.Read();)
			{
				privilages.Add(new Privilage_item(reader.GetString(0), Shared_data.perm_list.Exists(x => x == reader.GetString(0))));
			}
			Shared_data.conn.Close();
			main.comm = new NpgsqlCommand("SELECT * FROM f_language ORDER BY name", Shared_data.conn);
			Shared_data.conn.Open();
			reader = main.comm.ExecuteReader();
			while (reader.Read())
			{
				languages.Add(new Lang_item(reader.GetInt32(0), reader.GetString(1) + "(" + reader.GetString(2) + ")"));
			}
			Shared_data.conn.Close();
			main.comm = new NpgsqlCommand("SELECT lang_id FROM lib_user WHERE login = current_user", Shared_data.conn);
			Shared_data.conn.Open();
			st_lang_id = (int)main.comm.ExecuteScalar();
			_Languages.SelectedItem = languages.Find(x => x.id == st_lang_id);
			Shared_data.conn.Close();
		}
		private class Privilage_item
		{
			public bool IsChecked { get; set; }
			public string Privilege { get; set; }
			public Privilage_item(string priv_name, bool has)
			{
				IsChecked = has;
				Privilege = priv_name;
			}
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

		private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			Lang_item row = _Languages.SelectedValue as Lang_item;
			if (row.id != st_lang_id)
			{
				switch(MessageBox.Show("Save changes?", "Saving", MessageBoxButton.YesNoCancel, MessageBoxImage.Question))
				{
					case MessageBoxResult.Yes:
						main.comm = new NpgsqlCommand("UPDATE lib_user SET lang_id = " + row.id + " WHERE login = current_user", Shared_data.conn);
						Shared_data.conn.Open();
						main.comm.ExecuteNonQuery();
						Shared_data.conn.Close();
						Shared_data.lang_id = row.id;
						break;
					case MessageBoxResult.No:
						break;
					case MessageBoxResult.Cancel:
						e.Cancel = true;
						break;
				}
			}
		}
	}
}
