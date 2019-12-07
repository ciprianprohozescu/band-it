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

        public BandIndex(CreateBand createBand)
        {
            this.createBand = createBand;
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
            mainWindow.GoToBandShow((Band)dataGridBands.SelectedItem);
        }

        private void Apply_Button_Click(object sender, RoutedEventArgs e)
        {
            var band = (Band)dataGridBands.SelectedItem;
            var applied = false;
            var bandUser = false;
            foreach (var applications in band.Applications)
            {
                if(applications.UserID == LoggedInUser.User.ID)
                {
                    applied = true;
                }
            }
            foreach (var bandMember in band.BandUsers)
            {
                if(bandMember.UserID == LoggedInUser.User.ID)
                {
                    bandUser = true;
                }
            }
            if(applied == false && bandUser == false)
            {
                try
                {
                    var application = new ApplicationForm();
                    application.BandID = band.ID;
                    application.UserID = LoggedInUser.User.ID;
                    var dialog = new DialogBox();
                    if (dialog.ShowDialog() == true)
                    {
                        application.Message = dialog.ResponseText;
                        band.Applications.Add(ViewToLogic(application));

                        var client = new RestClient(ConfigurationManager.AppSettings.Get("APIURL"));
                        var request = new RestRequest("/application", Method.POST);
                        request.AddJsonBody(ViewToLogic(application));
                        var response = client.Execute(request);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
            else
            {
                if(bandUser == true)
                {
                    MessageBox.Show("You are already a member of this band");
                }
                else
                {
                    MessageBox.Show("You have already applied for this band");
                }
            }
            
        }

        private Models.Application ViewToLogic(ApplicationForm applicationForm)
        {
            var applicationLogic = new Models.Application();

            if (applicationForm != null)
            {
                applicationLogic.Message = applicationForm.Message;
                applicationLogic.BandID = applicationForm.BandID;
                applicationLogic.UserID = applicationForm.UserID;

                return applicationLogic;
            }

            return null;
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

        private void CreateBand_Button_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.GoToBandCreate();
        }
    }
}