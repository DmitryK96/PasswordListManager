using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordListManager.Models
{
    public class User
    {
        public string Username { get; set; }
        public ObservableCollection<PasswordEntry> Passwords { get; set; } = new();
    }
}
