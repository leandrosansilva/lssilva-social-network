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

    private string _content = null;

    private User _user = null;

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

        Regex re = new Regex(@"(#\w+)");

        MatchCollection match = re.Matches(content);

        Console.WriteLine(match.Count);

        for (int i = 0; i < match.Count; i++) {
          HashTag hashTag = HashTags.associate(match[i].ToString(),message);

          Console.WriteLine(match[i].ToString());

          Console.WriteLine(hashTag.hash);

          message.hashTags.Add(hashTag);
        }

        _messages.Add(message);
      } catch (InvalidUserName) {
        throw new InvalidUserName();
      }
    }
  }
}