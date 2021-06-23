using Npgsql;
using System.Collections.Generic;
using System.Windows;

namespace Medialib_proj
{
	/// <summary>
	/// Логика взаимодействия для Add_movie.xaml
	/// </summary>
	public partial class Add_movie : Window
    {
		int? id { get; } = null;
		List<Item_genre> languages = new List<Item_genre>();
		List<object> genres = new List<object>();
		List<object> producers = new List<object>();
		List<object> actors = new List<object>();
        public Add_movie()
        {
            InitializeComponent();
			cb_lang.ItemsSource = languages;

			NpgsqlCommand comm = new NpgsqlCommand("SELECT id, name FROM f_language", Shared_data.conn);
			Shared_data.conn.Open();
			NpgsqlDataReader r = comm.ExecuteReader();
			while (r.Read())
				languages.Add(new Item_genre(r.GetInt32(0), r.GetString(1)));
			Shared_data.conn.Close();
		}
		public Add_movie(int id):this()
		{
			this.id = id;
			b_add.Content = "Save";
			Title = "Edit movie record";

			NpgsqlCommand comm = new NpgsqlCommand("SELECT * FROM view_film WHERE id = " + id, Shared_data.conn);
			Shared_data.conn.Open();
			NpgsqlDataReader r = comm.ExecuteReader();
			r.Read();
			tb_name.Text = r.GetString(1);
			num_year.Value = r.GetInt32(3);
			tb_place.Text = r.GetValue(4).ToString();
			tb_descript.Text = r.GetValue(5).ToString();
			tp_duration.Text = r.GetTimeSpan(6).ToString();
			Shared_data.conn.Close();

			comm = new NpgsqlCommand("SELECT origlang_id FROM media_film WHERE id = " + id, Shared_data.conn);
			Shared_data.conn.Open();
			r = comm.ExecuteReader();
			r.Read();
			cb_lang.SelectedItem = languages.Find(x => x.id == r.GetInt32(0));
			languages.Find(x => x.id == r.GetInt32(0)).isCheck = true;
			Shared_data.conn.Close();

			//Жанры
			comm = new NpgsqlCommand("SELECT id, name FROM f_genre", Shared_data.conn);
			Shared_data.conn.Open();
			r = comm.ExecuteReader();
			while (r.Read())
				genres.Add(new Item_genre(r.GetInt32(0), r.GetString(1)));
			Shared_data.conn.Close();

			comm = new NpgsqlCommand("SELECT genre_id FROM _film_genre WHERE film_id = " + id, Shared_data.conn);
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

			//Продюсеры
			comm = new NpgsqlCommand("SELECT id, name FROM person", Shared_data.conn);
			Shared_data.conn.Open();
			r = comm.ExecuteReader();
			while (r.Read())
				producers.Add(new Item_person(r.GetInt32(0), r.GetString(1)));
			Shared_data.conn.Close();

			comm = new NpgsqlCommand("SELECT person_id FROM _film_director WHERE film_id = " + id, Shared_data.conn);
			Shared_data.conn.Open();
			r = comm.ExecuteReader();
			List<int> producer_id = new List<int>();
			while (r.Read())
				producer_id.Add(r.GetInt32(0));
			Shared_data.conn.Close();

			for (int i = 0; i < producers.Count; i++)
			{
				Item_person item = producers[i] as Item_person;
				item.isCheck = producer_id.Exists(x => x == item.id);
			}
			FillProducerList();

			//Актёры
			comm = new NpgsqlCommand("SELECT id, name FROM person", Shared_data.conn);
			Shared_data.conn.Open();
			r = comm.ExecuteReader();
			while (r.Read())
				actors.Add(new Item_person(r.GetInt32(0), r.GetString(1)));
			Shared_data.conn.Close();

			comm = new NpgsqlCommand("SELECT person_id FROM _film_actor WHERE film_id = " + id, Shared_data.conn);
			Shared_data.conn.Open();
			r = comm.ExecuteReader();
			List<int> actor_id = new List<int>();
			while (r.Read())
				actor_id.Add(r.GetInt32(0));
			Shared_data.conn.Close();

			for (int i = 0; i < actors.Count; i++)
			{
				Item_person item = actors[i] as Item_person;
				item.isCheck = actor_id.Exists(x => x == item.id);
			}
			FillActorList();
		}

