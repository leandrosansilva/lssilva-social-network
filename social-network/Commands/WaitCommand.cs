using System;

namespace socialnetwork
{
  public class WaitCommand: Command
  {
    public WaitCommand ()
    {
    }

    public string execute ()
    {
      Console.WriteLine("antes de dormir");
      System.Threading.Thread.Sleep(10000);
      Console.WriteLine("depois de dormir");
      return "lala";
    }
  }
}

