#pragma checksum "C:\Users\ryana\Documents\GitHub\Chat-V2\Chat-V2\Chat-V2\Pages\Chat.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "79de0a091e93513de6d267e7bbbe41258325c871"
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"79de0a091e93513de6d267e7bbbe41258325c871", @"/Pages/Chat.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"54bebb9c9bbee8d51a45538557928179fb546f89", @"/Pages/_ViewImports.cshtml")]
    public class Pages_Chat : global::Microsoft.AspNetCore.Mvc.RazorPages.Page
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-page-handler", "CreateNewGroup", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("type", new global::Microsoft.AspNetCore.Html.HtmlString("submit"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-dark text-white"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("placeholder", new global::Microsoft.AspNetCore.Html.HtmlString("Find New Group"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("form-control"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_5 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-page-handler", "SearchGroups", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_6 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("method", "post", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_7 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-page", "/Groups", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_8 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-dark text-white w-100 h-100"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormActionTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormActionTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.InputTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
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
    <div id=""head-row"" class=""row""></div>
    <div class=""row"">&nbsp;</div>
    <div class=""row"">
        <div id=""left-pane"" class=""col-2 col-sm-2 col-md-3 col-lg-3 col-xl-3"">
            <div class=""card text-black-50 h-100"">
                <div class=""card-header"" id=""left-header-desktop"" hidden>
                    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "79de0a091e93513de6d267e7bbbe41258325c8717029", async() => {
                WriteLiteral("\r\n                        <div class=\"input-group\">\r\n                            <div class=\"input-group-prepend\">\r\n                                ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("button", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "79de0a091e93513de6d267e7bbbe41258325c8717441", async() => {
                    WriteLiteral("+");
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_FormActionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormActionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormActionTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_FormActionTagHelper.PageHandler = (string)__tagHelperAttribute_0.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n                            </div>\r\n                            ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("input", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "79de0a091e93513de6d267e7bbbe41258325c8718921", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.InputTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper);
#nullable restore
#line 20 "C:\Users\ryana\Documents\GitHub\Chat-V2\Chat-V2\Chat-V2\Pages\Chat.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For = ModelExpressionProvider.CreateModelExpression(ViewData, __model => __model.SearchGroupInput.NameQuery);

#line default
#line hidden
#nullable disable
                __tagHelperExecutionContext.AddTagHelperAttribute("asp-for", __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_4);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n                            <div class=\"input-group-append\">\r\n                                ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("button", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "79de0a091e93513de6d267e7bbbe41258325c87110692", async() => {
                    WriteLiteral("Search");
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_FormActionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormActionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormActionTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_FormActionTagHelper.PageHandler = (string)__tagHelperAttribute_5.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_5);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n                            </div>\r\n                        </div>\r\n                    ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_6.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_6);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n                </div>\r\n                <div class=\"card-header p-0\" id=\"left-header-mobile\" hidden>\r\n                    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "79de0a091e93513de6d267e7bbbe41258325c87113507", async() => {
                WriteLiteral("+");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Page = (string)__tagHelperAttribute_7.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_7);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_8);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral(@"
                </div>
                <ul id=""groups-list"" class=""list-group list-group-flush"">
                    <!--<li class=""group-data-container list-group-item"">
                        <img src=""/DefaultFiles/defaultGroupImage.png"" width=""32"" height=""32"" class=""rounded-circle img"" />
                        <span class=""group-data-name"">Sample Group Name </span>
                    </li>-->
                </ul>
            </div>
        </div>

        <div id=""center-pane"" class=""col-7 col-sm-7 col-md-6 col-lg-6 col-xl-6"">
            <div class=""text-center"">
                <button id=""load-previous-messages-button"" class=""btn btn-secondary"">Load More Messages</button>
            </div>
            <div id=""messages-list"" class=""console-output"">

            </div>

            <br />

            <textarea id=""message-input"" class=""console-input form-control"" rows=""1"" placeholder=""Enter message here""></textarea>
        </div>

        <div id=""right-pane"" class=""col-");
            WriteLiteral(@"3 col-sm-3 col-md-3 col-lg-3 col-xl-3"">
            <div class=""card h-100"">
                <div class=""card-header bg-dark"">
                    <a href=""/Chat"" id=""current-group-name"" class=""text-white"">
                        No Group Selected
                    </a>
                </div>
                <ul class=""list-group list-group-flush"">
                    <li class=""list-group-item"">
                        <div class=""container"">
                            <div class=""row"">
                                <div id=""current-group-members"" class=""col-auto"">Members:&nbsp;</div>
                                <div id=""current-group-members-count"" class=""col"">
                                    N/A
                                </div>
                                <div id=""current-group-online"" class=""col-auto"">Online:&nbsp;</div>
                                <div id=""current-group-online-count"" class=""col"">
                                    N/A
                       ");
            WriteLiteral(@"         </div>
                            </div>
                        </div>
                    </li>
                </ul>
                <div class=""card-body"">
                    <ul id=""online-members-list"" class=""list-group h-100"">
                        <!--<li class=""user-data-container list-group-item"">
                            <img src=""/DefaultFiles/defaultGroupImage.png"" width=""32"" height=""32"" class=""rounded-circle img"" />
                            <span class=""user-data-name"">Sample User Name </span>
                            <span class=""badge badge-dark text-wrap"">Sample User Rank</span>
                            <span class=""badge badge-success"">Online</span>
                        </li>-->
                    </ul>
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
        d");
            WriteLiteral(@"isplay: block;
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
        
    }

    .user-data-container-inactive {
        
    }

    .user-data-container:hover {
        border: solid 1px #AAAAAA;
    }

    .user-data-name {
        
    }

    .user-data-rank {
        
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
        width: 100%;
        height: 75vh;
        overflow-x: hidden;
    }

    /* The input area for the message terminal */
    #message-input {

    }

    #center-pane {
        height: 100%;
    }

    #left-pane {
        height: 75vh;
    }");
            WriteLiteral(@"

    #right-pane {
        height: 75vh;
    }

    #current-group-name {

    }

    #current-group-members-count {

    }

    #current-group-online-count {

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

    var isMobile = isMobile();

    //Get the sound file
    var sound = new Audio('/media/new-message.mp3');
    var play_sound = false;

    //Get ViewModel data
    const viewmodel = {
        UserID: ");
#nullable restore
#line 208 "C:\Users\ryana\Documents\GitHub\Chat-V2\Chat-V2\Chat-V2\Pages\Chat.cshtml"
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

    function isMobile() {
        if (window.innerWidth <= 800 && window.innerHeight <= 600) {
            return true;
        } else {
            return false;
        }
    }

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

    function userConnected(userId, userName, userImage, userRankName) {
        var container = document.createElement(""li"");
        container.setAttribute(""id"", """);
            WriteLiteral(@"user-"" + userId);
        container.setAttribute(""class"", ""user-data-container list-group-item"");

        var img = document.createElement(""img"");
        img.setAttribute(""src"", userImage);
        if (isMobile) {
            img.setAttribute(""width"", ""16"");
            img.setAttribute(""height"", ""16"");
        } else {
            img.setAttribute(""width"", ""32"");
            img.setAttribute(""height"", ""32"");
        }
        img.setAttribute(""class"", ""rounded-circle img"");
        container.appendChild(img);

        var name = document.createElement(""span"");
        name.setAttribute(""class"", ""user-data-name"");
        if (isMobile) {
            hide(name);
        }
        name.textContent = userName + "" "";
        container.appendChild(name);

        var rank = document.createElement(""span"");
        rank.setAttribute(""class"", ""badge badge-dark text-wrap"");
        if (isMobile) {
            hide(rank);
        }
        rank.textContent = ""    "" + userRankName;
       ");
            WriteLiteral(@" container.appendChild(rank);

        document.getElementById(""online-members-list"").appendChild(container);
        var count = document.getElementById(""current-group-online-count"");
        count.textContent = parseInt(count.textContent) + 1;
    }

    function userDisconnected(userId) {
        document.getElementById(""online-members-list"")
            .removeChild(document.getElementById(""user-"" + userId));
        var count = document.getElementById(""current-group-online-count"");
        count.textContent = parseInt(count.textContent) - 1;
    }

    function userActive(userId) {
        document.getElementById(""user-"" + userId)
            .setAttribute(""class"", ""list-group-item user-data-container"");
    }

    function userInactive(userId) {
        document.getElementById(""user-"" + userId)
            .setAttribute(""class"", ""list-group-item list-group-item-primary user-data-container user-data-container-inactive"");
    }

    function clearUsers() {
        $(""#online-membe");
            WriteLiteral(@"rs-list"").empty();
    }

    function userTyping(userId, userProfileImage) {

    }

    function userNotTyping(userId) {

    }

    function clearTyping() {

    }

    function updateGroupData(groupID, groupName, numUsers) {
        var name = document.getElementById(""current-group-name"");
        name.textContent = groupName;
        name.setAttribute(""href"", ""/Group?groupID="" + groupID);
        var members = document.getElementById(""current-group-members-count"");
        members.textContent = numUsers;
        var online = document.getElementById(""current-group-online-count"");
        online.textContent = ""1"";
        if (isMobile) {
            hide(document.getElementById(""current-group-members""));
            hide(document.getElementById(""current-group-online""));
            hide(members);
            hide(online);
        } else {
            show(document.getElementById(""current-group-members""));
            show(document.getElementById(""current-group-online""));
    ");
            WriteLiteral(@"        show(members);
            show(online);
        }
    }

    function clearData() {
        var name = document.getElementById(""current-group-name"");
        name.textContent = ""No Group Selected"";
        name.setAttribute(""groupID"", -1);
        document.getElementById(""current-group-members-count"").textContent = ""N/A"";
        document.getElementById(""current-group-online-count"").textContent = ""N/A"";
    }

    //Clears messages, clears users, disconnects from the current group, and resets data.
    function clear() {
        clearMessages();
        clearUsers();
        clearTyping();
        clearData();

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
        var node = document.getElementById(");
            WriteLiteral(@"""group-data-new-"" + groupId);
        show(node);
    }

    function removeNewGroupMessage(groupId) {
        hide(document.getElementById(""group-data-new-"" + groupId));
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
                parseInt(container.get");
            WriteLiteral(@"Attribute(""groupId"")),
                parseInt(container.getAttribute(""membershipId""))
            );
        });

        var img = document.createElement(""img"");
        img.setAttribute(""src"", groupImage);
        if (isMobile) {
            img.setAttribute(""width"", ""16"");
            img.setAttribute(""height"", ""16"");
        } else {
            img.setAttribute(""width"", ""32"");
            img.setAttribute(""height"", ""32"");
        }
        img.setAttribute(""class"", ""rounded-circle img"");
        container.appendChild(img);

        var name = document.createElement(""span"");
        name.setAttribute(""class"", ""group-data-name"");
        if (isMobile) {
            hide(name);
        }
        name.textContent = groupName + "" "";
        container.appendChild(name);

        
        var newBadge = document.createElement(""span"");
        newBadge.setAttribute(""id"", ""group-data-new-"" + groupId);
        newBadge.setAttribute(""class"", ""badge badge-success"");
        hide(newBad");
            WriteLiteral(@"ge);
        newBadge.textContent = ""NEW"";
        container.appendChild(newBadge);

        var groupsList = document.getElementById(""groups-list"");
        groupsList.append(container);
    }

    function removeGroup(groupId) {
        document.getElementById(""groups-list"")
            .removeChild(document.getElementById(""group-"" + groupId));

        if (groupId == currentGroupID) {
            clear();
        }
    }

    function loadPreviousMessages(startIndex, count) {
        var args = {
            MembershipID: membershipID,
            StartIndex: startIndex,
            Count: count
        };

        connection.invoke(""GetPreviousMessages"", args);
    }

    function debounce(func, wait, immediate) {
        var timeout;
        return function () {
            var context = this, args = arguments;
            var later = function () {
                timeout = null;
                if (!immediate) func.apply(context, args);
            };
            var c");
            WriteLiteral(@"allNow = immediate && !timeout;
            clearTimeout(timeout);
            timeout = setTimeout(later, wait);
            if (callNow) func.apply(context, args);
        };
    }

    function show(node) {
        node.removeAttribute(""hidden"");
    }

    function hide(node) {
        node.setAttribute(""hidden"", ""hidden"");
    }

    if (isMobile) {
        show(document.getElementById(""left-header-mobile""));
        hide(document.getElementById(""left-header-desktop""));
    } else {
        hide(document.getElementById(""left-header-mobile""));
        show(document.getElementById(""left-header-desktop""));
    }

    //Start SignalR connection
    const connection = new signalR.HubConnectionBuilder()
        .withUrl(""/chatHub"")
        .configureLogging(signalR.LogLevel.Information)
        .build();

    connection.onclose(function (e) {
        var head = document.getElementById(""head-row"");

        var node = document.createElement(""div"");
        node.setAttribute(""cla");
            WriteLiteral(@"ss"", ""alert alert-danger w-100"");
        node.setAttribute(""role"", ""alert"");
        node.textContent = ""Your connection has closed. Refresh the page to reconnect."";

        head.append(node);
    });

    //On connection established
    connection.start().then(function () {
        console.log(""connected to chat hub"");

        var connectionArgs = {
            UserID: viewmodel.UserID
        };

        //Send the connection event
        connection.invoke(""Connected"", connectionArgs).catch(function (err) {
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
    window.addEventListener('unload', functio");
            WriteLiteral(@"n (event) {
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

    //Load Previous Messages
    document.getElementById(""load-previous-messages-button"").addEventListener(""click"", function (even");
            WriteLiteral(@"t) {
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
            appendMessage(args.message);
            numMessages++;
        } else {
            newGroupMessage(args.groupID);
        }

        if ((args.UserID != viewmodel.UserID)) {
            if (play_sound) {
                sound.play();
            }

            var node = document.getElementById(""group-"" + args.groupID);
            var parent = document.getElementById(""groups-list"");

            parent.removeChild(node);
            parent.prepend(node);
        }

    });

    ");
            WriteLiteral(@"//Send Message
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

                event.preventDefault();
            }
        }
    })

    //Typing Indicator
    document.getElementById(""message-input"").addEventListener(""keydown"", isTypingCallback);

    connection.on(""ReceiveGroupData"", function (args) {
        if (args");
            WriteLiteral(@".groupID == currentGroupID) {
            updateGroupData(args.groupID, args.groupName, args.numUsers);
        }
    });

    //On User Connected to system. May not be for the active group!
    connection.on(""OtherUserConnectedToGroup"", function (args) {
        if (args.userID != parseInt(viewmodel.UserID)) {
            if (args.groupID == currentGroupID) {
                userConnected(args.userID, args.userName, args.userImage, args.userRankName);
            }
        }
    });

    //On User Disconnected from system.
    connection.on(""OtherUserDisconnectedFromGroup"", function (args) {
        if (args.userID != parseInt(viewmodel.UserID)) {
            if (args.groupID == currentGroupID) {
                userDisconnected(args.userID);
            }
        }
    });

    //On User become Active
    connection.on(""OtherUserActiveInGroup"", function (args) {
        if (args.userID != parseInt(viewmodel.UserID)) {
            if (args.groupID == currentGroupID) {
             ");
            WriteLiteral(@"   userActive(args.userID);
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
        addGroup(args.groupID, args.groupImage, args.groupName, args.membershipID);
    });

    connection.on(""RemoveGroup"", function (args) {
        removeGroup(args.groupID);
    });

    connection.on(""ClientBannedFromGroup"", function (args) {
        removeGroup(args.groupID);
    });

    connection.on(""OtherUserTyping"", function (args) {
        if (args.groupID == currentGroupID) {
            userTyping(args.userID, args.userProfileImage);
        }
    });

    connection.on(""OtherUserNotTyping"", function (args) {
        if (args.groupID == currentGroupID) {
            userNotTyping(a");
            WriteLiteral("rgs.userID);\r\n        }\r\n    });\r\n</script>\r\n");
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
