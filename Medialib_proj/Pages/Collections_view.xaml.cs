using Npgsql;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;
using static Medialib_proj.Pages.Movies_start;
using static Medialib_proj.Pages.Music_start;
using static Medialib_proj.Pages.Pictures_start;
using static Medialib_proj.Pages.Texts_start;

namespace Medialib_proj.Pages
{
	/// <summary>
	/// Логика взаимодействия для Collections_view.xaml
	/// </summary>
	public partial class Collections_view : Page
	{ 
		int id { get; }
        public Collections_view(int id)
        {
            InitializeComponent();
			this.id = id;
			NpgsqlCommand comm = new NpgsqlCommand("SELECT name FROM collection WHERE id = " + id, Shared_data.conn);
			Shared_data.conn.Open();
			l_name.Content = comm.ExecuteScalar();
			Shared_data.conn.Close();

			//Фильмы
			comm = new NpgsqlCommand("SELECT vf.id, vf.orig_name, get_local_film(id, " + Shared_data.lang_id + "), year, duration, rating FROM view_film as vf JOIN view_film_collection as fc ON fc.film_id = vf.id WHERE coll_id = " + id, Shared_data.conn);
			Shared_data.conn.Open();
			NpgsqlDataReader r = comm.ExecuteReader();
			while (r.Read())
			{
				grid_movie.Items.Add(new Movie_item(
					r.GetInt32(0),
					r.GetString(1),
					r.GetValue(2).ToString(),
					r.GetInt32(3),
					r.GetTimeSpan(4).ToString(),
					r.GetDouble(5)));
			}
			if (grid_movie.Items.Count == 0)
				movie_part.Visibility = Visibility.Collapsed;
			Shared_data.conn.Close();

			//Музыка
			comm = new NpgsqlCommand("SELECT song_id, band.name, album_name, year, number, song_name, duration, rating FROM view_music as vm JOIN view_music_collection as mc ON mc.song_id = vm.id "+
				"JOIN m_album as ma ON ma.id = vm.album_id "+
				"LEFT JOIN m_band as band ON ma.band_id = band.id "+
				"WHERE collection_id =" + id, Shared_data.conn);
			Shared_data.conn.Open();
			r = comm.ExecuteReader();
			while (r.Read())
			{
				grid_music.Items.Add(new Music_item(
					r.GetInt32(0),
					r.GetString(1),
					r.GetString(2),
					r.GetInt32(3),
					r.GetInt32(4),
					r.GetString(5),
					r.GetTimeSpan(6).ToString(),
					r.GetDouble(7)));
			}
			if (grid_music.Items.Count == 0)
				music_part.Visibility = Visibility.Collapsed;
			Shared_data.conn.Close();

			//Изображения
			comm = new NpgsqlCommand("SELECT id, name, year, rating FROM view_picture as vp JOIN view_picture_collection as pc ON pc.picture_id = vp.id WHERE pc.collection_id = " + id, Shared_data.conn);
			Shared_data.conn.Open();
			r = comm.ExecuteReader();
			while (r.Read())
			{
				grid_picture.Items.Add(new Picture_item(
					r.GetInt32(0),
					r.GetString(1),
					r.GetInt32(2),
					r.GetDouble(3)));
			}
			if (grid_picture.Items.Count == 0)
				picture_part.Visibility = Visibility.Collapsed;
			Shared_data.conn.Close();

			//Тексты
			comm = new NpgsqlCommand("SELECT id, name, year, rating FROM view_text as vt JOIN view_text_collection as tc ON tc.text_id = vt.id WHERE tc.collection_id = " + id, Shared_data.conn);
			Shared_data.conn.Open();
			r = comm.ExecuteReader();
			while (r.Read())
			{
				grid_text.Items.Add(new Text_item(
					r.GetInt32(0),
					r.GetString(1),
					r.GetInt32(2),
					r.GetDouble(3)));
			}
			if (grid_text.Items.Count == 0)
				text_part.Visibility = Visibility.Collapsed;
			Shared_data.conn.Close();
		}

		private void Movie_MDC(object sender, MouseButtonEventArgs e)
		{
			Movie_item row = grid_movie.SelectedItem as Movie_item;
			if (row == null) return;
			int id = row.id;
			Page page = new Movies_view(id);
			NavigationService.Navigate(page);
		}

		private void Music_MDC(object sender, MouseButtonEventArgs e)
		{
			Music_item row = grid_music.SelectedItem as Music_item;
			if (row == null) return;
			int id = row.id;
			Page page = new Music_view(id);
			NavigationService.Navigate(page);
		}

		private void Picture_MDC(object sender, MouseButtonEventArgs e)
		{
			Picture_item row = grid_picture.SelectedItem as Picture_item;
			if (row == null) return;
			int id = row.id;
			Page page = new Pictures_view(id);
			NavigationService.Navigate(page);
		}

		private void Text_MDC(object sender, MouseButtonEventArgs e)
		{
			Text_item row = grid_text.SelectedItem as Text_item;
			if (row == null) return;
			int id = row.id;
			Page page = new Texts_view(id);
			NavigationService.Navigate(page);
		}
	}
}
