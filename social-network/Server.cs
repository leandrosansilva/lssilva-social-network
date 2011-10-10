using System;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.IO;

namespace socialnetwork
{
  public class Server
  {
    private TcpListener _serverListener;
    public Server(int port)
    {
      _serverListener = new TcpListener(IPAddress.Parse("127.0.0.1"),port);
      _serverListener.Start();
    }

    public void run()
    {
      while (true) {
        TcpClient client = _serverListener.AcceptTcpClient();
        
        NetworkStream stream = client.GetStream();
        
        string dataFromClient = new StreamReader(stream).ReadLine();
        
        Console.WriteLine(dataFromClient);
        Console.WriteLine(dataFromClient.Length);

        Byte[] sendBytes = Encoding.UTF8.GetBytes("Hello World");

        stream.Write(sendBytes,0,sendBytes.Length);
      }
    }
  }
}

