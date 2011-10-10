using System;

namespace socialnetwork
{
	public class ErrorCommand: Command
	{
		public ErrorCommand(string command)
		{
		}
		
		public string output()
		{
			return "Este é um comando de erro!";
		}
		
		public int execute()
		{
			return 1;
		}
	}
}

