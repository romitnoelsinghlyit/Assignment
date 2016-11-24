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
    /// Interaction logic for UserAdministration.xaml
    /// </summary>
    public partial class UserAdministration : Page
    {
        HMDatabaseEntities dbEntities = new HMDatabaseEntities();
        List<User> userList = new List<User>();
        User currentUser = new User();
        string entityState = "Modify";

        public UserAdministration()
        {
            try
            {
                InitializeComponent();
                mtdPopulateUserTable();
                lstUsersList.ItemsSource = userList;
            }
            catch (Exception)
            {
                MessageBox.Show("Problem during initialisation of application");
            }
        } 

        private void mtdPopulateUserTable()
        {
            userList.Clear();
            foreach (var user in dbEntities.Users)
            {
                userList.Add(user);
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {

        }

        private void lstUsersList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (userList.Count > 0)//Make sure a user record exists in the database
            {
                if (lstUsersList.SelectedIndex > -1)//ensures a record is selected
                {
                    //Gets the user from the userList at the same position it is in within the ListView
                    currentUser = userList.ElementAt(this.lstUsersList.SelectedIndex);
                    mtdPopulateUserDetails(currentUser);
                }
            }
        }

        private void mtdPopulateUserDetails(User selectedUser)
        {
            try
            {
                dockUserPanel.Visibility = Visibility.Visible;
                tbxUserForename.Text = selectedUser.Forename;
                tbxUserSurname.Text = selectedUser.Surname;
                tbxUserPassword.Text = selectedUser.Password;
                tbxUsername.Text = selectedUser.UserName;
                if (selectedUser.AccessLevel == 1)
                {
                    cboAccessLevel.SelectedIndex = 0;
                }
                if (selectedUser.AccessLevel == 2)//A new record may need to be created and its index will =0
                {
                    cboAccessLevel.SelectedIndex = 1;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Problem importing user information");
            }

        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            currentUser.Forename = tbxUserForename.Text.Trim();
            currentUser.Surname = tbxUserSurname.Text.Trim();
            currentUser.Password = tbxUserPassword.Text.Trim();
            currentUser.UserName = tbxUsername.Text.Trim();
            currentUser.AccessLevel = cboAccessLevel.SelectedIndex;
            bool userVerified = mtdVerifyUserDetails(currentUser);
            if (userVerified)
            {
                mtdUpdateUser(currentUser, entityState);
                mtdPopulateUserTable();
                lstUsersList.Items.Refresh();
            }
            dockUserPanel.Visibility = Visibility.Collapsed;
        }

        private bool mtdVerifyUserDetails(User user)
        {
            bool userVerified = false;
            try
            {
                if (user.Forename == null)
                {
                    user.Forename = "";
                }
                if (user.Surname == null)
                {
                    user.Surname = "";
                }
                if (user.UserName == null)
                {
                    user.UserName = "";
                }
                if (user.Password == null)
                {
                    user.Password = "";
                }
                if (user.AccessLevel == -1)
                {
                    user.AccessLevel = 2;
                }
                userVerified = true;
            }
            catch (Exception)
            {
                MessageBox.Show("Problem verifying user");
            }
            return userVerified;
        }

        private void mtdUpdateUser(User user, string modifyState)
        {
            try
            {
                if (modifyState == "Add")
                {
                    user.UserID = Guid.NewGuid().ToString();//Create new UsedID for database                 
                    dbEntities.Configuration.AutoDetectChangesEnabled = false;
                    dbEntities.Configuration.ValidateOnSaveEnabled = false;
                    dbEntities.Entry(user).State = System.Data.Entity.EntityState.Added;
                    MessageBox.Show("New user added");
                }
                if (modifyState == "Modify")
                {
                    foreach (var userRecord in dbEntities.Users.Where(t => t.UserID == user.UserID))
                    {
                        userRecord.Forename = user.Forename;
                        userRecord.Surname = user.Surname;
                        userRecord.UserName = user.UserName;
                        userRecord.Password = user.Password;
                        userRecord.AccessLevel = user.AccessLevel;
                        MessageBox.Show("User modified");
                    }
                }
                if (modifyState == "Delete")
                {
                    dbEntities.Users.RemoveRange(
                 dbEntities.Users.Where(t => t.UserID == user.UserID));
                    MessageBox.Show("User deleted");
                }
                dbEntities.SaveChanges();
                dbEntities.Configuration.AutoDetectChangesEnabled = true;
                dbEntities.Configuration.ValidateOnSaveEnabled = true;
            }
            catch (Exception)
            {
                MessageBox.Show("Problem writing to database");
            }
        }

        private void btnAddUser_Click(object sender, RoutedEventArgs e)
        {
            dockUserPanel.Visibility = Visibility.Visible;
            mtdClearUserDetails();
            entityState = "Add";
        }

        private void mtdClearUserDetails()
        {
            tbxUserForename.Text = "";
            tbxUserSurname.Text = "";
            tbxUsername.Text = "";
            tbxUserPassword.Text = "";
            cboAccessLevel.SelectedIndex = 0;
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            entityState = "Delete";
            mtdUpdateUser(currentUser, entityState);
            mtdPopulateUserTable();
            lstUsersList.Items.Refresh();
            dockUserPanel.Visibility = Visibility.Collapsed;
        }
    }
}
