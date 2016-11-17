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
using System.Windows.Shapes;

namespace Login
{
    /// <summary>
    /// Interaction logic for Dashboard.xaml
    /// </summary>
    public partial class Dashboard : Window
    {
        public User user = new User();
        public Dashboard()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void frmeMainFrame_Navigated(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {

        }

        private void btnPatients_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnSearchText_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnResetSearch_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnEmergency_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnElective_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnGeneralWard_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnSurgicalWard_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnICU_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnOrthopaedicWard_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnPaediatricWard_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnTestsScans_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnBilling_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnAdministration_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            Environment.Exit(0);
        }

    }
}