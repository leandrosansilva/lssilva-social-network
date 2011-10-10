using System;

namespace socialnetwork
{
	public interface Command
	{
		int execute();
		string output(); 
	}
}

