using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Cinema.Models;

namespace Cinema.Pages
{
    public partial class LoginPage : Page
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string login = LoginTextBox.Text;
            string password = PasswordBox.Password;

            using (var context = new CinemaDBEntities3())
            {
                var user = context.Users.FirstOrDefault(u => u.login == login && u.password == password);

                if (user != null)
                {
                    MessageBox.Show("Вход выполнен успешно!");
                    NavigationService.Navigate(new FilmsDataGridPage());
                }
                else
                {
                    MessageBox.Show("Неверный логин или пароль.");
                }
            }
        }
    }
}
