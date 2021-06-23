using Npgsql;
using System.Collections.Generic;
using System.Windows;

namespace Medialib_proj
{
	/// <summary>
	/// Логика взаимодействия для Add_music.xaml
	/// </summary>
	public partial class Add_music : Window
    {
		int? id { get; } = null;
		List<object> albums = new List<object>();
		List<object> genres = new List<object>();
		List<object> singers = new List<object>();
		List<object> composers = new List<object>();

        public Add_music()
        {
            InitializeComponent();
        }
		public Add_music(int id)
		{
			InitializeComponent();
			this.id = id;
			Title = "Edit music record";
			b_add.Content = "Save";
			//Заполнить поля

			NpgsqlCommand comm = new NpgsqlCommand("SELECT * FROM media_music WHERE id = " + id, Shared_data.conn);
			Shared_data.conn.Open();
			NpgsqlDataReader r = comm.ExecuteReader();
			r.Read();
			tb_name.Text = r.GetString(1);
			num_year.Value = r.GetInt32(2);
			num_number.Value = r.GetInt32(4);
			tp_duration.Text = r.GetTimeSpan(5).ToString();
			tb_descript.Text = r.GetValue(6).ToString();
			Shared_data.conn.Close();

			comm = new NpgsqlCommand("SELECT id, name FROM m_genre", Shared_data.conn);
			Shared_data.conn.Open();
			r = comm.ExecuteReader();
			while (r.Read())
				genres.Add(new Item_genre(r.GetInt32(0), r.GetString(1)));
			Shared_data.conn.Close();

			comm = new NpgsqlCommand("SELECT genre_id FROM _music_genre WHERE music_id = " + id, Shared_data.conn);
			Shared_data.conn.Open();
			r = comm.ExecuteReader();
			List<int> gen_id = new List<int>();
			while (r.Read())
				gen_id.Add(r.GetInt32(0));
			Shared_data.conn.Close();

			for (int i = 0; i < genres.Count; i++)
			{
				Item_genre item = genres[i] as Item_genre;
				item.isCheck = gen_id.Exists(x => x == item.id);
			}
			FillGenreList();

			comm = new NpgsqlCommand("SELECT id, name FROM person", Shared_data.conn);
			Shared_data.conn.Open();
			r = comm.ExecuteReader();
			while (r.Read())
				composers.Add(new Item_person(r.GetInt32(0), r.GetString(1)));
			Shared_data.conn.Close();

			comm = new NpgsqlCommand("SELECT person_id FROM _music_composer WHERE music_id = " + id, Shared_data.conn);
			Shared_data.conn.Open();
			r = comm.ExecuteReader();
			List<int> compos_id = new List<int>();
			while (r.Read())
				compos_id.Add(r.GetInt32(0));
			Shared_data.conn.Close();

			for (int i = 0; i < composers.Count; i++)
			{
				Item_person item = composers[i] as Item_person;
				item.isCheck = compos_id.Exists(x => x == item.id);
			}
			FillComposerList();

			comm = new NpgsqlCommand("SELECT id, name FROM person", Shared_data.conn);
			Shared_data.conn.Open();
			r = comm.ExecuteReader();
			while (r.Read())
				singers.Add(new Item_person(r.GetInt32(0), r.GetString(1)));
			Shared_data.conn.Close();

			comm = new NpgsqlCommand("SELECT person_id FROM _music_singer WHERE music_id = " + id, Shared_data.conn);
			Shared_data.conn.Open();
			r = comm.ExecuteReader();
			List<int> sing_id = new List<int>();
			while (r.Read())
				sing_id.Add(r.GetInt32(0));
			Shared_data.conn.Close();

			for (int i = 0; i < singers.Count; i++)
			{
				Item_person item = singers[i] as Item_person;
				item.isCheck = sing_id.Exists(x => x == item.id);
			}
			FillSingerList();

			comm = new NpgsqlCommand("SELECT alb.id, band.name, alb.name FROM m_album as alb LEFT JOIN m_band as band ON band.id = alb.band_id", Shared_data.conn);
			Shared_data.conn.Open();
			r = comm.ExecuteReader();
			while (r.Read())
				albums.Add(new Item_album(r.GetInt32(0), r.GetString(1), r.GetString(2)));
			Shared_data.conn.Close();

			comm = new NpgsqlCommand("SELECT album_id FROM media_music WHERE id = " + id, Shared_data.conn);
			Shared_data.conn.Open();
			r = comm.ExecuteReader();
			int album_id;
			r.Read();
			album_id = r.GetInt32(0);
			Shared_data.conn.Close();

			for (int i = 0; i < albums.Count; i++)
			{
				Item_album item = albums[i] as Item_album;
				item.isCheck = item.id == album_id;
			}
			FillAlbumField();
		}

