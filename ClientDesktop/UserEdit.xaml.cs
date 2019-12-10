using Models;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ClientDesktop
{
    public partial class UserEdit : Page
    {
        MainWindow mainWindow;
        User user;

        public UserEdit(MainWindow mainWindow, User user)
        {
            InitializeComponent();

            this.mainWindow = mainWindow;
            this.user = user;

            title.Content = $"Edit {user.Username}";
            usernameText.Text = user.Username;
            firstNameText.Text = user.FirstName;
            lastNameText.Text = user.LastName;
            descriptionText.Text = user.Description;
            emailText.Text = user.Email;
            passwordText.Password = user.Password;

            usernameError.Content = user.UsernameError;
            emailError.Content = user.EmailError;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            user.Username = usernameText.Text;
            user.FirstName = firstNameText.Text;
            user.LastName = lastNameText.Text;
            user.Description = descriptionText.Text;
            user.Email = emailText.Text;
            user.Password = passwordText.Password;

            var client = new RestClient(ConfigurationManager.AppSettings.Get("APIURL"));
            var request = new RestRequest($"user/update", Method.POST);
            request.AddJsonBody(user);

            var newUser = JsonConvert.DeserializeObject<User>(client.Execute(request).Content);
            mainWindow.GoToUserEdit(newUser);
        }

        private void Back_Button_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.GoToUserIndex();
        }
    }
}
