using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
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
using AutoMapper;
using MedicalStore.Data;
using MedicalStore.Data.Infrastructure;
using MedicalStore.Model.Entities;
using MedicalStore.ViewModel;

namespace MedicalStore.View
{
    /// <summary>
    /// Interaction logic for medicineView.xaml
    /// </summary>
    public partial class medicineView : UserControl
    {
        private MapperConfiguration _cfg;

        public medicineView()
        {
            InitializeComponent();
            using (var db = new ObjectContext())
            {

                InvoiceDg.ItemsSource = db.Medicines.ToList();
                Cat.ItemsSource = db.Categories.ToList();
            }
        }

        private void RefreshBtn_OnClick(object sender, RoutedEventArgs e)
        {
            CustomerInfo.DataContext = new MedicineViewModel();
            InvoiceDg.SelectedIndex = -1;
        }

        private void SaveBtn_OnClick(object sender, RoutedEventArgs e)
        {
            var medicineViewModel = CustomerInfo.DataContext as MedicineViewModel;
            if (medicineViewModel != null && (string.IsNullOrEmpty(
                                                 medicineViewModel.Name) || string.IsNullOrWhiteSpace(medicineViewModel.Name)))
            {
                MessageBox.Show("Medicine is null", "Attention!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            _cfg = AutomapperConfig.Config<MedicineViewModel, Medicine>();
            var medicine = _cfg.CreateMapper().Map<Medicine>(medicineViewModel);
            medicine.CreatedAt = DateTime.Now;
            switch (CbMedicineType.Text)
            {
                case "Tablet":
                    medicine.MedicineType = MedicineType.Tablet;
                    break;
                case "Injection":
                    medicine.MedicineType = MedicineType.Injection;
                    break;
                case "Cream":
                    medicine.MedicineType = MedicineType.Cream;
                    break;
                case "Ointment":
                    medicine.MedicineType = MedicineType.Ointment;
                    break;
                case "Syrup":
                    medicine.MedicineType = MedicineType.Syrup;
                    break;
                case "Other":
                    medicine.MedicineType = MedicineType.Other;
                    break;
            }

            using (var db = new ObjectContext())
            {
                if (medicine.Id == 0)
                {
                    db.Medicines.Add(medicine);
                    db.SaveChanges();
                }
                else
                {
                    db.Medicines.AddOrUpdate(medicine);
                    db.SaveChanges();
                }
                    
                InvoiceDg.ItemsSource = db.Medicines.ToList();
            }
        }

        private void AddBtn_OnClicktn_OnClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void DEletebtn_OnClick(object sender, RoutedEventArgs e)
        {
            var medicine = InvoiceDg.SelectedItem as Medicine;

            if (medicine == null) return;
            var result = MessageBox.Show("Are you sure you want to delete this record!", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (!result.ToString().Equals("Yes")) return;

            using (var db = new ObjectContext())
            {
                var med = db.Medicines.Single(c => c.Id == medicine.Id);
                db.Medicines.Remove(med);
                db.SaveChanges();
                InvoiceDg.ItemsSource = db.Medicines.ToList();
            }
        }

        private void ExpPdfBtn_OnClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void TextBoxBase_OnSelectionChanged(object sender, RoutedEventArgs e)
        {
            var term = ((TextBox)sender).Text;
            using (var db = new ObjectContext())
            {
                InvoiceDg.ItemsSource = db.Medicines.Where(x => x.Name.Contains(term)).ToList();
            }
        }

        private void InvoiceDg_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!(InvoiceDg.SelectedItem is Medicine medicine)) return;
            _cfg = AutomapperConfig.Config<Medicine, MedicineViewModel>();
            CustomerInfo.DataContext = _cfg.CreateMapper().Map<MedicineViewModel>(medicine);
        }
    }
}
