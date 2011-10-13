using System;

namespace socialnetwork
{
  public class ListFollowedMessages: Command
  {
    private string _userName;

    public ListFollowedMessages (string command)
    {
      _userName = command;
    }

    public string execute()
    {
      try {
        System.Collections.Generic.List<Message> messages = Users.getFollowerMessages(_userName);
        string output = "";

        foreach(Message message in messages) {
          output += message.user.name + " " + message.content + "\n";
        }

        return output;
      } catch (InvalidUserName) {
        return "usuario-nao-encontrado";
      }
    }
  }
}

