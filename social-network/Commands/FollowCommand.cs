using System;
using System.Text.RegularExpressions;

namespace socialnetwork
{
  public class FollowCommand: Command
  {
    private string _user;
    private string _followed;

    public FollowCommand(string command)
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
        Users.follow(_user,_followed);
        return "ok";
      } catch (InvalidUserName) {
        return "seguidor-nao-encontrado";
      } catch (InvalidFollowed) {
        return "seguido-nao-encontrado";
      } catch (FollowedAlreadExists) {
        return "ja-seguindo";
      } catch (UsersAreTheSame) {
        return "seguidor-e-seguidos-sao-iguais";
      }

    }
  }
}

