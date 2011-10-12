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
      private Thread _thread = null;
      
      public InternalHandler(Command command, StreamWriter writer)
      {
        _command = command;
        _writer = writer;
      }
      
      public void execute()
      {
        string output = _command.execute();
        Console.WriteLine(output);
        _writer.WriteLine(output);
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
    
    public void handle(Command command, StreamWriter writer)
    {
      InternalHandler handler = new InternalHandler(command,writer);
      handler.run();
    }
  }
}

