using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GOLDENBOX.Abstract;

namespace GOLDENBOX.Model
{
    public class CertificateTemplate : AbstractNotifyPropertyChanged
    {
        private string _url;
        private int _code;
        private string _name;

        public string Url
        {
            get
            {
                return _url;
            }
            set
            {
                if (_url == value) return;
                _url = value;
                OnPropertyChanged("Url");
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
    }
}
