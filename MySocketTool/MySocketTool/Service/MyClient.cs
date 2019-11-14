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
        /// <summary>
        /// 发送数据
        /// </summary>
        /// <param name="bytes">数据字节</param>
        public void Send(byte[] bytes)
        {
            _client.Send(bytes);
        }

        /// <summary>
        /// 发送字符串（默认使用UTF-8编码）
        /// </summary>
        /// <param name="msgStr">字符串</param>
        public void Send(string msgStr)
        {
            _client.Send(msgStr);
        }

        /// <summary>
        /// 发送字符串（使用自定义编码）
        /// </summary>
        /// <param name="msgStr">字符串消息</param>
        /// <param name="encoding">使用的编码</param>
        public void Send(string msgStr, Encoding encoding)
        {
            _client.Send(encoding.GetBytes(msgStr));
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

        public bool Connected{ get => _client == null?false:_client.Connected; }
        #endregion
    }
}
