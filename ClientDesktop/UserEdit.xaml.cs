using Models;
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

        public User user;
        public UserEdit(MainWindow mainWindow, User user)
        {
            InitializeComponent();

            this.mainWindow = mainWindow;
            this.user = user;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            string username = usernameText.Text;
            string firstName = firstNameText.Text;
            string lastName = lastNameText.Text;
            string description = descriptionText.Text;
            string email = emailText.Text;
            string password = passwordText.Password.ToString();

            var client = new RestClient(ConfigurationManager.AppSettings.Get("APIURL"));
            var request = new RestRequest($"user/update", Method.POST);

            var user = new User();
            user.Username = username;
            user.FirstName = firstName;
            user.LastName = lastName;
            user.Description = description;
            user.Email = email;
            user.Password = password;

            request.AddJsonBody(user);

            client.Execute(request);

            mainWindow.GoToUserShow(user);
        }
    }
}
