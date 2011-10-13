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

  public class InvalidFollowed: Exception
  {
  }

  public class FollowedAlreadExists: Exception
  {
  }
  public class FollowedDoesNotExist: Exception
  {
  }

  public class UsersAreTheSame: Exception
  {
  }

  public class NotFollowed: Exception
  {
  }

  // classe que representa as estatísticas de um usuário
  public class UserStats
  {
    private int _followers;
    private int _followed;
    private int _messagesCount;

    public int followers {
      get {
        return _followers;
      }
      set {
        _followers = value;
      }
    }

    public int followed {
      get {
        return _followed;
      }
      set {
        _followed = value;
      }
    }

    public int messagesCount {
      get {
        return _messagesCount;
      }
      set {
        _messagesCount = value;
      }
    }
  }

  public class User
  {
    private System.Collections.Generic.List<Message> _messages
      = new System.Collections.Generic.List<Message>();

    private System.Collections.Generic.List<User> _followers
      = new System.Collections.Generic.List<User>();

    private System.Collections.Generic.List<User> _followed
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

    public System.Collections.Generic.List<User> followed {
      get {
        return _followed;
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
        if (!Regex.IsMatch(userName,@"^\p{L}{3,20}$")) {
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
        System.Collections.Generic.List<Message> list
          = new System.Collections.Generic.List<Message>(_users[userName].messages);
        list.Reverse();
        return list;
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
      User followed = null;

      try {
        user = _users[userName];
      } catch (System.Collections.Generic.KeyNotFoundException) {
        throw new InvalidUserName();
      } catch (ArgumentNullException) {
        throw new InvalidUserName();
      }

      try {
        followed = _users[followName];
      } catch (System.Collections.Generic.KeyNotFoundException) {
        throw new InvalidFollowed();
      } catch (ArgumentNullException) {
        throw new InvalidFollowed();
      }

      // se forem iguais, é erro!
      if (userName == followName) {
        throw new UsersAreTheSame();
      }

      if (user.followed.Contains(followed)) {
        throw new FollowedAlreadExists();
      }

      user.followed.Add(followed);
      followed.followers.Add(user);
    }

    static public System.Collections.Generic.List<User> getFollowed(string userName)
    {
      try {
        User user = _users[userName];
        return user.followed;
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
      User followed = null;

      try {
        user = _users[userName];
      } catch (System.Collections.Generic.KeyNotFoundException) {
        throw new InvalidUserName();
      } catch (ArgumentNullException) {
        throw new InvalidUserName();
      }

      try {
        followed = _users[followName];
      } catch (System.Collections.Generic.KeyNotFoundException) {
        throw new InvalidFollowed();
      } catch (ArgumentNullException) {
        throw new InvalidFollowed();
      }

      // se forem iguais, é erro!
      if (userName == followName) {
        throw new UsersAreTheSame();
      }

      if (!user.followed.Contains(followed)) {
        throw new FollowedDoesNotExist();
      }

      // tudo ok, posso remover 
      user.followed.Remove(followed);
      followed.followers.Remove(user);
    }

    static public UserStats getUserStats(string userName)
    {
      try {
        User user = _users[userName];
        return new UserStats() {
          followed = user.followed.Count,
          followers = user.followers.Count,
          messagesCount = user.messages.Count
        };
      } catch (System.Collections.Generic.KeyNotFoundException) {
        throw new InvalidUserName();
      } catch (ArgumentNullException) {
        throw new InvalidUserName();
      }
    }

    static public System.Collections.Generic.List<Message> getFollowedMessages(string userName)
    {
      System.Collections.Generic.List<Message> messages = new System.Collections.Generic.List<Message>();

      User user = null;

      try {
        user = _users[userName];
      } catch (System.Collections.Generic.KeyNotFoundException) {
        throw new InvalidUserName();
      } catch (ArgumentNullException) {
        throw new InvalidUserName();
      }

      foreach (User follower in user.followed) {
        messages.AddRange(follower.messages);
      }

      // ordena a lista de acordo com a data, mas em ordem inversa
      messages.Sort(delegate(Message message1, Message message2){
        return - message1.created.CompareTo(message2.created);
      });

      return messages;
    }

    static public void reset()
    {
      _users = new System.Collections.Generic.Dictionary<string,User>();
      Messages.reset();
      HashTags.reset();
    }
  }
  
}