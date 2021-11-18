using System;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace OneUseEchoServer
{
    class Program
    {
        static void Main(string[] args)
        {
            //Writing to the console to be able to differentiate what is running
            Console.WriteLine("Server:");

            //Creates a listener able to listen for connections incoming on port 7 only on the loopback adapter
            //loopback adapter is only used for connections on the local machine
            TcpListener listener = new TcpListener(IPAddress.Loopback, 7);
            
            //Actually starts the listener
            listener.Start();

            //Here the code will wait, until a client connects and then returns an instance of the TcpClient class
            TcpClient socket = listener.AcceptTcpClient();

            //Gets the stream object from the socket. The stream object is able to recieve and send data
            NetworkStream ns = socket.GetStream();
            //The StreamReader is an easier way to read data from a Stream, it uses the NetworkStream
            StreamReader reader = new StreamReader(ns);
            //The StreamWriter is an easier way to write data to a Stream, it uses the NetworkStream
            StreamWriter writer = new StreamWriter(ns);

            //Here it reads all data send until a line break (cr lf) is received; notice the Line part of the ReadLine
            string message = reader.ReadLine();
            //Here it writes the received data to the Console
            //this is only for testing purposes, to verify that the server recieves the data
            Console.WriteLine(message);
            //Here it writes the data back to the client and appends a line break (cr lf); notice the line part of WriteLine
            writer.WriteLine(message);

            //Because it doesn't expect more messages from the client, it closes the socket/connection
            socket.Close();

            //this is a single use server, therefore we cleanup and close the listener
            listener.Stop();
        }
    }
}
