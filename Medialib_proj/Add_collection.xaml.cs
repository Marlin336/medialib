using Npgsql;
using System.Collections.Generic;
using System.Windows;

namespace Medialib_proj
{
	/// <summary>
	/// Логика взаимодействия для Add_collection.xaml
	/// </summary>
	public partial class Add_collection : Window
    {
		int? id { get; } = null;
		List<object> movies = new List<object>();
		List<object> music = new List<object>();
		List<object> pictures = new List<object>();
		List<object> texts = new List<object>();
		public Add_collection()
		{
			InitializeComponent();
		}
		public Add_collection(int id)
		{
			InitializeComponent();
			this.id = id;
			b_add.Content = "Save";
			
			NpgsqlCommand comm = new NpgsqlCommand("SELECT name FROM collection WHERE id = " + id, Shared_data.conn);
			Shared_data.conn.Open();
			NpgsqlDataReader r = comm.ExecuteReader();
			r.Read();
			tb_name.Text = r.GetString(0);
			Shared_data.conn.Close();

			//Фильмы

			comm = new NpgsqlCommand("SELECT id, orig_name FROM view_film", Shared_data.conn);
			Shared_data.conn.Open();
			r = comm.ExecuteReader();
			while (r.Read())
			{
				movies.Add(new Item(r.GetInt32(0), r.GetString(1)));
			}
			Shared_data.conn.Close();

			comm = new NpgsqlCommand("SELECT film_id FROM _collection_f WHERE coll_id = " + id, Shared_data.conn);
			Shared_data.conn.Open();
			r = comm.ExecuteReader();
			List<int> movie_id = new List<int>();
			while (r.Read())
			{
				movie_id.Add(r.GetInt32(0));
			}
			Shared_data.conn.Close();

			for (int i = 0; i < movies.Count; i++)
			{
				Item item = movies[i] as Item;
				item.isCheck = movie_id.Exists(x => x == item.id);
			}
			FillMovieList();

			//Музыка
			comm = new NpgsqlCommand("SELECT id, name FROM view_music", Shared_data.conn);
			Shared_data.conn.Open();
			r = comm.ExecuteReader();
			while (r.Read())
			{
				music.Add(new Item(r.GetInt32(0), r.GetString(1)));
			}
			Shared_data.conn.Close();

			comm = new NpgsqlCommand("SELECT music_id FROM _collection_m WHERE coll_id = " + id, Shared_data.conn);
			Shared_data.conn.Open();
			r = comm.ExecuteReader();
			List<int> music_id = new List<int>();
			while (r.Read())
			{
				music_id.Add(r.GetInt32(0));
			}
			Shared_data.conn.Close();

			for (int i = 0; i < music.Count; i++)
			{
				Item item = music[i] as Item;
				item.isCheck = music_id.Exists(x => x == item.id);
			}
			FillMusicList();

			//Изображения
			comm = new NpgsqlCommand("SELECT id, name FROM view_picture", Shared_data.conn);
			Shared_data.conn.Open();
			r = comm.ExecuteReader();
			while (r.Read())
			{
				pictures.Add(new Item(r.GetInt32(0), r.GetString(1)));
			}
			Shared_data.conn.Close();

			comm = new NpgsqlCommand("SELECT picture_id FROM _collection_p WHERE coll_id = " + id, Shared_data.conn);
			Shared_data.conn.Open();
			r = comm.ExecuteReader();
			List<int> picture_id = new List<int>();
			while (r.Read())
			{
				picture_id.Add(r.GetInt32(0));
			}
			Shared_data.conn.Close();

			for (int i = 0; i < pictures.Count; i++)
			{
				Item item = pictures[i] as Item;
				item.isCheck = picture_id.Exists(x => x == item.id);
			}
			FillPictureList();

			//Тексты
			comm = new NpgsqlCommand("SELECT id, name FROM view_text", Shared_data.conn);
			Shared_data.conn.Open();
			r = comm.ExecuteReader();
			while (r.Read())
			{
				texts.Add(new Item(r.GetInt32(0), r.GetString(1)));
			}
			Shared_data.conn.Close();

			comm = new NpgsqlCommand("SELECT text_id FROM _collection_t WHERE coll_id = " + id, Shared_data.conn);
			Shared_data.conn.Open();
			r = comm.ExecuteReader();
			List<int> text_id = new List<int>();
			while (r.Read())
			{
				text_id.Add(r.GetInt32(0));
			}
			Shared_data.conn.Close();

			for (int i = 0; i < texts.Count; i++)
			{
				Item item = texts[i] as Item;
				item.isCheck = text_id.Exists(x => x == item.id);
			}
			FillTextList();
		}

