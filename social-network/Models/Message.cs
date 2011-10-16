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
  public class Message
  {
    public string content {
      get {
        return _content;
      }

      set {
        _content = value;
      }
    }

    public User user {
      get {
        return _user;
      }
      set {
        _user = value;
      }
    }

    public System.Collections.Generic.List<HashTag> hashTags {
      get {
        return _hashTags;
      }
    }

    public DateTime created
    {
      get {
        return _created;
      }
    }

    private string _content = null;

    private User _user = null;

    private DateTime _created = DateTime.Now;

    private System.Collections.Generic.List<HashTag> _hashTags
      = new System.Collections.Generic.List<HashTag>();

    public Message()
    {
    }
  }
  
  public class InvalidMessage: Exception
  {
  }
  
  static public class Messages
  {
    static private System.Collections.Generic.List<Message> _messages
      = new System.Collections.Generic.List<Message>();

    static public void add(string userName, string content)
    {
      if (content.Length == 0 || content.Length >= 140) {
        throw new InvalidMessage();
      }

      try {
        Message message = new Message() { content = content };
        User user = Users.addMessage(userName,message);
        message.user = user;

        // FIXME: isto está errado, pois pega # no meio de palavras (malha#ção gera #ção)
        // e não pega #coca-cola, mas só #coca
        Regex re = new Regex(@"(#\w+)");

        MatchCollection match = re.Matches(content);

        Console.WriteLine(match.Count);

        for (int i = 0; i < match.Count; i++) {
          HashTags.associate(match[i].ToString(),message);
        }

        _messages.Add(message);
      } catch (InvalidUserName) {
        throw new InvalidUserName();
      }
    }

    static public void reset()
    {
      _messages = new System.Collections.Generic.List<Message>();
    }
  }
}