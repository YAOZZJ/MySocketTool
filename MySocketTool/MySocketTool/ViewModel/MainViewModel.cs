using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MySocketTool.Service;
using System.Diagnostics;
using System.Reflection;
using System.Threading;

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
        void ActionTcpClientConnect()
        {
            if (_client == null) _client = new MyClient();
            if (!_socketclientConnected) { _client.Connect(2333); _socketclientConnected = true; return; }
            else _client.Disconnect();
            _socketclientConnected = false;
        }
        void ActionTcpClientSend()
        {
            if (!_socketclientConnected || !_client.Connected) return;
            _client.Send(ClientSendData);
        }
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
        MyClient _client;
        bool _socketclientConnected = false;
        #endregion

        #region "Command"
        RelayCommand cmd1;
        RelayCommand cmd2;
        RelayCommand cmd3;
        RelayCommand cmd4;
        RelayCommand cmd5;
        RelayCommand cmdSocketTcpClientConnect;
        RelayCommand cmdSocketTcpClientSend;
        #endregion
        #region "Public"

        public string ClientSendData { get; set; }

        public RelayCommand Cmd1 { get => cmd1 ?? (cmd1 = new RelayCommand(Action1)); }
        public RelayCommand Cmd2 { get => cmd2 ?? (cmd2 = new RelayCommand(Action2)); }
        public RelayCommand Cmd3 { get => cmd3 ?? (cmd3 = new RelayCommand(Action3)); }
        public RelayCommand Cmd4 { get => cmd4 ?? (cmd4 = new RelayCommand(Action4)); }
        public RelayCommand Cmd5 { get => cmd5 ?? (cmd5 = new RelayCommand(Action5)); }
        public RelayCommand CmdSocketTcpClientConnect { get => cmdSocketTcpClientConnect ?? (cmdSocketTcpClientConnect = new RelayCommand(ActionTcpClientConnect)); }
        public RelayCommand CmdSocketTcpClientSend { get => cmdSocketTcpClientSend ?? (cmdSocketTcpClientSend = new RelayCommand(ActionTcpClientSend)); }
        #endregion
    }
}