		class Item
		{
			public bool isCheck { get; set; } = false;
			public int id { get; }
			public string name { get; }
			public Item(int id, string name)
			{
				this.id = id;
				this.name = name;
			}
		}

		private void B_add_Click(object sender, RoutedEventArgs e)
		{
			if (id == null)
			{
				NpgsqlCommand comm = new NpgsqlCommand("INSERT INTO collection(name) VALUES($$" + tb_name.Text + "$$) RETURNING id", Shared_data.conn);
				Shared_data.conn.Open();
				int new_id = (int)comm.ExecuteScalar();
				Shared_data.conn.Close();

				//Фильмы
				List<int> sub_movie = new List<int>();
				for (int i = 0; i < movies.Count; i++)
				{
					Item item = movies[i] as Item;
					if (item.isCheck)
						sub_movie.Add(item.id);
				}
				for (int i = 0; i < sub_movie.Count; i++)
				{
					comm = new NpgsqlCommand("INSERT INTO _collection_f(coll_id , film_id) VALUES(" + new_id + ", " + sub_movie[i] + ")", Shared_data.conn);
					Shared_data.conn.Open();
					comm.ExecuteNonQuery();
					Shared_data.conn.Close();
				}

				//Музыка
				List<int> sub_music = new List<int>();
				for (int i = 0; i < music.Count; i++)
				{
					Item item = music[i] as Item;
					if (item.isCheck)
						sub_music.Add(item.id);
				}
				for (int i = 0; i < sub_music.Count; i++)
				{
					comm = new NpgsqlCommand("INSERT INTO _collection_m(coll_id , music_id) VALUES(" + new_id + ", " + sub_music[i] + ")", Shared_data.conn);
					Shared_data.conn.Open();
					comm.ExecuteNonQuery();
					Shared_data.conn.Close();
				}

				//Изображения
				List<int> sub_picture = new List<int>();
				for (int i = 0; i < pictures.Count; i++)
				{
					Item item = pictures[i] as Item;
					if (item.isCheck)
						sub_picture.Add(item.id);
				}
				for (int i = 0; i < sub_picture.Count; i++)
				{
					comm = new NpgsqlCommand("INSERT INTO _collection_p(coll_id , picture_id) VALUES(" + new_id + ", " + sub_picture[i] + ")", Shared_data.conn);
					Shared_data.conn.Open();
					comm.ExecuteNonQuery();
					Shared_data.conn.Close();
				}

				//Тексты
				List<int> sub_text = new List<int>();
				for (int i = 0; i < texts.Count; i++)
				{
					Item item = texts[i] as Item;
					if (item.isCheck)
						sub_text.Add(item.id);
				}
				for (int i = 0; i < sub_text.Count; i++)
				{
					comm = new NpgsqlCommand("INSERT INTO _collection_t(coll_id , text_id) VALUES(" + new_id + ", " + sub_text[i] + ")", Shared_data.conn);
					Shared_data.conn.Open();
					comm.ExecuteNonQuery();
					Shared_data.conn.Close();
				}
			}
			else
			{
				NpgsqlCommand comm = new NpgsqlCommand("UPDATE collection SET name = $$" + tb_name.Text + "$$ WHERE id = " + id, Shared_data.conn);
				Shared_data.conn.Open();
				comm.ExecuteNonQuery();
				Shared_data.conn.Close();

				//Фильмы
				List<int> sub_movie = new List<int>();
				for (int i = 0; i < movies.Count; i++)
				{
					Item item = movies[i] as Item;
					if (item.isCheck)
						sub_movie.Add(item.id);
				}

				comm = new NpgsqlCommand("DELETE FROM _collection_f WHERE coll_id = " + id, Shared_data.conn);
				Shared_data.conn.Open();
				comm.ExecuteNonQuery();
				Shared_data.conn.Close();

				for (int i = 0; i < sub_movie.Count; i++)
				{
					comm = new NpgsqlCommand("INSERT INTO _collection_f(coll_id , film_id) VALUES(" + id + ", " + sub_movie[i] + ")", Shared_data.conn);
					Shared_data.conn.Open();
					comm.ExecuteNonQuery();
					Shared_data.conn.Close();
				}

				//Музыка
				List<int> sub_music = new List<int>();
				for (int i = 0; i < music.Count; i++)
				{
					Item item = music[i] as Item;
					if (item.isCheck)
						sub_music.Add(item.id);
				}

				comm = new NpgsqlCommand("DELETE FROM _collection_m WHERE coll_id = " + id, Shared_data.conn);
				Shared_data.conn.Open();
				comm.ExecuteNonQuery();
				Shared_data.conn.Close();

				for (int i = 0; i < sub_music.Count; i++)
				{
					comm = new NpgsqlCommand("INSERT INTO _collection_m(coll_id , music_id) VALUES(" + id + ", " + sub_music[i] + ")", Shared_data.conn);
					Shared_data.conn.Open();
					comm.ExecuteNonQuery();
					Shared_data.conn.Close();
				}

				//Изображения
				List<int> sub_picture = new List<int>();
				for (int i = 0; i < pictures.Count; i++)
				{
					Item item = pictures[i] as Item;
					if (item.isCheck)
						sub_picture.Add(item.id);
				}

				comm = new NpgsqlCommand("DELETE FROM _collection_p WHERE coll_id = " + id, Shared_data.conn);
				Shared_data.conn.Open();
				comm.ExecuteNonQuery();
				Shared_data.conn.Close();

				for (int i = 0; i < sub_picture.Count; i++)
				{
					comm = new NpgsqlCommand("INSERT INTO _collection_p(coll_id , picture_id) VALUES(" + id + ", " + sub_picture[i] + ")", Shared_data.conn);
					Shared_data.conn.Open();
					comm.ExecuteNonQuery();
					Shared_data.conn.Close();
				}

				//Тексты
				List<int> sub_text = new List<int>();
				for (int i = 0; i < texts.Count; i++)
				{
					Item item = texts[i] as Item;
					if (item.isCheck)
						sub_text.Add(item.id);
				}

				comm = new NpgsqlCommand("DELETE FROM _collection_t WHERE coll_id = " + id, Shared_data.conn);
				Shared_data.conn.Open();
				comm.ExecuteNonQuery();
				Shared_data.conn.Close();

				for (int i = 0; i < sub_text.Count; i++)
				{
					comm = new NpgsqlCommand("INSERT INTO _collection_t(coll_id , text_id) VALUES(" + id + ", " + sub_text[i] + ")", Shared_data.conn);
					Shared_data.conn.Open();
					comm.ExecuteNonQuery();
					Shared_data.conn.Close();
				}
			}
			Close();
		}

