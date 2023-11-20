using System.Windows;
using System.Windows.Controls;
using Cinema.Models;
using System.Linq;
// Импортируйте необходимые пространства имен для работы с базой данных

namespace Cinema.Pages
{
    public partial class FilmsDataGridPage : Page
    {
        public FilmsDataGridPage()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            using (var context = new CinemaDBEntities3())
            {
                var films = context.Film.Include("Genre").ToList();
                FilmsDataGrid.Items.Clear(); 
                FilmsDataGrid.ItemsSource = films;
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var newFilm = FilmsDataGrid.SelectedItem as Film;
            if (newFilm != null)
            {
                using (var context = new CinemaDBEntities3())
                {
                    context.Film.Add(newFilm);
                    context.SaveChanges();
                }

                LoadData(); 
            }
        }


        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedFilm = FilmsDataGrid.SelectedItem as Film;
            if (selectedFilm != null)
            {
                using (var context = new CinemaDBEntities3())
                {
                    context.Film.Remove(selectedFilm);
                    context.SaveChanges();
                }

                LoadData(); // Перезагрузка данных для обновления списка
            }
            else
            {
                MessageBox.Show("Выберите фильм для удаления.");
            }
        }
        private void FilmsDataGrid_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            if (e.EditAction == DataGridEditAction.Commit && e.Row.Item is Film newFilm && newFilm.id == 0)
            {
                using (var context = new CinemaDBEntities3())
                {
                    context.Film.Add(newFilm);
                    context.SaveChanges();
                }

                LoadData();
            }
        }

    }
}
