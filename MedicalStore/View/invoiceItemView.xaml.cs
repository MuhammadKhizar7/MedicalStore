using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using iTextSharp.text;
using iTextSharp.text.pdf;
using MedicalStore.Data;
using MedicalStore.Model.Entities;
using MedicalStore.PdfHelper;


namespace MedicalStore.View
{
    /// <summary>
    /// Interaction logic for invoiceItemView.xaml
    /// </summary>
    public partial class invoiceItemView
    {
        private readonly List<InvoiceItem> _invoiceItems = new List<InvoiceItem>();
        public invoiceItemView()
        {
            InitializeComponent();
          
            using (var db = new ObjectContext())
            {
                ProductDg.ItemsSource = db.Stocks.Include(s=>s.Medicine).Where(s=>s.ExpireDate > DateTime.Today).OrderByDescending(s=>s.ExpireDate).ToList();
                CustomersCb.ItemsSource = db.Patients.OrderByDescending(x=>x.CreatedAt).ToList();
            }
        }

        private void AddBtn_OnClick(object sender, RoutedEventArgs e)
        {
            if (!(ProductDg.SelectedItem is Stock stock))
                return;
            
            if (_invoiceItems.All(x => x.MedicineId !=stock.MedicineId))
            {
                var invoiceItem = new InvoiceItem();
                invoiceItem.MedicineId = stock.MedicineId;
                invoiceItem.Medicine = stock.Medicine;
                if (TxtPrice.Value.HasValue) invoiceItem.UnitPrice = Convert.ToDecimal(TxtPrice.Value);
                if (TxtQnt.Value.HasValue)
                    invoiceItem.Qnt += (int)TxtQnt.Value;
                else invoiceItem.Qnt += 1;

                invoiceItem.THT = invoiceItem.Qnt * invoiceItem.UnitPrice;
                invoiceItem.TTC = (invoiceItem.Qnt * invoiceItem.UnitPrice) + invoiceItem.THT * invoiceItem.Tax;
                _invoiceItems.Add(invoiceItem);
            }
            else
            {
                InvoiceItem first = _invoiceItems.FirstOrDefault(x=>x.MedicineId == stock.MedicineId);
               
                if (first != null)
                {
                    if (TxtQnt.Value != null)
                        first.Qnt += (int) TxtQnt.Value;
                    else first.Qnt += 1;
                    first.THT = first.Qnt * first.UnitPrice;
                    first.TTC = (first.Qnt * first.UnitPrice) + first.THT * first.Tax;
                }
               
            }
            InvoiceItemDg.ItemsSource = null;
            InvoiceItemDg.ItemsSource = _invoiceItems;
            if (_invoiceItems.Any())
            {
                TotalTxt.Text = _invoiceItems.Sum(x => x.TTC).ToString(CultureInfo.InvariantCulture);
                TotalNetTxt.Text = _invoiceItems.Sum(x => x.THT).ToString(CultureInfo.InvariantCulture);
            }
        }

