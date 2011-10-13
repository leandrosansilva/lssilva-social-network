using System;
using System.Net.Sockets;
using System.Net;
using System.IO;
using System.Threading;

namespace socialnetwork
{
  public class ClientHandler
  {
    private class InternalHandler 
    {
      private Command _command = null;
      private StreamWriter _writer = null;
      private StreamReader _reader = null;
      private NetworkStream _stream = null;
      private Thread _thread = null;
      
      public InternalHandler(Command command, StreamWriter writer, StreamReader reader,NetworkStream stream)
      {
        _command = command;
        _writer = writer;
        _reader = reader;
        _stream = stream;
      }
      
      public void execute()
      {
        string output = _command.execute();
        Console.WriteLine(output);
        _writer.WriteLine(output);
        _writer.Flush();

        _writer.Close();
        _reader.Close();
        _stream.Close();
      }
      
      public void run()
      {
        _thread = new Thread(execute);
        _thread.Start();
      }
    }
  
    public ClientHandler ()
    {
    }
    
    public void handle(Command command, StreamWriter writer, StreamReader reader,NetworkStream stream)
    {
      InternalHandler handler = new InternalHandler(command,writer,reader,stream);
      handler.run();
    }
  }
}

