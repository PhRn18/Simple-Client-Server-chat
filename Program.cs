using System.Net;
using System.Net.Sockets;

namespace ConsoleApp1;

public class Program
{
    static void Main(string[] args)
    {
        bool serverStatus = true;
        Console.Title = "SERVER";
        IPAddress myIp = IPAddress.Parse("192.168.15.10");
        int port = 3000;

        Server server = new Server(myIp, port);
        server.startListening();
        Console.WriteLine("Start listening");

        
        Thread.Sleep(1000);
        Console.Clear();
        Console.WriteLine("Waiting for connection...");
        
        server.acceptClient();
        Console.WriteLine("Client connect");

        string messageFromClient = "";
        string messageToClient = "";
        try
        {
            server.clientData();
            while (serverStatus)
            {
                if (server.socketForClient.Connected)
                {
                    messageFromClient = server.streamReader.ReadLine();
                    Console.WriteLine(messageFromClient);
                    if (messageFromClient == "exit")
                    {
                        serverStatus = false;
                        server
                            .streamReader
                            .Close();
                        server
                            .streamWriter
                            .Close();
                        server
                            .networkStream
                            .Close();
                    }
                    Console.WriteLine("Server: ");
                    messageToClient = Console.ReadLine();
                    server
                        .streamWriter
                        .WriteLine(messageToClient);
                    server
                        .streamWriter
                        .Flush();
                }
            }
            server.disconnect();
        }
        catch(Exception e)
        {
            Console.WriteLine(e);
        }


    }
}