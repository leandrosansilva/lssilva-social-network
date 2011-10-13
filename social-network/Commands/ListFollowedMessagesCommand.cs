using System;

namespace socialnetwork
{
  public class ListFollowedMessagesCommand: Command
  {
    private string _userName;

    public ListFollowedMessagesCommand (string command)
    {
      _userName = command;
    }

    public string execute()
    {
      try {
        System.Collections.Generic.List<Message> messages = Users.getFollowedMessages(_userName);
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