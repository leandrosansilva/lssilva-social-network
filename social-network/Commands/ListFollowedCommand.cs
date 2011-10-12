using System;

namespace socialnetwork
{
  public class ListFollowedCommand: Command
  {
    private string _userName;

    public ListFollowedCommand(string command)
    {
      _userName = command;
    }
    
    public string execute()
    {
      try {
        System.Collections.Generic.List<User> followed = Users.getFollowed(_userName);
        string output = "";

        foreach(User f in followed) {
          output += f.name + "\n";
        }

        return output;

      } catch (InvalidUserName) {
        return "usuario-invalido";
      }
    }
  }
}

