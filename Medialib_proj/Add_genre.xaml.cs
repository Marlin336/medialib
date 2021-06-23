using Npgsql;
using System.Windows;

namespace Medialib_proj
{
	/// <summary>
	/// Логика взаимодействия для Add_genre.xaml
	/// </summary>
	public partial class Add_genre : Window
    {
		int? id { get; } = null;
		Added_item ai { get; }
        public Add_genre(Added_item ai)
        {
            InitializeComponent();
			this.ai = ai;
			switch (ai)
			{
				case Added_item.movie_g:
					Title = "New movie genre";
					break;
				case Added_item.music_g:
					Title = "New music genre";
					break;
				case Added_item.picture_g:
					Title = "New picture genre";
					break;
				case Added_item.text_g:
					Title = "New text genre";
					break;
			}
		}
		public Add_genre(Added_item ai, int id)
		{
			InitializeComponent();
			this.id = id;
			this.ai = ai;
			switch (ai)
			{
				case Added_item.movie_g:
					Title = "Edit movie genre";
					break;
				case Added_item.music_g:
					Title = "Edit music genre";
					break;
				case Added_item.picture_g:
					Title = "Edit picture genre";
					break;
				case Added_item.text_g:
					Title = "Edit text genre";
					break;
			}
		}

		private void B_add_Click(object sender, RoutedEventArgs e)
		{
			if (id == null)
			{
				NpgsqlCommand comm;
				switch (ai)
				{
					case Added_item.movie_g:
						comm = new NpgsqlCommand("INSERT INTO f_genre(name) VALUES('" + tb_name.Text + "')", Shared_data.conn);
						Shared_data.conn.Open();
						comm.ExecuteNonQuery();
						Shared_data.conn.Close();
						break;
					case Added_item.music_g:
						comm = new NpgsqlCommand("INSERT INTO m_genre(name) VALUES('" + tb_name.Text + "')", Shared_data.conn);
						Shared_data.conn.Open();
						comm.ExecuteNonQuery();
						Shared_data.conn.Close();
						break;
					case Added_item.picture_g:
						comm = new NpgsqlCommand("INSERT INTO p_genre(name) VALUES('" + tb_name.Text + "')", Shared_data.conn);
						Shared_data.conn.Open();
						comm.ExecuteNonQuery();
						Shared_data.conn.Close();
						break;
					case Added_item.text_g:
						comm = new NpgsqlCommand("INSERT INTO t(name) VALUES('" + tb_name.Text + "')", Shared_data.conn);
						Shared_data.conn.Open();
						comm.ExecuteNonQuery();
						Shared_data.conn.Close();
						break;
				}
			}
			else
			{
				NpgsqlCommand comm;
				switch (ai)
				{
					case Added_item.movie_g:
						comm = new NpgsqlCommand("UPDATE f_genre SET name = $$" + tb_name.Text + "$$ WHERE id = " + id, Shared_data.conn);
						Shared_data.conn.Open();
						comm.ExecuteNonQuery();
						Shared_data.conn.Close();
						break;
					case Added_item.music_g:
						comm = new NpgsqlCommand("UPDATE m_genre SET name = $$" + tb_name.Text + "$$ WHERE id = " + id, Shared_data.conn);
						Shared_data.conn.Open();
						comm.ExecuteNonQuery();
						Shared_data.conn.Close();
						break;
					case Added_item.picture_g:
						comm = new NpgsqlCommand("UPDATE p_genre SET name = $$" + tb_name.Text + "$$ WHERE id = " + id, Shared_data.conn);
						Shared_data.conn.Open();
						comm.ExecuteNonQuery();
						Shared_data.conn.Close();
						break;
					case Added_item.text_g:
						comm = new NpgsqlCommand("UPDATE t_genre SET name = $$" + tb_name.Text + "$$ WHERE id = " + id, Shared_data.conn);
						Shared_data.conn.Open();
						comm.ExecuteNonQuery();
						Shared_data.conn.Close();
						break;
				}
			}
			Close();
		}
	}
}
