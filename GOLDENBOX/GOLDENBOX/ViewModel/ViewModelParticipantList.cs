using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GOLDENBOX.Abstract;
using GOLDENBOX.Model;
using GOLDENBOX.Bridge;
using System.Threading;
using System.Collections.ObjectModel;

namespace GOLDENBOX.ViewModel
{
    public class ViewModelParticipantList : AbstractNotifyPropertyChanged, IModel
    {
        private readonly DataSourceParticipants _dataSourceParticipant = new DataSourceParticipants();

        private string _xlsPath = "";
        public string XlsPath
        {
            get { return _xlsPath; }
            set {
                if (_xlsPath == value) return;
                _xlsPath = value;
                var thread = new Thread(() => _dataSourceParticipant.getParticipantList(out _listModelParticipant,out _progressInput,_xlsPath,callback));
                thread.Start();
                OnPropertyChanged("XlsPath");
                OnPropertyChanged("ListModelParticipant");
                OnPropertyChanged("CanGenerate");
                OnPropertyChanged("CanClear");
            }
        }

        private int _progressInput;
        public int ProgressInput
        {
            get { return _progressInput; }
            set
            {
                if (_progressInput == value) return;
                _progressInput = value;
                OnPropertyChanged("ProgressInput");
                OnPropertyChanged("CanGenerate");
            }
        }

        private int _progressOutput;
        public int ProgressOutput
        {
            get { return _progressOutput; }
            set
            {
                if (_progressOutput == value) return;
                _progressOutput = value;
                OnPropertyChanged("ProgressOutput");
            }
        }

        private void callback(string prop)
        {
            OnPropertyChanged(prop);
        }

        private string _topResult = "";
        public string TopResult
        {
            get { return _topResult; }
            set
            {
                if (_topResult == value) return;
                _topResult = value;
                OnPropertyChanged("TopResult");
                OnPropertyChanged("CertificateList");
                OnPropertyChanged("CanGenerate");
                OnPropertyChanged("CanClear");
            }
        }

        private string _middleResult = "";
        public string MiddleResult
        {
            get { return _middleResult; }
            set
            {
                if (_middleResult == value) return;
                _middleResult = value;
                OnPropertyChanged("MiddleResult");
                OnPropertyChanged("CertificateList");
                OnPropertyChanged("CanGenerate");
                OnPropertyChanged("CanClear");
            }
        }

        private string _lowResult = "";
        public string LowResult
        {
            get { return _lowResult; }
            set
            {
                if (_lowResult == value) return;
                _lowResult = value;
                OnPropertyChanged("LowResult");
                OnPropertyChanged("CertificateList");
                OnPropertyChanged("CanGenerate");
                OnPropertyChanged("CanClear");
            }
        }

        private string _outputFolder = "";
        public string OutputFolder
        {
            get { return _outputFolder; }
            set
            {
                if (_outputFolder == value) return;
                _outputFolder = value;
                OnPropertyChanged("OutputFolder");
                OnPropertyChanged("CanGenerate");
                OnPropertyChanged("CanClear");
            }
        }

        public bool CanGenerate => (!String.IsNullOrWhiteSpace(XlsPath) && !String.IsNullOrWhiteSpace(TopResult) &&
                                !String.IsNullOrWhiteSpace(MiddleResult) && !String.IsNullOrWhiteSpace(LowResult) &&
                                !String.IsNullOrWhiteSpace(OutputFolder)) && (ListModelParticipant.Count != 0);
        public bool CanClear => !String.IsNullOrWhiteSpace(XlsPath) || !String.IsNullOrWhiteSpace(TopResult) ||
                                !String.IsNullOrWhiteSpace(MiddleResult) || !String.IsNullOrWhiteSpace(LowResult) ||
                                !String.IsNullOrWhiteSpace(OutputFolder);

        private List<Participant> _listModelParticipant = new List<Participant>();
        public List<Participant> ListModelParticipant {
            get
            {
                return _listModelParticipant;

            }
            set
            {
                if (_listModelParticipant == value) return;
                _listModelParticipant = value;
                OnPropertyChanged("ListModelParticipant");
                OnPropertyChanged("CanGenerate");
            }
        }
        
        public List<CertificateTemplate> CertificateList
        {
            get
            {
                return new List<CertificateTemplate>
                    {

                        new CertificateTemplate
                        {
                            Code = 0,
                            Url =LowResult,
                            Name = "Участник"
                        },
                        new CertificateTemplate {
                            Code = 1,
                            Url = MiddleResult,
                            Name = "Хорошо"
                        },
                        new CertificateTemplate {
                            Code = 2,
                            Url = TopResult,
                            Name = "Отлично"
                        }
                    };
            }
        }

        public Command CommandClearFields { get; set; }
        public Command CommandGenerate { get; set; }
        
        public ViewModelParticipantList()
        {
            //ListModelParticipant = new ObservableCollection<Participant>(_dataSourceParticipant.getParticipantList());
           
            CommandGenerate = new Command(obj => OutputParticipantListToFiles());
            CommandClearFields = new Command(obj => ClearViewModel());
        }



        public void OutputParticipantListToFiles()
        {
            var thread = new Thread(() => _dataSourceParticipant.OutputParticipantList(ListModelParticipant, CertificateList, OutputFolder, out _progressOutput, callback));
            thread.Start();
        }
        public string GetCertificateTemplateByCode(string certName)
        {
            return CertificateList.FirstOrDefault(obj => obj.Name == certName).Url;
        }

        public void ClearViewModel()
        {
            XlsPath = "";
            TopResult = "";
            MiddleResult = "";
            LowResult = "";
            OutputFolder = "";
        }
        public void DoCallback(object sender, Guid[] e)
        {
            return;
        }
    }
}
