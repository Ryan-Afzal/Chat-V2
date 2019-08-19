#pragma checksum "C:\Users\ryana\Documents\GitHub\Chat-V2\Chat-V2\Chat-V2\Pages\Chat.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "e4f23c11b8680049ae5113518ac6165131c16f96"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(Chat_V2.Pages.Pages_Chat), @"mvc.1.0.razor-page", @"/Pages/Chat.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.RazorPages.Infrastructure.RazorPageAttribute(@"/Pages/Chat.cshtml", typeof(Chat_V2.Pages.Pages_Chat), null)]
namespace Chat_V2.Pages
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#line 1 "C:\Users\ryana\Documents\GitHub\Chat-V2\Chat-V2\Chat-V2\Pages\_ViewImports.cshtml"
using Chat_V2;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"e4f23c11b8680049ae5113518ac6165131c16f96", @"/Pages/Chat.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"54bebb9c9bbee8d51a45538557928179fb546f89", @"/Pages/_ViewImports.cshtml")]
    public class Pages_Chat : global::Microsoft.AspNetCore.Mvc.RazorPages.Page
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/lib/signalr/dist/browser/signalr.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#line 3 "C:\Users\ryana\Documents\GitHub\Chat-V2\Chat-V2\Chat-V2\Pages\Chat.cshtml"
  
    ViewData["Title"] = "Chat";
    Layout = "~/Pages/Shared/_Layout.cshtml";

#line default
#line hidden
            BeginContext(126, 715, true);
            WriteLiteral(@"
<div class=""container"">
    <div class=""row"">&nbsp;</div>
    <div class=""row"">
        <div class=""col-6"">&nbsp;</div>
        <div id=""messages-list"" class=""col-6"" style=""overflow-y: scroll"">
            <div class=""message"">
                <span class=""message-header"">&lt;Example Header&gt;: </span>
                <span class=""message-content"">Example Message</span>
            </div>
        </div>
    </div>
    <div class=""row"">
        <div class=""col-12"">
            <hr />
        </div>
    </div>
    <div class=""row"">
        <div class=""col-6"">&nbsp;</div>
        <div class=""col-6"">
            <input type=""text"" id=""messageInput"" />
        </div>
    </div>
</div>
");
            EndContext();
            BeginContext(841, 61, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "e4f23c11b8680049ae5113518ac6165131c16f964348", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(902, 1233, true);
            WriteLiteral(@"
<script type=""text/javascript"">
    ""use strict"";

    //SignalR connection
    const connection = new signalR.HubConnectionBuilder()
        .withUrl(""/chatHub"")
        .configureLogging(signalR.LogLevel.Information)
        .build();
    
    //What to do when the connection is established
    connection.start().then(function () {
        document.getElementById(""sendButton"").disabled = false;
        console.log(""connected"");
    }).catch(function (err) {
        return console.error(err.toString());
    });

    //Disable send button until connection is established
    document.getElementById(""sendButton"").disabled = true;

    //Recieve Message
	connection.on(""ReceiveMessage"", function (username, message) {
        var msg = message.replace(/&/g, ""&amp;"").replace(/</g, ""&lt;"").replace(/>/g, ""&gt;"");
        var encodedMsg = ""<"" + username + "">: "" + msg;
        var li = document.createElement(""span"");
        li.textContent = encodedMsg;
        document.getElementById(""messa");
            WriteLiteral("gesList\").appendChild(li);\r\n    });\r\n\r\n    //Send Message\r\n    document.getElementById(\"messageInput\").addEventListener(\"keyup\", function (event) {\r\n        if (event.keyCode == 13) {\r\n            var user = \'");
            EndContext();
            BeginContext(2137, 27, false);
#line 64 "C:\Users\ryana\Documents\GitHub\Chat-V2\Chat-V2\Chat-V2\Pages\Chat.cshtml"
                    Write(Model.ViewModel.ChatUser.Id);

#line default
#line hidden
            EndContext();
            BeginContext(2165, 307, true);
            WriteLiteral(@"';
            var message = document.getElementById(""messageInput"").value;
            connection.invoke(""SendMessageToAll"", user, message).catch(function (err) {
                return console.error(err.toString());
            });
            event.preventDefault();
        }
    });
</script>
");
            EndContext();
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<Chat_V2.Pages.ChatModel> Html { get; private set; }
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary<Chat_V2.Pages.ChatModel> ViewData => (global::Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary<Chat_V2.Pages.ChatModel>)PageContext?.ViewData;
        public Chat_V2.Pages.ChatModel Model => ViewData.Model;
    }
}
#pragma warning restore 1591
