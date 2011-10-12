using System;

namespace socialnetwork
{
  public class UserAlreadExists: Exception 
  {
    
  }
  
  public class InvalidUserName: Exception
  {
  
  }

  public class User
  {
    private System.Collections.Generic.List<Message> _messages
      = new System.Collections.Generic.List<Message>();

    private System.Collections.Generic.List<User> _followers
      = new System.Collections.Generic.List<User>();

    private System.Collections.Generic.List<User> _following
      = new System.Collections.Generic.List<User>();

    public System.Collections.Generic.List<Message> messages {
      get {
        return _messages;
      }
    }

    public System.Collections.Generic.List<User> followers {
      get {
        return _followers;
      }
    }

    public System.Collections.Generic.List<User> following {
      get {
        return _following;
      }
    }

    public void addMessage(Message message)
    {
      _messages.Add(message);
    }

    public User()
    {
    }
  }

  public class Users
  {
    static private System.Collections.Generic.Dictionary<string,User> _users
    = new System.Collections.Generic.Dictionary<string,User>();

    static public void add(string userName)
    {
      lock(typeof(Users)) {
        if (userName.Length > 20 || userName.Length < 3) {
          throw new InvalidUserName();
        }
        
        try {
          _users.Add(userName,new User());
        } catch (System.ArgumentException) {
          throw new UserAlreadExists();
        }
      }
    }

    static public System.Collections.Generic.List<Message> getMessages(string userName) {
      try {
        return _users[userName].messages;
      } catch (System.Collections.Generic.KeyNotFoundException) {
        throw new InvalidUserName();
      }
    }

    static public User addMessage(string userName, Message message)
    {
      User user = null;
      try {
        user = _users[userName];
        user.addMessage(message);
      } catch (System.Collections.Generic.KeyNotFoundException) {
        throw new InvalidUserName();
      }
      return user;
    }

  }
  
}

