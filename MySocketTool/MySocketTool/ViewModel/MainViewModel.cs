using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MySocketTool.Service;
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
            _server = new MyServer();
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

        #endregion

        #region "Variable"
        MyServer _server;
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
        #endregion
    }
}