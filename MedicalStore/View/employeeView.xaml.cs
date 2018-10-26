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
    /// Interaction logic for employeeView.xaml
    /// </summary>
    public partial class employeeView : UserControl
    {
        private MapperConfiguration _cfg;

        public employeeView()
        {
            InitializeComponent();
            using (var db = new ObjectContext())
            {
                InvoiceDg.ItemsSource = db.Employees.ToList();
            }
        }

        private void SaveBtn_OnClick(object sender, RoutedEventArgs e)
        {

            var model = EmployeeInfo.DataContext as EmployeeViewModel;
            if (model != null && (string.IsNullOrEmpty(
                                      model.FullName) || string.IsNullOrWhiteSpace(model.FullName) ||
                                  string.IsNullOrEmpty(model.UserName) || string.IsNullOrWhiteSpace(model.UserName)))
            {
                MessageBox.Show("Name or Username is null", "Attention!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            _cfg = AutomapperConfig.Config<EmployeeViewModel, Employee>();
            var employee = _cfg.CreateMapper().Map<Employee>(model);
            using (var db = new ObjectContext())
            {
                if (employee.Id == 0)
                {
                    db.Employees.Add(employee);
                    db.SaveChanges();
                }
                else
                {
                    db.Employees.AddOrUpdate(employee);
                    db.SaveChanges();
                }
                InvoiceDg.ItemsSource = db.Employees.ToList();
            }
        }

        private void PrintBtn_OnClick(object sender, RoutedEventArgs e)
        {


        }

        private void InvoiceDg_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!(InvoiceDg.SelectedItem is Employee employee)) return;
            _cfg = AutomapperConfig.Config<Employee, EmployeeViewModel>();
            EmployeeInfo.DataContext = _cfg.CreateMapper().Map<EmployeeViewModel>(employee);
        }

        private void DEletebtn_OnClick(object sender, RoutedEventArgs e)
        {
            var employee = InvoiceDg.SelectedItem as Employee;

            if (employee == null) return;
            var result = MessageBox.Show("Are you sure you want to delete this record", "Warning",
                MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (!result.ToString().Equals("Yes")) return;

            using (var db = new ObjectContext())
            {
                var emp = db.Employees.Single(em => em.Id == employee.Id);
                db.Employees.Remove(emp);
                db.SaveChanges();
                InvoiceDg.ItemsSource = db.Employees.ToList();
            }
        }

        private void RefreshBtn_OnClick(object sender, RoutedEventArgs e)
        {
            EmployeeInfo.DataContext = new EmployeeViewModel();
        }

        private void ExpPdfBtn_OnClick(object sender, RoutedEventArgs e)
        {

        }
    }
}
