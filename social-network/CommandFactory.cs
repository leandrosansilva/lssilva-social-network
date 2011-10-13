using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace socialnetwork
{
  public delegate Command CommandDelegate(string parameter);

  static public class CommandFactory
  {
    static private Dictionary<string,CommandDelegate> _commandDelegate
      = new Dictionary<string,CommandDelegate>() {
      {"criar-usuario",delegate(string parameter) {
        return new CreateUserCommand(parameter);
      }},
      {"listar-mensagens-usuario",delegate(string parameter) {
          return new ListUserMessagesCommand(parameter);
      }},
      {"postar-mensagem",delegate(string parameter) {
        return new PostMessageCommand(parameter);
      }},
      {"listar-seguidores",delegate(string parameter) {
        return new ListFollowersCommand(parameter);
      }},
      {"seguir",delegate(string parameter) {
        return new FollowCommand(parameter);
      }},
      {"listar-seguidos",delegate(string parameter) {
        return new ListFollowedCommand(parameter);
      }},
      {"deixar-de-seguir",delegate(string parameter) {
        return new UnfollowCommand(parameter);
      }},
      {"listar-mensagens-seguidos",delegate(string parameter) {
        return new ListFollowedMessagesCommand(parameter);
      }},
      {"listar-tendencia",delegate(string parameter) {
        return new ListTendenciesCommand();
      }},
      {"listar-mensagens-com-palavra-marcada",delegate(string parameter) {
        return new ListMessagesWithHashTagCommand(parameter);
      }},
      {"listar-estatisticas-usuario",delegate(string parameter) {
        return new UserStatsCommand(parameter);
      }},
      {"resetar",delegate(string parameter) {
        return new ResetCommand();
      }}
    };

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
      string parameter = commandInput.Substring(i).Trim();
      
      try {
        return _commandDelegate[key](parameter);
      } catch (KeyNotFoundException) {
        // um comando de erro
        return new ErrorCommand(parameter);
      }
    }
  }
}

