using MyToolkits.Sockets;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Text;

namespace MySocketTool.Service
{
    public class MyServer
    {
        public MyServer()
        {
            
            
        }

        #region 公共方法
        public void Listen(int port = 9600, string ip = "127.0.0.1")
        {
            Ip = ip;
            Port = port;
            _server = new SocketServer(Ip, Port);

            //处理从客户端收到的消息
            _server.HandleRecMsg = new Action<byte[], SocketConnection, SocketServer>((bytes, client, theServer) =>
            {
                string msg = Encoding.UTF8.GetString(bytes);
                Debug.WriteLine($"MyServer | 收到消息:{msg}");
            });

            //处理服务器启动后事件
            _server.HandleServerStarted = new Action<SocketServer>(theServer =>
            {
                Debug.WriteLine("MyServer | 服务已启动");
            });

            //处理新的客户端连接后的事件
            _server.HandleNewClientConnected = new Action<SocketServer, SocketConnection>((theServer, theCon) =>
            {
                Debug.WriteLine($@"MyServer | 一个新的客户端接入，当前连接数：{theServer.GetConnectionCount()}");
            });

            //处理客户端连接关闭后的事件
            _server.HandleClientClose = new Action<SocketConnection, SocketServer>((theCon, theServer) =>
            {
                Debug.WriteLine($@"MyServer | 一个客户端关闭，当前连接数为：{theServer.GetConnectionCount()}");
            });

            //处理异常
            _server.HandleException = new Action<Exception>(ex =>
            {
                Debug.WriteLine(ex.Message);
            });

            //服务器启动
            _server.StartServer();
        }
        public void Stop()
        {

        }
        /// <summary>
        /// 发送数据
        /// </summary>
        /// <param name="bytes">数据字节</param>
        public void Send(byte[] bytes)
        {
        }

        /// <summary>
        /// 发送字符串（默认使用UTF-8编码）
        /// </summary>
        /// <param name="msgStr">字符串</param>
        public void Send(string msgStr)
        {
        }

        /// <summary>
        /// 发送字符串（使用自定义编码）
        /// </summary>
        /// <param name="msgStr">字符串消息</param>
        /// <param name="encoding">使用的编码</param>
        public void Send(string msgStr, Encoding encoding)
        {
            _server.Send(msgStr, encoding);
        }
        #endregion

        #region 私有变量

        SocketServer _server;
        int _port = 9600;
        string _ip;


        #endregion

        #region 公共变量
        public int Port { get => _port; set => _port = value; }
        public string Ip { get => _ip; set => _ip = value; }
        public IPEndPoint RemoteIPEndPoint { get => (IPEndPoint)_server.RemoteEndPoint; }
        public EndPoint LocalEndPoint { get => _server.LocalEndPoint; }
        public IPEndPoint LocalIPEndPoint { get => (IPEndPoint)_server.LocalEndPoint; }
        public List<EndPoint> RemoteEndPointList => _server.GetRemoteEndPointList();

        #endregion

    }
}
