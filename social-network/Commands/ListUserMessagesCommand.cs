using System;

namespace socialnetwork
{
  public class ListUserMessagesCommand: Command
  {
    private string _user = "";

    public ListUserMessagesCommand(string user)
    {
      _user = user;
    }

    public string execute()
    {
      try {
        System.Collections.Generic.List<Message> messages = Users.getMessages(_user);

        string output = "";

        foreach (Message message in messages) {
          output += message.content + "\n";
        }

        return output;

      } catch (InvalidUserName) {
        return "usuario-invalido";
      }
    }
  }
}