        private void SaveBtn_OnClick(object sender, RoutedEventArgs e)
        {
            if (CustomersCb.SelectedIndex == -1)
            {
                MessageBox.Show("You have to select a customer", "Attention", MessageBoxButton.OK,
                    MessageBoxImage.Information);
                return;
            }
            if (_invoiceItems.Count == 0)
            {
                MessageBox.Show("At least one line must be added to the invoice ", "Attention", MessageBoxButton.OK,
                    MessageBoxImage.Information);
                return;
            }
            using (var db = new ObjectContext())
            {
                var invoice = new Invoice();
                invoice.CreatedAt = DateTime.Now;
                invoice.InvoiceNumber =DateTime.Now.Year+"-"+ (db.Invoices.Count() + 1).ToString().PadLeft(4,'0');
                invoice.PatientId = ((Patient) CustomersCb.SelectedItem).Id;
                invoice.InvoiceType = InvoiceType.Bill;
                invoice.IsValid = true;
                if (AmmoutpaidXTxt.Value.HasValue)
                    invoice.PaidAmmount = Convert.ToDecimal(AmmoutpaidXTxt.Value);
                invoice.THT =(decimal) _invoiceItems.Sum(x=>x.THT);
                invoice.TTC = _invoiceItems.Sum(x=>x.TTC);
                invoice.Left = decimal.Subtract(invoice.TTC , invoice.PaidAmmount);
                db.Invoices.Add(invoice);
                db.SaveChanges();
                var newInvoice = db.Invoices.AsNoTracking().FirstOrDefault(x =>
                     x.PatientId == invoice.PatientId &&
                    x.InvoiceNumber == invoice.InvoiceNumber);
                foreach (var invoiceItem in _invoiceItems)
                {
                    if (newInvoice == null) continue;
                    invoiceItem.InvoiceId = newInvoice.Id;
                    invoiceItem.Medicine = null;
//                    db.InvoiceItems.Add(invoiceItem);
//                    db.SaveChanges();
                    var stock = db.Stocks.FirstOrDefault(x => x.MedicineId == invoiceItem.MedicineId);
                    if (stock != null && stock.Quentity >= invoiceItem.Qnt)
                    {
                        stock.Quentity -= invoiceItem.Qnt;
                     db.InvoiceItems.Add(invoiceItem);
                        db.Stocks.AddOrUpdate(stock);
                        db.SaveChanges();
                    }
                    else if (stock != null)
                    {
                        stock.Quentity = 0;
                        db.Stocks.AddOrUpdate(stock);
                        db.SaveChanges();
                    }
                }
                MessageBox.Show("Registration completed successfully !", "Registration", MessageBoxButton.OK,
                    MessageBoxImage.Information);


            }

        }

