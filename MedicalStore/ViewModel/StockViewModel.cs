using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MedicalStore.Data;
using MedicalStore.Model.Entities;

namespace MedicalStore.ViewModel
{
    public class StockViewModel : IDataErrorInfo, INotifyPropertyChanged
    {
        private decimal _costPerUnit;
        private int _quentity;
        private DateTime? _creationDate;
        private DateTime? _expireDate;
        private decimal _pricePerUnit;
        private Int64 _medicineId;
//        private int? _storeId;
        private int? _supplierId;
        private List<Medicine> _medicines;
        public Int64 Id { get; set; }

        public List<Medicine> Medicines
        {
            get
            {
                using (var db = new ObjectContext())
                {
                    return _medicines ?? db.Medicines.ToList();
                }

            }
            set
            {
                if (Equals(value, _medicines)) return;
                _medicines = value;
                OnPropertyChanged("_medicines");
            }
        }
        private List<Supplier> _suppliers;

        public List<Supplier> Suppliers
        {
            get
            {
                using (var db = new ObjectContext())
                {
                   
                    return _suppliers;
                }
            }
            set
            {
                _suppliers = value;
                
                OnPropertyChanged("_suppliers");

            }
        }


        public decimal CostPerUnit
        {
            get { return _costPerUnit; }
            set
            {
                if (value == _costPerUnit) return;
                _costPerUnit = value;
                OnPropertyChanged("_costPerUnit");
            }
        }


        public int Quentity
        {
            get { return _quentity; }
            set
            {
                if (value == _quentity) return;
                _quentity = value;
                OnPropertyChanged("_quentity");
            }
        }


        public DateTime? CreationDate
        {
            get { return _creationDate; }
            set
            {
                if (value.Equals(_creationDate)) return;
                _creationDate = value;
                OnPropertyChanged("_creationDate");
            }
        }

        public DateTime? ExpireDate
        {
            get { return _expireDate; }
            set
            {
                if (value.Equals(_expireDate)) return;
                _expireDate = value;
                OnPropertyChanged("_expireDate");
            }
        }

        public decimal PricePerUnit
        {
            get { return _pricePerUnit; }
            set
            {
                if (value.Equals(_pricePerUnit)) return;
                _pricePerUnit = value;
                OnPropertyChanged("_pricePerUnit");
            }
        }

        public Int64 MedicineId
        {
            get { return _medicineId; }
            set
            {
                if (value == _medicineId) return;
                _medicineId = value;
                OnPropertyChanged("_medicineId");
            }
        }

//        public int? Store_Id
//        {
//            get { return _storeId; }
//            set
//            {
//                if (value == _storeId) return;
//                _storeId = value;
//                OnPropertyChanged("_storeId");
//            }
//        }

        public int? SupplierId
        {
            get { return _supplierId; }
            set
            {
                if (value == _supplierId) return;
                _supplierId = value;
                OnPropertyChanged("_supplierId");
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;

      //  [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public string this[string columnName]
        {
            get
            {
                string result = null;
                if (columnName == "MedicineId")
                {
                    if (MedicineId == 0)
                        result = "The Medicine field is required";

                }
                if (columnName == "Quentity")
                {
                    if (Quentity <= 0)
                        result = "The quantity must be greater than zero";
                }
                if (columnName == "CostPerUnit")
                {
                    if (CostPerUnit <= 0)
                        result = "The price per unit must be greater than zero";
                }
                if (columnName == "PricePerUnit")
                {
                    if (PricePerUnit <= 0)
                        result = "The sale price per unit must be greater than zero";
                }


                return result;
            }
        }

        public string Error { get; }
    }
}
