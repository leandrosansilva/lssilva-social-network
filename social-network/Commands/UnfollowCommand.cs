using System;
using System.Text.RegularExpressions;

namespace socialnetwork
{
  public class UnfollowCommand: Command
  {
    private string _user;
    private string _following;

    public UnfollowCommand(string command)
    {
      Regex er = new Regex(@"^(\w+) +(.+)$");
      Match result = er.Match(command.TrimEnd());

      if (!result.Success || result.Groups.Count != 3) {
        return;
      }

      _user = result.Groups[1].ToString();
      _following = result.Groups[2].ToString();
    }

    public string execute()
    {
      try {
        Users.unfollow(_user,_following);
        return "ok";
      } catch (InvalidUserName) {
        return "seguidor-nao-encontrado";
      } catch (FollowingDoesNotExists) {
        return "seguido-nao-encontrado";
      }
    }
  }
}

