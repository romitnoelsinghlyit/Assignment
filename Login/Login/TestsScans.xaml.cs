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
    /// Interaction logic for TestsScans.xaml
    /// </summary>
    public partial class TestsScans : Page
    {
        HMDatabaseEntities dbEntities = new HMDatabaseEntities();
        Billing billing = new Billing();
        Dashboard dashboard = new Dashboard();

        public TestsScans()
        {
            InitializeComponent();
        }

        private void chkXRAY_Checked(object sender, RoutedEventArgs e)
        {
            foreach (var patientRecord in dbEntities.Billings.Where(t => t.PatientID == billing.PatientID))
            {
                patientRecord.XRAY = 30;                
            }
        }

        private void chkMRI_Checked(object sender, RoutedEventArgs e)
        {

            foreach (var patientRecord in dbEntities.Billings.Where(t => t.PatientID == billing.PatientID))
            {
                patientRecord.MRI = 40;
            }
        }

        private void chkCTScan_Checked(object sender, RoutedEventArgs e)
        {

            foreach (var patientRecord in dbEntities.Billings.Where(t => t.PatientID == billing.PatientID))
            {
                patientRecord.CTScan = 40;
            }
        }

        private void chkUltrasound_Checked(object sender, RoutedEventArgs e)
        {

            foreach (var patientRecord in dbEntities.Billings.Where(t => t.PatientID == billing.PatientID))
            {
                patientRecord.Ultrasound = 30;
            }
        }

        private void chkBloodTest_Checked(object sender, RoutedEventArgs e)
        {

            foreach (var patientRecord in dbEntities.Billings.Where(t => t.PatientID == billing.PatientID))
            {
                patientRecord.BloodTest = 10;
            }
        }

        private void chkSugarTest_Checked(object sender, RoutedEventArgs e)
        {

            foreach (var patientRecord in dbEntities.Billings.Where(t => t.PatientID == billing.PatientID))
            {
                patientRecord.SugarTest = 10;
            }
        }

        private void chkAllergyTest_Checked(object sender, RoutedEventArgs e)
        {

            foreach (var patientRecord in dbEntities.Billings.Where(t => t.PatientID == billing.PatientID))
            {
                patientRecord.AllergyTest = 30;
            }
        }

        private void btnTestsScansOK_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var blankScreen = new BlankScreen();
                dashboard.frmeMainFrame.Navigate(blankScreen);
            }
            catch (Exception)
            {

                MessageBox.Show("Problem loading ICU Ward screen");
            }

        }
    }
}
