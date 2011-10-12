using System;
using System.Text.RegularExpressions;

namespace socialnetwork
{
  public class CommandFactory
  {
    private CommandFactory ()
    {
    }
    
    static public Command create(string commandInput)
    {
      // FIXME: usar uma regexp para esxtrair comando e parametros

      // obtenho o comando passado
      string key = "";

      int i = 0;
      for (;i < commandInput.Length && commandInput[i] != ' '; i++) {
        key += commandInput[i];
      }
      
      // o que foi passado, sem o comando, só os dados
      string payload = commandInput.Substring(i).TrimStart();
      
      // TODO: mudar este switch para um mapa chave -> classe
      switch (key) {
        case "criar-usuario":
          return new CreateUserCommand(payload);
        case "listar-mensagens-usuario":
          return new ListUserMessagesCommand(payload);
        case "postar-mensagem":
          return new PostMessageCommand(payload);
        case "listar-seguidores":
          return new ListFollowersCommand(payload);
        case "seguir":
          return new FollowCommand(payload);
        case "listar-seguidos":
          return new ListFollowedCommand(payload);
        case "deixar-de-seguir":
          return new UnfollowCommand(payload);
        case "listar-mensagens-seguidos":
          return new ListFollowedMessages(payload);
      }
      
      // um comando de erro
      return new ErrorCommand(payload);
    }
  }
}

