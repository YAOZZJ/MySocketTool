using MyToolkits.Sockets;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySocketTool.Service
{
    public class MyServer
    {
        public MyServer()
        {
            //创建服务器对象，默认监听本机0.0.0.0，端口9600
            SocketServer server = new SocketServer(9600);

            //处理从客户端收到的消息
            server.HandleRecMsg = new Action<byte[], SocketConnection, SocketServer>((bytes, client, theServer) =>
            {
                string msg = Encoding.UTF8.GetString(bytes);
                Debug.WriteLine($"MyServer | 收到消息:{msg}");
            });

            //处理服务器启动后事件
            server.HandleServerStarted = new Action<SocketServer>(theServer =>
            {
                Debug.WriteLine("MyServer | 服务已启动");
            });

            //处理新的客户端连接后的事件
            server.HandleNewClientConnected = new Action<SocketServer, SocketConnection>((theServer, theCon) =>
            {
                Debug.WriteLine($@"MyServer | 一个新的客户端接入，当前连接数：{theServer.GetConnectionCount()}");
            });

            //处理客户端连接关闭后的事件
            server.HandleClientClose = new Action<SocketConnection, SocketServer>((theCon, theServer) =>
            {
                Debug.WriteLine($@"MyServer | 一个客户端关闭，当前连接数为：{theServer.GetConnectionCount()}");
            });

            //处理异常
            server.HandleException = new Action<Exception>(ex =>
            {
                Debug.WriteLine(ex.Message);
            });

            //服务器启动
            server.StartServer();
        }
        
    }
}
