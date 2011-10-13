using System;
using System.Collections.Generic;

namespace socialnetwork
{
  public class ListMessagesWithHashTagCommand: Command
  {
    private string _hash;
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

