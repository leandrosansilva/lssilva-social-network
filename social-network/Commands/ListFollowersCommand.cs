using System;

namespace socialnetwork
{
  public class ListFollowersCommand: Command
  {
    private string _userName;

    public ListFollowersCommand(string command)
    {
      _userName = command;
    }
    
    public string execute()
    {
      try {
        System.Collections.Generic.List<User> followers = Users.getFollowers(_userName);

        string output = "";

        foreach(User f in followers) {
          output += f.name + "\n";
        }

        return output;

      } catch (InvalidUserName) {
        return "usuario-invalido";
      }
    }
  }
}

