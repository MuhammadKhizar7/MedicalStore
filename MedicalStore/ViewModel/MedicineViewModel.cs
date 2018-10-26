using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MedicalStore.Model.Entities;

namespace MedicalStore.ViewModel
{
    public class MedicineViewModel : IDataErrorInfo, INotifyPropertyChanged
    {
        private string _name;
        private int _packSize;
        private int _quentityInPack;
        private string _formula;
        private int? _categoryId;
        private int? _supplierId;
        private MedicineType _medicineType;
      //  private decimal _packSize;
        private Int64 _id;
        private string _strength;
        private string _company;

        public Int64 Id
        {
            get { return _id; }
            set
            {
                if (value == _id) return;
                _id = value;
                OnPropertyChanged("_id");
            }
        }

        public string Name
        {
            get { return _name; }
            set
            {
                if (value == _name) return;
                _name = value;
                OnPropertyChanged(_name);
            }
        }

        public string Formula
        {
            get { return _formula; }
            set
            {
                if (value == _formula) return;
                _formula = value;
                OnPropertyChanged("_formula");
            }
        }

        public string Strength
        {
            get { return _strength; }
            set
            {
                if (value == _strength) return;
                _strength = value;
                OnPropertyChanged("_strength");
            }
        }
        public string Company
        {
            get { return _company; }
            set
            {
                if (value == _company) return;
                _company = value;
                OnPropertyChanged("_company");
            }
        }

        public int QuentityInPack
        {
            get { return _quentityInPack; }
            set
            {
                if (value == _quentityInPack) return;
                _quentityInPack = value;
                OnPropertyChanged("_quentityInPack");
            }
        }


        public int? CategoryId
        {
            get { return _categoryId; }
            set
            {
                if (value == _categoryId) return;
                _categoryId = value;
                OnPropertyChanged("_categoryId");
            }
        }

        public int? Supplier_Id
        {
            get { return _supplierId; }
            set
            {
                if (value == _supplierId) return;
                _supplierId = value;
                OnPropertyChanged("_categoryId");
            }
        }

        public MedicineType MedicineType
        {
            get { return _medicineType; }
            set
            {
                if (value == _medicineType) return;
                _medicineType = value;
                OnPropertyChanged("_medicineType");
            }
        }

        public int PackSize
        {
            get { return _packSize; }
            set
            {
                if (value == _packSize) return;
                _packSize = value;
                OnPropertyChanged("_packSize");
            }
        }

        public string this[string columnName]
        {
            get
            {
                string result = null;
                if (columnName == "Name")
                {
                    if (string.IsNullOrEmpty(Name))
                        result = "The name is required field";
                }
                if (columnName == "Formula")
                {
                    if (string.IsNullOrEmpty(Formula))
                        result = "The formula name is required field";
                }
                if (columnName == "Strength")
                {
                    if (string.IsNullOrEmpty(Formula))
                        result = "The Strength is required field";
                }

//                if (columnName == "QuentityInPack")
//                {
//                    if (QuentityInPack <= 0)
//                        result = "The Quentity must be greater than zero";
//                }


                return result;
            }
        }

        public string Error { get; }

        public event PropertyChangedEventHandler PropertyChanged;

        //   [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}