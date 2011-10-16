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
using System.Collections.Generic;

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
    private List<Message> _messages
      = new List<Message>();

    private List<User> _followers
      = new List<User>();

    private List<User> _followed
      = new List<User>();

    private string _name;

    public string name {
      get {
        return _name;
      }
      set {
        _name = value;
      }
    }

    public List<Message> messages {
      get {
        return _messages;
      }
    }

    public List<User> followers {
      get {
        return _followers;
      }
    }

    public List<User> followed {
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
    static private Dictionary<string,User> _users
    = new Dictionary<string,User>();

    static public void add(string userName)
    {
      // nome do usuário entre 3 e 20 chars, só com letras ou números
      if (!Regex.IsMatch(userName,@"^\p{L}{3,20}$")) {
        throw new InvalidUserName();
      }

      try {
        lock (typeof(Users)) {
          // duplicando o valor nome, na chave e como atributo :-(
          _users.Add(userName,new User() { name = userName });
        }
      } catch (System.ArgumentException) {
        throw new UserAlreadExists();
      }
    }

    static public List<Message> getMessages(string userName) {
      try {
        List<Message> list = new List<Message>(_users[userName].messages);
        list.Reverse();
        return list;
      } catch (KeyNotFoundException) {
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
      } catch (KeyNotFoundException) {
        throw new InvalidUserName();
      }
    }

    static public void follow(string userName, string followName) {
      User user = null;
      User followed = null;

      try {
        user = _users[userName];
      } catch (KeyNotFoundException) {
        throw new InvalidUserName();
      } catch (ArgumentNullException) {
        throw new InvalidUserName();
      }

      try {
        followed = _users[followName];
      } catch (KeyNotFoundException) {
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

    static public List<User> getFollowed(string userName)
    {
      try {
        User user = _users[userName];
        List<User> list = new List<User>(user.followed);

        list.Reverse();

        return list;
      } catch (KeyNotFoundException) {
        throw new InvalidUserName();
      } catch (ArgumentNullException) {
        throw new InvalidUserName();
      }
    }

    static public List<User> getFollowers(string userName)
    {
      try {
        User user = _users[userName];

        List<User> list = new List<User>(user.followers);

        list.Reverse();

        return list;
      } catch (KeyNotFoundException) {
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
      } catch (KeyNotFoundException) {
        throw new InvalidUserName();
      } catch (ArgumentNullException) {
        throw new InvalidUserName();
      }

      try {
        followed = _users[followName];
      } catch (KeyNotFoundException) {
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
      } catch (KeyNotFoundException) {
        throw new InvalidUserName();
      } catch (ArgumentNullException) {
        throw new InvalidUserName();
      }
    }

    static public List<Message> getFollowedMessages(string userName)
    {
      List<Message> messages = new List<Message>();

      User user = null;

      try {
        user = _users[userName];
      } catch (KeyNotFoundException) {
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
      _users = new Dictionary<string,User>();
      Messages.reset();
      HashTags.reset();
    }
  }

}