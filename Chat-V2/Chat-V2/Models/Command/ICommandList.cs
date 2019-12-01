using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chat_V2.Models.Command {
	[Obsolete]
	public interface ICommandList {
		bool DoesCommandExist(string name);
		ICommand GetCommandByName(string name);
		IEnumerable<ICommand> GetCommandsAtRank(int rank);
	}
}
