using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordListManager.Models
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<User> Users { get; set; } = new();

        private User _selectedUser;
        public User SelectedUser
        {
            get => _selectedUser;
            set
            {
                if (_selectedUser != value)
                {
                    _selectedUser = value;
                    OnPropertyChanged(nameof(SelectedUser));
                }
            }
        }

        public MainViewModel()
        {
            // Пример начальных данных
            Users.Add(new User
            {
                Username = "Иван",
                Passwords = new ObservableCollection<PasswordEntry>
                {
                    new PasswordEntry { Purpose = "Почта", Login = "ivan@mail.com", Password = "12345" },
                    new PasswordEntry { Purpose = "GitHub", Login = "ivan-dev", Password = "gh_pass" }
                }
            });

            Users.Add(new User
            {
                Username = "Мария",
                Passwords = new ObservableCollection<PasswordEntry>
                {
                    new PasswordEntry { Purpose = "Банк", Login = "maria", Password = "bank123" }
                }
            });
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
