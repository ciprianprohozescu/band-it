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
        public User user;

        public UserShow(User user)
        {
            InitializeComponent();

            this.user = user;
            listSkills.ItemsSource = user.Skills;
        }
    }
}
