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
    /// Interaction logic for ElectiveAdmissons.xaml
    /// </summary>
    public partial class EmergencyAdmissons : Page
    {
        HMDatabaseEntities dbEntities = new HMDatabaseEntities();
        List<Emergency> emergencyList = new List<Emergency>();
        Dashboard dashboard = new Dashboard();
        Emergency emergency = new Emergency();

        public EmergencyAdmissons()
        {
            InitializeComponent();
            lstEmergencyPatients.ItemsSource = emergencyList;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            foreach (var emergencyPatient in dbEntities.Emergencies)
            {
                emergencyList.Add(emergencyPatient);
            }
        }



        private void lstEmergencyPatients_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                BlankScreen blankScreen = new BlankScreen();
                dashboard.frmeMainFrame.Navigate(blankScreen);
            }
            catch (Exception)
            {

                MessageBox.Show("Problem loading Blank screen");
            }
        }
    }
}
