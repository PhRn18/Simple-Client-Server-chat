using System.Net.Sockets;

namespace ConsoleApp1;

public class Client
{
    public string myIp { get; private set; }
    public int port { get;set; }
    private TcpClient socketForServer;
    public bool clientStatus = true;
    public NetworkStream networkStream { get; set; }
    public StreamWriter streamWriter { get; set; }
    public StreamReader streamReader { get; set; }
    public Client(string myIp,int port)
    {
        this.myIp = myIp;
        this.port = port;
    }
    public void ConnectToServer()
    {
        try
        {
            socketForServer = new TcpClient(myIp.ToString(), port);
            
        }
        catch(Exception e)
        {
            Console.WriteLine(e);
        }
    }
    public void serverData()
    {
        networkStream = socketForServer.GetStream();
        streamReader = new StreamReader(networkStream);
        streamWriter = new StreamWriter(networkStream);
    }

    public void disconnect()
    {
        streamReader.Close();
        streamWriter.Close();
        networkStream.Close();
        socketForServer.Close();
    }
}