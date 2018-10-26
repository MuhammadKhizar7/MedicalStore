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
using MedicalStore.View;
using Visibility = System.Windows.Visibility;

namespace MedicalStore
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MahApps.Metro.Controls.MetroWindow
    {
        private bool _admin;

        public bool Admin { get => _admin; set => _admin = value; }
        private bool _pharmasist;

        public bool Pharmasist
        {
            get { return _pharmasist; }
            set { _pharmasist = value; }
        }


        public MainWindow()
        {
           
           
            InitializeComponent();
           
            DataContext = this;
            RoleCheck();  
          
        }

        private void CatBtn_Click(object sender, RoutedEventArgs e)
        {
            ContentControl.Content = new categoryView();
        }

        private void ProductBtn_Click(object sender, RoutedEventArgs e)
        {
            ContentControl.Content = new medicineView();
        }

        private void EmployeeBtn_Click(object sender, RoutedEventArgs e)
        {
            ContentControl.Content = new employeeView();
        }

        private void SuppBtn_Click(object sender, RoutedEventArgs e)
        {
            ContentControl.Content = new supplierView();
            
        }

        private void StockBtn_Click(object sender, RoutedEventArgs e)
        {
            ContentControl.Content = new stockView();
        }

        private void CustomerBtn_Click(object sender, RoutedEventArgs e)
        {
            ContentControl.Content = new patientView();
        }

        private void Invoicebtn_Click(object sender, RoutedEventArgs e)
        {
            ContentControl.Content= new invoiceListView();
        }

        private void NewOrderBtn_Click(object sender, RoutedEventArgs e)
        {
            ContentControl.Content = new invoiceItemView();
        }

        private void RoleCheck()
        {
            _admin = true;
            _pharmasist = true;
            if (_admin==false)
            {
                PharmacyTabItem.IsSelected = true;
            }
        }
    }
}
