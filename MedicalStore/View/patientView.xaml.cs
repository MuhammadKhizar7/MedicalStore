using System;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using AutoMapper;
using MedicalStore.Data;
using MedicalStore.Data.Infrastructure;
using MedicalStore.Model.Entities;
using MedicalStore.ViewModel;

namespace MedicalStore.View
{
    /// <summary>
    /// Interaction logic for patientView.xaml
    /// </summary>
    public partial class patientView : UserControl
    {
        private MapperConfiguration _cfg;
        public patientView()
        {
            InitializeComponent();
            using (var db = new ObjectContext())
            {
                InvoiceDg.ItemsSource = db.Patients.ToList();
            }
        }

        private void InvoiceDg_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!(InvoiceDg.SelectedItem is Patient patient)) return;
            _cfg = AutomapperConfig.Config<Patient, PatientViewModel>();
            PatientInfo.DataContext = _cfg.CreateMapper().Map<PatientViewModel>(patient);
        }

        private void RefreshBtn_OnClick(object sender, RoutedEventArgs e)
        {
            PatientInfo.DataContext = new PatientViewModel();
        }

        private void SaveBtn_OnClick(object sender, RoutedEventArgs e)
        {
            var patientViewModel = PatientInfo.DataContext as PatientViewModel;
            if (patientViewModel != null && (string.IsNullOrEmpty(
                                            patientViewModel.Name) || string.IsNullOrWhiteSpace(patientViewModel.Name) ||
                                        string.IsNullOrEmpty(patientViewModel.Disease) || string.IsNullOrWhiteSpace(patientViewModel.Disease)))
            {
                MessageBox.Show("Name or Disease is null", "Attention!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            _cfg = AutomapperConfig.Config<PatientViewModel, Patient>();
            var patient = _cfg.CreateMapper().Map<Patient>(patientViewModel);
            using (var db = new ObjectContext())
            {
                if (patient.Id == 0)
                {
                    db.Patients.Add(patient);
                    db.SaveChanges();
                }

                else
                {
                    db.Patients.AddOrUpdate(patient);
                    db.SaveChanges();
                }

                InvoiceDg.ItemsSource = db.Patients.ToList();
            }
        }

        private void DEletebtn_OnClick(object sender, RoutedEventArgs e)
        {
            var patient = InvoiceDg.SelectedItem as Patient;

            if (patient == null) return;
            var result = MessageBox.Show("Are you sure you want to delete this record!", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (!result.ToString().Equals("Yes")) return;

            using (var db = new ObjectContext())
            {
                db.Patients.Remove(patient);
                db.SaveChanges();
                InvoiceDg.ItemsSource = db.Patients.ToList();
            }
        }

        private void PrintBtn_OnClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void ExpPdfBtn_OnClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
