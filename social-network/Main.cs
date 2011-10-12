using System;

namespace socialnetwork
{
  class MainClass
  {
    public static void Main (string[] args)
    {
      Server server = new Server(1234);
      server.run();
    }
  }
}