        private void PrintBtn_OnClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void ProductDg_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!(ProductDg.SelectedItem is Stock stock))
                return;
            TxtPrice.Value =Convert.ToDouble( stock.PricePerUnit);
        }

        private void DeleteBtn_OnClick(object sender, RoutedEventArgs e)
        {
            var s = InvoiceItemDg.SelectedItem as InvoiceItem;
            if (s==null) return;
            _invoiceItems.Remove(s);
            InvoiceItemDg.ItemsSource = null;
            InvoiceItemDg.ItemsSource = _invoiceItems;

        }

        private void ExpPdfBtn_OnClick(object sender, RoutedEventArgs e)
        {
            if (_invoiceItems.Count ==0)
            {
                MessageBox.Show("Save the invoice before printing", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;

            }
            Invoice invoice;
            //Store store;
            using (var db = new ObjectContext())
            {
                var i  = _invoiceItems.FirstOrDefault().InvoiceId;
                invoice = db.Invoices.Include(x=>x.Patient)
                    .FirstOrDefault(x => x.Id ==i );
//                store = db.Stores.FirstOrDefault();
            }
//            if (store == null)
//            {
//                MessageBox.Show("We must save the information of your establishment");
//                return;
//            }
            if (invoice == null )
            {
                MessageBox.Show("Save the invoice before printing", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;

            }
            
            string[] headers = { "Medicine","Unit Price", "Quantity", "THT", "TTC" };
            float[] columnWidths = { 200f, 200,200, 200f, 140f };

            Document doc = new Document(PageSize.A4, 15, 15, 15, 15);
            MemoryStream stream = new MemoryStream();
            PdfWriter.GetInstance(doc, stream).CloseStream = false;
            doc.Open();
            PdfPTable tblHeader = new PdfPTable(3);
            tblHeader.WidthPercentage = 100;

            PdfPCell leftCell = new PdfPCell();
            leftCell.Border = 0;

            iTextSharp.text.Paragraph paragraph = new iTextSharp.text.Paragraph($"Delivery form",
                FontFactory.GetFont("Calibri", 18, BaseColor.BLACK));
        
            paragraph.Alignment = Element.ALIGN_LEFT;
            leftCell.AddElement(paragraph);
            leftCell.AddElement(null);
            leftCell.AddElement(null);
            leftCell.AddElement(null);
            PdfPCell rightCell = new PdfPCell();
            rightCell.Border = 0;
//            var prg = new Paragraph(store.StoreName);
//            rightCell.AddElement(prg);
//            prg = new Paragraph("NRC: "+store.NRC);
//            rightCell.AddElement(prg);
//            prg = new Paragraph("NIF: "+store.NIF);
//            rightCell.AddElement(prg);
//            prg = new Paragraph(store.City);
//            rightCell.AddElement(prg);

            PdfPCell emptyCell = new PdfPCell();
            emptyCell.Border = 0;

            tblHeader.AddCell(leftCell);
            tblHeader.AddCell(emptyCell);
            tblHeader.AddCell(rightCell);
            doc.Add(tblHeader);
           
            doc.Add(new Chunk(Environment.NewLine));
            iTextSharp.text.Paragraph p = new iTextSharp.text.Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(0.0F, 100.0F, BaseColor.BLACK, Element.ALIGN_LEFT, 1)));
            // call the below to add the line when required.
            doc.Add(p);
            doc.Add(new Chunk(Environment.NewLine));
            doc.Add(new Chunk(Environment.NewLine));
            PdfPTable table = new PdfPTable(2);
            table.WidthPercentage = 90;
            var bodyLeftCell = new PdfPCell();
            bodyLeftCell.Border = 0;
            bodyLeftCell.AddElement(new Paragraph(invoice.Patient.Name));
            bodyLeftCell.AddElement(new Paragraph(invoice.Patient.Address));
            bodyLeftCell.AddElement(new Paragraph(invoice.Patient.City));

            var bodyRightCell = new PdfPCell();
            bodyRightCell.Border = 0;
            table.AddCell(bodyLeftCell);
            // table.AddCell(emptyCell);
            table.AddCell(bodyRightCell);
            table.DefaultCell.Border = 0; ;

            doc.Add(table);
            doc.Add(new Chunk(Environment.NewLine));
            p.Alignment = Element.ALIGN_CENTER;


            doc.Add(new Chunk(Environment.NewLine));
            doc.Add(new Chunk(Environment.NewLine));

            PdfPTable pTable = new PdfPTable(headers.Length);
            pTable.WidthPercentage = 90;
            pTable.SetWidths(columnWidths);
            foreach (string t in headers)
            {
                var cell = new PdfPCell(new Phrase(t, FontFactory.GetFont("Calibri", 12, BaseColor.BLACK)))
                {
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    BackgroundColor = new BaseColor(230, 230, 250)
                };


                pTable.AddCell(cell);
            }
            foreach (var invoiceItem in _invoiceItems)
            {
                pTable.AddCell(ItextSharpHelper.Cell(invoiceItem.Medicine.Name, BaseColor.BLACK));
//                pTable.AddCell(ItextSharpHelper.Cell(invoiceItem.Medicine.MesureUnit.ToString(), BaseColor.BLACK));
                pTable.AddCell(ItextSharpHelper.Cell(invoiceItem.UnitPrice.ToString("F"), BaseColor.BLACK));
                pTable.AddCell(ItextSharpHelper.Cell(invoiceItem.Qnt.ToString(), BaseColor.BLACK));
                pTable.AddCell(ItextSharpHelper.Cell(invoiceItem.THT.ToString(CultureInfo.InvariantCulture), BaseColor.BLACK));
                pTable.AddCell(ItextSharpHelper.Cell(invoiceItem.TTC.ToString(CultureInfo.InvariantCulture), BaseColor.BLACK));
            }
            doc.Add(p);
            doc.Add(pTable);
            doc.Close();
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.DefaultExt = ".pdf";
            dlg.Filter = "pdf documents (.pdf)|*.pdf";
            if (dlg.ShowDialog() != true) return;

            string filename = dlg.FileName;
            FileManager.SavePdfFile(stream, filename);
        }

        private void TextBoxBase_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            var term = ((TextBox)sender).Text;
            using (var db = new ObjectContext())
            {
//               ProductDg.ItemsSource = db.Stocks.Include(x => x.Medicine).Where(x => x.Medicine.Name.Contains(term)||x.PricePerUnit.ToString(CultureInfo.InvariantCulture).Contains(term)).ToList();
             ProductDg.ItemsSource = db.Stocks.Include(x=>x.Medicine).Where(x => x.Medicine.Name.Contains(term)).ToList();
            }
        }
    }
}
