using Cinema.Models;
using System.Windows.Controls;
using System.Windows;

namespace Cinema.Pages
{
    public partial class RegistryPage : Page
    {
        public RegistryPage()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string login = Login.Text;
            string password = Password.Password;

            // Добавляем проверки
            if (string.IsNullOrWhiteSpace(login) || login.Length < 5)
            {
                MessageBox.Show("Логин должен быть не менее 5 символов.");
                return;
            }

            if (string.IsNullOrWhiteSpace(password) || password.Length < 8)
            {
                MessageBox.Show("Пароль должен быть не менее 8 символов.");
                return;
            }

            var newUser = new Users { login = login, password = password };

            using (var context = new CinemaDBEntities3())
            {
                context.Users.Add(newUser);
                context.SaveChanges();
            }

            Login.Text = "";
            Password.Password = "";

            MessageBox.Show("Регистрация успешна!");
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new LoginPage());
        }
    }
}
