#pragma checksum "C:\Users\ryana\Documents\GitHub\Chat-V2\Chat-V2\Chat-V2\Pages\Chat.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "577214820e3479db7cc0489e2cfdb66e7d47105f"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(Chat_V2.Pages.Pages_Chat), @"mvc.1.0.razor-page", @"/Pages/Chat.cshtml")]
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
#nullable restore
#line 1 "C:\Users\ryana\Documents\GitHub\Chat-V2\Chat-V2\Chat-V2\Pages\_ViewImports.cshtml"
using Chat_V2;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"577214820e3479db7cc0489e2cfdb66e7d47105f", @"/Pages/Chat.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"54bebb9c9bbee8d51a45538557928179fb546f89", @"/Pages/_ViewImports.cshtml")]
    public class Pages_Chat : global::Microsoft.AspNetCore.Mvc.RazorPages.Page
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 3 "C:\Users\ryana\Documents\GitHub\Chat-V2\Chat-V2\Chat-V2\Pages\Chat.cshtml"
  
    ViewData["Title"] = "Chat";
    Layout = "~/Pages/Shared/_Layout.cshtml";

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
<div class=""container"">
    <div class=""row"">&nbsp;</div>
    <div class=""row"">
        <div id=""left-pane"" class=""col-3 card"">
            <div class=""card-body"">
                <div class=""container"">
                    <div class=""row"">
                        <div class=""col-12"">
                            <ul id=""groups-list"" class=""list-group"">
                                <li class=""group-data-container list-group-item"">
                                    <img src=""/defaultFiles/defaultGroupImage.png"" width=""32"" height=""32"" class=""rounded-circle img"" />
                                    <span class=""group-data-name"">Sample Group Name </span>
                                </li>
                            </ul>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div class=""col-6"">
            <div class=""text-center"">
                <button id=""load-previous-messages-button"" class=""btn btn-second");
            WriteLiteral(@"ary"">Load More Messages</button>
            </div>
            <div id=""messages-list"" class=""console-output"">

            </div>

            <br />

            <input type=""text"" id=""message-input"" class=""console-input form-control"" placeholder=""Enter message here"" />
        </div>

        <div id=""right-pane"" class=""col-3 card"">
            <div class=""card-body"">
                <div class=""container"">
                    <div class=""row"">
                        <div class=""col-12"">
                            <ul id=""online-members-list"" class=""list-group"">
                                <li class=""list-group-item list-group-item-primary user-data-container"">
                                    <div class=""user-data-name"">
                                        Sample User Name
                                    </div>
                                    <div class=""user-data-rank"">
                                        Sample User Rank
                                  ");
            WriteLiteral(@"  </div>
                                </li>
                            </ul>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
<style>

    .message {
        padding: 4px 4px 4px 4px;
        word-break: break-word;
    }

    .message-image {
    }

    .message-container {
        display: block;
    }

    .message-header {
        display: inline;
    }

    .message-username {
        font-size: 13px;
    }

    .message-timestamp {
        font-size: 11px;
    }

    .message-content {
        font-size: 13px;
    }

    .user-data-container {
        display: block;
        width: 100%;
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

    .group-data-container");
            WriteLiteral(@" {
    }

    .group-data-container-blocked {
        color: red;
    }

    .group-data-container:hover {
        border: solid 1px #AAAAAA;
    }

    .group-data-name {
    }

    /* The output area for the message terminal */
    #messages-list {
        width: 100%;
        overflow-x: hidden;
    }

    /* The input area for the message terminal */
    #message-input {
    }

    #left-pane {
        height: 100%;
    }

    #right-pane {
        height: 100%;
    }

    /* The list of online group members */
    #online-members-list {
        overflow: auto;
    }

    #groups-list {
        overflow: auto;
    }
