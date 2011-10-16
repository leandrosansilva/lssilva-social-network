/*
  * This file is part of lssilva-social-network.
  *
  * lssilva-social-network is free software: you can redistribute it and/or modify
  * it under the terms of the GNU General Public License as published by
  * the Free Software Foundation, either version 3 of the License, or
  * (at your option) any later version.
  *
  * lssilva-social-network is distributed in the hope that it will be useful,
  * but WITHOUT ANY WARRANTY; without even the implied warranty of
  * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
  * GNU General Public License for more details.
  *
  * You should have received a copy of the GNU General Public License
  * along with lssilva-social-network.  If not, see <http://www.gnu.org/licenses/>.
*/

using System;
using System.Text.RegularExpressions;

namespace socialnetwork
{
  public class FollowCommand: Command
  {
    private string _user = "";
    private string _followed = "";

    public FollowCommand(string command)
    {
      Regex er = new Regex(@"^(\w+) +(.+)$");
      Match result = er.Match(command);

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
        return "seguidor-e-seguido-sao-iguais";
      }

    }
  }
}