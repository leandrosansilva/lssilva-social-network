using System;

namespace socialnetwork
{
  public class UserAlreadExists: Exception 
  {
  	
  }
  
  public class InvalidUserName: Exception
  {
  
  }

  public class Users
  {
    static private System.Collections.Generic.Dictionary<string,int> _users 
    = new System.Collections.Generic.Dictionary<string,int>();
    static private int _count = 0;
   
    static public void add(string user)
    {
      lock(typeof(Users)) {
	      if (user.Length > 20 || user.Length < 3) {
	      	throw new InvalidUserName();
	      }
	      
	      try {
	      	_users.Add(user,_count++);
	      } catch (System.ArgumentException) {
	      	throw new UserAlreadExists();
	      }
      }
    }
   
    // FIXME: 
    static public bool find(string user)
    { 
		lock(typeof(Users)) {
			return _users.ContainsKey(user);
		}
    }
  }
  
}

