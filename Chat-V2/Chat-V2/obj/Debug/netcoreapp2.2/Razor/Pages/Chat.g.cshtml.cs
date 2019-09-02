#pragma checksum "C:\Users\ryana\Documents\GitHub\Chat-V2\Chat-V2\Chat-V2\Pages\Chat.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "006e91f99d744703f952e0ff43e85cd65ff1a4b7"
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"006e91f99d744703f952e0ff43e85cd65ff1a4b7", @"/Pages/Chat.cshtml")]
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
            BeginContext(126, 2763, true);
            WriteLiteral(@"
<div class=""container"">
    <div class=""row"">&nbsp;</div>
    <div class=""row"">
        <div class=""col-6"">
            <div id=""messages-list"" class=""console-output"">

            </div>

            <br />

            <input type=""text"" id=""message-input"" class=""console-input"" placeholder=""Enter message here"" />
        </div>

        <div class=""col-1""></div>

        <div id=""data-pane"" class=""col-5"">
            <div id=""command-output-list"" class=""console-output"">

            </div>

            <br />

            <input type=""text"" id=""command-input"" class=""console-input"" placeholder=""Enter commands here. Type 'help' for a list of commands."" />

            <hr />

            <div id=""info-pane"" class=""container"">
                <div class=""row"" style=""height: 100%;"">
                    <div class=""col-6"">

                    </div>
                    <div class=""col-6"">
                        <div id=""online-members-list"">
                            <div c");
            WriteLiteral(@"lass=""user-data-container"">
                                <div class=""user-data-name"">Sample User Name</div>
                                <div class=""user-data-rank"">&nbsp;&nbsp;&nbsp;&nbsp;Rank</div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
<style>

    .message {

    }

    .message-header {
        font-family: Consolas;
        font-size: medium;
    }

    .message-content {
        font-family: Consolas;
        font-size: small;
        color: #000000;
    }

    .user-data-container {
        display: block;
        width: 100%;
    }

    .user-data-container:hover {
        border: solid 1px #AAAAAA;
    }

    .user-data-name {
        width: 100%;
    }

    .user-data-rank {
        width: 100%;
    }

    /* The output area for the message terminal */
    #messages-list {
        height: 750px;
    }

    /* The inp");
            WriteLiteral(@"ut area for the message terminal */
    #message-input {

    }

    /* The output area for the command terminal */
    #command-output-list {
        height: 200px;
    }

    /* The input area for the command terminal */
    #command-input {
        
    }

    /* The panel containing the command terminal and info pane */
    #data-pane {
        border: solid 5px #00FFBB;
        border-radius: 4px 4px;
        width: 100%;
    }

    /* The panel containing group data */
    #info-pane {
        width: 100%;
    }

    /* The list of online group members */
    #online-members-list {
        overflow: scroll;
        width: 100%;
        height: 100%;
    }

