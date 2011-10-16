/*
  * This file is part of lssilva-social-network.
  *
  * lssilva-social-network is free software: you can redistribute it and/or modify
  * it under the terms of the GNU General Public License as published by
  * the Free Software Foundation, either version 3 of the License, or
  * (at your option) any later version.
  *
  * lssilva-social-network is distributed in the hope that it will be useful,
  * but WITHOUT ANY WARRANTY; without even the implied warranty of
  * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
  * GNU General Public License for more details.
  *
  * You should have received a copy of the GNU General Public License
  * along with lssilva-social-network.  If not, see <http://www.gnu.org/licenses/>.
*/

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

