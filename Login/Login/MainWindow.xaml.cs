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
            tbxUserName.Focus();
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
            mtdLoadUsers();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            Environment.Exit(0);
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            User userDetails = new User();
            string currentUser = tbxUserName.Text.Trim();
            string currentPassword = tbxPassword.Password;
            userDetails = mtdGetUserDetails(currentUser, currentPassword);
             try
             {
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
                    tbxUserName.Text = "";
                    tbxPassword.Password = "";
                    tbxUserName.Focus();
                }
             }
             catch (InvalidCastException)
             {
                Console.WriteLine(currentUser+" user is invalid.");
                Console.WriteLine(currentPassword+" password is invalid.");
            }
            catch (FormatException)
            {
                Console.WriteLine(currentUser + " user has wrong format.");
                Console.WriteLine(currentPassword + " user has wrong format.");
            }
        }

        private User mtdGetUserDetails(string UserName, string password)
        {
            User userDetails = new User();
            foreach (var user in userList)
            {
                if (UserName == user.UserName && password == user.Password)
                {
                    userDetails = user;
                }
            }
            return userDetails;
        }
    }
}

