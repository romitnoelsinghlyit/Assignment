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
    /// Interaction logic for PatientsPage.xaml
    /// </summary>
    public partial class PatientsPage : Page
    {

        HMDatabaseEntities dbEntities = new HMDatabaseEntities();
        List<Patient> patientList = new List<Patient>();
        Patient currentPatient = new Patient();
        string entityState = "Modify";
         
        public PatientsPage()
        {
            //try
            //{
            //    InitializeComponent();
            //    mtdPopulateUserTable();
            //    lstPatientsList.ItemsSource = patientList;
            //}
            //catch (Exception)
            //{
            //    MessageBox.Show("Problem during initialisation of application");
            //}
        }

        private void mtdPopulateUserTable()
        {
            patientList.Clear();
            foreach (var patient in dbEntities.Patients)
            {
                patientList.Add(patient);
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {

        }

        //private void lstPatientsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    if (patientList.Count > 0)//Make sure a patient record exists in the database
        //    {
        //        if (lstPatientsList.SelectedIndex > -1)//ensures a record is selected
        //        {
        //            //Gets the patient from the patientList at the same position it is in within the ListView
        //            currentPatient = patientList.ElementAt(this.lstPatientsList.SelectedIndex);
        //            mtdPopulateUserDetails(currentPatient);
        //        }
        //    }
        //}

        private void mtdPopulateUserDetails(Patient selectedPatient)
        {
            try
            {
                dtpArrivalDate.SelectedDate = selectedPatient.ArrivalDate;
                dtpDateOfBirth.SelectedDate = selectedPatient.DateOfBirth;

                tbxForename.Text = selectedPatient.Forename;
                tbxSurname.Text = selectedPatient.Surname;
                tbxReligion.Text = selectedPatient.Religion;
                tbxInsurance.Text = selectedPatient.Insurance;
                tbxMaritalStatus.Text = selectedPatient.MaritalStatus;
                tbxInsurance.Text = selectedPatient.Insurance;
                tbxReligion.Text = selectedPatient.Religion;
                tbxInsurance.Text = selectedPatient.Insurance;
                tbxOccupation.Text = selectedPatient.Occupation;
                tbxAddress.Text = selectedPatient.Address;
                tbxGP.Text = selectedPatient.GP;

                // ADMISSION TYPE
                if (rdoEmergency.IsChecked == true)
                {
                    selectedPatient.AdmissionType = "Emergency";
                }
                if (rdoElective.IsChecked == true)
                {
                    selectedPatient.AdmissionType = "Elective";
                }

                // CATEGORY
                if (rdoGeneral.IsChecked == true)
                {
                    selectedPatient.Category = "General";
                }

                if (rdoICU.IsChecked == true)
                {
                    selectedPatient.Category = "ICU";
                }

                if (rdoSurgery.IsChecked == true)
                {
                    selectedPatient.Category = "Surgery";
                }

                if (rdoOrthopaedic.IsChecked == true)
                {
                    selectedPatient.Category = "Orthopaedic";
                }
                if (rdoPaediatric.IsChecked == true)
                {
                    selectedPatient.Category = "Paediatric";
                }

            }
            catch (Exception)
            {
                MessageBox.Show("Problem importing patient information");
            }

        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            currentPatient.Forename = tbxUserForename.Text.Trim();
            currentPatient.Surname = tbxPatientsurname.Text.Trim();
            currentPatient.Password = tbxUserPassword.Text.Trim();
            currentPatient.UserName = tbxUsername.Text.Trim();
            currentPatient.AccessLevel = cboAccessLevel.SelectedIndex;
            bool userVerified = mtdVerifyUserDetails(currentPatient);
            if (userVerified)
            {
                mtdUpdateUser(currentPatient, entityState);
                mtdPopulateUserTable();
                lstPatientsList.Items.Refresh();
            }
            dockUserPanel.Visibility = Visibility.Collapsed;
        }

        private bool mtdVerifyUserDetails(Patient patient)
        {
            bool userVerified = false;
            try
            {
                if (patient.Forename == null)
                {
                    patient.Forename = "";
                }
                if (patient.Surname == null)
                {
                    patient.Surname = "";
                }
                if (patient.UserName == null)
                {
                    patient.UserName = "";
                }
                if (patient.Password == null)
                {
                    patient.Password = "";
                }
                if (patient.AccessLevel == -1)
                {
                    patient.AccessLevel = 2;
                }
                userVerified = true;
            }
            catch (Exception)
            {
                MessageBox.Show("Problem verifying patient");
            }
            return userVerified;
        }

        private void mtdUpdateUser(Patient patient, string modifyState)
        {
            try
            {
                if (modifyState == "Add")
                {
                    patient.UserID = Guid.NewGuid().ToString();//Create new UsedID for database                 
                    dbEntities.Configuration.AutoDetectChangesEnabled = false;
                    dbEntities.Configuration.ValidateOnSaveEnabled = false;
                    dbEntities.Entry(patient).State = System.Data.Entity.EntityState.Added;
                    MessageBox.Show("New patient added");
                }
                if (modifyState == "Modify")
                {
                    foreach (var userRecord in dbEntities.Patients.Where(t => t.UserID == patient.UserID))
                    {
                        userRecord.Forename = patient.Forename;
                        userRecord.Surname = patient.Surname;
                        userRecord.UserName = patient.UserName;
                        userRecord.Password = patient.Password;
                        userRecord.AccessLevel = patient.AccessLevel;
                        MessageBox.Show("Patient modified");
                    }
                }
                if (modifyState == "Delete")
                {
                    dbEntities.Patients.RemoveRange(
                 dbEntities.Patients.Where(t => t.UserID == patient.UserID));
                    MessageBox.Show("Patient deleted");
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
            mtdClearUserDetails();
            entityState = "Add";
        }

        private void mtdClearUserDetails()
        {
            tbxUserForename.Text = "";
            tbxPatientsurname.Text = "";
            tbxUsername.Text = "";
            tbxUserPassword.Text = "";
            cboAccessLevel.SelectedIndex = 0;
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            entityState = "Delete";
            mtdUpdateUser(currentPatient, entityState);
            mtdPopulateUserTable();
            lstPatientsList.Items.Refresh();
            dockUserPanel.Visibility = Visibility.Collapsed;
        }
    }
}
