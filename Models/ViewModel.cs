using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
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


        private const string DataFile = "users.json";

        public async Task SaveAsync()
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            using FileStream createStream = File.Create(DataFile);
            await JsonSerializer.SerializeAsync(createStream, Users, options);
            await createStream.FlushAsync();
        }

        public async Task LoadAsync()
        {
            if (File.Exists(DataFile))
            {
                using FileStream openStream = File.OpenRead(DataFile);
                var users = await JsonSerializer.DeserializeAsync<ObservableCollection<User>>(openStream);
                if (users != null)
                {
                    Users = users;
                    OnPropertyChanged(nameof(Users));
                    SelectedUser = Users.FirstOrDefault();
                }
            }
        }
    }
}
