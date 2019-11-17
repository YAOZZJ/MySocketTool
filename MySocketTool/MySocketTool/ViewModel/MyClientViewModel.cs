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

namespace MySocketTool.ViewModel
{
    public class MyClientViewModel : ViewModelBase
    {
        public MyClientViewModel()
        {
            Ip = "127.0.0.1";
            Port = 9600;
            PortList = new ObservableCollection<int>() { 9600, 2333, 1234 };
            IpList = new ObservableCollection<string>() { "127.0.0.1", "192.168.250.1", "192.168.250.2", "192.168.250.10" };
        }
        #region Action
        public void Connect()
        {
            _client = new SocketClient(Ip, Port);

            //绑定当收到服务器发送的消息后的处理事件
            _client.HandleRecMsg = new Action<byte[], SocketClient>((bytes, theClient) =>
            {
                string msg = Encoding.Default.GetString(bytes);
                Debug.WriteLine($"MyClient |收到消息:{msg}");
                theClient.Send($"MyClient |收到消息:{msg}", Encoding.Default);
            });
            //连接成功事件
            _client.HandleClientStarted = new Action<SocketClient>((theClient) =>
            {

                Debug.WriteLine($"MyClient |Connected with {_client.RemoteIPEndPoint.Address} {_client.RemoteIPEndPoint.Port}");
            });
            //断开连接事件
            _client.HandleClientClose = new Action<SocketClient>((theClient) =>
            {
                Debug.WriteLine($"MyClient |Disonnected with {Ip} {Port}");

            });
            //
            _client.HandleException = new Action<Exception>((ex) =>
            {
                Debug.WriteLine($"MyClient |{ex.Message}");
            });
            _client.StartClient();
        }
        public void Disconnect()
        {
            _client.Close();
        }
        public void Send()
        {
            _client.Send(_sendData);
        }
        #endregion

        #region 私有字段
        SocketClient _client;
        int _port;
        string _ip;
        string _sendData;

        RelayCommand connectCmd;
        RelayCommand sendCmd;
        #endregion

        #region 公共字段
        public ObservableCollection<int> PortList { get; set; }
        public ObservableCollection<string> IpList { get; set; }
        public int Port { get => _port; set => Set(ref _port, value); }
        public string Ip { get => _ip; set => Set(ref _ip, value); }
        public string SendData { get => _sendData; set => Set(ref _sendData, value); }

        public RelayCommand ConnectCmd { get => connectCmd??(connectCmd = new RelayCommand(Connect)); }
        public RelayCommand SendCmd { get => sendCmd ?? (sendCmd = new RelayCommand(Send)); }
        #endregion
    }
}
