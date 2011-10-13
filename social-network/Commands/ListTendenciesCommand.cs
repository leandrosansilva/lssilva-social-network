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

      return "";
    }
  }
}

