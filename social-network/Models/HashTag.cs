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
      message.hashTags.Add(hashTag);

      return hashTag;
    }

    static public List<Message> getMessagesWithHashTag(string v)
    {
      if (v[0] != '#') {
        throw new InvalidHashTag();
      }

      try {
        List<Message> list = new List<Message>(_hashTags[v].messages);
        list.Sort(delegate(Message m1, Message m2){
          return - m1.created.CompareTo(m2.created);
        });
        return list;
      } catch (System.Collections.Generic.KeyNotFoundException){
        throw new HashTagDoesNotExist();
      }
    }

    static public List<HashTag> getMostUsed(int count)
    {
      List<HashTag> list = new List<HashTag>();

      foreach (var h in _hashTags) {
        list.Add(h.Value);
      }

      list.Sort(delegate(HashTag h1, HashTag h2) {
        Int32 r = - h1.messages.Count.CompareTo(h2.messages.Count);

        if (r != 0) {
          return r;
        }

        // caso as duas hashtags tenham o mesmo número de mensagens,
        // devo comparar pela ordem alfabética
        return h1.hash.CompareTo(h2.hash);
      });

      try {
        // pega os count primeiros
        return list.GetRange(0,count);
      } catch (ArgumentException) {
        // ou, caso exista menos hashs, a lista inteira
        return list;
      }
    }

    static public void reset()
    {
      _hashTags = new Dictionary<string,HashTag>();
    }
  }
}

