﻿using ClientDesktop.ViewModels;
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
    /// Interaction logic for CreateBand.xaml
    /// </summary>
    public partial class CreateBand : Page
    {
        MainWindow mainWindow;
        
        public CreateBand(MainWindow mainWindow)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
        }
       
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (Validation.GetHasError(txtBand))
            {
                return;
            }
            
            var band = new Band();
            band.Name = txtBand.Text;

            var client = new RestClient(ConfigurationManager.AppSettings.Get("APIURL"));
            var request = new RestRequest("/band/register", Method.POST);
            request.AddJsonBody(band);
            var response = client.Execute(request);

            mainWindow.GoToBandIndex();
        }

        private void txtBand_SelectionChanged(object sender, RoutedEventArgs e)
        {
            string Name = txtBand.Text;

            var client = new RestClient(ConfigurationManager.AppSettings.Get("APIURL"));
            var request = new RestRequest("band/name", Method.GET);
            request.AddParameter("name", Name);


            var content = client.Execute(request).Content;
            var band = JsonConvert.DeserializeObject<Band>(content);

            if (band != null)
            {
                txtErrorBandName.Text = "This band name is taken.";
                txtBand.BorderThickness = new Thickness(2, 2, 2, 2);
                txtBand.BorderBrush = Brushes.Red;
            }
            else
            {
                txtErrorBandName.Text = "";
                txtBand.BorderThickness = new Thickness(0, 0, 0, 0);
            }
        }
    }
}
