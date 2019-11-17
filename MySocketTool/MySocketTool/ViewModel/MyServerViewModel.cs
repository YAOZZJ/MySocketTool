using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MyToolkits.Sockets;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace MySocketTool.ViewModel
{
    public class MyServerViewModel:ViewModelBase
    {
        public MyServerViewModel()
        {
            Ip = "127.0.0.1";
            Port = 9600;
            PortList = new ObservableCollection<int>() { 9600, 2333, 1234 };
            IpList = new ObservableCollection<string>() { "127.0.0.1","192.168.250.1", "192.168.250.2", "192.168.250.10" };
            ConnectionList = new ObservableCollection<string>();
        }
        #region Action
        void Debug()
        {

        }
        void Start() { }
        void Stop() { }
        void Connect() 
        {
            Listen(Port, Ip);
        }
        void Send() 
        {
            if (string.IsNullOrEmpty(_sendData) || !_server.IsListen || _server.GetConnectionCount() == 0) return;
            _server.Send(_sendData, SelectedSession);
        }
        void Listen(int port = 9600, string ip = "127.0.0.1")
        {
            Ip = ip;
            Port = port;
            _server = new SocketServer(Ip, Port);

            //处理从客户端收到的消息
            _server.HandleRecMsg = new Action<byte[], SocketConnection, SocketServer>((bytes, client, theServer) =>
            {
                string msg = Encoding.UTF8.GetString(bytes);
                System.Diagnostics.Debug.WriteLine($"MyServer | {client.RemoteEndPoint.ToString()}=>:{msg}");
            });

            //处理服务器启动后事件
            _server.HandleServerStarted = new Action<SocketServer>(theServer =>
            {
                System.Diagnostics.Debug.WriteLine("MyServer | 服务已启动");
            });

            //处理新的客户端连接后的事件
            _server.HandleNewClientConnected = new Action<SocketServer, SocketConnection>((theServer, theCon) =>
            {
                System.Diagnostics.Debug.WriteLine($@"MyServer | 一个新的客户端接入，当前连接数：{theServer.GetConnectionCount()}");
                Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.SystemIdle, new Action(() =>
                {
                    //ConnectionList.Add(theCon.RemoteEndPoint.ToString());
                    ConnectionList.Clear();
                    foreach (var node in _server.GetRemoteEndPointList())
                    {
                        ConnectionList.Add(node.ToString());
                    }
                }));
            });

            //处理客户端连接关闭后的事件
            _server.HandleClientClose = new Action<SocketConnection, SocketServer>((theCon, theServer) =>
            {
                System.Diagnostics.Debug.WriteLine($@"MyServer | 一个客户端关闭，当前连接数为：{theServer.GetConnectionCount()}");
                Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.SystemIdle, new Action(() =>
                {
                    ConnectionList.Clear();
                    if (_server.GetConnectionCount() < 1) return;
                    foreach (var node in _server.GetRemoteEndPointList())
                    {
                        ConnectionList.Add(node.ToString());
                    }
                }));
            });

            //处理异常
            _server.HandleException = new Action<Exception>(ex =>
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            });

            //服务器启动
            _server.StartServer();
        }
        #endregion

        #region 私有成员
        SocketServer _server;

        int _port;
        string _ip;
        string _sendData;

        RelayCommand connect;
        RelayCommand send;
        RelayCommand debug;

        #endregion

        #region 公共成员
        public ObservableCollection<int> PortList { get; set; }
        public ObservableCollection<string> IpList { get; set; }
        public ObservableCollection<string> ConnectionList { get; set; }
        public int Port { get => _port; set => Set(ref _port,value); }
        public string Ip { get => _ip; set => Set(ref _ip, value); }
        public string SendData { get => _sendData; set => Set(ref _sendData,value); }
        public string SelectedSession { get; set; }
        public RelayCommand ConnectCmd { get => connect ?? (connect = new RelayCommand(Connect)); }
        public RelayCommand SendCmd { get => send ?? (send = new RelayCommand(Send)); }
        public RelayCommand DebugCmd { get => debug ?? (debug = new RelayCommand(Debug)); }

        #endregion
    }
}
