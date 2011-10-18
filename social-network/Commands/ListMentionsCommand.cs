using System;
using System.Collections.Generic;

namespace socialnetwork
{
  public class ListMentionsCommand: Command
  {
    private string _param = "";

    public ListMentionsCommand(string param)
    {
      _param = param;
    }

    public string execute()
    {
      try {
        List<Message> messages = Users.mentions(_param);
        string output = "";
        foreach(Message message in messages) {
          output += message.user.name + " " + message.content + "\n";
        }
        return output;
      } catch (InvalidUserName) {
        return "usuario-invalido";
      } catch (PatternNotFound) {
        return "nenhuma-mencao-encontrada";
      }
    }
  }
}

