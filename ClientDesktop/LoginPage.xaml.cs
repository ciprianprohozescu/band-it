using ClientDesktop.ViewModels;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ClientDesktop
{
    /// <summary>
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        MainWindow mainWindow;

        public LoginPage(MainWindow mainWindow)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
        }
        private void LinkClick(object sender, RoutedEventArgs e)
        {
            mainWindow.GoBackToRegister();
        }
        private void LogIn(object sender, RoutedEventArgs e)
        {
            var username = txtUser.Text;
            var password = txtPassword.Password;

            var client = new RestClient(ConfigurationManager.AppSettings.Get("APIURL"));
            var request = new RestRequest("user/login", Method.GET);

            request.AddParameter("username", username);
            request.AddParameter("password", password);

            var content = client.Execute(request);

            var user = JsonConvert.DeserializeObject<User>(content.Content);

            if (user == null)
            {
                msgError.Visibility = Visibility.Visible;
            } else
            {
                msgError.Visibility = Visibility.Hidden;

                mainWindow.GoToBandIndex();
            }
        }
    }
}
