namespace ConsoleApp1;

public class ProgramClient
{
    static void StartClient(string[] args)
    {
        string myIp = "";
        int port = 3000;
        Client client = new Client(myIp,port);
        client.ConnectToServer();
        Console.WriteLine("Connect to server");
        Thread.Sleep(1000);
        client.serverData();
        try
        {
            string messageToServer = "";
            string messageFromServer = "";
            while (client.clientStatus)
            {
                messageToServer = Console.ReadLine();
                if (messageToServer == "exit")
                {
                    client.clientStatus = false;
                    client
                        .streamWriter
                        .WriteLine("CONNECTION CLOSED");
                    client
                        .streamWriter
                        .Flush();
                }
                else
                {
                    client
                        .streamWriter
                        .WriteLine(messageToServer);
                    client
                        .streamWriter
                        .Flush();
                    messageFromServer = client.streamReader.ReadLine();
                    Console.WriteLine("SERVER RESPONSE ->"+messageFromServer);
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }
}