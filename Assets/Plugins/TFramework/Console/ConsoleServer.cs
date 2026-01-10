using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
#if UNITY_EDITOR
using UnityEditor;
#endif

using UnityEngine;
using Task = System.Threading.Tasks.Task;

namespace TFramework.Console
{
    public static class ConsoleServer
    {
        
// #if UNITY_EDITOR
//         [InitializeOnLoadMethod]
// #else
//         [RuntimeInitializeOnLoadMethod]
// #endif
        public static void Init()
        {
            InitTcpServer();
        }
        private static Socket server;
        private static bool serverRunning = false;
        private static string sendMessage = "";
        private static async void InitTcpServer()
        {
            ushort port = 11451;
            string ip = "127.0.0.1";
            server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPAddress ipAddress = IPAddress.Parse(ip);
            IPEndPoint ipEndPoint = new IPEndPoint(ipAddress, port);
            server.Bind(ipEndPoint);
            server.Listen(1);
            serverRunning = true;
            Listener();
            Application.logMessageReceived += (condition, stackTrace, type) =>
            {
                string message = $"[{type}]{condition}";
                sendMessage = message;
            };
            async void Listener()
            {
                while (serverRunning)
                {
                    var socketEnd = await server.AcceptAsync();
                    Debug.Log($"监听到新的远程端:{socketEnd.RemoteEndPoint}");
                    Accept(socketEnd);
                    // Send(socketEnd);
                }
            }
            async void Accept(Socket socketSend)
            {
                while (serverRunning)
                {
                    if (!socketSend.Connected)
                    {
                        Debug.Log("远程端已断开连接"); 
                        return;
                    }
                    try
                    {
                        var buffer = new byte[1024 * 6];
                        Memory<byte> bufferMemory = new Memory<byte>(buffer);
                        int len = await socketSend.ReceiveAsync(bufferMemory, SocketFlags.None);
                        if (len == 0)
                            break;
                        string message = Encoding.UTF8.GetString(buffer, 0, len);
                        Debug.Log(message);
                        ConsoleControl.ExecuteCommand(message);
                    }
                    catch (SocketException e)
                    {
                        sendMessage = null;
                        Debug.Log("远程端已断开连接");
                        return;
                    }
                    catch (Exception e)
                    {
                        sendMessage = null;
                        Debug.LogError(e);
                    }
                   
                }
            }

            async void Send(Socket socketSend)
            {
                while (serverRunning)
                {
                    if (!string.IsNullOrEmpty(sendMessage))
                    {
                        byte[] buffer = Encoding.UTF8.GetBytes(sendMessage);
                        await socketSend.SendAsync(buffer, SocketFlags.None);
                        sendMessage = "";
                    }
                    else
                    {
                        await Task.Delay(300);
                    }
                }
            }
        }
    }
}