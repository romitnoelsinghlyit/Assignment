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
            try
            {
                InitializeComponent();
                mtdPopulatePatientTable();
            //    lstPatientsList.ItemsSource = patientList;
            }
            catch (Exception)
            {
                MessageBox.Show("Problem during initialisation of application");
            }
        }

        private void mtdPopulatePatientTable()
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
        //            mtdPopulatePatientDetails(currentPatient);
        //        }
        //    }
        //}

        private void mtdPopulatePatientDetails(Patient selectedPatient)
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
            currentPatient.Forename = tbxForename.Text.Trim();
            currentPatient.Surname = tbxSurname.Text.Trim();
            currentPatient.MaritalStatus = tbxMaritalStatus.Text.Trim();
            currentPatient.Insurance = tbxInsurance.Text.Trim();
            currentPatient.Occupation = tbxOccupation.Text.Trim();
            currentPatient.Religion = tbxReligion.Text.Trim();
            currentPatient.Address = tbxAddress.Text.Trim();
            currentPatient.GP = tbxGP.Text.Trim();






            currentPatient.Occupation = cboAccessLevel.SelectedIndex;
            bool patientVerified = mtdVerifyPatientDetails(currentPatient);
            if (patientVerified)
            {
                mtdUpdatePatient(currentPatient, entityState);
                mtdPopulatePatientTable();               
            }           
        }

        private bool mtdVerifyPatientDetails(Patient patient)
        {
            bool patientVerified = false;
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
                if (patient.PatientID == null)
                {
                    patient.PatientID = "";
                }
               patientVerified = true;
            }
            catch (Exception)
            {
                MessageBox.Show("Problem verifying patient");
            }
            return patientVerified;
        }

        private void mtdUpdatePatient(Patient patient, string modifyState)
        {
            try
            {
                if (modifyState == "Add")
                {
                    patient.PatientID = Guid.NewGuid().ToString();//Create new UsedID for database                 
                    dbEntities.Configuration.AutoDetectChangesEnabled = false;
                    dbEntities.Configuration.ValidateOnSaveEnabled = false;
                    dbEntities.Entry(patient).State = System.Data.Entity.EntityState.Added;
                    MessageBox.Show("New patient added");
                }
                if (modifyState == "Modify")
                {
                    foreach (var userRecord in dbEntities.Patients.Where(t => t.PatientID == patient.PatientID))
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



        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            mtdClearPatientDetails();
            entityState = "Add"; 
        } 

        private void mtdClearPatientDetails()
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
            mtdUpdatePatient(currentPatient, entityState);
            mtdPopulatePatientTable();
            lstPatientsList.Items.Refresh();
            dockUserPanel.Visibility = Visibility.Collapsed;
        }       
    }
}
