using Chat_V2.Areas.Identity.Data;
using Chat_V2.Models;
using Chat_V2.Models.Command;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Chat_V2.Hubs {
	public class GroupsHub : Hub {

		public UserManager<ChatUser> UserManager { get; }
		public ChatContext ChatContext { get; }

		public GroupsHub(UserManager<ChatUser> userManager, ChatContext context) {
			UserManager = userManager;
			ChatContext = context;
		}



	}
}
