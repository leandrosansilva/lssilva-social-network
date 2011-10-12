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

      if (!result.Success || result.Groups.Count != 3) {
        return;
      }

      _user = result.Groups[1].ToString();
      _following = result.Groups[2].ToString();
    }
    
    public string execute()
    {
      try {
        Users.follow(_user,_following);
        return "ok";
      } catch (InvalidUserName) {
        return "seguidor-nao-encontrado";
      } catch (InvalidFollowing) {
        return "seguido-nao-encontrado";
      } catch (FollowingAlreadExists) {
        return "ja-seguindo";
      } catch (UsersAreTheSame) {
        return "seguidor-e-seguidos-sao-iguais";
      }

    }
  }
}

