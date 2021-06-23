using Npgsql;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace Medialib_proj.Pages
{
	/// <summary>
	/// Логика взаимодействия для Music_view.xaml
	/// </summary>
	public partial class Music_view : Page
    {
		private int id { get; }
		private int grade = 0;
		/// <summary>
		/// Вывести страницу с информацией о музыке
		/// </summary>
		/// <param name="id">id музыки</param>
		public Music_view(int id)
        {
            InitializeComponent();
			this.id = id;

			NpgsqlCommand comm = new NpgsqlCommand("SELECT vm.*, band.name FROM view_music as vm " +
			"LEFT JOIN m_album as ma ON ma.id = vm.album_id " +
			"LEFT JOIN m_band as band ON ma.band_id = band.id WHERE vm.id = " + id, Shared_data.conn);
			Shared_data.conn.Open();
			NpgsqlDataReader r = comm.ExecuteReader();
			r.Read();

			lab_name.Content = r.GetString(1);
			lab_year.Content = r.GetInt32(5);
			string[] arr = (string[])r.GetValue(13);
			lab_genre.Content = string.Join(", ", arr);
			lab_bandname.Content = r.GetString(16);
			arr = (string[])r.GetValue(9);
			lab_composlist.Content = string.Join(", ", arr);
			arr = (string[])r.GetValue(11);
			lab_singlist.Content = string.Join(", ", arr);
			lab_albumname.Content = r.GetString(3);
			lab_num.Content = r.GetInt32(4);
			lab_duration.Content = r.GetTimeSpan(6).ToString();
			lab_rating.Content = r.GetDouble(14);
			tb_descript.Text = r.GetValue(7).ToString();

			Shared_data.conn.Close();
		}

		private void Fill_comment()
		{
			lb_comment.Items.Clear();
			NpgsqlCommand comm = new NpgsqlCommand("SELECT * FROM public.com_music WHERE music_id = " + id + " ORDER BY datetime", Shared_data.conn);
			Shared_data.conn.Open();
			NpgsqlDataReader r = comm.ExecuteReader();
			while (r.Read())
				lb_comment.Items.Add(new Comment_item(r.GetInt32(0), !string.IsNullOrEmpty(r.GetValue(5).ToString()) ? r.GetString(5) : "[user deleted]", r.GetDateTime(3).ToLongDateString() + " " + r.GetDateTime(3).ToLongTimeString(), r.GetString(1)));
			Shared_data.conn.Close();
		}

		private void Comment_field_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Enter)
			{
				NpgsqlCommand comm = new NpgsqlCommand("CALL add_music_comment(" + id + ", $$" + comment_field.Text + "$$)", Shared_data.conn);
				Shared_data.conn.Open();
				comm.ExecuteNonQuery();
				Shared_data.conn.Close();
				Fill_comment();
				comment_field.Text = string.Empty;
			}
		}

		private void TabItem_GotFocus(object sender, RoutedEventArgs e)
		{
			Fill_comment();
			NpgsqlCommand comm = new NpgsqlCommand("select exists (SELECT * FROM rat_music where music_id = " + id + " AND user_name = current_user)", Shared_data.conn);
			Shared_data.conn.Open();
			bool already_rating = (bool)comm.ExecuteScalar();
			Shared_data.conn.Close();
			if (already_rating)
			{
				comm = new NpgsqlCommand("SELECT rating FROM rat_music where music_id = " + id + " AND user_name = current_user", Shared_data.conn);
				Shared_data.conn.Open();
				grade = (short)comm.ExecuteScalar();
				Shared_data.conn.Close();

				string img_path = "../Resourses/img_star.png";
				BitmapImage image = new BitmapImage();
				image.BeginInit();
				image.UriSource = new Uri(img_path, UriKind.Relative);
				image.EndInit();
				if (grade >= 20)
				{
					star_1.Source = image;
					if (grade >= 40)
					{
						star_2.Source = image;
						if (grade >= 60)
						{
							star_3.Source = image;
							if (grade >= 80)
							{
								star_4.Source = image;
								if (grade == 100)
								{
									star_5.Source = image;
								}
							}
						}
					}
				}
			}
		}

		private class Comment_item
		{
			public int id { get; }
			public string data { get; }
			public string comment { get; }
			public Comment_item(int id, string user, string date, string comment)
			{
				this.id = id;
				data = user + " (" + date + ")";
				this.comment = comment;
			}
		}

		private void Star_MouseEnter(object sender, MouseEventArgs e)
		{
			if (grade == 0)
			{
				string img_path = "../Resourses/img_star.png";
				BitmapImage image = new BitmapImage();
				image.BeginInit();
				image.UriSource = new Uri(img_path, UriKind.Relative);
				image.EndInit();
				if (sender.Equals(star_1))
				{
					star_1.Source = image;
				}
				else if (sender.Equals(star_2))
				{
					star_1.Source = star_2.Source = image;
				}
				else if (sender.Equals(star_3))
				{
					star_1.Source = star_2.Source = star_3.Source = image;
				}
				else if (sender.Equals(star_4))
				{
					star_1.Source = star_2.Source = star_3.Source = star_4.Source = image;
				}
				else if (sender.Equals(star_5))
				{
					star_1.Source = star_2.Source = star_3.Source = star_4.Source = star_5.Source = image;
				}
			}
		}

		private void Star_MouseLeave(object sender, MouseEventArgs e)
		{
			if (grade == 0)
			{
				string img_path = "../Resourses/img_unstar.png";
				BitmapImage image = new BitmapImage();
				image.BeginInit();
				image.UriSource = new Uri(img_path, UriKind.Relative);
				image.EndInit();
				star_1.Source = star_2.Source = star_3.Source = star_4.Source = star_5.Source = image;
			}
		}

		private void Star_MouseDown(object sender, MouseButtonEventArgs e)
		{
			if (sender.Equals(star_1))
			{
				grade = 20;
			}
			else if (sender.Equals(star_2))
			{
				grade = 40;
			}
			else if (sender.Equals(star_3))
			{
				grade = 60;
			}
			else if (sender.Equals(star_4))
			{
				grade = 80;
			}
			else if (sender.Equals(star_5))
			{
				grade = 100;
			}
			NpgsqlCommand comm = new NpgsqlCommand("INSERT INTO public.rat_music(music_id, rating, user_name) VALUES(" + id + ", " + grade + ", current_user); ", Shared_data.conn);
			Shared_data.conn.Open();
			comm.ExecuteNonQuery();
			Shared_data.conn.Close();
		}

		private void Border_MouseDown(object sender, MouseButtonEventArgs e)
		{
			if (Shared_data.perm_list.Exists(x => x == "admin") || Shared_data.perm_list.Exists(x => x == "moderor"))
			{
				if (MessageBox.Show("Delete this comment?", "", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
				{
					Border border = sender as Border;
					NpgsqlCommand comm = new NpgsqlCommand("DELETE FROM comment WHERE id = " + border.Tag, Shared_data.conn);
					Shared_data.conn.Open();
					comm.ExecuteNonQuery();
					Shared_data.conn.Close();
					Fill_comment();
				}
			}
		}
	}
}
