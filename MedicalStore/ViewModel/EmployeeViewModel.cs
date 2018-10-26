using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MedicalStore.Model.Entities;

namespace MedicalStore.ViewModel
{
    public class EmployeeViewModel : IDataErrorInfo, INotifyPropertyChanged
    {



        public Int64 Id { get; set; }
        private string _fullName;

        public string FullName
        {
            get { return _fullName; }
            set
            {
                _fullName = value;
                OnPropertyChanged("_firstName");
            }
        }

        private string _userName;
        private string _tel;
        private string _password;
        private string _city;
        private string _street;
        private EmployeeType _employeeType;

        public string UserName
        {
            get { return _userName; }
            set
            {
                _userName = value;
                OnPropertyChanged("_lastName");
            }
        }
        public EmployeeType EmployeeType
        {
            get { return _employeeType; }
            set
            {
                if (value == _employeeType) return;
                _employeeType = value;
                OnPropertyChanged("_employeeType");
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

        public string Password
        {
            get { return _password; }
            set
            {
                if (value == _password) return;
                _password = value;
                OnPropertyChanged(_password);
            }
        }

      

        public string City
        {
            get { return _city; }
            set
            {
                if (value == _city) return;
                _city = value;
                OnPropertyChanged(_city);
            }
        }

        public string Street
        {
            get { return _street; }
            set
            {
                if (value == _street) return;
                _street = value;
                OnPropertyChanged(_street);
            }
        }

        public string Street2 { get; set; }


        public string this[string columnName]
        {
            get
            {
                string result = null;
                if (columnName == "FullName")
                {
                    if (string.IsNullOrEmpty(FullName))
                        result = "The  name field is required";
                }
                if (columnName == "UserName")
                {
                    if (string.IsNullOrEmpty(UserName))
                        result = "The username field is required";
                }
                if (columnName == "Password")
                {
                    if (string.IsNullOrEmpty(Password))
                        result = "The password field is required";
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
