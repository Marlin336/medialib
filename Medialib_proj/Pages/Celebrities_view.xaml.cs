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
	/// Логика взаимодействия для Celebrities_view.xaml
	/// </summary>
	public partial class Celebrities_view : Page
    {
		private int id { get; }
        public Celebrities_view(int id)
        {
            InitializeComponent();
			this.id = id;

			//Общие данные о человеке
			NpgsqlCommand comm = new NpgsqlCommand("SELECT * FROM view_person where id = " + id, Shared_data.conn);
			Shared_data.conn.Open();
			NpgsqlDataReader r = comm.ExecuteReader();
			r.Read();
			lab_name.Content = r.GetString(1);
			lab_age.Content = r.GetInt32(4);
			lab_birthday.Content = r.GetDate(3).ToString();
			tb_descript.Text = r.GetString(2);
			Shared_data.conn.Close();

			//Информация о продюсируемых фильмах
			comm = new NpgsqlCommand("SELECT id, orig_name, get_local_film(id, "+Shared_data.lang_id+"), year, duration, rating FROM view_film WHERE " + id + " = ANY(director_id_list)", Shared_data.conn);
			Shared_data.conn.Open();
			r = comm.ExecuteReader();
			while (r.Read())
			{
				grid_movie_produced.Items.Add(new Movie_item(r.GetInt32(0), 
					r.GetString(1), 
					r.GetString(2), 
					r.GetInt32(3), 
					r.GetTimeSpan(4).ToString(), 
					r.GetDouble(5)));
			}
			if (grid_movie_produced.Items.Count == 0)
				movie_produced_part.Visibility = Visibility.Collapsed;
			Shared_data.conn.Close();

			//Информация о фильмах с ролью
			comm = new NpgsqlCommand("SELECT id, orig_name, get_local_film(id, " + Shared_data.lang_id + "), year, duration, rating FROM view_film WHERE " + id + " = ANY(actor_id_list)", Shared_data.conn);
			Shared_data.conn.Open();
			r = comm.ExecuteReader();
			while (r.Read())
			{
				grid_movie_actor.Items.Add(new Movie_item(r.GetInt32(0),
					r.GetString(1),
					r.GetValue(2).ToString(),
					r.GetInt32(3),
					r.GetTimeSpan(4).ToString(),
					r.GetDouble(5)));
			}
			if (grid_movie_actor.Items.Count == 0)
				movie_actor_part.Visibility = Visibility.Collapsed;
			Shared_data.conn.Close();

			//Информация о композиторстве
			comm = new NpgsqlCommand("SELECT vm.id, band.name, album_name, year, \"number\", vm.name, duration, rating FROM public.view_music as vm " +
			"LEFT JOIN m_album as ma ON ma.id = vm.album_id " +
			"LEFT JOIN m_band as band ON ma.band_id = band.id WHERE " + id + " = ANY(composer_id_list)", Shared_data.conn);
			Shared_data.conn.Open();
			r = comm.ExecuteReader();
			while (r.Read())
			{
				grid_music_composed.Items.Add(new Music_item(
					r.GetInt32(0),
					r.GetString(1),
					r.GetString(2),
					r.GetInt32(3),
					r.GetInt32(4),
					r.GetString(5),
					r.GetTimeSpan(6).ToString(),
					r.GetDouble(7)));
			}
			if (grid_music_composed.Items.Count == 0)
				music_composed_part.Visibility = Visibility.Collapsed;
			Shared_data.conn.Close();

			//Информация о спетых песнях
			comm = new NpgsqlCommand("SELECT vm.id, band.name, album_name, year, \"number\", vm.name, duration, rating FROM public.view_music as vm " +
			"LEFT JOIN m_album as ma ON ma.id = vm.album_id " +
			"LEFT JOIN m_band as band ON ma.band_id = band.id WHERE " + id + " = ANY(singer_id_list)", Shared_data.conn);
			Shared_data.conn.Open();
			r = comm.ExecuteReader();
			while (r.Read())
			{
				grid_music_sung.Items.Add(new Music_item(
					r.GetInt32(0),
					r.GetString(1),
					r.GetString(2),
					r.GetInt32(3),
					r.GetInt32(4),
					r.GetString(5),
					r.GetTimeSpan(6).ToString(),
					r.GetDouble(7)));
			}
			if (grid_music_sung.Items.Count == 0)
				music_sung_part.Visibility = Visibility.Collapsed;
			Shared_data.conn.Close();

			//Информация о нарисованых картинах
			comm = new NpgsqlCommand("SELECT id, name, year, rating FROM public.view_picture WHERE " + id + " = ANY(artist_id_list)", Shared_data.conn);
			Shared_data.conn.Open();
			r = comm.ExecuteReader();
			while (r.Read())
			{
				grid_picture_drawn.Items.Add(new Picture_item(
					r.GetInt32(0),
					r.GetString(1),
					r.GetInt32(2),
					r.GetDouble(3)));
			}
			if (grid_picture_drawn.Items.Count == 0)
				picture_drawn_part.Visibility = Visibility.Collapsed;
			Shared_data.conn.Close();

			//Информация о написаных текстах
			comm = new NpgsqlCommand("SELECT id, name, year, rating FROM public.view_text WHERE " + id + " = ANY(author_id_list)", Shared_data.conn);
			Shared_data.conn.Open();
			r = comm.ExecuteReader();
			while (r.Read())
			{
				grid_text_written.Items.Add(new Text_item(
					r.GetInt32(0),
					r.GetString(1),
					r.GetInt32(2),
					r.GetDouble(3)));
			}
			if (grid_text_written.Items.Count == 0)
				text_written_part.Visibility = Visibility.Collapsed;
			Shared_data.conn.Close();
		}

		private void Movie_prod_MDC(object sender, MouseButtonEventArgs e)
		{
			Movie_item row = grid_movie_produced.SelectedItem as Movie_item;
			if (row == null) return;
			int id = row.id;
			Page page = new Movies_view(id);
			NavigationService.Navigate(page);
		}

		private void Movie_act_MDC(object sender, MouseButtonEventArgs e)
		{
			Movie_item row = grid_movie_actor.SelectedItem as Movie_item;
			if (row == null) return;
			int id = row.id;
			Page page = new Movies_view(id);
			NavigationService.Navigate(page);
		}

		private void Music_comp_MDC(object sender, MouseButtonEventArgs e)
		{
			Music_item row = grid_music_composed.SelectedItem as Music_item;
			if (row == null) return;
			int id = row.id;
			Page page = new Music_view(id);
			NavigationService.Navigate(page);
		}

		private void Music_sing_MDC(object sender, MouseButtonEventArgs e)
		{
			Music_item row = grid_music_sung.SelectedItem as Music_item;
			if (row == null) return;
			int id = row.id;
			Page page = new Music_view(id);
			NavigationService.Navigate(page);
		}

		private void Picture_MDC(object sender, MouseButtonEventArgs e)
		{
			Picture_item row = grid_picture_drawn.SelectedItem as Picture_item;
			if (row == null) return;
			int id = row.id;
			Page page = new Pictures_view(id);
			NavigationService.Navigate(page);
		}

		private void Text_MDC(object sender, MouseButtonEventArgs e)
		{
			Text_item row = grid_text_written.SelectedItem as Text_item;
			if (row == null) return;
			int id = row.id;
			Page page = new Texts_view(id);
			NavigationService.Navigate(page);
		}
	}
}
