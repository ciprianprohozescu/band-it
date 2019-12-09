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

using Band = Models.Band;

namespace ClientDesktop
{
    /// <summary>
    /// Interaction logic for BandShow.xaml
    /// </summary>
    public partial class BandShow : Page
    {
        MainWindow mainWindow;
        public Band band;

        public BandShow(MainWindow mainWindow, Band band)
        {
            InitializeComponent();

            this.mainWindow = mainWindow;
            this.band = band;

            if (band.BandUsers.Select(x => x.UserID).Contains(LoggedInUser.User.ID))
            {
                foreach(var application in band.Applications)
                {
                    var client = new RestClient(ConfigurationManager.AppSettings.Get("APIURL"));
                    var request = new RestRequest($"user/{application.UserID}", Method.GET); ;

                    var content = client.Execute(request);

                    application.User = JsonConvert.DeserializeObject<User>(content.Content);
                }
                for (int i = 0; i < band.Applications.Count; i++)
                {
                    if (band.Applications[i].Result != null)
                    {
                        band.Applications.Remove(band.Applications[i]);
                        i--;
                    }
                }
                dataGridApplications.ItemsSource = band.Applications;
            }
        }

        private void Accept_Button_Click(object sender, RoutedEventArgs e)
        {
            var application = (Models.Application)dataGridApplications.SelectedItem;
            var client = new RestClient(ConfigurationManager.AppSettings.Get("APIURL"));
            var request = new RestRequest($"application/accept/" +
                $"{application.ID}/{application.UserID}/{application.BandID}", Method.POST);
            client.Execute(request);

            band.Applications.Remove(application);
            mainWindow.GoToBandShow(band);
        }

        private void Decline_Button_Click(object sender, RoutedEventArgs e)
        {
            var application = (Models.Application)dataGridApplications.SelectedItem;
            var client = new RestClient(ConfigurationManager.AppSettings.Get("APIURL"));
            var request = new RestRequest($"application/decline/{application.ID}", Method.POST);
            client.Execute(request);

            band.Applications.Remove(application);
            mainWindow.GoToBandShow(band);
        }

        private void Back_Button_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.GoToBandIndex();
        }
    }
}
