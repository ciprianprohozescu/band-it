using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
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

using Band = Models.Band;
using ClientDesktop.ViewModels;

namespace ClientDesktop
{
    public partial class BandIndex : Page
    {
        MainWindow mainWindow;

        public BandIndex(MainWindow mainWindow)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
        }

        private void Search_Button_Click(object sender, RoutedEventArgs e)
        {
            string search = SearchText.Text;
            var client = new RestClient(ConfigurationManager.AppSettings.Get("APIURL"));
            var request = new RestRequest("band", Method.GET);
            request.AddParameter("search", search);
            var content = client.Execute(request).Content;

            var bands = JsonConvert.DeserializeObject<List<Band>>(content);

            dataGridBands.ItemsSource = bands;
        }

        private void Row_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            mainWindow.GoToBandEdit((Band)dataGridBands.SelectedItem);
        }

        private void LogOut_Button_Click(object sender, RoutedEventArgs e)
        {
            LoggedInUser.User = null;
            mainWindow.GoToLogin();
        }

        private void UserIndex_Button_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.GoToUserIndex();
        }
    }
}