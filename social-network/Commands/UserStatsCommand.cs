using System;

namespace socialnetwork
{
  public class UserStatsCommand: Command
  {
    private string _user;
    public UserStatsCommand(string user)
    {
      _user = user;
    }

    public string execute()
    {
      try {
        UserStats stats = Users.getUserStats(_user);

        string output = stats.messagesCount + "\n" +
          stats.followed + "\n" +
          stats.followers + "\n";

        return output;
        
      } catch (InvalidUserName) {
        return "usuario-invalido";
      }
    }
  }
}

