using System;

namespace socialnetwork
{
	public class Message
	{
		private string _user = null;
		private string _content = null;
		public Message (string user,string content)
		{
			_user = user;
			_content = content;
		}
	}
	
	public class InvalidMessage: Exception
	{
	
	}
	
	public class InvalidMessageUser: Exception
	{
	
	}
	
	static public class Messages
	{
		static private Message[] _messages;
		
		static void add(Message message) 
		{
		
		}
	}
}