</style>
");
            EndContext();
            BeginContext(2889, 61, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "006e91f99d744703f952e0ff43e85cd65ff1a4b76486", async() => {
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
            BeginContext(2950, 125, true);
            WriteLiteral("\r\n<script type=\"text/javascript\">\r\n    \"use strict\";\r\n\r\n    //Get ViewModel data\r\n    const viewmodel = {\r\n        GroupID: \'");
            EndContext();
            BeginContext(3076, 34, false);
#line 132 "C:\Users\ryana\Documents\GitHub\Chat-V2\Chat-V2\Chat-V2\Pages\Chat.cshtml"
             Write(Model.ViewModel.Membership.GroupID);

#line default
#line hidden
            EndContext();
            BeginContext(3110, 21, true);
            WriteLiteral("\',\r\n        UserID: \'");
            EndContext();
            BeginContext(3132, 37, false);
#line 133 "C:\Users\ryana\Documents\GitHub\Chat-V2\Chat-V2\Chat-V2\Pages\Chat.cshtml"
            Write(Model.ViewModel.Membership.ChatUserID);

#line default
#line hidden
            EndContext();
            BeginContext(3169, 19, true);
            WriteLiteral("\',\r\n        Rank: \'");
            EndContext();
            BeginContext(3189, 31, false);
#line 134 "C:\Users\ryana\Documents\GitHub\Chat-V2\Chat-V2\Chat-V2\Pages\Chat.cshtml"
          Write(Model.ViewModel.Membership.Rank);

#line default
#line hidden
            EndContext();
            BeginContext(3220, 5615, true);
            WriteLiteral(@"'
    };

    function appendMessage(senderName, senderRankColor, message) {
        var head = ""<"" + senderName + "">: "";
        var msg = message.replace(/&/g, ""&amp;"").replace(/</g, ""&lt;"").replace(/>/g, ""&gt;"");

        var container = document.createElement(""div"");
        container.setAttribute(""class"", ""message"");

        var header = document.createElement(""span"");
        header.setAttribute(""class"", ""message-header"");
        header.setAttribute(""style"", ""color: #"" + senderRankColor + "";"");
        header.textContent = head;
        container.appendChild(header);

        var content = document.createElement(""span"");
        content.setAttribute(""class"", ""message-content"");
        content.setAttribute(""style"", ""color: #"" + senderRankColor + "";"");
        content.textContent = msg;
        container.appendChild(content);

        var messagesList = document.getElementById(""messages-list"");

        messagesList.appendChild(container);
        messagesList.scrollTop = messa");
            WriteLiteral(@"gesList.scrollHeight;
    }

    function appendCommandMessage(color, message) {
        var head = ""$ "";
        var msg = message;

        var container = document.createElement(""div"");
        container.setAttribute(""class"", ""message"");

        var header = document.createElement(""span"");
        header.setAttribute(""class"", ""message-header"");
        header.setAttribute(""style"", ""color: #"" + color + "";"");
        header.textContent = head;
        container.appendChild(header);

        var content = document.createElement(""span"");
        content.setAttribute(""class"", ""message-content"");
        content.setAttribute(""style"", ""color: #"" + color + "";"");
        content.textContent = msg;
        container.appendChild(content);

        var messagesList = document.getElementById(""command-output-list"");

        messagesList.appendChild(container);
        messagesList.scrollTop = messagesList.scrollHeight;
    }

    function userConnected(userId, userName, userRankName) {
   ");
            WriteLiteral(@"     
    }

    function userDisconnected(userId) {
        document.getElementById(""online-members-list"")
            .removeChild(document.getElementById(""user-"" + userId));
    }

    //Start SignalR connection
    const connection = new signalR.HubConnectionBuilder()
        .withUrl(""/chatHub"")
        .configureLogging(signalR.LogLevel.Information)
        .build();
    
    //On connection established
    connection.start().then(function () {
        console.log(""connected"");

        var args = {
            GroupID: viewmodel.GroupID,
            SenderID: viewmodel.UserID
        };

        //Send the connection event
        connection.invoke(""ClientConnected"", args);

        //Load the last 50 messages
        connection.invoke(""GetPreviousMessages"", viewmodel.GroupID, viewmodel.Rank, 0, 50);
    }).catch(function (err) {
        return console.error(err.toString());
    });

    window.addEventListener('unload', (event) => {
        var args = {
            Gr");
            WriteLiteral(@"oupID: viewmodel.GroupID,
            SenderID: viewmodel.UserID
        };

        //Send the disconnection event
        connection.invoke(""ClientDisconnected"", args);
    });

    connection.on(""ReceivePreviousMessages"", function (messages) {
        $.each(messages, function () {
            var args = this;
            appendMessage(args.senderName, args.senderRankColor, args.message);
        });
    });

    //Receive Message
	connection.on(""ReceiveMessage"", function (args) {
        appendMessage(args.senderName, args.senderRankColor, args.message);
    });

    //Send Message
    document.getElementById(""message-input"").addEventListener(""keyup"", function (event) {
        if (event.keyCode == 13) {
            var element = document.getElementById(""message-input"");
            var message = element.value;

            var args = {
                GroupID: viewmodel.GroupID,
                SenderID: viewmodel.UserID,
                SenderRank: viewmodel.Rank,
         ");
            WriteLiteral(@"       MinRank: viewmodel.Rank,
                Message: message
            };
            connection.invoke(""SendMessage"", args).catch(function (err) {
                return console.error(err.toString());
            });

            element.value = """";

            event.preventDefault();
        }
    });

    //Receive Command-Related Message
    connection.on(""ReceiveCommandMessage"", function (args) {
        appendCommandMessage(args.color, args.message);
    });

    //Send Command
    document.getElementById(""command-input"").addEventListener(""keyup"", function (event) {
        if (event.keyCode == 13) {
            var element = document.getElementById(""command-input"");
            var text = element.value;

            var args = {
                GroupID: viewmodel.GroupID,
                SenderID: viewmodel.UserID,
                SenderRank: viewmodel.Rank,
                Text: text
            };
            connection.invoke(""ProcessCommand"", args).catch(functio");
            WriteLiteral(@"n (err) {
                return console.error(err.toString());
            });

            element.value = """";

            event.preventDefault();
        }
    });

    connection.on(""UserConnected"", function (args) {
        if (args.userId != viewmodel.UserID) {
            userConnected(args.userId, args.userName, args.userRankName);
        }
    });

    connection.on(""UserDisconnected"", function (args) {
        userDisconnected(args.userId);
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
