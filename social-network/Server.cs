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
      _serverListener = new TcpListener(IPAddress.Any,port);
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
          reader.Close();
          writer.Close();
          stream.Close();
          Console.WriteLine("command error");
        }
      }
    }
  }
}

