using System;

namespace socialnetwork
{
  class MainClass
  {
    public static void Main (string[] args)
    {
      Executor.doSomething("Jóia");
      Server server = new Server(1234);
      server.run();
    }
  }
}

