using System;
using System.Collections.Generic;

namespace socialnetwork
{
  public class InvalidHashTag: Exception
  {
  }
  public class HashTagDoesNotExist: Exception
  {
  }

  public class HashTag
  {
    private List<Message> _messages = new List<Message>();
    private string _hash;

    public string hash
    {
      get {
        return _hash;
      }

      set {
        _hash = value;
      }
    }

    public List<Message> messages
    {
      get {
        return _messages;
      }
    }

    public HashTag()
    {
    }
  }

  static public class HashTags
  {
    static private Dictionary<string,HashTag> _hashTags = new Dictionary<string,HashTag>();

    static private HashTag getHash(string v)
    {
      try {
        return _hashTags[v];
      } catch (System.Collections.Generic.KeyNotFoundException){
        HashTag hashTag = new HashTag() { hash = v };
        _hashTags[v] = hashTag;
        return hashTag;
      }
    }

    static public HashTag associate(string v, Message message) {
      HashTag hashTag = getHash(v);

      Console.Write(v);
      Console.Write(" ");
      Console.WriteLine(message.content);

      hashTag.messages.Add(message);
      return hashTag;
    }

    static public List<Message> getMessagesWithHashTag(string v)
    {
      if (v[0] != '#') {
        throw new InvalidHashTag();
      }

      try {
        return _hashTags[v].messages;
      } catch (System.Collections.Generic.KeyNotFoundException){
        throw new HashTagDoesNotExist();
      }
    }

    static public List<HashTag> getMostUsed(int count)
    {
      return null;
    }
  }
}