		private void B_movie_add_Click(object sender, RoutedEventArgs e)
		{
			if (movies.Count == 0)
			{
				NpgsqlCommand comm = new NpgsqlCommand("SELECT id, orig_name FROM view_film", Shared_data.conn);
				Shared_data.conn.Open();
				NpgsqlDataReader r = comm.ExecuteReader();
				while (r.Read())
					movies.Add(new Item(r.GetInt32(0), r.GetString(1)));
				Shared_data.conn.Close();
			}
			Select_win select = new Select_win(movies, Added_item.movie);
			select.ShowDialog();
			FillMovieList();
		}

		void FillMovieList()
		{
			cb_movies.Items.Clear();
			for (int i = 0; i < movies.Count; i++)
			{
				Item item = movies[i] as Item;
				if (item.isCheck)
					cb_movies.Items.Add(item.name);
			}
			cb_movies.IsEnabled = !cb_movies.Items.IsEmpty;
			if (!cb_movies.Items.IsEmpty)
				cb_movies.SelectedIndex = 0;
			else
				cb_movies.SelectedIndex = -1;
		}

		private void B_music_add_Click(object sender, RoutedEventArgs e)
		{
			if (music.Count == 0)
			{
				NpgsqlCommand comm = new NpgsqlCommand("SELECT id, name FROM view_music", Shared_data.conn);
				Shared_data.conn.Open();
				NpgsqlDataReader r = comm.ExecuteReader();
				while (r.Read())
					music.Add(new Item(r.GetInt32(0), r.GetString(1)));
				Shared_data.conn.Close();
			}
			Select_win select = new Select_win(music, Added_item.music);
			select.ShowDialog();
			FillMusicList();
		}