		public class Item_album
		{
			public bool isCheck { get; set; } = false;
			public int id { get; }
			public string band { get; }
			public string name { get; }
			public Item_album(int id, string band, string name)
			{
				this.id = id;
				this.band = band;
				this.name = name;
			}
		}
		public class Item_genre
		{
			public bool isCheck { get; set; } = false;
			public int id { get; }
			public string name { get; }
			public Item_genre(int id, string name)
			{
				this.id = id;
				this.name = name;
			}
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
				int alb_id = 0;
				for (int i = 0; i < albums.Count; i++)
				{
					Item_album item = albums[i] as Item_album;
					if (item.isCheck)
						alb_id = item.id;
				}

				NpgsqlCommand comm = new NpgsqlCommand("INSERT INTO public.media_music(name, year, album_id, number, duration, description)VALUES($$" + tb_name.Text + "$$, " + num_year.Value + ", " + alb_id + ", " + num_number.Value + ", '"+ tp_duration.Value.Value.ToLongTimeString() + "', $$" + tb_descript.Text + "$$) RETURNING id; ", Shared_data.conn);
				Shared_data.conn.Open();
				int new_id = (int)comm.ExecuteScalar();
				Shared_data.conn.Close();

				List<Item_genre> sub_genre = new List<Item_genre>();
				for (int i = 0; i < genres.Count; i++)
				{
					Item_genre item = genres[i] as Item_genre;
					if (item.isCheck)
						sub_genre.Add(new Item_genre(item.id, item.name));
				}

				for (int i = 0; i < sub_genre.Count; i++)
				{
					comm = new NpgsqlCommand("INSERT INTO _music_genre(music_id, genre_id)VALUES(" + new_id + ", " + sub_genre[i].id + "); ", Shared_data.conn);
					Shared_data.conn.Open();
					comm.ExecuteNonQuery();
					Shared_data.conn.Close();
				}

				List<Item_person> sub_composer = new List<Item_person>();
				for (int i = 0; i < composers.Count; i++)
				{
					Item_person item = composers[i] as Item_person;
					if (item.isCheck)
						sub_composer.Add(new Item_person(item.id, item.name));
				}

				for (int i = 0; i < sub_composer.Count; i++)
				{
					comm = new NpgsqlCommand("INSERT INTO _music_composer(music_id, person_id)VALUES(" + new_id + ", " + sub_composer[i].id + "); ", Shared_data.conn);
					Shared_data.conn.Open();
					comm.ExecuteNonQuery();
					Shared_data.conn.Close();
				}

				List<Item_person> sub_singer = new List<Item_person>();
				for (int i = 0; i < singers.Count; i++)
				{
					Item_person item = singers[i] as Item_person;
					if (item.isCheck)
						sub_singer.Add(new Item_person(item.id, item.name));
				}

