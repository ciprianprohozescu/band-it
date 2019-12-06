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
    /// Interaction logic for BandForm.xaml
    /// </summary>
    public partial class BandForm : Page
    {
        MainWindow mainWindow;
        Band band;

        public BandForm(MainWindow mainWindow, Band band)
        {
            InitializeComponent();

            this.mainWindow = mainWindow;
            this.band = band;

            title.Content = $"Edit {band.Name}";
            nameInput.Text = band.Name;
            descriptionInput.Text = band.Description;
            inviteMessageInput.Text = band.InviteMessage;

            nameError.Content = band.NameError;
            concurrencyError.Content = band.RowVersionError;
        }

        private void Back_Button_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.GoToBandIndex();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            band.Name = nameInput.Text;
            band.Description = descriptionInput.Text;
            band.InviteMessage = inviteMessageInput.Text;

            var client = new RestClient(ConfigurationManager.AppSettings.Get("APIURL"));
            var request = new RestRequest("band/update", Method.POST);
            request.AddJsonBody(band);

            var newBand = JsonConvert.DeserializeObject<Band>(client.Execute(request).Content);
            mainWindow.GoToBandEdit(newBand);
        }
    }
}
