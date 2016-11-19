﻿using System;
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
            try
            {
                var patientsPage = new PatientsPage();
                frmeMainFrame.Navigate(patientsPage);
            }
            catch (Exception)
            {

                MessageBox.Show("Problem loading patients screen");
            }
            
        }

        private void btnSearchText_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnResetSearch_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnEmergency_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var emergencyAdmissons = new EmergencyAdmissons();
                frmeMainFrame.Navigate(emergencyAdmissons);
            }
            catch (Exception)
            {

                MessageBox.Show("Problem loading Emergency screen");
            }

        }

        private void btnElective_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var electiveAdmissons = new ElectiveAdmissons();
                frmeMainFrame.Navigate(electiveAdmissons);
            }
            catch (Exception)
            {

                MessageBox.Show("Problem loading Elective screen");
            }
        }

        private void btnGeneralWard_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var generalWard = new GeneralWard();
                frmeMainFrame.Navigate(generalWard);
            }
            catch (Exception)
            {

                MessageBox.Show("Problem loading General Ward screen");
            }
        }

        private void btnSurgicalWard_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var surgicalWard = new SurgicalWard();
                frmeMainFrame.Navigate(surgicalWard);
            }
            catch (Exception)
            {

                MessageBox.Show("Problem loading Surgical Ward screen");
            }
        }

        private void btnICU_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var icuWard = new ICU();
                frmeMainFrame.Navigate(icuWard);
            }
            catch (Exception)
            {

                MessageBox.Show("Problem loading ICU Ward screen");
            }
        }

        private void btnOrthopaedicWard_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var orthopaedicWard = new OrthopaedicWard();
                frmeMainFrame.Navigate(orthopaedicWard);
            }
            catch (Exception)
            {

                MessageBox.Show("Problem loading Orthopaedic Ward screen");
            }
        }

        private void btnPaediatricWard_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var paediatricWard = new PaediatricWard();
                frmeMainFrame.Navigate(paediatricWard);
            }
            catch (Exception)
            {

                MessageBox.Show("Problem loading Paediatric Ward screen");
            }
        }

        private void btnTestsScans_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var testsScans = new TestsScans();
                frmeMainFrame.Navigate(testsScans);
            }
            catch (Exception)
            {

                MessageBox.Show("Problem loading Paediatric Ward screen");
            }
        }
    

        private void btnBilling_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var billing = new Billing();
                frmeMainFrame.Navigate(billing);
            }
            catch (Exception)
            {

                MessageBox.Show("Problem loading Billing screen");
            }
        }

        private void btnAdministration_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var administration = new Administration();
                frmeMainFrame.Navigate(administration);
            }
            catch (Exception)
            {

                MessageBox.Show("Problem loading Administration screen");
            }
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            Environment.Exit(0);
        }

    }
}