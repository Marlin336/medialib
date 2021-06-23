using Npgsql;
using System;
using System.Windows;

namespace Medialib_proj
{
	/// <summary>
	/// Логика взаимодействия для Add_celebritie.xaml
	/// </summary>
	public partial class Add_celebritie : Window
	{
		int? id { get; } = null;
		public Add_celebritie()
		{
			InitializeComponent();
		}
		public Add_celebritie(int id)
		{
			InitializeComponent();
			this.id = id;
			b_add.Content = "Save";
			//Заполнить поля
			NpgsqlCommand comm = new NpgsqlCommand("SELECT name, birthday, description FROM person WHERE id = " + id, Shared_data.conn);
			Shared_data.conn.Open();
			NpgsqlDataReader r = comm.ExecuteReader();
			r.Read();
			tb_name.Text = r.GetString(0);
			var date = r.GetDate(1);
			tp_birth.Text = date.Day + "." + date.Month + "." + date.Year;
			tb_descript.Text = r.GetValue(2).ToString();
			Shared_data.conn.Close();
		}

		private void B_add_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				if (id == null)
				{
					NpgsqlCommand comm = new NpgsqlCommand("INSERT INTO person(name, birthday, description) VALUES($$" + tb_name.Text + "$$, '" + tp_birth.Value.Value.ToShortDateString() + "', $$" + tb_descript.Text + "$$)", Shared_data.conn);
					Shared_data.conn.Open();
					comm.ExecuteNonQuery();
					//Shared_data.conn.Close();
				}
				else
				{
					NpgsqlCommand comm = new NpgsqlCommand("UPDATE person SET name = $$" + tb_name.Text + "$$, birthday = '" + tp_birth.Value.Value.ToShortDateString() + "', description = $$" + tb_descript.Text + "$$ WHERE id = " + id, Shared_data.conn);
					Shared_data.conn.Open();
					comm.ExecuteNonQuery();
					//Shared_data.conn.Close();
				}
				Close();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
			finally { Shared_data.conn.Close(); }
		}
	}
}