		#region classes
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
		#endregion

		private void B_add_Click(object sender, RoutedEventArgs e)
		{
			if (id == null)
			{
				Item_genre new_lang = cb_lang.SelectedItem as Item_genre;

				NpgsqlCommand comm = new NpgsqlCommand("SELECT * FROM new_film_id(" + new_lang.id + ", $$" + tb_name.Text + "$$, " + num_year.Value + "::smallint, $$" + tb_place.Text + "$$, $$" + tb_descript.Text + "$$, '" + tp_duration.Value.Value.ToLongTimeString() + "'::interval)", Shared_data.conn);
				Shared_data.conn.Open();
				NpgsqlDataReader r = comm.ExecuteReader();
				r.Read();
				int new_id = r.GetInt32(0);
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
					comm = new NpgsqlCommand("INSERT INTO _film_genre(film_id, genre_id)VALUES(" + new_id + ", " + sub_genre[i].id + "); ", Shared_data.conn);
					Shared_data.conn.Open();
					comm.ExecuteNonQuery();
					Shared_data.conn.Close();
				}

				List<Item_person> sub_producer = new List<Item_person>();
				for (int i = 0; i < producers.Count; i++)
				{
					Item_person item = producers[i] as Item_person;
					if (item.isCheck)
						sub_producer.Add(new Item_person(item.id, item.name));
				}

				for (int i = 0; i < sub_producer.Count; i++)
				{
					comm = new NpgsqlCommand("INSERT INTO _film_director(film_id, person_id)VALUES(" + new_id + ", " + sub_producer[i].id + "); ", Shared_data.conn);
					Shared_data.conn.Open();
					comm.ExecuteNonQuery();
					Shared_data.conn.Close();
				}

				List<Item_person> sub_actor = new List<Item_person>();
				for (int i = 0; i < actors.Count; i++)
				{
					Item_person item = actors[i] as Item_person;
					if (item.isCheck)
						sub_actor.Add(new Item_person(item.id, item.name));
				}

				for (int i = 0; i < sub_actor.Count; i++)
				{
					comm = new NpgsqlCommand("INSERT INTO _film_actor(film_id, person_id)VALUES(" + new_id + ", " + sub_actor[i].id + "); ", Shared_data.conn);
					Shared_data.conn.Open();
					comm.ExecuteNonQuery();
					Shared_data.conn.Close();
				}

			}
			else
			{
				Item_genre old_lang = languages.Find(x => x.isCheck);
				Item_genre new_lang = cb_lang.SelectedItem as Item_genre;
				NpgsqlCommand comm = new NpgsqlCommand("DELETE FROM _lang_film WHERE film_id = " + id + " AND lang_id = " + new_lang.id, Shared_data.conn);
				Shared_data.conn.Open();
				comm.ExecuteNonQuery();
				Shared_data.conn.Close();

				comm = new NpgsqlCommand("INSERT INTO _lang_film (lang_id, film_id, value) VALUES(" + new_lang.id + ", " + id + " , $$" + tb_name.Text + "$$)", Shared_data.conn);
				Shared_data.conn.Open();
				comm.ExecuteNonQuery();
				Shared_data.conn.Close();

				Item_genre lang = cb_lang.SelectedItem as Item_genre;
				comm = new NpgsqlCommand("UPDATE media_film SET origlang_id = " + new_lang.id + ", year = " + num_year.Value + ", place = $$" + tb_place.Text + "$$, description = $$" + tb_descript.Text + "$$, duration = '" + tp_duration.Value.Value.ToLongTimeString() + "' WHERE id = " + id, Shared_data.conn);
				Shared_data.conn.Open();
				comm.ExecuteNonQuery();
				Shared_data.conn.Close();

				comm = new NpgsqlCommand("DELETE FROM _film_genre WHERE film_id = " + id, Shared_data.conn);
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
					comm = new NpgsqlCommand("INSERT INTO _film_genre(film_id, genre_id)VALUES(" + id + ", " + sub_genre[i].id + "); ", Shared_data.conn);
					Shared_data.conn.Open();
					comm.ExecuteNonQuery();
					Shared_data.conn.Close();
				}

