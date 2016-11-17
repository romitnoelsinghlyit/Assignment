using System;
using System.Collections.Generic;
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

namespace Login
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<User> userList = new List<User>();

        HMDatabaseEntities dbEntities = new HMDatabaseEntities();

        public MainWindow()
        {
            InitializeComponent();
            tbxUserID.Focus();
        }

        private void mtdLoadUsers()
        {
            userList.Clear();
            foreach (var user in dbEntities.Users)
            {
                userList.Add(user);

            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            Environment.Exit(0);
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            User userDetails = new User();
            string currentUser = tbxUserID.Text.Trim();
            string currentPassword = tbxPassword.Password;
            userDetails = mtdGetUserDetails(currentUser, currentPassword);
            if (userDetails.AccessLevel > 0)
            {
                this.Hide();
                Dashboard dashBoard = new Dashboard();
                dashBoard.user = userDetails;
                dashBoard.ShowDialog();
            }
            else
            {
                lblLoginAdvice.Content = "Invalid details!";
                tbxUserID.Text = "";
                tbxPassword.Password = "";
                tbxUserID.Focus();
            }
        }

        private User mtdGetUserDetails(string userID, string password)
        {
            User userDetails = new User();
            foreach (var user in userList)
            {
                if (userID == user.UserID && password == user.Password)
                {
                    userDetails = user;
                }
            }
            return userDetails;
        }
    }
}

