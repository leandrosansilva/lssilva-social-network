using System;

namespace socialnetwork
{
  public class ErrorCommand: Command
  {
    public ErrorCommand(string command)
    {
    }
    
    public string execute()
    {
      return "commando-invalido";
    }
  }
}

