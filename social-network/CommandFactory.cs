using System;
using System.Text.RegularExpressions;

namespace socialnetwork
{
  static public class CommandFactory
  {
    static public Command create(string commandInput)
    {
      // FIXME: usar uma regexp para extrair comando e parametros

      // obtenho o comando passado
      string key = "";

      int i = 0;
      for (;i < commandInput.Length && commandInput[i] != ' '; i++) {
        key += commandInput[i];
      }
      
      // o que foi passado, sem o comando, só os dados
      string parameter = commandInput.Substring(i).TrimStart();
      
      // TODO: mudar este switch para um mapa chave -> classe
      switch (key) {
        case "criar-usuario":
          return new CreateUserCommand(parameter);
        case "listar-mensagens-usuario":
          return new ListUserMessagesCommand(parameter);
        case "postar-mensagem":
          return new PostMessageCommand(parameter);
        case "listar-seguidores":
          return new ListFollowersCommand(parameter);
        case "seguir":
          return new FollowCommand(parameter);
        case "listar-seguidos":
          return new ListFollowedCommand(parameter);
        case "deixar-de-seguir":
          return new UnfollowCommand(parameter);
        case "listar-mensagens-seguidos":
          return new ListFollowedMessages(parameter);
        case "listar-tendencias":
          return new ListTendenciesCommand();
        case "listar-mensagens-com-palavra-marcada":
          return new ListMessagesWithHashTagCommand(parameter);
        case "listar-estatisticas-usuario":
          return new UserStatsCommand(parameter);
      }
      
      // um comando de erro
      return new ErrorCommand(parameter);
    }
  }
}

