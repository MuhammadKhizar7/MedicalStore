using System;
using System.ComponentModel;

namespace MedicalStore.ViewModel
{
   public class PatientViewModel : IDataErrorInfo, INotifyPropertyChanged
    {
        public Int64 Id { get; set; }
        private string _name;

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged("_name");
            }
        }

        private string _disease;
        private string _tel;
        private string _city;
        private string _address;

        public string Disease
        {
            get { return _disease; }
            set
            {
                _disease = value;
                OnPropertyChanged("_disease");
            }
        }


        public string Tel
        {
            get { return _tel; }
            set
            {
                if (value == _tel) return;
                _tel = value;
                OnPropertyChanged(_tel);
            }
        }

      
        public string City
        {
            get { return _city; }
            set
            {
                if (value == _city) return;
                _city = value;
                OnPropertyChanged("_city");
            }
        }

        public string Address
        {
            get { return _address; }
            set
            {
                if (value == _address) return;
                _address = value;
                OnPropertyChanged("_address");
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
                        result = "The name field is required";
                }
                if (columnName == "Disease")
                {
                    if (string.IsNullOrEmpty(Disease))
                        result = "The disease field is required";
                }

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

