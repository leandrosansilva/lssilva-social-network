using System;

namespace socialnetwork
{
	public class CommandFactory
	{
		private CommandFactory ()
		{
		}
		
		static public Command create(string commandInput)
		{
			string[] splitted = commandInput.Split(' ');
		
			// o que foi passado, sem o comando, só os dados
			string payload = String.Join(" ",splitted,1,splitted.Length - 1);
			
			// splitted[0] guarda o comando a ser executado
			switch (splitted[0]) {
				case "create-user":
					return new CreateUserCommand(payload);
				break;
			}
			return new ErrorCommand(payload);
		}
	}
}

