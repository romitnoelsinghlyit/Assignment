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
        string entityState = "Modify";


        public PatientsPage()
        {
            try
            {
                InitializeComponent();
                mtdClearPatientDetails();                
            }
            catch (Exception)
            {
                MessageBox.Show("Problem during initialisation of application");
            }
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
            entityState = "Add";
            patient.PatientNumber = Convert.ToInt16(tbxPatientNumber.Text);
        }

        private void btnModify_Click(object sender, RoutedEventArgs e)
        {
           
        }


        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            //entityState = "Modify";

            bool patientVerified = mtdVerifyPatientDetails(currentPatient);
            if (patientVerified)
            {
                mtdUpdatePatient(currentPatient, entityState);
            }


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
            currentPatient.DateOfBirth = dtpDateOfBirth.SelectedDate.Value ;
            currentPatient.PatientNumber = Convert.ToInt16( tbxPatientNumber.Text);             

                      
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
                    patient.PatientID = Guid.NewGuid().ToString();                 
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
                        userRecord.Religion = patient.Religion;
                        userRecord.MaritalStatus = patient.MaritalStatus;
                        userRecord.Insurance = patient.Insurance;
                        userRecord.Occupation = patient.Occupation;
                        userRecord.Address = patient.Address;
                        userRecord.GP = patient.GP;

                        userRecord.Sex = patient.Sex;

                        userRecord.ArrivalDate = patient.ArrivalDate;
                        userRecord.DateOfBirth = patient.DateOfBirth;

                        userRecord.AdmissionType = patient.AdmissionType;
                        userRecord.Category = patient.Category;

                        MessageBox.Show("Patient modified");
                    }
                }
                if (modifyState == "Delete")
                {
                    dbEntities.Patients.RemoveRange(
                 dbEntities.Patients.Where(t => t.PatientID == patient.PatientID));
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



        

       

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            entityState = "Delete";
            mtdUpdatePatient(currentPatient, entityState);
        }

        private void rdoEmergency_Checked(object sender, RoutedEventArgs e)
        {
            currentPatient.AdmissionType = "Emergency";
            //currentEmergency.ArrivalDate = dtpArrivalDate.SelectedDate;
            //currentEmergency.DateOfBirth = dtpArrivalDate.SelectedDate;
            //currentEmergency.Forename = tbxForename.Text;
            //currentEmergency.Surname = tbxSurname.Text;



        }

        private void rdoEmergency_Unchecked(object sender, RoutedEventArgs e)
        {
            rdoElective.IsChecked = true;
        }

        private void rdoElective_Checked(object sender, RoutedEventArgs e)
        {
            currentPatient.AdmissionType = "Elective";
            //currentElective.ArrivalDate = dtpArrivalDate.SelectedDate;
            //currentlective.DateOfBirth = dtpArrivalDate.SelectedDate;
            //currentElective.Forename = tbxForename.Text;
            //currentElective.Surname = tbxSurname.Text;
        }

        private void rdoElective_Unchecked(object sender, RoutedEventArgs e)
        {
            rdoEmergency.IsChecked = true;
        }

        private void rdoGeneral_Checked(object sender, RoutedEventArgs e)
        {
            currentPatient.Category = "General";
        }

        private void rdoSurgery_Checked(object sender, RoutedEventArgs e)
        {
            currentPatient.Category = "Surgery";
        }

        private void rdoICU_Checked(object sender, RoutedEventArgs e)
        {
            currentPatient.Category = "ICU";
        }

        private void rdoOrthopaedic_Checked(object sender, RoutedEventArgs e)
        {
            currentPatient.Category = "Orthopaedic";
        }

        private void rdoPaediatric_Checked(object sender, RoutedEventArgs e)
        {
            currentPatient.Category = "Paediatric";
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            int searchPatientNumber = Convert.ToInt16( tbxPatientNumber.Text);
            Patient patient = new Patient();
        }

        private void cmbSex_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
