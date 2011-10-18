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
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace socialnetwork
{
  public delegate Command CommandDelegate(string parameter);

  class CommunicationError: Exception
  {
  }

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
      }},
      {"wait",delegate(string parameter) {
        return new WaitCommand();
      }},
      {"buscar",delegate(string parameter) {
        return new SearchCommand(parameter);
      }},
      {"listar-mencoes",delegate(string parameter) {
        return new ListMentionsCommand(parameter);
      }}
    };

    static public Command create(string commandInput)
    {
      // FIXME: usar uma regexp para extrair comando e parametros

      // obtenho o comando passado
      string key = "";
      // o que foi passado, sem o comando, só os dados
      string parameter = null;

      //try {
        int i = 0;
        for (;i < commandInput.Length && commandInput[i] != ' '; i++) {
          key += commandInput[i];
        }
      //}
      
      parameter = commandInput.Substring(i).Trim();
      
      try {
        return _commandDelegate[key](parameter);
      } catch (KeyNotFoundException) {
        // um comando de erro
        return new ErrorCommand(parameter);
      }
    }
  }
}

