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
    
    private ClientHandler _handler = new ClientHandler();
    
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
        
        StreamReader reader = new StreamReader(stream,new UTF8Encoding(false));
        StreamWriter writer = new StreamWriter(stream,new UTF8Encoding(false));

        try {
          string clientInput = reader.ReadLine();

          Console.WriteLine(clientInput);
        
          Command command = CommandFactory.create(clientInput);
        
          _handler.handle(command,writer,reader,stream);
        } catch (IOException) {
          Console.WriteLine("command error");
        }
      }
    }
  }
}

