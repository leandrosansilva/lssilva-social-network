using System;
using System.Text.RegularExpressions;

namespace socialnetwork
{
  public class UserAlreadExists: Exception 
  {
  }
  
  public class InvalidUserName: Exception
  {
  }

  public class InvalidFollowing: Exception
  {
  }

  public class FollowingAlreadExists: Exception
  {
  }
  public class FollowingDoesNotExists: Exception
  {
  }

  public class UsersAreTheSame: Exception
  {
  }

  public class NotFollowing: Exception
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

    private string _name;

    public string name {
      get {
        return _name;
      }
      set {
        _name = value;
      }
    }

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

  static public class Users
  {
    static private System.Collections.Generic.Dictionary<string,User> _users
    = new System.Collections.Generic.Dictionary<string,User>();

    static public void add(string userName)
    {
      lock(typeof(Users)) {

        // nome do usuário entre 3 e 20 chars, só com letras ou números
        if (!Regex.IsMatch(userName,@"^\w{3,20}$")) {
          throw new InvalidUserName();
        }
        
        try {
          // duplicando o valor nome, na chave e como atributo :-(
          _users.Add(userName,new User() { name = userName });
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
        return user;
      } catch (System.Collections.Generic.KeyNotFoundException) {
        throw new InvalidUserName();
      }
    }

    static public void follow(string userName, string followName) {
      User user = null;
      User following = null;

      try {
        user = _users[userName];
      } catch (System.Collections.Generic.KeyNotFoundException) {
        throw new InvalidUserName();
      } catch (ArgumentNullException) {
        throw new InvalidUserName();
      }

      try {
        following = _users[followName];
      } catch (System.Collections.Generic.KeyNotFoundException) {
        throw new InvalidFollowing();
      } catch (ArgumentNullException) {
        throw new InvalidFollowing();
      }

      // se forem iguais, é erro!
      if (userName == followName) {
        throw new UsersAreTheSame();
      }

      if (user.following.Contains(following)) {
        throw new FollowingAlreadExists();
      }

      user.following.Add(following);
      following.followers.Add(user);
    }

    static public System.Collections.Generic.List<User> getFollowed(string userName)
    {
      try {
        User user = _users[userName];
        return user.following;
      } catch (System.Collections.Generic.KeyNotFoundException) {
        throw new InvalidUserName();
      } catch (ArgumentNullException) {
        throw new InvalidUserName();
      }
    }

    static public System.Collections.Generic.List<User> getFollowers(string userName)
    {
      try {
        User user = _users[userName];
        return user.followers;
      } catch (System.Collections.Generic.KeyNotFoundException) {
        throw new InvalidUserName();
      } catch (ArgumentNullException) {
        throw new InvalidUserName();
      }
    }

    static public void unfollow(string userName, string followName)
    {
      // FIXME: isso daqui é uma cópia do código de follow()!

      User user = null;
      User following = null;

      try {
        user = _users[userName];
      } catch (System.Collections.Generic.KeyNotFoundException) {
        throw new InvalidUserName();
      } catch (ArgumentNullException) {
        throw new InvalidUserName();
      }

      try {
        following = _users[followName];
      } catch (System.Collections.Generic.KeyNotFoundException) {
        throw new InvalidFollowing();
      } catch (ArgumentNullException) {
        throw new InvalidFollowing();
      }

      // se forem iguais, é erro!
      if (userName == followName) {
        throw new UsersAreTheSame();
      }

      if (!user.following.Contains(following)) {
        throw new FollowingDoesNotExists();
      }

      user.following.Remove(following);
      following.followers.Remove(user);
    }
  }
  
}