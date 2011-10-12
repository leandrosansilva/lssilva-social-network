using System;

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

    private string _content = null;

    private User _user = null;

    private System.Collections.Generic.List<HashTag> _hashTags;

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

      Message message = new Message() { content = content };

      try {
        User user = Users.addMessage(userName,message);
        message.user = user;
        _messages.Add(message);
      } catch (InvalidUserName) {
        throw new InvalidUserName();
      }
    }
  }
}

