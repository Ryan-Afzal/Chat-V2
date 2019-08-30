using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Chat_V2.Pages {
	public class IndexModel : PageModel {
		public void OnGet() {

		}

		public async Task<IActionResult> OnPostAsync() {
			return new LocalRedirectResult("/chat?groupId=3");
		}
	}
}
