using System;

namespace socialnetwork
{
  public class CreateUserCommand: Command
  {
    private string _command = "";
    
    public CreateUserCommand(string command)
    {
      _command = command;
    }
    
    public string execute()
    {
      try {
        // adiciona o usuário em minúsculo, para os nomes não serem case-sensitive
        Users.add(_command.ToLower());
        return "ok";
      } catch (UserAlreadExists) {
        return "usuario-ja-existe";
      } catch(InvalidUserName) {
        return "nome-invalido";
      }
      
    }
  }
}