</style>
<script src=""lib/signalr/dist/browser/signalr.js""></script>
<script type=""text/javascript"">
    ""use strict"";

    //Get the sound file
    var sound = new Audio('/media/new-message.mp3');
    var play_sound = false;

    //Get ViewModel data
    const viewmodel = {
        UserID: ");
#nullable restore
#line 165 "C:\Users\ryana\Documents\GitHub\Chat-V2\Chat-V2\Chat-V2\Pages\Chat.cshtml"
           Write(Model.ViewModel.ChatUser.Id);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
    };

    //The number of messages currently displayed. Used for getting previous messages.
    var numMessages = 0;

    var currentGroupID = -1;
    var membershipID = -1;

    function appendMessage(message) {
        var messagesList = $(""#messages-list"");

        messagesList.append(message);
        messagesList.scrollTop(messagesList[0].scrollHeight);
    }

    function prependMessage(message) {
        var messagesList = $(""#messages-list"");

        messagesList.prepend(message);
        messagesList.scrollTop(messagesList[0].scrollHeight);
    }

    function clearMessages() {
        $(""#messages-list"").empty();
    }

    function userConnected(userId, userName, userRankName) {
        var container = document.createElement(""li"");
        container.setAttribute(""id"", ""user-"" + userId);
        container.setAttribute(""class"", ""list-group-item list-group-item-primary user-data-container"");

        var name = document.createElement(""div"");
        name.setAttrib");
            WriteLiteral(@"ute(""class"", ""user-data-name"");
        name.textContent = userName;
        container.appendChild(name);

        var rank = document.createElement(""div"");
        rank.setAttribute(""class"", ""user-data-rank"");
        rank.textContent = ""    "" + userRankName;
        container.appendChild(rank);

        document.getElementById(""online-members-list"").appendChild(container);
    }

    function userDisconnected(userId) {
        document.getElementById(""online-members-list"")
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

    function clearUsers() {");
            WriteLiteral(@"
        $(""#online-members-list"").empty();
    }

    function userTyping(userId, userProfileImage) {

    }

    function userNotTyping(userId) {

    }

    function clearTyping() {

    }

    //Clears messages, clears users, disconnects from the current group, and resets data.
    function clear() {
        clearMessages();
        clearUsers();
        clearTyping();

        if (membershipID != -1) {
            var args = {
                MembershipID: membershipID
            };

            connection.invoke(""DisconnectedFromGroup"", args);
        }

        numMessages = 0;
        currentGroupID = -1;
        membershipID = -1;
    }

    function newGroupMessage(groupId) {
        //Signal new Group Message
        var newBadge = document.createElement(""span"");
        newBadge.setAttribute(""class"", ""badge badge-success"");
        newBadge.textContent = ""NEW"";

        var node = document.getElementById(""group-data-new-"" + groupId);

        if (!node.ha");
            WriteLiteral(@"sChildNodes()) {
            node.appendChild(newBadge);
        }
    }

    function removeNewGroupMessage(groupId) {
        $(""#group-data-new-"" + groupId).empty();
    }

    function switchGroupTo(groupId, membershipId) {
        clear();

        currentGroupID = groupId;
        membershipID = membershipId;

        var args = {
            MembershipID: membershipID
        };

        connection.invoke(""ConnectedToGroup"", args);

        removeNewGroupMessage(groupId);
    }

    function addGroup(groupId, groupImage, groupName, membershipId) {
        var container = document.createElement(""li"");
        container.setAttribute(""class"", ""group-data-container list-group-item"");
        container.setAttribute(""id"", ""group-"" + groupId);
        container.setAttribute(""groupId"", groupId);
        container.setAttribute(""membershipId"", membershipId);
        container.addEventListener(""click"", function () {
            switchGroupTo(
                parseInt(container.getA");
            WriteLiteral(@"ttribute(""groupId"")),
                parseInt(container.getAttribute(""membershipId""))
            );
        });

        var img = document.createElement(""img"");
        img.setAttribute(""src"", groupImage);
        img.setAttribute(""width"", ""32"");
        img.setAttribute(""height"", ""32"");
        img.setAttribute(""class"", ""rounded-circle img"");
        container.appendChild(img);

        var name = document.createElement(""span"");
        name.setAttribute(""class"", ""group-data-name"");
        name.textContent = groupName + "" "";
        container.appendChild(name);

        var n = document.createElement(""span"");
        n.setAttribute(""id"", ""group-data-new-"" + groupId);
        container.appendChild(n);

        var groupsList = document.getElementById(""groups-list"");
        groupsList.append(container);
    }

    function removeGroup(groupId) {
        document.getElementById(""groups-list"")
            .removeChild(document.getElementById(""group-"" + groupId));

        if (gr");
            WriteLiteral(@"oupId == currentGroupID) {
            clear();
        }
    }

    function blockedGroup(groupId) {
        var group = document.getElementById(""group-"" + groupId);
        group.setAttribute(""class"", ""group-data-container-blocked"");
        group.removeEventListener(""click"");

        if (groupId == currentGroupID) {
            clear();
        }
    }

    function unblockedGroup(groupId) {
        var group = document.getElementById(""group-"" + groupId);
        group.setAttribute(""class"", ""group-data-container"");
        group.addEventListener(""click"", function () {
            switchGroupTo(
                parseInt(container.getAttribute(""groupId"")),
                parseInt(container.getAttribute(""membershipId""))
            );
        });
    }

    function loadPreviousMessages(startIndex, count) {
        var args = {
            MembershipID: membershipID,
            StartIndex: startIndex,
            Count: count
        };

        connection.invoke(""GetPrevio");
            WriteLiteral(@"usMessages"", args);
    }

    function debounce(func, wait, immediate) {
        var timeout;
        return function () {
            var context = this, args = arguments;
            var later = function () {
                timeout = null;
                if (!immediate) func.apply(context, args);
            };
            var callNow = immediate && !timeout;
            clearTimeout(timeout);
            timeout = setTimeout(later, wait);
            if (callNow) func.apply(context, args);
        };
    };

    //Start SignalR connection
    const connection = new signalR.HubConnectionBuilder()
        .withUrl(""/chatHub"")
        .configureLogging(signalR.LogLevel.Information)
        .build();

    //On connection established
    connection.start().then(function () {
        console.log(""connected"");

        var connectionArgs = {
            UserID: viewmodel.UserID
        };

        //Send the connection event
        connection.invoke(""Connected"", connectionArgs");
            WriteLiteral(@").catch(function (err) {
            return console.error(err.toString());
        });
    }).catch(function (err) {
        return console.error(err.toString());
    });

    const isTypingCallback = debounce(
            function () {
            var args = {
                MembershipID: membershipID
                };
                connection.invoke(""UserTyping"", args);
            },
            600,
            true);

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
        }

 ");
            WriteLiteral(@"       play_sound = false;

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

    //Load Previous Messages
    document.getElementById(""load-previous-messages-button"").addEventListener(""click"", function (event) {
        if (membershipID != -1) {
            loadPreviousMessages(numMessages, 50);
        }
    });

    //Receive Previous Messages
    connection.on(""ReceivePreviousMessages"", function (messages) {
        $.each(messages, function () {
            var args = this;
            prependMessage(args.message);
            numMessages++;
        });
    });

    //Receive Message
    connection.on(""ReceiveMessage"", function (args) {
        if (args.groupID == currentGroupID) {
    ");
            WriteLiteral(@"        appendMessage(args.message);
            numMessages++;
        } else {
            newGroupMessage(args.groupID);
        }

        if ((args.UserID != viewmodel.UserID) && (play_sound == true)) {
            sound.play();
        }

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
                    MinRank: 0,
                    Message: message
                };
                connection.invoke(""SendMessage"", args).catch(function (err) {
                    return console.error(err.toString());
                });

                element.value = """";
");
            WriteLiteral(@"
                event.preventDefault();
            }
        }
    })

    //Typing Indicator
    document.getElementById(""message-input"").addEventListener(""keydown"", isTypingCallback);

    //On User Connected to system. May not be for the active group!
    connection.on(""OtherUserConnectedToGroup"", function (args) {
        if (args.userID != parseInt(viewmodel.UserID)) {
            if (args.groupID == currentGroupID) {
                userConnected(args.userID, args.userName, args.userRankName);
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
            i");
            WriteLiteral(@"f (args.groupID == currentGroupID) {
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
    connection.on(""OtherUserActiveInGroup"", function (args) {
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
  ");
            WriteLiteral(@"      addGroup(args.groupID, args.groupImage, args.groupName, args.membershipID);
    });

    connection.on(""RemoveGroup"", function (args) {
        removeGroup(args.groupID);
    });

    connection.on(""ClientBannedFromGroup"", function (args) {
        removeGroup(args.groupID);
    });

    connection.on(""ClientBlockedFromGroup"", function (args) {
        blockedGroup(args.groupID);
    });

    connection.on(""ClientUnblockedFromGroup"", function (args) {
        unblockedGroup(args.groupID);
    });

    connection.on(""OtherUserTyping"", function (args) {
        if (args.groupID == currentGroupID) {
            userTyping(args.userID, args.userProfileImage);
        }
    });

    connection.on(""OtherUserNotTyping"", function (args) {
        if (args.groupID == currentGroupID) {
            userNotTyping(args.userID);
        }
    });
</script>
");
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
