#pragma checksum "C:\Users\ryana\Documents\GitHub\Chat-V2\Chat-V2\Chat-V2\Pages\Profile.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "77a8065fd60c1f202e449743016de2903661e3a3"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(Chat_V2.Pages.Pages_Profile), @"mvc.1.0.razor-page", @"/Pages/Profile.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"77a8065fd60c1f202e449743016de2903661e3a3", @"/Pages/Profile.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"54bebb9c9bbee8d51a45538557928179fb546f89", @"/Pages/_ViewImports.cshtml")]
    public class Pages_Profile : global::Microsoft.AspNetCore.Mvc.RazorPages.Page
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("type", new global::Microsoft.AspNetCore.Html.HtmlString("submit"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-page-handler", "AcceptJoinInvitation", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-success"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-page-handler", "RejectJoinInvitation", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-danger"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_5 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("type", "hidden", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_6 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("method", "post", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_7 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("onsubmit", new global::Microsoft.AspNetCore.Html.HtmlString("decrementInvitationsCount()"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 3 "C:\Users\ryana\Documents\GitHub\Chat-V2\Chat-V2\Chat-V2\Pages\Profile.cshtml"
  
    ViewData["Title"] = "Profile";
    Layout = "~/Pages/Shared/_Layout.cshtml";
    var returnUrl = (HttpContext.Request.Path.Value + HttpContext.Request.QueryString.Value);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<nav>\r\n    <div class=\"nav nav-tabs\" id=\"nav-tab\" role=\"tablist\">\r\n        <a class=\"nav-item nav-link active\" id=\"nav-profile-tab\" data-toggle=\"tab\" href=\"#nav-profile\" role=\"tab\" aria-controls=\"nav-profile\" aria-selected=\"true\">Profile</a>\r\n");
#nullable restore
#line 12 "C:\Users\ryana\Documents\GitHub\Chat-V2\Chat-V2\Chat-V2\Pages\Profile.cshtml"
         if (Model.IsThisUser) {

#line default
#line hidden
#nullable disable
            WriteLiteral(@"    <a class=""nav-item nav-link"" id=""nav-notifications-tab"" data-toggle=""tab"" href=""#nav-notifications"" role=""tab"" aria-controls=""nav-notifications"" aria-selected=""false"">Notifications <span id=""notifs-badge"" class=""badge badge-primary"" hidden>0</span></a>
            <a class=""nav-item nav-link"" id=""nav-invitations-tab"" data-toggle=""tab"" href=""#nav-invitations"" role=""tab"" aria-controls=""nav-invitations"" aria-selected=""false"">Invitations <span id=""invitations-badge"" class=""badge badge-primary"" hidden>");
#nullable restore
#line 14 "C:\Users\ryana\Documents\GitHub\Chat-V2\Chat-V2\Chat-V2\Pages\Profile.cshtml"
                                                                                                                                                                                                                                                    Write(Model.ChatUser.GroupJoinInvitations.Count);

#line default
#line hidden
#nullable disable
            WriteLiteral("</span></a>\r\n");
#nullable restore
#line 15 "C:\Users\ryana\Documents\GitHub\Chat-V2\Chat-V2\Chat-V2\Pages\Profile.cshtml"
        }

#line default
#line hidden
#nullable disable
            WriteLiteral(@"    </div>
</nav>
<div class=""tab-content"" id=""nav-tabContent"">
    <div class=""tab-pane fade show active"" id=""nav-profile"" role=""tabpanel"" aria-labelledby=""nav-profile-tab"">
        <br />

        <div class=""container"">
            <div class=""row"">
                <div class=""col-12 text-center"">
                    <img");
            BeginWriteAttribute("src", " src=\"", 1414, "\"", 1475, 1);
#nullable restore
#line 25 "C:\Users\ryana\Documents\GitHub\Chat-V2\Chat-V2\Chat-V2\Pages\Profile.cshtml"
WriteAttributeValue("", 1420, FileTools.FileSavePath + Model.ChatUser.ProfileImage, 1420, 55, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" width=\"256\" height=\"256\" class=\"img-thumbnail\" />\r\n                    <h3 class=\"display-3\">");
#nullable restore
#line 26 "C:\Users\ryana\Documents\GitHub\Chat-V2\Chat-V2\Chat-V2\Pages\Profile.cshtml"
                                     Write(Model.ChatUser.UserName);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h3>\r\n                </div>\r\n            </div>\r\n            <dl class=\"row text-center\">\r\n                <dt class=\"col-2\">\r\n                    ");
#nullable restore
#line 31 "C:\Users\ryana\Documents\GitHub\Chat-V2\Chat-V2\Chat-V2\Pages\Profile.cshtml"
               Write(Html.DisplayNameFor(model => model.ChatUser.Id));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </dt>\r\n                <dd class=\"col-10\">\r\n                    ");
#nullable restore
#line 34 "C:\Users\ryana\Documents\GitHub\Chat-V2\Chat-V2\Chat-V2\Pages\Profile.cshtml"
               Write(Html.DisplayFor(model => model.ChatUser.Id));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </dd>\r\n                <dt class=\"col-2\">\r\n                    ");
#nullable restore
#line 37 "C:\Users\ryana\Documents\GitHub\Chat-V2\Chat-V2\Chat-V2\Pages\Profile.cshtml"
               Write(Html.DisplayNameFor(model => model.ChatUser.FirstName));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </dt>\r\n                <dd class=\"col-10\">\r\n                    ");
#nullable restore
#line 40 "C:\Users\ryana\Documents\GitHub\Chat-V2\Chat-V2\Chat-V2\Pages\Profile.cshtml"
               Write(Html.DisplayFor(model => model.ChatUser.FirstName));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </dd>\r\n                <dt class=\"col-2\">\r\n                    ");
#nullable restore
#line 43 "C:\Users\ryana\Documents\GitHub\Chat-V2\Chat-V2\Chat-V2\Pages\Profile.cshtml"
               Write(Html.DisplayNameFor(model => model.ChatUser.LastName));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </dt>\r\n                <dd class=\"col-10\">\r\n                    ");
#nullable restore
#line 46 "C:\Users\ryana\Documents\GitHub\Chat-V2\Chat-V2\Chat-V2\Pages\Profile.cshtml"
               Write(Html.DisplayFor(model => model.ChatUser.LastName));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </dd>\r\n            </dl>\r\n            <div class=\"row text-center\">\r\n                <p>");
