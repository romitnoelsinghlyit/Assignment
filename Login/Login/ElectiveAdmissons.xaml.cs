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
    public partial class ElectiveAdmissons : Page
    {
        HMDatabaseEntities dbEntities = new HMDatabaseEntities();
        List<Elective> electiveList = new List<Elective>();
        Dashboard dashboard = new Dashboard();
        Elective elective = new Elective();

        public ElectiveAdmissons()
        {
            InitializeComponent();
            lstElectivePatients.ItemsSource = electiveList;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            foreach (var electivePatient in dbEntities.Electives)
            {
                electiveList.Add(electivePatient);
            }
        }



        private void lstElectivePatients_SelectionChanged(object sender, SelectionChangedEventArgs e)
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
