#pragma checksum "C:\Users\s-afzalr\Documents\GitHub\Chat-V2\Chat-V2\Chat-V2\Pages\Chat.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "e3fd0105e3e3103b655408cbc2da35b63b21e316"
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
#line 1 "C:\Users\s-afzalr\Documents\GitHub\Chat-V2\Chat-V2\Chat-V2\Pages\_ViewImports.cshtml"
using Chat_V2;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"e3fd0105e3e3103b655408cbc2da35b63b21e316", @"/Pages/Chat.cshtml")]
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
#line 3 "C:\Users\s-afzalr\Documents\GitHub\Chat-V2\Chat-V2\Chat-V2\Pages\Chat.cshtml"
  
    ViewData["Title"] = "Chat";
    Layout = "~/Pages/Shared/_Layout.cshtml";

#line default
#line hidden
            BeginContext(126, 3323, true);
            WriteLiteral(@"
<div class=""container-fluid"" style=""overflow: auto;"">
    <div class=""row"">&nbsp;</div>
    <div class=""row"">
        <div class=""col-6"">
            <div id=""messages-list"" class=""console-output"">

            </div>

            <br />

            <input type=""text"" id=""message-input"" class=""console-input form-control"" placeholder=""Enter message here"" />
        </div>

        <div class=""col-1""></div>

        <div id=""data-pane"" class=""col-5"">
            <div id=""command-output-list"" class=""console-output h-25"">

            </div>

            <br />

            <input type=""text"" id=""command-input"" class=""console-input form-control"" placeholder=""Enter commands here. Type 'help' for a list of commands."" />

            <hr />

            <div id=""info-pane"" class=""container-fluid"">
                <div class=""row"">
                    <div class=""col-6"">
                        <table class=""table h-100"">
                            <thead>
                          ");
            WriteLiteral(@"      <tr>
                                    <th scope=""col"">ID</th>
                                    <th scope=""col"">NAME</th>
                                    <!--<th scope=""col"">ONLINE</th>-->
                                </tr>
                            </thead>
                            <tbody id=""groups-list"">

                            </tbody>
                        </table>
                    </div>
                    <div class=""col-6"">
                        <ul id=""online-members-list"" class=""list-group h-100"">

                        </ul>
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
        width: 100%;");
            WriteLiteral(@"
    }

    .user-data-container-inactive {
        color: #BBBBBB;
    }

    .user-data-container:hover {
        border: solid 1px #BBBBBB;
    }

    .user-data-name {
        width: 100%;
    }

    .user-data-rank {
        width: 100%;
    }

    .group-data-container {
        
    }

    .group-data-container:hover {
        border: solid 1px #AAAAAA;
    }

    .group-data-name {
        
    }

    /* The output area for the message terminal */
    #messages-list {
        height: 75vh;
    }

    /* The input area for the message terminal */
    #message-input {

    }

    /* The output area for the command terminal */
    #command-output-list {
        margin-top: 15px;
    }

    /* The input area for the command terminal */
    #command-input {
        
    }

    /* The panel containing the command terminal and info pane */
    #data-pane {
        border: solid 5px #00FFBB;
        border-radius: 10px 50px;
        height: 75vh;
    }
