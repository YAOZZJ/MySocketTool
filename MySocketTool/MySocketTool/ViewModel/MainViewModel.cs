using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MySocketTool.Service;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Reflection;
using System.Text;

namespace MySocketTool.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            DefaultPortList = new ObservableCollection<int>()
                {
                    9600,2333
                };
            DefaultIPList = new ObservableCollection<string>()
            {
            "127.0.0.1",
            "192.168.250.1",
            "192.168.250.10"
            };
            DefaultPortListClient = new ObservableCollection<int>()
                {
                    9600,2333
                };
            DefaultIPListClient = new ObservableCollection<string>()
            {
            "127.0.0.1",
            "192.168.250.1",
            "192.168.250.10"
            };
        }


        #region "Action"
        void ActionTcpClientConnect()
        {
            if (_client == null) _client = new MyClient();
            if (!SocketclientConnected) { _client.Connect(PortClient, IpClient); return; }
            else _client.Disconnect();
        }
        void ActionTcpClientSend()
        {
            if (!SocketclientConnected || string.IsNullOrEmpty(ClientSendData)) return;
            _client.Send(ClientSendData);
        }
        void ActionTcpServerListen()
        {
            if (_server == null) _server = new MyServer();
            _server.Listen();
        }
        void ActionTcpServerSend()
        {
            if (_server == null || string.IsNullOrEmpty(ServerSendData)) return;
            _server.Send(ServerSendData, Encoding.Default);
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
        int _portServer;
        string _ipServer;
        int _portClient;
        string _ipClient;
        #endregion

        #region "Command"
        RelayCommand cmd1;
        RelayCommand cmd2;
        RelayCommand cmd3;
        RelayCommand cmd4;
        RelayCommand cmd5;
        RelayCommand cmdSocketTcpClientConnect;
        RelayCommand cmdSocketTcpClientSend;
        RelayCommand cmdSocketTcpServerListen;
        RelayCommand cmdSocketTcpServerSend;
        #endregion
        #region "Public"

        public string ServerSendData { get; set; }
        public string ClientSendData { get; set; }
        public int PortServer { get => _portServer; set => Set(ref _portServer, value); }
        public string IpServer { get => _ipServer; set => Set(ref _ipServer,value); }
        public int PortClient { get => _portClient; set => Set(ref _portClient,value); }
        public string IpClient { get => _ipClient; set => Set(ref _ipClient,value); }
        public ObservableCollection<int> DefaultPortList { get; set; }
        public ObservableCollection<string> DefaultIPList { get; set; }
        public ObservableCollection<int> DefaultPortListClient { get; set; }
        public ObservableCollection<string> DefaultIPListClient { get; set; }
        public bool SocketclientConnected { get => _client == null ? false : _client.Connected;  }

        public RelayCommand Cmd1 { get => cmd1 ?? (cmd1 = new RelayCommand(Action1)); }
        public RelayCommand Cmd2 { get => cmd2 ?? (cmd2 = new RelayCommand(Action2)); }
        public RelayCommand Cmd3 { get => cmd3 ?? (cmd3 = new RelayCommand(Action3)); }
        public RelayCommand Cmd4 { get => cmd4 ?? (cmd4 = new RelayCommand(Action4)); }
        public RelayCommand Cmd5 { get => cmd5 ?? (cmd5 = new RelayCommand(Action5)); }
        public RelayCommand CmdSocketTcpClientConnect { get => cmdSocketTcpClientConnect ?? (cmdSocketTcpClientConnect = new RelayCommand(ActionTcpClientConnect)); }
        public RelayCommand CmdSocketTcpClientSend { get => cmdSocketTcpClientSend ?? (cmdSocketTcpClientSend = new RelayCommand(ActionTcpClientSend)); }
        public RelayCommand CmdSocketTcpServerListen { get => cmdSocketTcpServerListen ?? (cmdSocketTcpServerListen = new RelayCommand(ActionTcpServerListen)); }
        public RelayCommand CmdSocketTcpServerSend { get => cmdSocketTcpServerSend ?? (cmdSocketTcpServerSend = new RelayCommand(ActionTcpServerSend)); }
        

        #endregion
    }
}