				comm = new NpgsqlCommand("DELETE FROM _film_director WHERE film_id = " + id, Shared_data.conn);
				Shared_data.conn.Open();
				comm.ExecuteNonQuery();
				Shared_data.conn.Close();

				List<Item_person> sub_producer = new List<Item_person>();
				for (int i = 0; i < producers.Count; i++)
				{
					Item_person item = producers[i] as Item_person;
					if (item.isCheck)
						sub_producer.Add(new Item_person(item.id, item.name));
				}

				for (int i = 0; i < sub_producer.Count; i++)
				{
					comm = new NpgsqlCommand("INSERT INTO _film_director(film_id, person_id)VALUES(" + id + ", " + sub_producer[i].id + "); ", Shared_data.conn);
					Shared_data.conn.Open();
					comm.ExecuteNonQuery();
					Shared_data.conn.Close();
				}

				comm = new NpgsqlCommand("DELETE FROM _film_actor WHERE film_id = " + id, Shared_data.conn);
				Shared_data.conn.Open();
				comm.ExecuteNonQuery();
				Shared_data.conn.Close();

				List<Item_person> sub_actor = new List<Item_person>();
				for (int i = 0; i < actors.Count; i++)
				{
					Item_person item = actors[i] as Item_person;
					if (item.isCheck)
						sub_actor.Add(new Item_person(item.id, item.name));
				}

				for (int i = 0; i < sub_actor.Count; i++)
				{
					comm = new NpgsqlCommand("INSERT INTO _film_actor(film_id, person_id)VALUES(" + id + ", " + sub_actor[i].id + "); ", Shared_data.conn);
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
				NpgsqlCommand comm = new NpgsqlCommand("SELECT id, name FROM f_genre", Shared_data.conn);
				Shared_data.conn.Open();
				NpgsqlDataReader r = comm.ExecuteReader();
				while (r.Read())
					genres.Add(new Item_genre(r.GetInt32(0), r.GetString(1)));
				Shared_data.conn.Close();
			}
			Select_win select = new Select_win(genres, Added_item.movie_g);
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

		private void B_pickproducers_Click(object sender, RoutedEventArgs e)
		{
			if (producers.Count == 0)
			{
				NpgsqlCommand comm = new NpgsqlCommand("SELECT id, name FROM person", Shared_data.conn);
				Shared_data.conn.Open();
				NpgsqlDataReader r = comm.ExecuteReader();
				while (r.Read())
				{
					producers.Add(new Item_person(r.GetInt32(0), r.GetString(1)));
				}
				Shared_data.conn.Close();
			}
			Select_win select = new Select_win(producers, Added_item.person);
			select.ShowDialog();
			FillProducerList();
		}

		void FillProducerList()
		{
			cb_producers.Items.Clear();
			for (int i = 0; i < producers.Count; i++)
			{
				Item_person item = producers[i] as Item_person;
				if (item.isCheck)
					cb_producers.Items.Add(item.name);
			}
			cb_producers.IsEnabled = !cb_producers.Items.IsEmpty;
			if (!cb_producers.Items.IsEmpty)
				cb_producers.SelectedIndex = 0;
			else
				cb_producers.SelectedIndex = -1;
		}

		private void B_pickactors_Click(object sender, RoutedEventArgs e)
		{
			if (actors.Count == 0)
			{
				NpgsqlCommand comm = new NpgsqlCommand("SELECT id, name FROM person", Shared_data.conn);
				Shared_data.conn.Open();
				NpgsqlDataReader r = comm.ExecuteReader();
				while (r.Read())
				{
					actors.Add(new Item_person(r.GetInt32(0), r.GetString(1)));
				}
				Shared_data.conn.Close();
			}
			Select_win select = new Select_win(actors, Added_item.person);
			select.ShowDialog();
			FillActorList();
		}

		void FillActorList()
		{
			cb_actors.Items.Clear();
			for (int i = 0; i < actors.Count; i++)
			{
				Item_person item = actors[i] as Item_person;
				if (item.isCheck)
					cb_actors.Items.Add(item.name);
			}
			cb_actors.IsEnabled = !cb_actors.Items.IsEmpty;
			if (!cb_actors.Items.IsEmpty)
				cb_actors.SelectedIndex = 0;
			else
				cb_actors.SelectedIndex = -1;
		}
	}
}
