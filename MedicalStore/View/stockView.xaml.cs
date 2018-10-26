using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using AutoMapper;
using MedicalStore.Data;
using MedicalStore.Data.Infrastructure;
using MedicalStore.Model.Entities;
using MedicalStore.ViewModel;

namespace MedicalStore.View
{
    /// <summary>
    /// Interaction logic for stockView.xaml
    /// </summary>
    public partial class stockView : UserControl
    {
        private MapperConfiguration _cfg;
        public stockView()
        {
            InitializeComponent();

            using (var db = new ObjectContext())
            {

                InvoiceDg.ItemsSource = db.Stocks.Where(x => x.Quentity > 0).Include(s=>s.Medicine).Include(s => s.Supplier).ToList();
                sup.ItemsSource = (from c in db.Suppliers

                                   select new
                                   {
                                       Name = c.FirstName + " " + c.LastName,
                                       c.Id
                                   }).ToList();

            }
        }

        private void RefreshBtn_OnClick(object sender, RoutedEventArgs e)
        {
            CustomerInfo.DataContext = new StockViewModel();
        }

        private void SaveBtn_OnClick(object sender, RoutedEventArgs e)
        {
            var stockViewModel = CustomerInfo.DataContext as StockViewModel;
            if (stockViewModel != null && (stockViewModel.MedicineId == 0 || stockViewModel.Quentity == 0))
            {
                MessageBox.Show("Product specification or stock quantity is zero", "Attention!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            _cfg = AutomapperConfig.Config<StockViewModel, Stock>();
            var stock = _cfg.CreateMapper().Map<Stock>(stockViewModel);

            using (var db = new ObjectContext())
            {

                if (stock.Id == 0)
                {
                    if (db.Stocks.Any(s=>s.MedicineId==stock.MedicineId && s.ExpireDate == stock.ExpireDate && s.SupplierId == stock.SupplierId))
                    {
                        var st = db.Stocks.SingleOrDefault(s =>
                            s.MedicineId == stock.MedicineId && s.ExpireDate == stock.ExpireDate &&
                            s.SupplierId == stock.SupplierId);
                        if (st != null)
                        {
                            stock.Id = st.Id;
                       //     stock.Quentity = st.Quentity;
                        }
                        db.Stocks.AddOrUpdate(stock);
                        db.SaveChanges();
                    }
                    else
                    {
                        db.Stocks.Add(stock);
                        db.SaveChanges();
                    }
                   
                }
                else
                {

                    var old = db.Stocks.FirstOrDefault(x => x.Id == stock.Id );
                    if (old != null && (old.Id==stock.Id && old.MedicineId==stock.MedicineId && old.ExpireDate== stock.ExpireDate) )
                    {
                     //   stock.Quentity += old.Quentity;
                        db.Stocks.AddOrUpdate(stock);
                        db.SaveChanges();
                    }
                    else
                    {
                        stock.Id = 0;
                        db.Stocks.Add(stock);
                        db.SaveChanges();
                    }
                    
                }

                InvoiceDg.ItemsSource = db.Stocks.Where(x => x.Quentity > 0).Include(s => s.Medicine).Include(s => s.Supplier).ToList();
            }
        }

        private void DEletebtn_OnClick(object sender, RoutedEventArgs e)
        {
            var stock = InvoiceDg.SelectedItem as Stock;

            if (stock == null) return;
            var result = MessageBox.Show("Are you sure you want to delete this record!", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (!result.ToString().Equals("Yes")) return;

            using (var db = new ObjectContext())
            {

                var st = db.Stocks.Single(s => s.Id == stock.Id && s.MedicineId == stock.MedicineId);
                db.Stocks.Remove(st);
                db.SaveChanges();
               
                InvoiceDg.ItemsSource = db.Stocks.Where(x => x.Quentity > 0).Include(s=>s.Medicine).Include(s => s.Supplier).ToList();
            }
        }

        private void PrintBtn_OnClick(object sender, RoutedEventArgs e)
        {
//            throw new NotImplementedException();
        }

        private void ExpPdfBtn_OnClick(object sender, RoutedEventArgs e)
        {
//            string[] headers = { "Designation", "Unit Price", "Quantity", "Date of Entry", "Expiry Date" };
//            float[] columnWidths = { 200f, 200, 200, 200f, 200f };
//
//            Document doc = new Document(PageSize.A4, 5, 5, 5, 5);
//            MemoryStream stream = new MemoryStream();
//            PdfWriter.GetInstance(doc, stream).CloseStream = false;
//
//            doc.Open();
//            Paragraph paragraph = new Paragraph($"Detailed stock report on: {DateTime.Now.Date.ToShortDateString()}",
//                FontFactory.GetFont("Calibri", 16, BaseColor.BLACK));
//            string imageUrl = Environment.CurrentDirectory + @"\Images\store56px.png";
//            iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance(imageUrl);
//            img.SpacingAfter = 1f;
//            img.Alignment = Element.ALIGN_RIGHT;
//            doc.Add(img);
//            //  doc.Add(new Chunk(Environment.NewLine));
//            paragraph.Alignment = Element.ALIGN_LEFT;
//            doc.Add(paragraph);
//            doc.Add(new Chunk(Environment.NewLine));
//            PdfPTable table = new PdfPTable(headers.Length);
//            table.WidthPercentage = 99;
//            table.SetWidths(columnWidths);
//            foreach (string t in headers)
//            {
//                var cell = new PdfPCell(new Phrase(t, FontFactory.GetFont("Calibri", 12, BaseColor.BLACK)))
//                {
//                    HorizontalAlignment = Element.ALIGN_CENTER,
//                    BackgroundColor = new BaseColor(230, 230, 250)
//                };
//
//
//                table.AddCell(cell);
            }
//            using (var db = new ObjectContext())
//            {
//
//                foreach (var stock in db.Stocks.LoadWith(w => w.Product).Where(x => x.Qnt > 0)
//                    .OrderBy(x => x.Product_Id))
//                {
//                    table.AddCell(ItextSharpHelper.Cell(stock.Product.Name, BaseColor.BLACK));
//                    table.AddCell(ItextSharpHelper.Cell(stock.Product.UnitPrice.ToString("F"), BaseColor.BLACK));
//                    table.AddCell(ItextSharpHelper.Cell(stock.Qnt.ToString(),
//                        (stock.Qnt <= stock.Product.MinQnt) ? BaseColor.RED : BaseColor.BLACK));
//                    table.AddCell(ItextSharpHelper.Cell(stock.CreationDate.ToString("d"), BaseColor.BLACK));
//
//                    table.AddCell(stock.LapsingDate != null
//                        ? ItextSharpHelper.Cell(stock.LapsingDate.Value.ToString("d"), BaseColor.BLACK)
//                        : ItextSharpHelper.Cell("", BaseColor.BLACK));
//                }
//            }
//            doc.Add(table);
//            doc.Close();
//            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
//            dlg.DefaultExt = ".pdf";
//            dlg.Filter = "pdf documents (.pdf)|*.pdf";
//            if (dlg.ShowDialog() != true) return;
//
//            string filename = dlg.FileName;
//            FileManager.SavePdfFile(stream, filename);
//        }

        private void TextBoxBase_OnSelectionChanged(object sender, RoutedEventArgs e)
        {
            var term = ((TextBox)sender).Text;
            using (var db = new ObjectContext())
            {
                InvoiceDg.ItemsSource = db.Stocks.Include(s=>s.Medicine).Where(x => x.Medicine.Name.Contains(term)).ToList();
            }
        }

        private void InvoiceDg_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!(InvoiceDg.SelectedItem is Stock stock)) return;
            _cfg = AutomapperConfig.Config<Stock, StockViewModel>();
            CustomerInfo.DataContext = _cfg.CreateMapper().Map<StockViewModel>(stock);
        }
    }
}
