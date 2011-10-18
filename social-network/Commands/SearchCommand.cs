using System;
using System.Collections.Generic;

namespace socialnetwork
{
  public class SearchCommand: Command
  {
    private string _pattern = "";
    public SearchCommand(string pattern)
    {
      _pattern = pattern;
    }

    public string execute ()
    {
      try {
        List<Message> messages = Messages.resultsOfSearch(_pattern);
        string output = "";
        foreach(Message message in messages) {
          output += message.user.name + " " + message.content + "\n";
        }
        return output;
      } catch (PatternNotFound) {
        return "padrao-nao-encontrado";
      }
    }
  }
}

