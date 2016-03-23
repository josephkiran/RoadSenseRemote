using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RoadSenseRemote
{
    public class SocketServer
    {
        public SocketServer()
        {
            
        }

        public Action<string> RecvEvt
        {
            set { AsynchronousSocketListener._recvEvt = value; }
        }

        public Action ConnectionDone
        {
            set { AsynchronousSocketListener._connected = value; }
        }
        public bool StartServer()
        {
           Task t = new Task(new Action (AsynchronousSocketListener.StartListening));
            t.Start();
            return true;
        }

        public void SendData(string msg)
        {
            AsynchronousSocketListener.Send(msg);
        }


    }


    public class StateObject
    {
        // Client  socket.
        public Socket workSocket = null;
        // Size of receive buffer.
        public const int BufferSize = 1024;
        // Receive buffer.
        public byte[] buffer = new byte[BufferSize];
        // Received data string.
        public StringBuilder sb = new StringBuilder();
    }

    public class AsynchronousSocketListener
    {
        // Thread signal.
        public static ManualResetEvent allDone = new ManualResetEvent(false);
        public static StateObject _state;
        public static Action<string> _recvEvt;
        public static Action _connected;

        public AsynchronousSocketListener()
        {
        }

        public static void StartListening()
        {
            // Data buffer for incoming data.
            byte[] bytes = new Byte[1024];

            // Establish the local endpoint for the socket.
            // The DNS name of the computer
            // running the listener is "host.contoso.com".
            IPHostEntry ipHostInfo = Dns.Resolve(Dns.GetHostName());
            IPAddress ipAddress = ipHostInfo.AddressList[0];
            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 4444);

            // Create a TCP/IP socket.
            Socket listener = new Socket(AddressFamily.InterNetwork,
                SocketType.Stream, ProtocolType.Tcp);

            // Bind the socket to the local endpoint and listen for incoming connections.
            try
            {
                listener.Bind(localEndPoint);
                listener.Listen(100);

                while (true)
                {
                    // Set the event to nonsignaled state.
                    allDone.Reset();

                    // Start an asynchronous socket to listen for connections.
                    Console.WriteLine("Waiting for a connection...");
                    listener.BeginAccept(
                        new AsyncCallback(AcceptCallback),
                        listener);

                    // Wait until a connection is made before continuing.
                    allDone.WaitOne();
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            Console.WriteLine("\nPress ENTER to continue...");
            Console.Read();

        }

        public static void AcceptCallback(IAsyncResult ar)
        {
            // Signal the main thread to continue.
            allDone.Set();

            // Get the socket that handles the client request.
            Socket listener = (Socket)ar.AsyncState;
            Socket handler = listener.EndAccept(ar);

            // Create the state object.
            _state = new StateObject();
            _state.workSocket = handler;
            handler.BeginReceive(_state.buffer, 0, StateObject.BufferSize, 0,
                new AsyncCallback(ReadCallback), _state);
            _connected();
        }

        public static void ReadCallback(IAsyncResult ar)
        {
            String content = String.Empty;

            // Retrieve the state object and the handler socket
            // from the asynchronous state object.
            _state = (StateObject)ar.AsyncState;
            Socket handler = _state.workSocket;

            // Read data from the client socket. 
            int bytesRead = handler.EndReceive(ar);

            if (bytesRead == 4)
            {
                //this is the length of the data to be recd.
                byte dataLen = _state.buffer[0];
                handler.Receive(_state.buffer, (int)dataLen, SocketFlags.None);
                string data = Encoding.ASCII.GetString(_state.buffer, 0, dataLen);

                // There  might be more data, so store the data received so far.
                _state.sb.Append(data);
                Console.WriteLine("Read {0} bytes from socket. \n Data : {1}",
                        dataLen, data);
                _recvEvt(data);
               // Send( Console.ReadLine());
                // Check for end-of-file tag. If it is not there, read 
                // more data.
                //content = state.sb.ToString();
                //if (content.IndexOf("<EOF>") > -1)
                //{
                //    // All the data has been read from the 
                //    // client. Display it on the console.
                //    Console.WriteLine("Read {0} bytes from socket. \n Data : {1}",
                //        content.Length, content);
                //    // Echo the data back to the client.
                //    Send(handler, content);
                //}
                //else {
                //    // Not all data received. Get more.
                //    handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                //    new AsyncCallback(ReadCallback), state);
                //}

                handler.BeginReceive(_state.buffer, 0, StateObject.BufferSize, 0,
                    new AsyncCallback(ReadCallback), _state);
            }
        }

        public static void Send(String data)
        {
            Socket handler = _state.workSocket;
            byte[] data1;
            data1 = Encoding.Default.GetBytes(data); // put the msg in the byte ( it automaticly uses the size of the msg )
            int length = data1.Length; // Gets the length of the byte data
            byte[] datalength = new byte[4]; // Creates a new byte with length of 4
            datalength = BitConverter.GetBytes(length); //put the length in a byte to send it
            byte[] byteData = Encoding.ASCII.GetBytes(data);

            handler.Send(datalength);
            handler.Send(byteData);

            // Begin sending the data to the remote device.
            //handler.BeginSend(byteData, 0, byteData.Length, 0,
            // new AsyncCallback(SendCallback), handler);
        }

        private static void SendCallback(IAsyncResult ar)
        {
            try
            {
                // Retrieve the socket from the state object.
                Socket handler = (Socket)ar.AsyncState;

                // Complete sending the data to the remote device.
                int bytesSent = handler.EndSend(ar);
                Console.WriteLine("Sent {0} bytes to client.", bytesSent);

                handler.Shutdown(SocketShutdown.Both);
                handler.Close();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }


     
    }
}