#nullable restore
#line 50 "C:\Users\ryana\Documents\GitHub\Chat-V2\Chat-V2\Chat-V2\Pages\Profile.cshtml"
              Write(Model.ChatUser.ProfileDescription);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n            </div>\r\n        </div>\r\n    </div>\r\n");
#nullable restore
#line 54 "C:\Users\ryana\Documents\GitHub\Chat-V2\Chat-V2\Chat-V2\Pages\Profile.cshtml"
     if (Model.IsThisUser) {

#line default
#line hidden
#nullable disable
            WriteLiteral(@"        <div class=""tab-pane fade"" id=""nav-notifications"" role=""tabpanel"" aria-labelledby=""nav-notifications-tab"">
            <br />

            <h4>Notifications</h4>

            <br />

            <div class=""container"">
                <div class=""row"">
                    <div class=""col-12"">
                        <div id=""no-notifs"" class=""card"">
                            <div class=""card-header"">No Notifications</div>
                        </div>
                    </div>
                </div>
                <div class=""row"">
                    <div class=""col-12"">
                        <div id=""notifications-list"">

                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class=""tab-pane fade"" id=""nav-invitations"" role=""tabpanel"" aria-labelledby=""nav-invitations-tab"">
            <br />

            <h4>Join Invitations</h4>

            <br />

            <div id=""join-invitations-lis");
            WriteLiteral("t\" class=\"col-12\">\r\n");
#nullable restore
#line 87 "C:\Users\ryana\Documents\GitHub\Chat-V2\Chat-V2\Chat-V2\Pages\Profile.cshtml"
                 if (Model.ChatUser.GroupJoinInvitations.Count == 0) {

#line default
#line hidden
#nullable disable
            WriteLiteral("                    <div id=\"no-invitations\" class=\"card\">\r\n                        <div class=\"card-header\">No Join Invitations</div>\r\n                    </div>\r\n");
#nullable restore
#line 91 "C:\Users\ryana\Documents\GitHub\Chat-V2\Chat-V2\Chat-V2\Pages\Profile.cshtml"
                } else {
                    

#line default
#line hidden
#nullable disable
#nullable restore
#line 92 "C:\Users\ryana\Documents\GitHub\Chat-V2\Chat-V2\Chat-V2\Pages\Profile.cshtml"
                     foreach (var invitation in Model.ChatUser.GroupJoinInvitations) {

#line default
#line hidden
#nullable disable
            WriteLiteral("                        <div");
            BeginWriteAttribute("id", " id=\"", 4099, "\"", 4142, 2);
            WriteAttributeValue("", 4104, "invitation-group-", 4104, 17, true);
#nullable restore
#line 93 "C:\Users\ryana\Documents\GitHub\Chat-V2\Chat-V2\Chat-V2\Pages\Profile.cshtml"
WriteAttributeValue("", 4121, invitation.GroupID, 4121, 21, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" class=\"card\">\r\n                            <div class=\"card-header\">");
#nullable restore
#line 94 "C:\Users\ryana\Documents\GitHub\Chat-V2\Chat-V2\Chat-V2\Pages\Profile.cshtml"
                                                 Write(invitation.Group.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</div>
                            <div class=""card-body"">
                                <dl>
                                    <dt>
                                        ID
                                    </dt>
                                    <dh>
                                        ");
#nullable restore
#line 101 "C:\Users\ryana\Documents\GitHub\Chat-V2\Chat-V2\Chat-V2\Pages\Profile.cshtml"
                                    Write(invitation.GroupID);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
                                    </dh>
                                    <dt>
                                        Group Name
                                    </dt>
                                    <dh>
                                        ");
#nullable restore
#line 107 "C:\Users\ryana\Documents\GitHub\Chat-V2\Chat-V2\Chat-V2\Pages\Profile.cshtml"
                                    Write(invitation.Group.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
                                    </dh>
                                    <dt>
                                        Message
                                    </dt>
                                    <dh>
                                        ");
#nullable restore
#line 113 "C:\Users\ryana\Documents\GitHub\Chat-V2\Chat-V2\Chat-V2\Pages\Profile.cshtml"
                                    Write(invitation.Message);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                                    </dh>\r\n                                </dl>\r\n                                ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "77a8065fd60c1f202e449743016de2903661e3a316543", async() => {
                WriteLiteral("\r\n                                    ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("button", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "77a8065fd60c1f202e449743016de2903661e3a316838", async() => {
                    WriteLiteral("Accept");
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_FormActionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormActionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormActionTagHelper);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
                __Microsoft_AspNetCore_Mvc_TagHelpers_FormActionTagHelper.PageHandler = (string)__tagHelperAttribute_1.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n                                    ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("button", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "77a8065fd60c1f202e449743016de2903661e3a318294", async() => {
                    WriteLiteral("Reject");
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_FormActionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormActionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormActionTagHelper);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
                __Microsoft_AspNetCore_Mvc_TagHelpers_FormActionTagHelper.PageHandler = (string)__tagHelperAttribute_3.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_3);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_4);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n                                    ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("input", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "77a8065fd60c1f202e449743016de2903661e3a319750", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.InputTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.InputTypeName = (string)__tagHelperAttribute_5.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_5);
#nullable restore
#line 119 "C:\Users\ryana\Documents\GitHub\Chat-V2\Chat-V2\Chat-V2\Pages\Profile.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For = ModelExpressionProvider.CreateModelExpression(ViewData, __model => __model.JoinInvitationInput.ChatUserID);

#line default
#line hidden
#nullable disable
                __tagHelperExecutionContext.AddTagHelperAttribute("asp-for", __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                BeginWriteTagHelperAttribute();
#nullable restore
#line 119 "C:\Users\ryana\Documents\GitHub\Chat-V2\Chat-V2\Chat-V2\Pages\Profile.cshtml"
                                                                                              WriteLiteral(invitation.ChatUserID);

#line default
#line hidden
#nullable disable
                __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
                __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.Value = __tagHelperStringValueBuffer;
                __tagHelperExecutionContext.AddTagHelperAttribute("value", __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.Value, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n                                    ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("input", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "77a8065fd60c1f202e449743016de2903661e3a322257", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.InputTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.InputTypeName = (string)__tagHelperAttribute_5.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_5);
#nullable restore
#line 120 "C:\Users\ryana\Documents\GitHub\Chat-V2\Chat-V2\Chat-V2\Pages\Profile.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For = ModelExpressionProvider.CreateModelExpression(ViewData, __model => __model.JoinInvitationInput.InvitationID);

#line default
#line hidden
#nullable disable
                __tagHelperExecutionContext.AddTagHelperAttribute("asp-for", __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                BeginWriteTagHelperAttribute();
#nullable restore
#line 120 "C:\Users\ryana\Documents\GitHub\Chat-V2\Chat-V2\Chat-V2\Pages\Profile.cshtml"
                                                                                                WriteLiteral(invitation.GroupJoinInvitationID);

#line default
#line hidden
#nullable disable
                __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
                __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.Value = __tagHelperStringValueBuffer;
                __tagHelperExecutionContext.AddTagHelperAttribute("value", __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.Value, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n                                    <input type=\"hidden\" name=\"returnUrl\"");
                BeginWriteAttribute("value", " value=\"", 5947, "\"", 5965, 1);
#nullable restore
#line 121 "C:\Users\ryana\Documents\GitHub\Chat-V2\Chat-V2\Chat-V2\Pages\Profile.cshtml"
WriteAttributeValue("", 5955, returnUrl, 5955, 10, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(" />\r\n                                ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_6.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_6);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_7);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n                            </div>\r\n                            <div class=\"card-footer text-muted\">");
#nullable restore
#line 124 "C:\Users\ryana\Documents\GitHub\Chat-V2\Chat-V2\Chat-V2\Pages\Profile.cshtml"
                                                            Write(invitation.DateSent.ToString());

#line default
#line hidden
#nullable disable
            WriteLiteral("</div>\r\n                        </div>\r\n");
#nullable restore
#line 126 "C:\Users\ryana\Documents\GitHub\Chat-V2\Chat-V2\Chat-V2\Pages\Profile.cshtml"
                    }

#line default
#line hidden
#nullable disable
#nullable restore
#line 126 "C:\Users\ryana\Documents\GitHub\Chat-V2\Chat-V2\Chat-V2\Pages\Profile.cshtml"
                     
                }

#line default
#line hidden
#nullable disable
            WriteLiteral("            </div>\r\n        </div>\r\n        <script>\r\n            \"use strict\";\r\n\r\n            if (");
#nullable restore
#line 133 "C:\Users\ryana\Documents\GitHub\Chat-V2\Chat-V2\Chat-V2\Pages\Profile.cshtml"
           Write(Model.ChatUser.GroupJoinInvitations.Count);

#line default
#line hidden
#nullable disable
            WriteLiteral(" != 0) {\r\n                document.getElementById(\"invitations-badge\").removeAttribute(\"hidden\");\r\n            }\r\n\r\n            notifConnectionPromise.then(function () {\r\n                var getNotifsArgs = {\r\n                    ChatUserID: ");
#nullable restore
#line 139 "C:\Users\ryana\Documents\GitHub\Chat-V2\Chat-V2\Chat-V2\Pages\Profile.cshtml"
                           Write(Model.ChatUser.Id);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
                };

                notifConnection.invoke(""GetNotifications"", getNotifsArgs).catch(function (err) {
                    return console.error(err.toString());
                });
            }).catch(function (err) {
                return console.error(err.toString());
            });

            notifConnection.on(""ReceiveNotifications"", function (list) {
                $.each(list, function () {
                    var args = this;

                    incrementNotifsCount();

                    console.log(args.text);

                    var card = document.createElement(""div"");
                    card.setAttribute(""class"", ""card"");
                    card.setAttribute(""id"", ""notif-"" + args.notificationID);
                    card.setAttribute(""notificationID"", args.notificationID);

                    var head = document.createElement(""div"");
                    head.setAttribute(""class"", ""card-header"");
                    card.append(head);

         ");
            WriteLiteral(@"           var title = document.createElement(""a"");
                    title.setAttribute(""class"", ""card-link"");
                    title.setAttribute(""data-toggle"", ""collapse"");
                    title.setAttribute(""href"", ""#notif-collapse-"" + args.notificationID);
                    title.textContent = args.title;
                    head.append(title);

                    var close = document.createElement(""button"");
                    close.setAttribute(""type"", ""button"");
                    close.setAttribute(""class"", ""close"");
                    close.addEventListener(""click"", function () {
                        var id = parseInt(card.getAttribute(""notificationID""));
                        document.getElementById(""notifications-list"").removeChild(document.getElementById(""notif-"" + id));
                        decrementNotifsCount();

                        var args = {
                            ChatUserID: ");
#nullable restore
#line 182 "C:\Users\ryana\Documents\GitHub\Chat-V2\Chat-V2\Chat-V2\Pages\Profile.cshtml"
                                   Write(Model.ChatUser.Id);

#line default
#line hidden
#nullable disable
            WriteLiteral(@",
                            NotificationID: id
                        };

                        notifConnection.invoke(""RemoveNotification"", args);
                    });
                    close.textContent = ""×"";
                    head.append(close);

                    var collapse = document.createElement(""div"");
                    collapse.setAttribute(""data-parent"", ""#notifications-list"");
                    collapse.setAttribute(""class"", ""collapse"");
                    collapse.setAttribute(""id"", ""notif-collapse-"" + args.notificationID);
                    card.append(collapse);

                    var body = document.createElement(""div"");
                    body.setAttribute(""class"", ""card-body"");
                    collapse.append(body);

                    var text = document.createElement(""p"");
                    text.setAttribute(""class"", ""card-text"");
                    text.textContent = args.text;
                    body.append(text);

              ");
            WriteLiteral(@"      var link = document.createElement(""a"");
                    link.setAttribute(""href"", args.viewURL);
                    link.setAttribute(""class"", ""card-link"");
                    link.textContent = ""View"";
                    body.append(link);

                    document.getElementById(""notifications-list"").prepend(card);
                });
            });

            function decrementInvitationsCount() {
                var node = document.getElementById(""invitations-badge"");
                var count = parseInt(node.textContent) - 1;

                if (count == 0) {
                    node.setAttribute(""hidden"", ""hidden"");
                }

                node.textContent = count;
            }

            function incrementNotifsCount() {
                var node = document.getElementById(""notifs-badge"");
                var count = parseInt(node.textContent) + 1;

                if (count > 0) {
                    node.removeAttribute(""hidden"");
          ");
            WriteLiteral(@"          document.getElementById(""no-notifs"").setAttribute(""hidden"", ""hidden"");
                }

                node.textContent = count;
            }

            function decrementNotifsCount() {
                var node = document.getElementById(""notifs-badge"");
                var count = parseInt(node.textContent) - 1;

                if (count == 0) {
                    node.setAttribute(""hidden"", ""hidden"");
                    document.getElementById(""no-notifs"").removeAttribute(""hidden"");
                }

                node.textContent = count;
            }
        </script>
");
#nullable restore
#line 251 "C:\Users\ryana\Documents\GitHub\Chat-V2\Chat-V2\Chat-V2\Pages\Profile.cshtml"
    }

#line default
#line hidden
#nullable disable
            WriteLiteral("</div>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<Chat_V2.Pages.ProfileModel> Html { get; private set; }
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary<Chat_V2.Pages.ProfileModel> ViewData => (global::Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary<Chat_V2.Pages.ProfileModel>)PageContext?.ViewData;
        public Chat_V2.Pages.ProfileModel Model => ViewData.Model;
    }
}
#pragma warning restore 1591
