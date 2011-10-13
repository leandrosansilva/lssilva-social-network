using System;

namespace socialnetwork
{
  public class PostMessageCommand: Command
  {
    private string _userName;
    private string _content;

    public PostMessageCommand(string command)
    {
      int i = 0;
      for (;i < command.Length && command[i] != ' '; i++) {
        _userName += command[i];
      }
    
      _content = command.Substring(i).TrimStart();
    }
    
    public string execute()
    {
      try {
        Messages.add(_userName,_content);
        return "ok";
      } catch (InvalidUserName) {
        return "usuario-nao-encontrado";
      } catch (InvalidMessage) {
        return "mensagem-invalida";
      }
    }
  }
}

