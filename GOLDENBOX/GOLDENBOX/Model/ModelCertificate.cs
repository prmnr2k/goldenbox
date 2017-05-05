using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GOLDENBOX.Abstract;

namespace GOLDENBOX.Model
{
    public class ModelCertificate : AbstractNotifyPropertyChanged
    {
        private string _name;
        private int _code;

        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                if (_name == value) return;
                _name = value;
                OnPropertyChanged("Name");
            }
        }

        public int Code
        {
            get
            {
                return _code;
            }
            set
            {
                if (_code == value) return;
                _code = value;
                OnPropertyChanged("Code");
            }
        }
    }
}
