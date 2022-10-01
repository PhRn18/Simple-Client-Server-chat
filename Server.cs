using System.Net;
using System.Net.Sockets;

namespace ConsoleApp1;

public class Server
{
    public IPAddress myIp { get; private set; }
    public int port { get; private set; }
    public bool serverStatus { get; private set; }
    private TcpListener tcpListener { get; set; }
    public Socket socketForClient { get; set; }
    public NetworkStream networkStream { get; set; }
    public StreamReader streamReader { get; set; }
    public StreamWriter streamWriter { get; set; }


    public Server(IPAddress myIp,int port)
    {
        this.myIp = myIp;
        this.port = port;
    }

    public void startListening()
    {
        try
        {
            tcpListener = new TcpListener(myIp, port);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }

    public void acceptClient()
    {
        try
        {
            tcpListener.Start();
            socketForClient = tcpListener.AcceptSocket();
        }
        catch(Exception e)
        {
            Console.WriteLine(e);
        }
    }

    public void clientData()
    {
        networkStream = new NetworkStream(socketForClient);

        streamReader = new StreamReader(networkStream);

        streamWriter = new StreamWriter(networkStream);
    }

    public void disconnect()
    {
        networkStream.Close();
        streamWriter.Close();
        streamReader.Close();
        socketForClient.Close();
    }
}