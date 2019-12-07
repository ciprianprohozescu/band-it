using Models;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
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
    public partial class MainWindow : Window
    {
        object content;

        public MainWindow()
        {
            InitializeComponent();

            this.content = Content;
        }
        private void LinkClick(object sender, RoutedEventArgs e)
        {
            GoToLogin();
        }

        public void GoBackToRegister()
        {
            Content = content;
        }

        public void GoToUserIndex()
        {
            UserIndex userIndex = new UserIndex(this);
            Content = userIndex;
        }

        public void GoToBandIndex()
        {
            BandIndex bandIndex = new BandIndex(this);
            Content = bandIndex;
        }

        public void GoToLogin()
        {
            LoginPage loginPage = new LoginPage(this);
            Content = loginPage;
        }

        public void GoToUserShow(User user)
        {
            UserShow userShow = new UserShow(this, user);
            Content = userShow;
        }

        public void GoToBandEdit(Band band)
        {
            BandForm bandForm = new BandForm(this, band);
            Content = bandForm;
        }

        public void GoToBandShow(Band band)
        {
            BandShow bandShow = new BandShow(this, band);
            Content = bandShow;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (Validation.GetHasError(txtUser) || Validation.GetHasError(txtEmail) || Validation.GetHasError(txtPassword))
            {
                return;
            }

            var user = new User();
            user.Username = txtUser.Text;
            user.Email = txtEmail.Text;
            user.Password = txtPassword.Password;

            var client = new RestClient(ConfigurationManager.AppSettings.Get("APIURL"));
            var request = new RestRequest("/user/register", Method.POST);
            request.AddJsonBody(user);
            var response = client.Execute(request);

            GoToLogin();    
        }

        private void txtUser_SelectionChanged(object sender, RoutedEventArgs e)
        {
            string Username = txtUser.Text;

            var client = new RestClient(ConfigurationManager.AppSettings.Get("APIURL"));
            var request = new RestRequest("user/username", Method.GET);
            request.AddParameter("username", Username);


            var content = client.Execute(request).Content;
            var user = JsonConvert.DeserializeObject<User>(content);


            if (user != null)
            {
                txtErrorUsername.Text = "This username is taken.";
                txtUser.BorderThickness = new Thickness(2, 2, 2, 2);
                txtUser.BorderBrush = Brushes.Red;
            }
            else
            {
                txtErrorUsername.Text = "";
                txtUser.BorderThickness = new Thickness(0, 0, 0, 0);
            }

        }

        private void txtEmail_SelectionChanged(object sender, RoutedEventArgs e)
        {
            string Email = txtEmail.Text;

            var client = new RestClient(ConfigurationManager.AppSettings.Get("APIURL"));
            var request = new RestRequest("user/email", Method.GET);
            request.AddParameter("email", Email);


            var content = client.Execute(request).Content;
            var user = JsonConvert.DeserializeObject<User>(content);
            if (user != null)
            {
                txtErrorEmail.Text = "There is an account on this email already.";
                txtEmail.BorderThickness = new Thickness(2,2,2,2);
                txtEmail.BorderBrush = Brushes.Red;
            }
            else
            {
                txtErrorEmail.Text = "";
                txtEmail.BorderThickness = new Thickness(0, 0, 0, 0);
            }
        }
    }
}
