using System;

namespace socialnetwork
{
  public class StopFollowingCommand: Command
  {
    public StopFollowingCommand(string command)
    {
    }
    public string execute()
    {
      return "ok";
    }
  }
}