");
            WriteLiteral("\n    /* The panel containing group data */\r\n    #info-pane {\r\n        \r\n    }\r\n\r\n    /* The list of online group members */\r\n    #online-members-list {\r\n        overflow: auto;\r\n    }\r\n\r\n    #groups-list {\r\n        overflow: auto;\r\n    }\r\n\r\n</style>\r\n");
            EndContext();
            BeginContext(3449, 61, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "e3fd0105e3e3103b655408cbc2da35b63b21e3167123", async() => {
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
            BeginContext(3510, 228, true);
            WriteLiteral("\r\n<script type=\"text/javascript\">\r\n    \"use strict\";\r\n\r\n    //Get the sound file\r\n    var sound = new Audio(\'new-message.mp3\');\r\n    var play_sound = false;\r\n\r\n    //Get ViewModel data\r\n    const viewmodel = {\r\n        UserID: \'");
            EndContext();
            BeginContext(3739, 27, false);
#line 162 "C:\Users\s-afzalr\Documents\GitHub\Chat-V2\Chat-V2\Chat-V2\Pages\Chat.cshtml"
            Write(Model.ViewModel.ChatUser.Id);

#line default
#line hidden
            EndContext();
            BeginContext(3766, 13037, true);
            WriteLiteral(@"'
    };

    //The number of messages currently displayed. Used for getting previous messages.
    var numMessages = 0;

    var currentGroupID = -1;
    var membershipID = -1;

    function appendMessage(senderName, senderRankColor, message, timestamp) {
        var head = ""["" + timestamp + ""] "" + senderName + "": "";
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
        content.textContent = m");
            WriteLiteral(@"sg;
        container.appendChild(content);

        var messagesList = document.getElementById(""messages-list"");

        messagesList.appendChild(container);
        messagesList.scrollTop = messagesList.scrollHeight;
    }

    function prependMessage(senderName, senderRankColor, message, timestamp) {
        var head = ""["" + timestamp + ""] "" + senderName + "": "";
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
        content.setAttribute(""style"", ""color: #"" + senderR");
            WriteLiteral(@"ankColor + "";"");
        content.textContent = msg;
        container.appendChild(content);

        var messagesList = document.getElementById(""messages-list"");

        messagesList.prepend(container);
    }

    function clearMessages() {
        $(""#messages-list"").empty();
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
        container.appendChild(conte");
            WriteLiteral(@"nt);

        var messagesList = document.getElementById(""command-output-list"");

        messagesList.appendChild(container);
        messagesList.scrollTop = messagesList.scrollHeight;
    }

    function userConnected(userId, userName, userRankName) {
        var container = document.createElement(""li"");
        container.setAttribute(""id"", ""user-"" + userId);
        container.setAttribute(""class"", ""list-group-item list-group-item-primary user-data-container"");

        var name = document.createElement(""div"");
        name.setAttribute(""class"", ""user-data-name"");
        name.textContent = userName;
        container.appendChild(name);

        var rank = document.createElement(""div"");
        rank.setAttribute(""class"", ""user-data-rank"");
        rank.textContent = ""    "" + userRankName;
        container.appendChild(rank);

        document.getElementById(""online-members-list"").appendChild(container);
    }

    function userDisconnected(userId) {
        document.getElementByI");
            WriteLiteral(@"d(""online-members-list"")
            .removeChild(document.getElementById(""user-"" + userId));
    }

    function userActive(userId) {
        document.getElementById(""user-"" + userId)
            .setAttribute(""class"", ""list-group-item list-group-item-secondary user-data-container"");
    }

    function userInactive(userId) {
        document.getElementById(""user-"" + userId)
            .setAttribute(""class"", ""list-group-item list-group-item-primary user-data-container user-data-container-inactive"");
    }

    function clearUsers() {
        $(""#online-members-list"").empty();
    }

    //Clears messages, clears users, disconnects from the current group, and resets data.
    function clear() {
        clearMessages();
        clearUsers();

        if (membershipID != -1) {
            var args = {
            MembershipID: membershipID
        };

        connection.invoke(""DisconnectedFromGroup"", args);
        }

        numMessages = 0;
        currentGroupID = -1;
     ");
            WriteLiteral(@"   membershipID = -1;
    }

    function newGroupMessage(groupId) {
        //Signal new Group Message
    }

    function switchGroupTo(groupId, membershipId) {
        clear();

        currentGroupID = groupId;
        membershipID = membershipId;

        var args = {
            MembershipID: membershipID
        };

        connection.invoke(""ConnectedToGroup"", args);
    }

    function addGroup(groupId, groupName, membershipId) {
        var container = document.createElement(""tr"");
        container.setAttribute(""class"", ""group-data-container"");
        container.setAttribute(""id"", ""group-"" + groupId);
        container.setAttribute(""groupId"", groupId);
        container.setAttribute(""membershipId"", membershipId);
        container.addEventListener(""click"", function () {
            switchGroupTo(
                parseInt(container.getAttribute(""groupId"")),
                parseInt(container.getAttribute(""membershipId""))
            );
        });

        var id = d");
            WriteLiteral(@"ocument.createElement(""th"");
        id.setAttribute(""scope"", ""row"");
        id.textContent = groupId;
        container.appendChild(id);

        var name = document.createElement(""td"");
        name.setAttribute(""class"", ""group-data-name"");
        name.textContent = groupName;
        container.appendChild(name);

        //var numOnline = document.createElement(""td"");
        //numOnline.setAttribute(""class"", ""group-data-online"");
        //numOnline.textContent = 1;
        //container.appendChild(numOnline);

        var groupsList = document.getElementById(""groups-list"");

        groupsList.prepend(container);
    }

    function removeGroup(groupId) {
        document.getElementById(""groups-list"")
            .removeChild(document.getElementById(""group-"" + groupId));

        if (groupId == currentGroupID) {
            clear();
        }
    }

    //Start SignalR connection
    const connection = new signalR.HubConnectionBuilder()
        .withUrl(""/chatHub"")
      ");
            WriteLiteral(@"  .configureLogging(signalR.LogLevel.Information)
        .build();
    
    //On connection established
    connection.start().then(function () {
        console.log(""connected"");

        var connectionArgs = {
            UserID: viewmodel.UserID
        };

        //Send the connection event
        connection.invoke(""Connected"", connectionArgs);
    }).catch(function (err) {
        return console.error(err.toString());
    });

    //On Disconnect
    window.addEventListener('unload', function (event) {
        clear();

        var args = {
            UserID: viewmodel.UserID
        };

        //Send the disconnection event
        connection.invoke(""Disconnected"", args);
    });

    //On Focus
    window.addEventListener('focus', function (event) {
        if (membershipID != -1) {
            var args = {
                MembershipID: membershipID
            };

            //Send the active event
            connection.invoke(""ActiveInGroup"", args);
      ");
            WriteLiteral(@"  }

        play_sound = false;

    });

    //On Blur
    window.addEventListener('blur', function (event) {
        if (membershipID != -1) {
            var args = {
                MembershipID: membershipID
            };

            //Send the active event
            connection.invoke(""InactiveInGroup"", args);
        }

        play_sound = true;

    });

    //Receive Previous Messages
    connection.on(""ReceivePreviousMessages"", function (messages) {
        $.each(messages, function () {
            var args = this;
            prependMessage(args.senderName, args.senderRankColor, args.message, args.timestamp);
            numMessages++;
        });
    });

    //Receive Message
    connection.on(""ReceiveMessage"", function (args) {
        if (args.groupID == currentGroupID) {
            appendMessage(args.senderName, args.senderRankColor, args.message, args.timestamp);
            numMessages++;
        } else {
            newGroupMessage(args.groupID);
");
            WriteLiteral(@"        }
        /*
        if (play_sound == true) {
            sound.play();
        }
        */
        sound.play();

    });

    //Send Message
    document.getElementById(""message-input"").addEventListener(""keyup"", function (event) {
        var keyCode = (event.keyCode ? event.keyCode : event.which); 
        if (keyCode == 13 && currentGroupID != -1) {
            var element = document.getElementById(""message-input"");
            var message = element.value;

            if (message.trim() != """") {
                var args = {
                    MembershipID: membershipID,
                    MinRank: viewmodel.Rank,
                    Message: message
                };
                connection.invoke(""SendMessage"", args).catch(function (err) {
                    return console.error(err.toString());
                });

                element.value = """";

                event.preventDefault();
            }
        }
    });

    //Receive Command-Related");
            WriteLiteral(@" Message
    connection.on(""ReceiveCommandMessage"", function (args) {
        appendCommandMessage(args.color, args.message);
    });

    //Send Command
    document.getElementById(""command-input"").addEventListener(""keyup"", function (event) {
        if (event.keyCode == 13) {
            var element = document.getElementById(""command-input"");
            var text = element.value;

            var args = {
                MembershipID: membershipID,
                Message: text
            };
            connection.invoke(""ProcessCommand"", args).catch(function (err) {
                return console.error(err.toString());
            });

            element.value = """";

            event.preventDefault();
        }
    });

    //On User Connected to system. May not be for the active group!
    connection.on(""OtherUserConnectedToGroup"", function (args) {
        if (args.userID != parseInt(viewmodel.UserID)) {
            if (args.groupID == currentGroupID) {
                use");
            WriteLiteral(@"rConnected(args.userID, args.userName, args.userRankName);
            } else {
                //var node = document.getElementById(""group-"" + args.groupID)
                //        .getElementsByClassName(""group-data-online"")
                //        .item(0);
                //    node.textContent = parseInt(node.textContent) + 1;
            }
        }
    });

    //On User Disconnected from system.
    connection.on(""OtherUserDisconnectedFromGroup"", function (args) {
        if (args.userID != parseInt(viewmodel.UserID)) {
            if (args.groupID == currentGroupID) {
                userDisconnected(args.userID);
            } else {
                //var node = document.getElementById(""group-"" + args.groupID)
                //        .getElementsByClassName(""group-data-online"")
                //        .item(0);
                //    node.textContent = parseInt(node.textContent) - 1;
            }
        }
    });

    //On User become Active
    connection.on(""Other");
            WriteLiteral(@"UserActiveInGroup"", function (args) {
        if (args.userID != parseInt(viewmodel.UserID)) {
            if (args.groupID == currentGroupID) {
                userActive(args.userID);
            }
        }
    });

    //On User become Inactive
    connection.on(""OtherUserInactiveInGroup"", function (args) {
        if (args.userID != parseInt(viewmodel.UserID)) {
            if (args.groupID == currentGroupID) {
                userInactive(args.userID);
            }
        }
    });

    connection.on(""AddGroup"", function (args) {
        addGroup(args.groupID, args.groupName, args.membershipID);
    });

    connection.on(""RemoveGroup"", function (args) {
        removeGroup(args.groupID);
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
