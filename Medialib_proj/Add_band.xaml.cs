using Npgsql;
using System.Collections.Generic;
using System.Windows;

namespace Medialib_proj
{
	/// <summary>
	/// Логика взаимодействия для Add_band.xaml
	/// </summary>
	public partial class Add_band : Window
    {
		int? id { get; } = null;
		List<object> members = new List<object>();
        public Add_band()
        {
            InitializeComponent();
        }
		public Add_band(int id):this()
		{
			this.id = id;
			b_add.Content = "Save";

			NpgsqlCommand comm = new NpgsqlCommand("SELECT name FROM m_band WHERE id = " + id, Shared_data.conn);
			Shared_data.conn.Open();
			NpgsqlDataReader r = comm.ExecuteReader();
			r.Read();
			tb_name.Text = r.GetString(0);
			Shared_data.conn.Close();

			comm = new NpgsqlCommand("SELECT id, name FROM person", Shared_data.conn);
			Shared_data.conn.Open();
			r = comm.ExecuteReader();
			while (r.Read())
				members.Add(new Item_person(r.GetInt32(0), r.GetString(1)));
			Shared_data.conn.Close();

			comm = new NpgsqlCommand("SELECT person_id FROM _band_person WHERE band_id = " + id, Shared_data.conn);
			Shared_data.conn.Open();
			r = comm.ExecuteReader();
			List<int> sing_id = new List<int>();
			while (r.Read())
				sing_id.Add(r.GetInt32(0));
			Shared_data.conn.Close();

			for (int i = 0; i < members.Count; i++)
			{
				Item_person item = members[i] as Item_person;
				item.isCheck = sing_id.Exists(x => x == item.id);
			}
			FillMemberList();
		}

		public class Item_person
		{
			public bool isCheck { get; set; } = false;
			public int id { get; }
			public string name { get; }
			public Item_person(int id, string name)
			{
				this.id = id;
				this.name = name;
			}
		}

		private void B_add_Click(object sender, RoutedEventArgs e)
		{
			if (id == null)
			{
				NpgsqlCommand comm = new NpgsqlCommand("INSERT INTO m_band(name)VALUES($$" + tb_name.Text + "$$) RETURNING id; ", Shared_data.conn);
				Shared_data.conn.Open();
				int new_id = (int)comm.ExecuteScalar();
				Shared_data.conn.Close();

				List<Item_person> sub_member = new List<Item_person>();
				for (int i = 0; i < members.Count; i++)
				{
					Item_person item = members[i] as Item_person;
					if (item.isCheck)
						sub_member.Add(new Item_person(item.id, item.name));
				}

				for (int i = 0; i < sub_member.Count; i++)
				{
					comm = new NpgsqlCommand("INSERT INTO _band_person(band_id, person_id)VALUES(" + new_id + ", " + sub_member[i].id + "); ", Shared_data.conn);
					Shared_data.conn.Open();
					comm.ExecuteNonQuery();
					Shared_data.conn.Close();
				}
			}
			else
			{
				NpgsqlCommand comm = new NpgsqlCommand("UPDATE m_band SET name = $$" + tb_name.Text + "$$ WHERE id = " + id, Shared_data.conn);
				Shared_data.conn.Open();
				comm.ExecuteNonQuery();
				Shared_data.conn.Close();

				comm = new NpgsqlCommand("DELETE FROM _band_person WHERE person_id = " + id, Shared_data.conn);
				Shared_data.conn.Open();
				comm.ExecuteNonQuery();
				Shared_data.conn.Close();

				List<Item_person> sub_member = new List<Item_person>();
				for (int i = 0; i < members.Count; i++)
				{
					Item_person item = members[i] as Item_person;
					if (item.isCheck)
						sub_member.Add(new Item_person(item.id, item.name));
				}

				for (int i = 0; i < sub_member.Count; i++)
				{
					comm = new NpgsqlCommand("INSERT INTO _band_person(band_id, person_id)VALUES(" + id + ", " + sub_member[i].id + "); ", Shared_data.conn);
					Shared_data.conn.Open();
					comm.ExecuteNonQuery();
					Shared_data.conn.Close();
				}
			}
			Close();
		}

		private void B_pickmembers_Click(object sender, RoutedEventArgs e)
		{
			if (members.Count == 0)
			{
				NpgsqlCommand comm = new NpgsqlCommand("SELECT id, name FROM person", Shared_data.conn);
				Shared_data.conn.Open();
				NpgsqlDataReader r = comm.ExecuteReader();
				while (r.Read())
				{
					members.Add(new Item_person(r.GetInt32(0), r.GetString(1)));
				}
				Shared_data.conn.Close();
			}
			Select_win select = new Select_win(members, Added_item.person);
			select.ShowDialog();
			FillMemberList();
		}
		

		void FillMemberList()
		{
			cb_members.Items.Clear();
			for (int i = 0; i < members.Count; i++)
			{
				Item_person item = members[i] as Item_person;
				if (item.isCheck)
					cb_members.Items.Add(item.name);
			}
			cb_members.IsEnabled = !cb_members.Items.IsEmpty;
			if (!cb_members.Items.IsEmpty)
				cb_members.SelectedIndex = 0;
			else
				cb_members.SelectedIndex = -1;
		}
	}
}
