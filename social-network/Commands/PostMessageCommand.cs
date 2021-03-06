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

namespace socialnetwork
{
  public class PostMessageCommand: Command
  {
    private string _userName = "";
    private string _content = "";

    public PostMessageCommand(string command)
    {
      // TODO: usar regex
      int i = 0;
      for (;i < command.Length && command[i] != ' '; i++) {
        _userName += command[i];
      }
    
      _content = command.Substring(i).Trim();
    }
    
    public string execute()
    {
      try {
        Messages.add(_userName,_content);
        return "ok";
      } catch (InvalidUserName) {
        return "usuario-nao-encontrado";
      } catch (InvalidMessage) {
        return "mensagem-invalida";
      }
    }
  }
}

