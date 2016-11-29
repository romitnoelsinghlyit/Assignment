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
        Patient currentPatient = new Patient();
        Emergency currentEmergency = new Emergency();
        Elective currentElective = new Elective();
        //string entityState = "Modify";
        string patientCategory;
        string patientAdmissionType;

        public PatientsPage()
        {
            //try
            //{
            InitializeComponent();
         
            //}
            //catch (Exception)
            //{
            //    MessageBox.Show("Problem during initialisation of application");
            //}
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            mtdClearPatientDetails();
        }


        private void mtdClearPatientDetails()
        {
            tbxForename.Text = "";
            tbxSurname.Text = "";
            tbxMaritalStatus.Text = "";
            tbxInsurance.Text = "";
            tbxOccupation.Text = "";
            tbxReligion.Text = "";
            tbxAddress.Text = "";
            tbxGP.Text = "";
            cmbSex.SelectedIndex = 1;
        }



        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            Patient patient = new Patient();

            int patientCount = dbEntities.Patients.Count() + 1;
            tbxPatientNumber.Text = patientCount.ToString();
            patient.PatientNumber = Convert.ToInt16(tbxPatientNumber.Text);

            patient.PatientID = Guid.NewGuid().ToString();
            dbEntities.Configuration.AutoDetectChangesEnabled = false;
            dbEntities.Configuration.ValidateOnSaveEnabled = false;
            dbEntities.Entry(patient).State = System.Data.Entity.EntityState.Added;
        }



        private void btnModify_Click(object sender, RoutedEventArgs e)
        {

        }


        private void btnSave_Click(object sender, RoutedEventArgs e)
        {

            //bool patientVerified = mtdVerifyPatientDetails(currentPatient);
            //if (patientVerified)
            //{
            currentPatient.Forename = tbxForename.Text.Trim();
            currentPatient.Surname = tbxSurname.Text.Trim();
            currentPatient.MaritalStatus = tbxMaritalStatus.Text.Trim();
            currentPatient.Insurance = tbxInsurance.Text.Trim();
            currentPatient.Occupation = tbxOccupation.Text.Trim();
            currentPatient.Religion = tbxReligion.Text.Trim();
            currentPatient.Address = tbxAddress.Text.Trim();
            currentPatient.GP = tbxGP.Text.Trim();
            currentPatient.PatientNumber = Convert.ToInt16(tbxPatientNumber.Text.Trim());
            currentPatient.Sex = cmbSex.SelectedItem.ToString();

            currentPatient.ArrivalDate = dtpArrivalDate.SelectedDate.Value;
            currentPatient.DateOfBirth = dtpDateOfBirth.SelectedDate.Value;
            currentPatient.PatientNumber = Convert.ToInt16(tbxPatientNumber.Text);

            currentPatient.Category = patientCategory;
            currentPatient.AdmissionType = patientAdmissionType;


            //mtdUpdatePatient(currentPatient, entityState);
            //}
        }

        //private bool mtdVerifyPatientDetails(Patient patient)
        //{
        //    bool patientVerified = false;
        //    try
        //    {
        //        if (patient.Forename == null)
        //        {
        //            patient.Forename = "";
        //        }
        //        if (patient.Surname == null)
        //        {
        //            patient.Surname = "";
        //        }
        //        if (patient.PatientID == null)
        //        {
        //            patient.PatientID = "";
        //        }
        //       patientVerified = true;
        //    }
        //    catch (Exception)
        //    {
        //        MessageBox.Show("Problem verifying patient");
        //    }
        //    return patientVerified;
        //}

        //private void mtdUpdatePatient(Patient patient, string modifyState)
        //{
        //    try
        //    {
        //        if (modifyState == "Modify")
        //        {
        //            foreach (var userRecord in dbEntities.Patients.Where(t => t.PatientID == patient.PatientID))
        //            {
        //                userRecord.Forename = patient.Forename;
        //                userRecord.Surname = patient.Surname;
        //                userRecord.Religion = patient.Religion;
        //                userRecord.MaritalStatus = patient.MaritalStatus;
        //                userRecord.Insurance = patient.Insurance;
        //                userRecord.Occupation = patient.Occupation;
        //                userRecord.Address = patient.Address;
        //                userRecord.GP = patient.GP;

        //                userRecord.Sex = patient.Sex;

        //                userRecord.ArrivalDate = patient.ArrivalDate.Date;
        //                userRecord.DateOfBirth = patient.DateOfBirth;

        //                userRecord.AdmissionType = patient.AdmissionType;
        //                userRecord.Category = patient.Category;

        //                MessageBox.Show("Patient modified");
        //            }
        //        }
        //        if (modifyState == "Delete")
        //        {
        //            dbEntities.Patients.RemoveRange(
        //         dbEntities.Patients.Where(t => t.PatientID == patient.PatientID));
        //            MessageBox.Show("Patient deleted");
        //        }
        //        dbEntities.SaveChanges();
        //        dbEntities.Configuration.AutoDetectChangesEnabled = true;
        //        dbEntities.Configuration.ValidateOnSaveEnabled = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Problem writing to database");
        //    }
        //}      


        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            // entityState = "Delete";
            //  mtdUpdatePatient(currentPatient, entityState);
        }


        // ///////////////////////////  ADMISSION TYPE   ///////////////////////////////////////////////////////
        private void rdoEmergency_Checked(object sender, RoutedEventArgs e)
        {
            patientAdmissionType = "Emergency";
        }

        private void rdoEmergency_Unchecked(object sender, RoutedEventArgs e)
        {
            rdoElective.IsChecked = true;
        }

        private void rdoElective_Checked(object sender, RoutedEventArgs e)
        {
            patientAdmissionType = "Elective";
        }

        private void rdoElective_Unchecked(object sender, RoutedEventArgs e)
        {
            rdoEmergency.IsChecked = true;
        }

        // //////////////////////////////////////////////////////////////////////////////////////////////////////


        // //////////////////////////////// PATIENT CATEGORY ///////////////////////////////////////////////////
        private void rdoGeneral_Checked(object sender, RoutedEventArgs e)
        {
            patientCategory = "General";
        }

        private void rdoSurgery_Checked(object sender, RoutedEventArgs e)
        {
            patientCategory = "Surgery";
        }

        private void rdoICU_Checked(object sender, RoutedEventArgs e)
        {
            patientCategory = "ICU";
        }

        private void rdoOrthopaedic_Checked(object sender, RoutedEventArgs e)
        {
            patientCategory = "Orthopaedic";
        }

        private void rdoPaediatric_Checked(object sender, RoutedEventArgs e)
        {
            patientCategory = "Paediatric";
        }
        // /////////////////////////////////////////////////////////////////////////////////////////////////////

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            int searchPatientNumber = Convert.ToInt16(tbxPatientNumber.Text);
            Patient patient = new Patient();
        }

        private void cmbSex_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var combobox = (System.Windows.Controls.ComboBox)sender;
            ComboBoxItem item = (ComboBoxItem)combobox.SelectedItem;
            string value;


            if (combobox.SelectedIndex > -1)
            {
                value = item.Content.ToString().Trim();
                //string value = item.Content.ToString().Trim();
                currentPatient.Sex = value;
            }

        }

    }
}



