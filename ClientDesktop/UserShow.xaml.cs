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
        MainWindow mainWindow;
        public User user;

        public UserShow(MainWindow mainWindow, User user)
        {
            InitializeComponent();

            this.mainWindow = mainWindow;
            this.user = user;
            listSkills.ItemsSource = user.Skills;
        }
    }
}