		void FillMusicList()
		{
			cb_music.Items.Clear();
			for (int i = 0; i < music.Count; i++)
			{
				Item item = music[i] as Item;
				if (item.isCheck)
					cb_music.Items.Add(item.name);
			}
			cb_music.IsEnabled = !cb_music.Items.IsEmpty;
			if (!cb_music.Items.IsEmpty)
				cb_music.SelectedIndex = 0;
			else
				cb_music.SelectedIndex = -1;
		}

		private void B_picture_add_Click(object sender, RoutedEventArgs e)
		{
			if (pictures.Count == 0)
			{
				NpgsqlCommand comm = new NpgsqlCommand("SELECT id, name FROM view_picture", Shared_data.conn);
				Shared_data.conn.Open();
				NpgsqlDataReader r = comm.ExecuteReader();
				while (r.Read())
					pictures.Add(new Item(r.GetInt32(0), r.GetString(1)));
				Shared_data.conn.Close();
			}
			Select_win select = new Select_win(pictures, Added_item.picture);
			select.ShowDialog();
			FillPictureList();
		}

		void FillPictureList()
		{
			cb_pictures.Items.Clear();
			for (int i = 0; i < pictures.Count; i++)
			{
				Item item = pictures[i] as Item;
				if (item.isCheck)
					cb_pictures.Items.Add(item.name);
			}
			cb_pictures.IsEnabled = !cb_pictures.Items.IsEmpty;
			if (!cb_pictures.Items.IsEmpty)
				cb_pictures.SelectedIndex = 0;
			else
				cb_pictures.SelectedIndex = -1;
		}

		private void B_text_add_Click(object sender, RoutedEventArgs e)
		{
			if (texts.Count == 0)
			{
				NpgsqlCommand comm = new NpgsqlCommand("SELECT id, name FROM view_text", Shared_data.conn);
				Shared_data.conn.Open();
				NpgsqlDataReader r = comm.ExecuteReader();
				while (r.Read())
					texts.Add(new Item(r.GetInt32(0), r.GetString(1)));
				Shared_data.conn.Close();
			}
			Select_win select = new Select_win(texts, Added_item.text);
			select.ShowDialog();
			FillTextList();
		}

		void FillTextList()
		{
			cb_texts.Items.Clear();
			for (int i = 0; i < texts.Count; i++)
			{
				Item item = texts[i] as Item;
				if (item.isCheck)
					cb_texts.Items.Add(item.name);
			}
			cb_texts.IsEnabled = !cb_texts.Items.IsEmpty;
			if (!cb_texts.Items.IsEmpty)
				cb_texts.SelectedIndex = 0;
			else
				cb_texts.SelectedIndex = -1;
		}
	}
}
