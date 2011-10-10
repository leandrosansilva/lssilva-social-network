using System;

namespace socialnetwork
{
	public class CreateUserCommand: Command
	{
		private string _command = "";
		
		public CreateUserCommand(string command)
		{
			_command = command;
		}
		
		public int execute()
		{
			return 1;
		}
		
		public string output()
		{
			return _command.ToUpper();
		}
	}
}

