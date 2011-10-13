using System;

namespace socialnetwork
{
  public class ResetCommand: Command
  {
    public ResetCommand()
    {
    }

    public string execute()
    {
      Users.reset();
      return "ok";
    }
  }
}

