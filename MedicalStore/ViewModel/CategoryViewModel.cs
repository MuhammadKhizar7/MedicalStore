using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalStore.ViewModel
{
    public class CategoryViewModel : IDataErrorInfo, INotifyPropertyChanged
    {
        public int Id { get; set; }
        private string _name;
        private string _descrption;

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged("_name");
            }
        }
        public string Description
        {
            get { return _descrption; }
            set
            {
                _descrption = value;
                OnPropertyChanged("_descrption");
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

                return result;
            }
        }

        public string Error { get; }


        public event PropertyChangedEventHandler PropertyChanged;

    //    [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
