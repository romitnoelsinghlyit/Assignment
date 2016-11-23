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
    /// Interaction logic for Administration.xaml
    /// </summary>
    public partial class Administration : Page
    {
        User localUser = new User();
        public HMDatabaseEntities dbEntities = new HMDatabaseEntities();

        public Administration()
        {
            InitializeComponent();
        }

            
        private void cboAccessLevel_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var comboBoxSelection = (ComboBox)sender;
            int comboBoxSelectedIndex = comboBoxSelection.SelectedIndex;
            string comboBoxSelectedValue = comboBoxSelection.SelectedItem.ToString();
            string comboBoxSelectedContent = comboBoxSelection.SelectedValue.ToString();
            string value = comboBoxSelectedValue;
            localUser.AccessLevel = comboBoxSelectedIndex + 1;
        }


        private void btnCreateUser_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                localUser.Forename = tbxCreateForename.Text.Trim();
                localUser.Surname = tbxCreateSurname.Text.Trim();
                localUser.UserName = tbxCreateUsername.Text.Trim();
                localUser.Password = tbxCreatePassword.Text.Trim();
                localUser.UserID = Guid.NewGuid().ToString();
                mtdWriteToDatabase(localUser, "Add");// we can either add, modify or delete one at atime
            }
            catch (Exception e)
            {
                MessageBox.Show("Problem with user information");
            }
        }

        private void mtdWriteToDatabase(UserControl user, string entityState)
        {
            try
            {


                if (entityState == "Add")
                {
                    dbEntities.Entry(user).State = System.Data.Entity.EntityState.Added;
                    /dbEntities.SaveChanges();
                }

                if (entityState == "Modify")
                {
                    foreach (var userRecord in dbEntities.Users.Where(tbxForename => tbxForename.userID == user.UserID))
                    {
                        userRecord = user;
                        userRecord.Forename = user.Forename;
                        userRecord.Surname = user.Surname;
                        userRecord.Password = user.Password;
                        userRecord.AccessLevel = user.AccessLevel;  //write every field
                    }
                }
                if (entityState == "Delete")
                {
                    dbEntities.Users.RemoveRange(
                        dbEntities.Users.Where(t => t.UserID == UserID));
                }
                dbEntities.SaveChanges();
            }
            catch (Exception)
            {

                MessageBox.Show("Problem");
            }
        }

    }
}
}
