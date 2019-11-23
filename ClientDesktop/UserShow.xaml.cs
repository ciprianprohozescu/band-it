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
    public partial class UserShow : Window
    {
        public UserShow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int id = Convert.ToInt32(SearchText.Text);

            var client = new RestClient(ConfigurationManager.AppSettings.Get("APIURL"));
            var request = new RestRequest($"user/{id}", Method.GET);

            var content = client.Execute(request);
            var user = JsonConvert.DeserializeObject<User>(content.Content);

            listSkills.ItemsSource = user.Skills;
        }
    }
}
