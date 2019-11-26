using ClientDesktop.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientDesktop.ViewModels
{
    public class MainWindowViewModel : ObservableObject
    {
        private string _username;
        private string _email;
        private string _password;

        
        [Required(ErrorMessage = "Must not be empty.")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Must be at least 5 characters.")]

        public string Username

        {

            get { return _username; }
            set
            {
                ValidateProperty(value, "Username");
                OnPropertyChanged(ref _username, value);
            }
        }

        [Required(ErrorMessage = "Must not be empty.")]
        [EmailAddress]
        public string Email
        {

            get { return _email; }
            set
            {
                ValidateProperty(value, "Email");
                OnPropertyChanged(ref _email, value);
            }
        }

        [Required(ErrorMessage = "Must not be empty.")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Must be at least 5 characters.")]
        public string Password
        {

            get { return _password; }
            set
            {
                ValidateProperty(value, "Password");
                OnPropertyChanged(ref _password, value);
            }
        }

        public MainWindowViewModel(string username, string email, string password)
        {
            Username = username;
            Email = email;
            Password = password;
        }
        public MainWindowViewModel()
        {
        }

        private void ValidateProperty<T>(T value, string name)
        {
            Validator.ValidateProperty(value, new ValidationContext(this, null, null)
            {
                MemberName = name
            });
        }
    }
}
