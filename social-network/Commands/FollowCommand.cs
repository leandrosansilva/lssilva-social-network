using System;
using System.Text.RegularExpressions;

namespace socialnetwork
{
  public class FollowCommand: Command
  {
    private string _user;
    private string _following;

    public FollowCommand(string command)
    {
      Regex er = new Regex(@"^(\w+) +(.+)$");
      Match result = er.Match(command.TrimEnd());

      Console.WriteLine(result.Groups.Count);

      if (!result.Success || result.Groups.Count != 3) {
        return;
      }

      _user = result.Groups[1].ToString();
      _following = result.Groups[2].ToString();

      Console.WriteLine(_user);
      Console.WriteLine(_following);
    }
    
    public string execute()
    {
      try {
        Users.follow(_user,_following);
        return "ok";
      } catch (InvalidUserName) {
        return "usuario-invalido";
      } catch (InvalidFollowing) {
        return "seguidor-nao-encontrado";
      } catch (FollowingAlreadExists) {
        return "ja-seguindo";
      }

    }
  }
}

