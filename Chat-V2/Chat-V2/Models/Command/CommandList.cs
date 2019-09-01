﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chat_V2.Models.Command {
	public class CommandList : ICommandList {

		private readonly IDictionary<string, ICommand> commandsByName;

		public CommandList() {
			this.commandsByName = new Dictionary<string, ICommand>();

			this.RegisterCommands();
		}

		private void RegisterCommands() {
			// Add commands here.
		}

		private void RegisterCommand(ICommand command) {
			this.commandsByName.Add(command.Name, command);
		}

		public bool DoesCommandExist(string name) {
			return this.commandsByName.ContainsKey(name);
		}

		public ICommand GetCommandByName(string name) {
			return this.commandsByName[name];
		}

		public IEnumerable<ICommand> GetCommandsAtRank(int rank) {
			return this.commandsByName.Values.Where(c => c.MinRank <= rank);
		}
	}
}
