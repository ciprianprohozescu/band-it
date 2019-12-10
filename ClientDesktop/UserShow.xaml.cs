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

using User = Models.User;

namespace ClientDesktop
{
    public partial class UserShow : Page
    {
        MainWindow mainWindow;
        User user;

        public UserShow(MainWindow mainWindow, User user)
        {
            InitializeComponent();

            this.mainWindow = mainWindow;
            this.user = user;

            listSkills.ItemsSource = user.Skills;

            username.Content = user.Username;
            if ((user.FirstName != "" && user.FirstName != null) || (user.LastName != "" && user.LastName != null))
            {
                username.Content += $" ({user.FirstName} {user.LastName})";
            }

            description.Text = user.Description;
        }

        private void Back_Button_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.GoToUserIndex();
        }

        private void Edit_Button_Click(object sender, RoutedEventArgs e)
        {
            var client = new RestClient(ConfigurationManager.AppSettings.Get("APIURL"));
            var request = new RestRequest($"user/update", Method.POST);
            request.AddJsonBody(user);

            var newUser = JsonConvert.DeserializeObject<User>(client.Execute(request).Content);
            mainWindow.GoToUserEdit(newUser);
        }
    }
}
