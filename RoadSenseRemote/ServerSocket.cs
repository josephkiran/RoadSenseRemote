//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Net;
//using System.Net.Sockets;
//using System.Text;
//using System.Threading.Tasks;

//namespace RoadSenseRemote
//{
//    public class ServerSocket
//    {
//        private Socket _serverSocket;

//        public bool StartServer()
//        {
//            IPAddress ipAddress =
//          Dns.Resolve(Dns.GetHostName()).AddressList[0];

//            IPEndPoint ipEndpoint =
//              new IPEndPoint(ipAddress, 1800);

//            Socket listenSocket =
//              new Socket(AddressFamily.InterNetwork,
//                         SocketType.Stream,
//                         ProtocolType.Tcp);

//            listenSocket.Bind(ipEndpoint);
//            listenSocket.Listen(1);
//            IAsyncResult asyncAccept = listenSocket.BeginAccept(
//              new AsyncCallback(acceptCallback),
//              listenSocket);
//            return true;
//        }

//        public void SendData(string msg)
//        {
//            AsynchronousSocketListener.Send(msg);
//        }

//        private void connectCallback(IAsyncResult asyncConnect)
//        {
//            _servSocket =
//              (Socket)asyncConnect.AsyncState;
//            _clientSocket.EndConnect(asyncConnect);
//            // arriving here means the operation completed
//            // (asyncConnect.IsCompleted = true) but not
//            // necessarily successfully
//            if (_clientSocket.Connected == false)
//            {
//                Console.WriteLine(".client is not connected.");
//                StatusEvent(ConnectionStatus.Disconnected, "disconnected");
//                return;
//            }
//            else
//            {
//                Console.WriteLine(".client is connected.");
//                StatusEvent(ConnectionStatus.Connected, "connected");
//                BeginRecieving(_clientSocket);
//            }


//            //writeDot(asyncSend);
//        }

//        private void acceptCallback(IAsyncResult asyncAccept)
//        {
//            Socket listenSocket = (Socket)asyncAccept.AsyncState;
//            Socket serverSocket =
//              listenSocket.EndAccept(asyncAccept);

//            // arriving here means the operation completed
//            // (asyncAccept.IsCompleted = true) but not
//            // necessarily successfully
//            if (serverSocket.Connected == false)
//            {
//                Console.WriteLine(".server is not connected.");
//                return;
//            }
//            else Console.WriteLine(".server is connected.");

//            listenSocket.Close();

//            StateObject1 stateObject =
//              new StateObject1(16, serverSocket);
//            BeginRecieving(stateObject);
//            //writeDot(asyncReceive);
//        }

//        private void BeginRecieving(StateObject1 so)
//        {
//            // this call passes the StateObject because it
//            // needs to pass the buffer as well as the socket
//            IAsyncResult asyncReceive =
//              _serverSocket.BeginReceive(
//                so.sBuffer,
//                0,
//                so.sBuffer.Length,
//                SocketFlags.None,
//                new AsyncCallback(receiveCallback),
//                so);

//            Console.Write("Receiving response.");
//            //writeDot(asyncReceive);
//        }


//    }
//}
