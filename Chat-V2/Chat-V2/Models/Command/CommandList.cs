using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chat_V2.Models.Command {
	public class CommandList : ICommandList {

		private readonly IDictionary<string, ICommand> commandsByName;

		public CommandList() {
			
		}

		public ICommand GetCommandByName(string name) {
			return this.commandsByName[name];
		}

		public IEnumerable<ICommand> GetCommandsAtRank(int rank) {
			throw new NotImplementedException();
		}
	}
}