				for (int i = 0; i < sub_singer.Count; i++)
				{
					comm = new NpgsqlCommand("INSERT INTO _music_singer(music_id, person_id)VALUES(" + new_id + ", " + sub_singer[i].id + "); ", Shared_data.conn);
					Shared_data.conn.Open();
					comm.ExecuteNonQuery();
					Shared_data.conn.Close();
				}
			}
			else
			{
				int alb_id = 0;
				for (int i = 0; i < albums.Count; i++)
				{
					Item_album item = albums[i] as Item_album;
					if (item.isCheck)
						alb_id = item.id;
				}
				NpgsqlCommand comm = new NpgsqlCommand("UPDATE media_music SET name = $$" + tb_name.Text + "$$, year = " + num_year.Value + ", album_id = " + alb_id + ", number = " + num_number.Value + ", duration = '" + tp_duration.Value.Value.ToLongTimeString() + "', description = $$" + tb_descript.Text + "$$ WHERE id = " + id, Shared_data.conn);
				Shared_data.conn.Open();
				comm.ExecuteNonQuery();
				Shared_data.conn.Close();

				comm = new NpgsqlCommand("DELETE FROM _music_genre WHERE music_id = " + id, Shared_data.conn);
				Shared_data.conn.Open();
				comm.ExecuteNonQuery();
				Shared_data.conn.Close();

				List<Item_genre> sub_genre = new List<Item_genre>();
				for (int i = 0; i < genres.Count; i++)
				{
					Item_genre item = genres[i] as Item_genre;
					if (item.isCheck)
						sub_genre.Add(new Item_genre(item.id, item.name));
				}

				for (int i = 0; i < sub_genre.Count; i++)
				{
					comm = new NpgsqlCommand("INSERT INTO _music_genre(music_id, genre_id)VALUES(" + id + ", " + sub_genre[i].id + "); ", Shared_data.conn);
					Shared_data.conn.Open();
					comm.ExecuteNonQuery();
					Shared_data.conn.Close();
				}

				comm = new NpgsqlCommand("DELETE FROM _music_composer WHERE music_id = " + id, Shared_data.conn);
				Shared_data.conn.Open();
				comm.ExecuteNonQuery();
				Shared_data.conn.Close();

				List<Item_person> sub_composer = new List<Item_person>();
				for (int i = 0; i < composers.Count; i++)
				{
					Item_person item = composers[i] as Item_person;
					if (item.isCheck)
						sub_composer.Add(new Item_person(item.id, item.name));
				}

				for (int i = 0; i < sub_composer.Count; i++)
				{
					comm = new NpgsqlCommand("INSERT INTO _music_composer(music_id, person_id)VALUES(" + id + ", " + sub_composer[i].id + "); ", Shared_data.conn);
					Shared_data.conn.Open();
					comm.ExecuteNonQuery();
					Shared_data.conn.Close();
				}

				comm = new NpgsqlCommand("DELETE FROM _music_singer WHERE music_id = " + id, Shared_data.conn);
				Shared_data.conn.Open();
				comm.ExecuteNonQuery();
				Shared_data.conn.Close();

				List<Item_person> sub_singer = new List<Item_person>();
				for (int i = 0; i < singers.Count; i++)
				{
					Item_person item = singers[i] as Item_person;
					if (item.isCheck)
						sub_singer.Add(new Item_person(item.id, item.name));
				}

				for (int i = 0; i < sub_singer.Count; i++)
				{
					comm = new NpgsqlCommand("INSERT INTO _music_singer(music_id, person_id)VALUES(" + id + ", " + sub_singer[i].id + "); ", Shared_data.conn);
					Shared_data.conn.Open();
					comm.ExecuteNonQuery();
					Shared_data.conn.Close();
				}
			}
			Close();
		}

		private void B_pickalbum_Click(object sender, RoutedEventArgs e)
		{
			if (genres.Count == 0)
			{
				NpgsqlCommand comm = new NpgsqlCommand("SELECT alb.id, band.name, alb.name FROM m_album as alb LEFT JOIN m_band as band ON band.id = alb.band_id", Shared_data.conn);
				Shared_data.conn.Open();
				NpgsqlDataReader r = comm.ExecuteReader();
				while (r.Read())
					albums.Add(new Item_album(r.GetInt32(0), r.GetString(1), r.GetString(2)));
				Shared_data.conn.Close();
			}
			Select_win select = new Select_win(albums, Added_item.album);
			select.ShowDialog();
			FillAlbumField();
		}

