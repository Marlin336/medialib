using Npgsql;
using System.Collections.Generic;
using System.Windows;

namespace Medialib_proj
{
	/// <summary>
	/// Логика взаимодействия для Add_album.xaml
	/// </summary>
	public partial class Add_album : Window
    {
		int? id { get; } = null;
		List<object> bands = new List<object>();
        public Add_album()
        {
            InitializeComponent();
        }


		public Add_album(int id):this()
		{
			this.id = id;
			b_add.Content = "Save";
			//Заполнение полей
			NpgsqlCommand comm = new NpgsqlCommand("SELECT name FROM m_album", Shared_data.conn);
			Shared_data.conn.Open();
			NpgsqlDataReader r = comm.ExecuteReader();
			r.Read();
			tb_name.Text = r.GetString(0);
			Shared_data.conn.Close();
		}

		public class Item_band
		{
			public bool isCheck { get; set; } = false;
			public int id { get; }
			public string name { get; }
			public Item_band(int id , string name)
			{
				this.id = id;
				this.name = name;
			}
		}

		private void B_pickband_Click(object sender, RoutedEventArgs e)
		{
			if (bands.Count == 0)
			{
				NpgsqlCommand comm = new NpgsqlCommand("SELECT id, name FROM m_band", Shared_data.conn);
				Shared_data.conn.Open();
				NpgsqlDataReader r = comm.ExecuteReader();
				while (r.Read())
					bands.Add(new Item_band(r.GetInt32(0), r.GetString(1)));
				Shared_data.conn.Close();
			}
			Select_win select = new Select_win(bands, Added_item.band);
			select.ShowDialog();
			FillBandField();
		}

		void FillBandField()
		{
			tb_band.Text = string.Empty;
			for (int i = 0; i < bands.Count; i++)
			{
				Item_band item = bands[i] as Item_band;
				if (item.isCheck)
				{
					tb_band.Text = item.name;
					break;
				}
			}
		}

		private void B_add_Click(object sender, RoutedEventArgs e)
		{
			if (id == null)
			{
				int band_id = 0;
				for (int i = 0; i < bands.Count; i++)
				{
					Item_band item = bands[i] as Item_band;
					if (item.isCheck)
						band_id = item.id;
				}

				NpgsqlCommand comm = new NpgsqlCommand("INSERT INTO m_album(name, band_id) VALUES($$" + tb_name.Text + "$$, " + band_id + ") RETURNING id; ", Shared_data.conn);
				Shared_data.conn.Open();
				int new_id = (int)comm.ExecuteScalar();
				Shared_data.conn.Close();
			}
			else
			{
				int band_id = 0;
				for (int i = 0; i < bands.Count; i++)
				{
					Item_band item = bands[i] as Item_band;
					if (item.isCheck)
						band_id = item.id;
				}

				NpgsqlCommand comm = new NpgsqlCommand("UPDATE m_album SET band_id = " + band_id + ", name = $$" + tb_name.Text + "$$", Shared_data.conn);
				Shared_data.conn.Open();
				comm.ExecuteNonQuery();
				Shared_data.conn.Close();
			}
			Close();
		}
	}
}
