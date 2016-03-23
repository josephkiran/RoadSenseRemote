using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace RoadSenseRemote
{
    public class StateObject1
    {
        internal byte[] sBuffer;
        internal Socket sSocket;
        internal StateObject1(int size, Socket sock)
        {
            sBuffer = new byte[size];
            sSocket = sock;
        }
    }

    public class ClientSocket
    {
        string _ipAddress;
        int _port;
        public Action<ConnectionStatus, string> StatusEvent { get; set; }

        public Action<string> RecdDataEvent { get; set; }

        private Socket _clientSocket;
        public ClientSocket(string ipAddress, int port)
        {
            _ipAddress = ipAddress;
            _port = port;
        }

        public void ConnectToServer()
        {
            IPAddress ipAddress =
          Dns.Resolve(_ipAddress).AddressList[0];

            IPEndPoint ipEndpoint =
              new IPEndPoint(ipAddress, _port);

            Socket clientSocket = new Socket(
              AddressFamily.InterNetwork,
              SocketType.Stream,
              ProtocolType.Tcp);

            IAsyncResult asyncConnect = clientSocket.BeginConnect(
              ipEndpoint,
              new AsyncCallback(connectCallback),
              clientSocket);
        }

        private void connectCallback(IAsyncResult asyncConnect)
        {
            _clientSocket =
              (Socket)asyncConnect.AsyncState;
            _clientSocket.EndConnect(asyncConnect);
            // arriving here means the operation completed
            // (asyncConnect.IsCompleted = true) but not
            // necessarily successfully
            if (_clientSocket.Connected == false)
            {
                Console.WriteLine(".client is not connected.");
                StatusEvent(ConnectionStatus.Disconnected, "disconnected");
                return;
            }
            else
            {
                Console.WriteLine(".client is connected.");
                StatusEvent(ConnectionStatus.Connected, "connected");
                BeginRecieving(_clientSocket);
            }


            //writeDot(asyncSend);
        }

        public void SendData(string msg)
        {
            byte[] sendBuffer = Encoding.ASCII.GetBytes(msg);
            IAsyncResult asyncSend = _clientSocket.BeginSend(
              sendBuffer,
              0,
              sendBuffer.Length,
              SocketFlags.None,
              new AsyncCallback(sendCallback),
              _clientSocket);

            Console.Write("Sending data.");
        }

        public void Stop()
        {
            _clientSocket.Shutdown(SocketShutdown.Both);
            _clientSocket.Close();
        }

        private void BeginRecieving(Socket sk)
        {
            StateObject1 stateObject =
             new StateObject1(16, sk);
            // this call passes the StateObject because it
            // needs to pass the buffer as well as the socket
            IAsyncResult asyncReceive =
              _clientSocket.BeginReceive(
                stateObject.sBuffer,
                0,
                stateObject.sBuffer.Length,
                SocketFlags.None,
                new AsyncCallback(receiveCallback),
                stateObject);

            Console.Write("Receiving response.");
            //writeDot(asyncReceive);
        }


        private void receiveCallback(IAsyncResult asyncReceive)
        {
            StateObject1 stateObject =
             (StateObject1)asyncReceive.AsyncState;

            int bytesReceived =
              stateObject.sSocket.EndReceive(asyncReceive);

            RecdDataEvent(Encoding.ASCII.GetString(stateObject.sBuffer));

            Console.WriteLine(
              ".{0} bytes received: {1}{2}{2}",
              bytesReceived.ToString(),
              Encoding.ASCII.GetString(stateObject.sBuffer),
              Environment.NewLine);

            BeginRecieving(stateObject.sSocket);
        }

        private void sendCallback(IAsyncResult asyncSend)
        {
            Socket clientSocket = (Socket)asyncSend.AsyncState;
            int bytesSent = clientSocket.EndSend(asyncSend);
            StatusEvent(ConnectionStatus.DataSent, bytesSent.ToString());
            Console.WriteLine(
              ".{0} bytes sent.",
              bytesSent.ToString());

        }

    }

    public enum ConnectionStatus
    {
        Connected,
        Disconnected,
        DataSent,
        DataRecd
    }
}
