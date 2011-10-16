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
using System.Collections.Generic;

namespace socialnetwork
{
  public class ListMessagesWithHashTagCommand: Command
  {
    private string _hash = "";
    public ListMessagesWithHashTagCommand(string hash)
    {
      _hash = hash;
    }

    public string execute()
    {
      try {
        List<Message> messages = HashTags.getMessagesWithHashTag(_hash);

        string output = "";

        foreach(Message message in messages) {
          output += message.user.name + " " + message.content + "\n";
        }

        return output;

      } catch (InvalidHashTag) {
        return "palavra-marcada-invalida";
      } catch (HashTagDoesNotExist) {
        return "palavra-marcada-nao-encontrada";
      }

    }

  }
}

