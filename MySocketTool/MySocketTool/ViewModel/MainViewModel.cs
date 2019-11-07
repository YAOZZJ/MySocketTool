using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MyToolkits.Log.TraceLog;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;

namespace MySocketTool.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            MessageAll = "";
            MessageCurrent = "";
            _trace = new MyTraceListener();
            Trace.Listeners.Add(_trace);
            _trace.PropertyChanged += traceOnPropertyChanged;
        }
        #region "Action"
        void Action1()
        {
            Message("Main | " + MethodBase.GetCurrentMethod().Name);
        }
        void Action2()
        {
            Message("Main | " + MethodBase.GetCurrentMethod().Name);
        }
        void Action3()
        {
            Message("Main | " + MethodBase.GetCurrentMethod().Name);
        }
        void Action4()
        {
            Message("Main | " + MethodBase.GetCurrentMethod().Name);
        }
        void Action5()
        {
            Message("Main | " + MethodBase.GetCurrentMethod().Name);
        }

        void Message(string msg)
        {
            Trace.WriteLine(msg);
        }
        void traceOnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Trace")
                MessageAll += $"{DateTime.Now.ToString(" HH:mm:ss fff")} | {_trace.Trace}";
            MessageCurrent = _trace.Trace.Replace("\r", "").Replace("\n", "");
        }
        #endregion

        #region "Variable"
        string _messageAll;
        string _messageCurrent;
        MyTraceListener _trace;
        #endregion

        #region "Command"
        RelayCommand cmd1;
        RelayCommand cmd2;
        RelayCommand cmd3;
        RelayCommand cmd4;
        RelayCommand cmd5;
        #endregion
        #region "Public"
        public RelayCommand Cmd1 { get => cmd1 ?? (cmd1 = new RelayCommand(Action1)); }
        public RelayCommand Cmd2 { get => cmd2 ?? (cmd2 = new RelayCommand(Action2)); }
        public RelayCommand Cmd3 { get => cmd3 ?? (cmd3 = new RelayCommand(Action3)); }
        public RelayCommand Cmd4 { get => cmd4 ?? (cmd4 = new RelayCommand(Action4)); }
        public RelayCommand Cmd5 { get => cmd5 ?? (cmd5 = new RelayCommand(Action5)); }
        public string MessageAll { get => _messageAll; set => Set(ref _messageAll, value); }
        public string MessageCurrent { get => _messageCurrent; set => Set(ref _messageCurrent, value); }
        #endregion
    }
}