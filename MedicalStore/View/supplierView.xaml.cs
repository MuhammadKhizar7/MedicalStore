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
    /// Interaction logic for supplierView.xaml
    /// </summary>
    public partial class supplierView
    {
        private MapperConfiguration _cfg;
        public supplierView()
        {
            InitializeComponent();
            using (var db = new ObjectContext())
            {
                InvoiceDg.ItemsSource = db.Suppliers.ToList();
            }
        }

        private void InvoiceDg_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!(InvoiceDg.SelectedItem is Supplier supplier)) return;
            _cfg = AutomapperConfig.Config<Supplier, SupplierViewModel>();
            CustomerInfo.DataContext = _cfg.CreateMapper().Map<SupplierViewModel>(supplier);
        }

        private void RefreshBtn_OnClick(object sender, RoutedEventArgs e)
        {
            CustomerInfo.DataContext = new SupplierViewModel();
        }

        private void SaveBtn_OnClick(object sender, RoutedEventArgs e)
        {
            var supplierViewModel = CustomerInfo.DataContext as SupplierViewModel;
            if (supplierViewModel != null && (string.IsNullOrEmpty(
                                            supplierViewModel.FirstName) || string.IsNullOrWhiteSpace(supplierViewModel.FirstName) ||
                                        string.IsNullOrEmpty(supplierViewModel.LastName) || string.IsNullOrWhiteSpace(supplierViewModel.LastName)))
            {
                MessageBox.Show("Name or First Name is null", "Attention!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            _cfg = AutomapperConfig.Config<SupplierViewModel, Supplier>();
            var supplier = _cfg.CreateMapper().Map<Supplier>(supplierViewModel);
            using (var db = new ObjectContext())
            {
                if (supplier.Id == 0)
                {
                    db.Suppliers.Add(supplier);
                    db.SaveChanges();
                }

                else
                {
                    db.Suppliers.AddOrUpdate(supplier);
                    db.SaveChanges();
                }
                   
                InvoiceDg.ItemsSource = db.Suppliers.ToList();
            }
        }

        private void DEletebtn_OnClick(object sender, RoutedEventArgs e)
        {
            var supplier = InvoiceDg.SelectedItem as Supplier;

            if (supplier == null) return;
            var result = MessageBox.Show("Are you sure you want to delete this record!", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (!result.ToString().Equals("Yes")) return;

            using (var db = new ObjectContext())
            {
                db.Suppliers.Remove(supplier);
                db.SaveChanges();
                InvoiceDg.ItemsSource = db.Suppliers.ToList();
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
