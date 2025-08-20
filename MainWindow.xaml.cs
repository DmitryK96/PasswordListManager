using PasswordListManager.Models;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PasswordListManager
{
    public partial class MainWindow : Window
    {
        private MainViewModel ViewModel => DataContext as MainViewModel;

        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainViewModel();
            Loaded += async (_, __) => await ViewModel.LoadAsync();
            Closing += async (_, __) => await ViewModel.SaveAsync();
        }

        private void AddUser_Click(object sender, RoutedEventArgs e)
        {
            var username = Microsoft.VisualBasic.Interaction.InputBox("Введите имя пользователя: ", "Добавить пользователя");
            if (!string.IsNullOrWhiteSpace(username))
            {
                if (ViewModel.Users.Any(u => u.Username == username))
                {
                    MessageBox.Show("Пользователь с таким именем уже существует.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                ViewModel.Users.Add(new User { Username = username });
            }
        }

        private void DeleteUser_Click(object sender, RoutedEventArgs e)
        {
            if (ViewModel.SelectedUser == null)
            {
                MessageBox.Show("Выберите пользователя для удаления.", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            var result = MessageBox.Show($"Удалить пользователя {ViewModel.SelectedUser.Username}?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                ViewModel.Users.Remove(ViewModel.SelectedUser);
                ViewModel.SelectedUser = null;
            }
        }

        private void AddPassword_Click(object sender, RoutedEventArgs e)
        {
            if (ViewModel.SelectedUser == null)
            {
                MessageBox.Show("Выберите пользователя, чтобы добавить пароль.", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            var purpose = Microsoft.VisualBasic.Interaction.InputBox("Введите назначение:", "Добавить пароль");
            if (string.IsNullOrWhiteSpace(purpose)) return;

            var login = Microsoft.VisualBasic.Interaction.InputBox("Введите логин:", "Добавить пароль");
            if (string.IsNullOrWhiteSpace(login)) return;

            var password = Microsoft.VisualBasic.Interaction.InputBox("Введите пароль:", "Добавить пароль");
            if (string.IsNullOrWhiteSpace(password)) return;

            ViewModel.SelectedUser.Passwords.Add(new PasswordEntry
            {
                Purpose = purpose,
                Login = login,
                Password = password
            });
        }

        private void DeletePassword_Click(object sender, RoutedEventArgs e)
        {
            var selectedPassword = PasswordsDataGrid.SelectedItem as PasswordEntry;
            if (selectedPassword == null)
            {
                MessageBox.Show("Выберите пароль для удаления.", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            var result = MessageBox.Show($"Удалить пароль для \"{selectedPassword.Purpose}\"?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                ViewModel.SelectedUser.Passwords.Remove(selectedPassword);
            }
        }
    }
}