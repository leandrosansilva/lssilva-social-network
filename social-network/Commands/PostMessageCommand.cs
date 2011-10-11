using System;

namespace socialnetwork
{
  public class PostMessageCommand: Command
  {
	private string _user;
	private string _content;
    public PostMessageCommand(string command)
    {
		int i = 0;
		for (;i < command.Length && command[i] != ' '; i++) {
			_user += command[i];
		}
		
		_content = command.Substring(i).TrimStart();
    }
    
    public string execute()
    {
	  Message message = new Message(_user,_content);
	  try {
		Messages.add(message);
		return "ok";
	  } catch (InvalidMessage e) {
	    return "mensagem-invalida"; 
	  } catch (InvalidMessageUser) {
	    return "usuario-nao-encontrado";
	  }
    }
  }
}

