using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using static Medialib_proj.Add_album;
using static Medialib_proj.Add_music;

namespace Medialib_proj
{
	/// <summary>
	/// Логика взаимодействия для Select_win.xaml
	/// </summary>
	public partial class Select_win : Window
    {
		List<object> input = new List<object>();
		Added_item Added_Item { get; }
		public Select_win(List<object>input, Added_item added)
        {
            InitializeComponent();
			Added_Item = added;
			this.input = input;
			grid_unsel.ItemsSource = input;
			switch (Added_Item)
			{
				case Added_item.movie_g:
					b_add_item.Content = "Create new movie genre";
					break;
				case Added_item.music_g:
					b_add_item.Content = "Create new music genre";
					break;
				case Added_item.picture_g:
					b_add_item.Content = "Create new picture genre";
					break;
				case Added_item.text_g:
					b_add_item.Content = "Create new text genre";
					break;
				case Added_item.movie:
					b_add_item.Content = "Create new movie record";
					break;
				case Added_item.music:
					b_add_item.Content = "Create new music record";
					break;
				case Added_item.picture:
					b_add_item.Content = "Create new picture record";
					break;
				case Added_item.text:
					b_add_item.Content = "Create new text record";
					break;
				case Added_item.person:
					b_add_item.Content = "Create new celebritie record";
					break;
				case Added_item.album:
					b_add_item.Content = "Create new album record";
					break;
				case Added_item.band:
					b_add_item.Content = "Create new band record";
					break;
			}
		}

		private void Grid_unsel_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
		{
			string col_name = (string)e.Column.Header;
			if (col_name == "id")
			{
				e.Column.Visibility = Visibility.Collapsed;
			}
			if (col_name == "isCheck")
			{
				e.Column.Header = "";
				if (Added_Item == Added_item.album || Added_Item == Added_item.band)
					e.Column.Visibility = Visibility.Collapsed;
			}
		}

		private void B_add_item_Click(object sender, RoutedEventArgs e)
		{
			Window win;
			switch (Added_Item)
			{
				case Added_item.movie_g:
				case Added_item.music_g:
				case Added_item.picture_g:
				case Added_item.text_g:
					Add_genre add_g = new Add_genre(Added_Item);
					add_g.ShowDialog();
					grid_unsel.Items.Refresh();
					break;
				case Added_item.movie:
					win = new Add_movie();
					win.ShowDialog();
					break;
				case Added_item.music:
					win = new Add_music();
					win.ShowDialog();
					break;
				case Added_item.picture:
					win = new Add_picture();
					win.ShowDialog();
					break;
				case Added_item.text:
					win = new Add_text();
					win.ShowDialog();
					break;
				case Added_item.person:
					win = new Add_celebritie();
					win.ShowDialog();
					break;
				case Added_item.album:
					win = new Add_album();
					win.ShowDialog();
					break;
				case Added_item.band:
					win = new Add_band();
					win.ShowDialog();
					break;
				default:
					break;
			}
		}

		private void Grid_unsel_Selected(object sender, RoutedEventArgs e)
		{
			if (Added_Item == Added_item.album)
			{
				for (int i = 0; i < input.Count; i++)
				{
					var item = input[i] as Item_album;
					item.isCheck = false;
				}
				var row = grid_unsel.SelectedItem as Item_album;
				var album = input.Find(x => x == row) as Item_album;
				album.isCheck = true;
				Close();
			}
			if (Added_Item == Added_item.band)
			{
				for (int i = 0; i < input.Count; i++)
				{
					var item = input[i] as Item_band;
					item.isCheck = false;
				}
				var row = grid_unsel.SelectedItem as Item_band;
				var album = input.Find(x => x == row) as Item_band;
				album.isCheck = true;
				Close();
			}
		}
	}
}
