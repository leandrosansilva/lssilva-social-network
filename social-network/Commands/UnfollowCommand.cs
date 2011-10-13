using System;
using System.Text.RegularExpressions;

namespace socialnetwork
{
  public class UnfollowCommand: Command
  {
    private string _user = "";
    private string _followed = "";

    public UnfollowCommand(string command)
    {
      Regex er = new Regex(@"^(\w+) +(.+)$");
      Match result = er.Match(command.TrimEnd());

      if (!result.Success || result.Groups.Count != 3) {
        return;
      }

      _user = result.Groups[1].ToString();
      _followed = result.Groups[2].ToString();
    }

    public string execute()
    {
      try {
        Users.unfollow(_user,_followed);
        return "ok";
      } catch (InvalidUserName) {
        return "seguidor-nao-encontrado";
      } catch (InvalidFollowed) {
        return "seguido-nao-encontrado";
      } catch (FollowedDoesNotExist) {
        return "não-seguindo";
      }
    }
  }
}

