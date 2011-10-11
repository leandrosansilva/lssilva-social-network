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
			private StreamReader _reader = null;
			private StreamWriter _writer = null;
			private Thread _thread = null;
			
			public InternalHandler(Command command, StreamReader reader, StreamWriter writer)
			{
				_command = command;
				_reader = reader;
				_writer = writer;
			}
			
			public void execute()
			{
				Console.WriteLine(_command.execute());
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
		
		public void handle(Command command, StreamReader reader, StreamWriter writer)
		{
			InternalHandler handler = new InternalHandler(command,reader,writer);
			handler.run();
		}
	}
}

