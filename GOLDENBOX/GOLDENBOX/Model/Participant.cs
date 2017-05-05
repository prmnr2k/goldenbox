using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GOLDENBOX.Abstract;

namespace GOLDENBOX.Model
{
    public class Participant : AbstractNotifyPropertyChanged
    {
        private string _code;
        private string _fullName;
        private string _city;
        private string _school;
        private int _grade;
        private string _email;
        private string _phone;
        private int _result;
        private string _delivery;
        private string _teacher;


        public string Teacher
        {
            get
            {
                return _teacher;
            }
            set
            {
                if (_teacher == value) return;
                _teacher = value;
                OnPropertyChanged("Teacher");
            }
        }
        private string _certificate;
        public string Certificate
        {
            get
            {
                return _certificate;
            }
            set
            {
                if (_certificate == value) return;
                _certificate = value;
                OnPropertyChanged("Certificate");
            }
        }

        public string Delivery
        {
            get
            {
                return _delivery;
            }
            set
            {
                if (_delivery == value) return;
                _delivery = value;
                OnPropertyChanged("Delivery");
            }
        }

        public int Result
        {
            get
            {
                return _result;
            }
            set
            {
                if (_result == value) return;
                _result = value;
                OnPropertyChanged("Result");
            }
        }

        public string Phone
        {
            get
            {
                return _phone;
            }
            set
            {
                if (_phone == value) return;
                _phone = value;
                OnPropertyChanged("Phone");
            }
        }

        public string Email
        {
            get
            {
                return _email;
            }
            set
            {
                if (_email == value) return;
                _email = value;
                OnPropertyChanged("Email");
            }
        }

        public int Grade
        {
            get
            {
                return _grade;
            }
            set
            {
                if (_grade == value) return;
                _grade = value;
                OnPropertyChanged("Grade");
            }
        }

        public string School
        {
            get
            {
                return _school;
            }
            set
            {
                if (_school == value) return;
                _school = value;
                OnPropertyChanged("School");
            }
        }

        public string City
        {
            get
            {
                return _city;
            }
            set
            {
                if (_city == value) return;
                _city = value;
                OnPropertyChanged("City");
            }
        }

        public string FullName
        {
            get
            {
                return _fullName;
            }
            set
            {
                if (_fullName == value) return;
                _fullName = value;
                OnPropertyChanged("FullName");
            }
        }

        public string Code
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
