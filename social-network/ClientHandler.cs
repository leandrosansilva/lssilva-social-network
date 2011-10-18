/*
  * This file is part of lssilva-social-network.
  *
  * lssilva-social-network is free software: you can redistribute it and/or modify
  * it under the terms of the GNU General Public License as published by
  * the Free Software Foundation, either version 3 of the License, or
  * (at your option) any later version.
  *
  * lssilva-social-network is distributed in the hope that it will be useful,
  * but WITHOUT ANY WARRANTY; without even the implied warranty of
  * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
  * GNU General Public License for more details.
  *
  * You should have received a copy of the GNU General Public License
  * along with lssilva-social-network.  If not, see <http://www.gnu.org/licenses/>.
*/

using System;
using System.Net.Sockets;
using System.Net;
using System.IO;
using System.Threading;

namespace socialnetwork
{
  public class ClientHandler
  {
    // FIXME: não preciso desta classe. Posso usar uma função anônima (delegate)
    // no lugar de uma classe no ClientHandler
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

        /* FIXME: gambiarra: se o último caractere da resposta for uma quebra de linha,
         * usa write, para não duplica-la. Caso contrário, usa writeline
        */
        if (output.EndsWith("\n")) {
          _writer.Write(output);
        } else {
          _writer.WriteLine(output);
        }

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