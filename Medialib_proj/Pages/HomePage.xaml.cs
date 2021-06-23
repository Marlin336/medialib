using Medialib_proj.Pages;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace Medialib_proj
{
	/// <summary>
	/// Логика взаимодействия для FirstPage.xaml
	/// </summary>
	public partial class HomePage : Page
    {
        public HomePage()
        {
            InitializeComponent();
        }

		private void B_collections_Click(object sender, RoutedEventArgs e)
		{
			Page page = new Collections_start();// new ListPage(ListPage.ListType.Collection);
			NavigationService.Navigate(page);
		}

		private void B_movies_Click(object sender, RoutedEventArgs e)
		{
			Page page = new Movies_start();// new ListPage(ListPage.ListType.Movie);
			NavigationService.Navigate(page);
		}

		private void B_celebrities_Click(object sender, RoutedEventArgs e)
		{
			Page page = new Celebrities_start();// new ListPage(ListPage.ListType.Selebritie);
			NavigationService.Navigate(page);
		}

		private void B_books_Click(object sender, RoutedEventArgs e)
		{
			Page page = new Texts_start();// new ListPage(ListPage.ListType.Book);
			NavigationService.Navigate(page);
		}

		private void B_music_Click(object sender, RoutedEventArgs e)
		{
			Page page = new Music_start();// new ListPage(ListPage.ListType.Music);
			NavigationService.Navigate(page);
		}

		private void B_pictures_Click(object sender, RoutedEventArgs e)
		{
			Page page = new Pictures_start();// new ListPage(ListPage.ListType.Picture);
			NavigationService.Navigate(page);
		}
	}
}
