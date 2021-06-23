using Npgsql;
using System.Collections.Generic;
using System.Windows;

namespace Medialib_proj
{
	/// <summary>
	/// Логика взаимодействия для Add_picture.xaml
	/// </summary>
	public partial class Add_picture : Window
    {
		int? id { get; } = null;
		List<object> genres = new List<object>();
		List<object> artists = new List<object>();

		public Add_picture()
        {
            InitializeComponent();
        }
		public Add_picture(int id)
		{
			InitializeComponent();
			this.id = id;
			Title = "Edit picture record";
			b_add.Content = "Save";

			NpgsqlCommand comm = new NpgsqlCommand("SELECT * FROM media_picture WHERE id = " + id, Shared_data.conn);
			Shared_data.conn.Open();
			NpgsqlDataReader r = comm.ExecuteReader();
			r.Read();
			tb_name.Text = r.GetString(1);
			num_year.Value = r.GetInt32(2);
			tb_descript.Text = r.GetValue(3).ToString();
			Shared_data.conn.Close();

			comm = new NpgsqlCommand("SELECT id, name FROM p_genre", Shared_data.conn);
			Shared_data.conn.Open();
			r = comm.ExecuteReader();
			while (r.Read())
				genres.Add(new Item_genre(r.GetInt32(0), r.GetString(1)));
			Shared_data.conn.Close();

			comm = new NpgsqlCommand("SELECT genre_id FROM _picture_genre WHERE picture_id = " + id, Shared_data.conn);
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
				artists.Add(new Item_person(r.GetInt32(0), r.GetString(1)));
			Shared_data.conn.Close();

			comm = new NpgsqlCommand("SELECT person_id FROM _picture_artist WHERE picture_id = " + id, Shared_data.conn);
			Shared_data.conn.Open();
			r = comm.ExecuteReader();
			List<int> art_id = new List<int>();
			while (r.Read())
				art_id.Add(r.GetInt32(0));
			Shared_data.conn.Close();

			for (int i = 0; i < artists.Count; i++)
			{
				Item_person item = artists[i] as Item_person;
				item.isCheck = art_id.Exists(x => x == item.id);
			}
			FillArtistList();
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
				NpgsqlCommand comm = new NpgsqlCommand("INSERT INTO public.media_picture(name, year, description)VALUES($$" + tb_name.Text + "$$, " + num_year.Value + ", $$" + tb_descript.Text + "$$) RETURNING id; ", Shared_data.conn);
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
					comm = new NpgsqlCommand("INSERT INTO public._picture_genre(picture_id, genre_id)VALUES("+new_id+", "+sub_genre[i].id+"); ", Shared_data.conn);
					Shared_data.conn.Open();
					comm.ExecuteNonQuery();
					Shared_data.conn.Close();
				}

				List<Item_person> sub_artist = new List<Item_person>();
				for (int i = 0; i < artists.Count; i++)
				{
					Item_person item = artists[i] as Item_person;
					if (item.isCheck)
						sub_artist.Add(new Item_person(item.id, item.name));
				}

				for (int i = 0; i < sub_artist.Count; i++)
				{
					comm = new NpgsqlCommand("INSERT INTO public._picture_artist(picture_id, person_id)VALUES(" + new_id + ", " + sub_artist[i].id + "); ", Shared_data.conn);
					Shared_data.conn.Open();
					comm.ExecuteNonQuery();
					Shared_data.conn.Close();
				}
			}
			else
			{
				NpgsqlCommand comm = new NpgsqlCommand("UPDATE media_picture SET name = $$" + tb_name.Text + "$$, year = " + num_year.Value + ", description = $$" + tb_descript.Text + "$$ WHERE id = " + id, Shared_data.conn);
				Shared_data.conn.Open();
				comm.ExecuteNonQuery();
				Shared_data.conn.Close();

				comm = new NpgsqlCommand("DELETE FROM _picture_genre WHERE picture_id = " + id, Shared_data.conn);
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
					comm = new NpgsqlCommand("INSERT INTO public._picture_genre(picture_id, genre_id)VALUES(" + id + ", " + sub_genre[i].id + "); ", Shared_data.conn);
					Shared_data.conn.Open();
					comm.ExecuteNonQuery();
					Shared_data.conn.Close();
				}

				comm = new NpgsqlCommand("DELETE FROM _picture_artist WHERE picture_id = " + id, Shared_data.conn);
				Shared_data.conn.Open();
				comm.ExecuteNonQuery();
				Shared_data.conn.Close();

				List<Item_person> sub_artist = new List<Item_person>();
				for (int i = 0; i < artists.Count; i++)
				{
					Item_person item = artists[i] as Item_person;
					if (item.isCheck)
						sub_artist.Add(new Item_person(item.id, item.name));
				}

				for (int i = 0; i < sub_artist.Count; i++)
				{
					comm = new NpgsqlCommand("INSERT INTO public._picture_artist(picture_id, person_id)VALUES(" + id + ", " + sub_artist[i].id + "); ", Shared_data.conn);
					Shared_data.conn.Open();
					comm.ExecuteNonQuery();
					Shared_data.conn.Close();
				}
			}
			Close();
		}

		private void B_pickgenres_Click(object sender, RoutedEventArgs e)
		{
			if (genres.Count == 0)
			{
				NpgsqlCommand comm = new NpgsqlCommand("SELECT id, name FROM p_genre", Shared_data.conn);
				Shared_data.conn.Open();
				NpgsqlDataReader r = comm.ExecuteReader();
				while (r.Read())
					genres.Add(new Item_genre(r.GetInt32(0), r.GetString(1)));
				Shared_data.conn.Close();
			}
			Select_win select = new Select_win(genres, Added_item.picture_g);
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

		private void B_pickartisits_Click(object sender, RoutedEventArgs e)
		{
			if (artists.Count == 0)
			{
				NpgsqlCommand comm = new NpgsqlCommand("SELECT id, name FROM person", Shared_data.conn);
				Shared_data.conn.Open();
				NpgsqlDataReader r = comm.ExecuteReader();
				while (r.Read())
				{
					artists.Add(new Item_person(r.GetInt32(0), r.GetString(1)));
				}
				Shared_data.conn.Close();
			}
			Select_win select = new Select_win(artists, Added_item.person);
			select.ShowDialog();
			FillArtistList();
		}
		
		void FillArtistList()
		{
			cb_artists.Items.Clear();
			for (int i = 0; i < artists.Count; i++)
			{
				Item_person item = artists[i] as Item_person;
				if (item.isCheck)
					cb_artists.Items.Add(item.name);
			}
			cb_artists.IsEnabled = !cb_artists.Items.IsEmpty;
			if (!cb_artists.Items.IsEmpty)
				cb_artists.SelectedIndex = 0;
			else
				cb_artists.SelectedIndex = -1;
		}
	}
}
