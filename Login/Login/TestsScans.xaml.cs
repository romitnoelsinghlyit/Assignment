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
               private void btnTestsScansOK_Click(object sender, RoutedEventArgs e)
        {
            foreach (var patientRecord in dbEntities.Billings.Where(t => t.PatientID == billing.PatientID))
            {
                if (chkXRAY.IsChecked==true)
                {
                    patientRecord.XRAY = 30;
                }
     
                if (chkMRI.IsChecked==true)
                {
                    patientRecord.MRI = 40;
                }

                if (chkCTScan.IsChecked==true)
                {
                    patientRecord.CTScan = 40;
                }

                if (chkUltrasound.IsChecked==true)
                {
                    patientRecord.Ultrasound = 30;
                }

                if (chkBloodTest.IsChecked==true)
                {
                    patientRecord.BloodTest = 10;
                }

                if (chkSugarTest.IsChecked==true)
                {
                    patientRecord.SugarTest = 10;
                }

                if (chkAllergyTest.IsChecked==true)
                {
                    patientRecord.AllergyTest = 30;
                }

            }

        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
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
