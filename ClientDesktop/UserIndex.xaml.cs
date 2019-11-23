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

using User = Models.User;

namespace ClientDesktop
{
    public partial class UserIndex : Window
    {
        public UserIndex()
        {
            InitializeComponent();
        }

        private void Search_Button_Click(object sender, RoutedEventArgs e)
        {
            string search = SearchText.Text;
            var client = new RestClient(ConfigurationManager.AppSettings.Get("APIURL"));
            var request = new RestRequest("user", Method.GET);
            request.AddParameter("search", search);
            var content = client.Execute(request).Content;

            var users = JsonConvert.DeserializeObject<List<User>>(content);

          

            dataGridUsers.ItemsSource = users;
        }
    }
}