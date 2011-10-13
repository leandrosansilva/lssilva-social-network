using System;

namespace socialnetwork
{
  public class ListTendenciesCommand: Command
  {
    public ListTendenciesCommand ()
    {
    }

    public string execute()
    {
      System.Collections.Generic.List<HashTag> hashTags = HashTags.getMostUsed(5);

      string output = "";

      foreach(HashTag hashTag in hashTags) {
        output += hashTag.hash + "\n";
      }

      return output;
    }
  }
}