		void FillAlbumField()
		{
			tb_album.Text = string.Empty;
			for (int i = 0; i < albums.Count; i++)
			{
				Item_album item = albums[i] as Item_album;
				if (item.isCheck)
				{
					tb_album.Text = item.name + " (" + item.band + ")";
					break;
				}
			}
		}

		private void B_pickgenres_Click(object sender, RoutedEventArgs e)
		{
			if (genres.Count == 0)
			{
				NpgsqlCommand comm = new NpgsqlCommand("SELECT id, name FROM m_genre", Shared_data.conn);
				Shared_data.conn.Open();
				NpgsqlDataReader r = comm.ExecuteReader();
				while (r.Read())
					genres.Add(new Item_genre(r.GetInt32(0), r.GetString(1)));
				Shared_data.conn.Close();
			}
			Select_win select = new Select_win(genres, Added_item.music_g);
			select.ShowDialog();
			FillGenreList();
		}

		void FillGenreList()
		{
			cb_genres.Items.Clear();
			for (int i = 0; i < genres.Count; i++)
			{
				Item_genre item = genres[i] as Item_genre;
				if (item.isCheck)
					cb_genres.Items.Add(item.name);
			}
			cb_genres.IsEnabled = !cb_genres.Items.IsEmpty;
			if (!cb_genres.Items.IsEmpty)
				cb_genres.SelectedIndex = 0;
			else
				cb_genres.SelectedIndex = -1;
		}

		private void B_pickcomposers_Click(object sender, RoutedEventArgs e)
		{
			if (composers.Count == 0)
			{
				NpgsqlCommand comm = new NpgsqlCommand("SELECT id, name FROM person", Shared_data.conn);
				Shared_data.conn.Open();
				NpgsqlDataReader r = comm.ExecuteReader();
				while (r.Read())
				{
					composers.Add(new Item_person(r.GetInt32(0), r.GetString(1)));
				}
				Shared_data.conn.Close();
			}
			Select_win select = new Select_win(composers, Added_item.person);
			select.ShowDialog();
			FillComposerList();
		}

		void FillComposerList()
		{
			cb_composers.Items.Clear();
			for (int i = 0; i < composers.Count; i++)
			{
				Item_person item = composers[i] as Item_person;
				if (item.isCheck)
					cb_composers.Items.Add(item.name);
			}
			cb_composers.IsEnabled = !cb_composers.Items.IsEmpty;
			if (!cb_composers.Items.IsEmpty)
				cb_composers.SelectedIndex = 0;
			else
				cb_composers.SelectedIndex = -1;
		}

		private void B_picksingers_Click(object sender, RoutedEventArgs e)
		{
			if (singers.Count == 0)
			{
				NpgsqlCommand comm = new NpgsqlCommand("SELECT id, name FROM person", Shared_data.conn);
				Shared_data.conn.Open();
				NpgsqlDataReader r = comm.ExecuteReader();
				while (r.Read())
				{
					singers.Add(new Item_person(r.GetInt32(0), r.GetString(1)));
				}
				Shared_data.conn.Close();
			}
			Select_win select = new Select_win(singers, Added_item.person);
			select.ShowDialog();
			FillSingerList();
		}

		void FillSingerList()
		{
			cb_singers.Items.Clear();
			for (int i = 0; i < singers.Count; i++)
			{
				Item_person item = singers[i] as Item_person;
				if (item.isCheck)
					cb_singers.Items.Add(item.name);
			}
			cb_singers.IsEnabled = !cb_singers.Items.IsEmpty;
			if (!cb_singers.Items.IsEmpty)
				cb_singers.SelectedIndex = 0;
			else
				cb_singers.SelectedIndex = -1;
		}
	}
}
