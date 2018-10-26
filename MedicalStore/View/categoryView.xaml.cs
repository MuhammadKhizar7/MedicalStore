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
    /// Interaction logic for categoryView.xaml
    /// </summary>
    public partial class categoryView : UserControl
    {
        private MapperConfiguration _cfg;
        public categoryView()
        {
            InitializeComponent();
            using (var db = new ObjectContext())
            {
                CategoryDg.ItemsSource = db.Categories.ToList();
            }
        }

        private void RefreshBtn_OnClick(object sender, RoutedEventArgs e)
        {
            CategoryInfo.DataContext = new CategoryViewModel();
        }

        private void SaveBtn_OnClick(object sender, RoutedEventArgs e)
        {
            var categoryViewModel = CategoryInfo.DataContext as CategoryViewModel;
            if (categoryViewModel != null && (string.IsNullOrEmpty(
                                                  categoryViewModel.Name) || string.IsNullOrWhiteSpace(categoryViewModel.Name)))
            {
                MessageBox.Show("Name  is null", "Attention!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            _cfg = AutomapperConfig.Config<CategoryViewModel, Category>();
            var category = _cfg.CreateMapper().Map<Category>(categoryViewModel);
            using (var db = new ObjectContext())
            {
                if (category.Id == 0)
                {
                    db.Categories.Add(category);
                    db.SaveChanges();
                }
                else
                {
                    db.Categories.AddOrUpdate(category);
                    db.SaveChanges();
                }
                   
                CategoryDg.ItemsSource = db.Categories.ToList();
            }
        }

        private void CategoryDg_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!(CategoryDg.SelectedItem is Category category)) return;
            _cfg = AutomapperConfig.Config<Category, CategoryViewModel>();
            CategoryInfo.DataContext = _cfg.CreateMapper().Map<CategoryViewModel>(category);
        }

        private void DEletebtn_OnClick(object sender, RoutedEventArgs e)
        {
            var category = CategoryDg.SelectedItem as Category;

            if (category == null) return;
            var result = MessageBox.Show("Are you sure you want to delete this record!", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (!result.ToString().Equals("Yes")) return;

            using (var db = new ObjectContext())
            {
                var cat = db.Categories.Single(c => c.Id == category.Id);
                db.Categories.Remove(cat);
                db.SaveChanges();
                CategoryDg.ItemsSource = db.Categories.ToList();
            }
        }
    }
}
