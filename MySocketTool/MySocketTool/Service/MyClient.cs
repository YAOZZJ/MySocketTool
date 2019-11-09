using MyToolkits.Sockets;
using System;
using System.Diagnostics;
using System.Text;

namespace MySocketTool.Service
{
    public class MyClient
    {
        #region 构造函数
        public MyClient()
        {

        }
        #endregion

        #region 公共方法
        public void Connect(int port = 9600, string ip = "127.0.0.1")
        {
            //创建客户端对象，默认连接本机127.0.0.1,端口为9600
            Ip = ip;
            Port = port;
            _client = new SocketClient(Ip, Port);

            //绑定当收到服务器发送的消息后的处理事件
            _client.HandleRecMsg = new Action<byte[], SocketClient>((bytes, theClient) =>
            {
                string msg = Encoding.UTF8.GetString(bytes);
                Debug.WriteLine($"MyClient |收到消息:{msg}");
            });

            //绑定向服务器发送消息后的处理事件
            _client.HandleSendMsg = new Action<byte[], SocketClient>((bytes, theClient) =>
            {
                string msg = Encoding.UTF8.GetString(bytes);
                Debug.WriteLine($"MyClient |向服务器发送消息:{msg}");
            });

            _client.StartClient();
        }
        public void Disconnect()
        {
            _client.Close();
        }
        #endregion

        #region 私有变量
        SocketClient _client;
        int _port = 9600;
        string _ip;

        #endregion

        #region 公共变量
        public int Port { get => _port; set => _port = value; }
        public string Ip { get => _ip; set => _ip = value; }

        public bool Connected{ get => _client.Connected; }
        #endregion
    }
}
