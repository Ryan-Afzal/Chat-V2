#pragma checksum "C:\Users\s-afzalr\Documents\GitHub\Chat-V2\Chat-V2\Chat-V2\Pages\Profile.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "ea84887034c5afcf8d10933566a2422db9815dc0"
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
#line 1 "C:\Users\s-afzalr\Documents\GitHub\Chat-V2\Chat-V2\Chat-V2\Pages\_ViewImports.cshtml"
using Chat_V2;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"ea84887034c5afcf8d10933566a2422db9815dc0", @"/Pages/Profile.cshtml")]
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
#line 3 "C:\Users\s-afzalr\Documents\GitHub\Chat-V2\Chat-V2\Chat-V2\Pages\Profile.cshtml"
  
    ViewData["Title"] = "Profile";
    Layout = "~/Pages/Shared/_Layout.cshtml";
    var returnUrl = (HttpContext.Request.Path.Value + HttpContext.Request.QueryString.Value);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<nav>\r\n    <div class=\"nav nav-tabs\" id=\"nav-tab\" role=\"tablist\">\r\n        <a class=\"nav-item nav-link active\" id=\"nav-profile-tab\" data-toggle=\"tab\" href=\"#nav-profile\" role=\"tab\" aria-controls=\"nav-profile\" aria-selected=\"true\">Profile</a>\r\n");
#nullable restore
#line 12 "C:\Users\s-afzalr\Documents\GitHub\Chat-V2\Chat-V2\Chat-V2\Pages\Profile.cshtml"
         if (Model.IsThisUser) {

#line default
#line hidden
#nullable disable
            WriteLiteral(@"            <a class=""nav-item nav-link"" id=""nav-notifications-tab"" data-toggle=""tab"" href=""#nav-notifications"" role=""tab"" aria-controls=""nav-notifications"" aria-selected=""false"">Notifications</a>
            <a class=""nav-item nav-link"" id=""nav-invitations-tab"" data-toggle=""tab"" href=""#nav-invitations"" role=""tab"" aria-controls=""nav-invitations"" aria-selected=""false"">Invitations</a>
");
#nullable restore
#line 15 "C:\Users\s-afzalr\Documents\GitHub\Chat-V2\Chat-V2\Chat-V2\Pages\Profile.cshtml"
        }

#line default
#line hidden
#nullable disable
            WriteLiteral(@"    </div>
</nav>
<div class=""tab-content"" id=""nav-tabContent"">
    <div class=""tab-pane fade show active"" id=""nav-profile"" role=""tabpanel"" aria-labelledby=""nav-profile-tab"">
        <div class=""container"">
            <div class=""row"">
                <div class=""col-12 text-center"">
");
#nullable restore
#line 23 "C:\Users\s-afzalr\Documents\GitHub\Chat-V2\Chat-V2\Chat-V2\Pages\Profile.cshtml"
                      
                        string img64 = Convert.ToBase64String(Model.ChatUser.ProfileImage.Data);
                        string img64Url = string.Format("data:image/" + Model.ChatUser.ProfileImage.ContentType + ";base64,{0}", img64); //ContentType can be gif, jpeg, png etc.
                    

#line default
#line hidden
#nullable disable
            WriteLiteral("                    <img");
            BeginWriteAttribute("src", " src=\"", 1546, "\"", 1561, 1);
#nullable restore
#line 27 "C:\Users\s-afzalr\Documents\GitHub\Chat-V2\Chat-V2\Chat-V2\Pages\Profile.cshtml"
WriteAttributeValue("", 1552, img64Url, 1552, 9, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" width=\"256\" height=\"256\" class=\"img-thumbnail\" />\r\n                    <h3 class=\"display-3\">");
#nullable restore
#line 28 "C:\Users\s-afzalr\Documents\GitHub\Chat-V2\Chat-V2\Chat-V2\Pages\Profile.cshtml"
                                     Write(Model.ChatUser.UserName);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h3>\r\n                </div>\r\n            </div>\r\n            <dl class=\"row text-center\">\r\n                <dt class=\"col-2\">\r\n                    ");
#nullable restore
#line 33 "C:\Users\s-afzalr\Documents\GitHub\Chat-V2\Chat-V2\Chat-V2\Pages\Profile.cshtml"
               Write(Html.DisplayNameFor(model => model.ChatUser.Id));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </dt>\r\n                <dd class=\"col-10\">\r\n                    ");
#nullable restore
#line 36 "C:\Users\s-afzalr\Documents\GitHub\Chat-V2\Chat-V2\Chat-V2\Pages\Profile.cshtml"
               Write(Html.DisplayFor(model => model.ChatUser.Id));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </dd>\r\n                <dt class=\"col-2\">\r\n                    ");
#nullable restore
#line 39 "C:\Users\s-afzalr\Documents\GitHub\Chat-V2\Chat-V2\Chat-V2\Pages\Profile.cshtml"
               Write(Html.DisplayNameFor(model => model.ChatUser.FirstName));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </dt>\r\n                <dd class=\"col-10\">\r\n                    ");
#nullable restore
#line 42 "C:\Users\s-afzalr\Documents\GitHub\Chat-V2\Chat-V2\Chat-V2\Pages\Profile.cshtml"
               Write(Html.DisplayFor(model => model.ChatUser.FirstName));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </dd>\r\n                <dt class=\"col-2\">\r\n                    ");
#nullable restore
#line 45 "C:\Users\s-afzalr\Documents\GitHub\Chat-V2\Chat-V2\Chat-V2\Pages\Profile.cshtml"
               Write(Html.DisplayNameFor(model => model.ChatUser.LastName));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </dt>\r\n                <dd class=\"col-10\">\r\n                    ");
#nullable restore
#line 48 "C:\Users\s-afzalr\Documents\GitHub\Chat-V2\Chat-V2\Chat-V2\Pages\Profile.cshtml"
               Write(Html.DisplayFor(model => model.ChatUser.LastName));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </dd>\r\n            </dl>\r\n            <div class=\"row\">\r\n\r\n            </div>\r\n        </div>\r\n    </div>\r\n");
#nullable restore
#line 56 "C:\Users\s-afzalr\Documents\GitHub\Chat-V2\Chat-V2\Chat-V2\Pages\Profile.cshtml"
     if (Model.IsThisUser) {

#line default
#line hidden
#nullable disable
            WriteLiteral(@"        <div class=""tab-pane fade"" id=""nav-notifications"" role=""tabpanel"" aria-labelledby=""nav-notifications-tab"">
            <h1 class=""text-center"">Notifications are a Work In Progress</h1>
            <h2 class=""text-center"">Check back later!</h2>
        </div>
        <div class=""tab-pane fade"" id=""nav-invitations"" role=""tabpanel"" aria-labelledby=""nav-invitations-tab"">
            <h4>Join Invitations</h4>
            <div id=""join-invitations-list"" class=""col-12"">
");
#nullable restore
#line 64 "C:\Users\s-afzalr\Documents\GitHub\Chat-V2\Chat-V2\Chat-V2\Pages\Profile.cshtml"
                 if (Model.ChatUser.GroupJoinInvitations.Count == 0) {

#line default
#line hidden
#nullable disable
            WriteLiteral("                    <div id=\"no-invitations\" class=\"card\">\r\n                        <div class=\"card-header\">No Join Invitations</div>\r\n                    </div>\r\n");
#nullable restore
#line 68 "C:\Users\s-afzalr\Documents\GitHub\Chat-V2\Chat-V2\Chat-V2\Pages\Profile.cshtml"
                } else {
                    

#line default
#line hidden
#nullable disable
#nullable restore
#line 69 "C:\Users\s-afzalr\Documents\GitHub\Chat-V2\Chat-V2\Chat-V2\Pages\Profile.cshtml"
                     foreach (var invitation in Model.ChatUser.GroupJoinInvitations) {

#line default
#line hidden
#nullable disable
            WriteLiteral("                        <div");
            BeginWriteAttribute("id", " id=\"", 3555, "\"", 3598, 2);
            WriteAttributeValue("", 3560, "invitation-group-", 3560, 17, true);
#nullable restore
#line 70 "C:\Users\s-afzalr\Documents\GitHub\Chat-V2\Chat-V2\Chat-V2\Pages\Profile.cshtml"
WriteAttributeValue("", 3577, invitation.GroupID, 3577, 21, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" class=\"card\">\r\n                            <div class=\"card-header\">");
#nullable restore
#line 71 "C:\Users\s-afzalr\Documents\GitHub\Chat-V2\Chat-V2\Chat-V2\Pages\Profile.cshtml"
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
#line 78 "C:\Users\s-afzalr\Documents\GitHub\Chat-V2\Chat-V2\Chat-V2\Pages\Profile.cshtml"
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
#line 84 "C:\Users\s-afzalr\Documents\GitHub\Chat-V2\Chat-V2\Chat-V2\Pages\Profile.cshtml"
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
#line 90 "C:\Users\s-afzalr\Documents\GitHub\Chat-V2\Chat-V2\Chat-V2\Pages\Profile.cshtml"
                                    Write(invitation.Message);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                                    </dh>\r\n                                </dl>\r\n                                ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "ea84887034c5afcf8d10933566a2422db9815dc015173", async() => {
                WriteLiteral("\r\n                                    ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("button", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "ea84887034c5afcf8d10933566a2422db9815dc015468", async() => {
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
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("button", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "ea84887034c5afcf8d10933566a2422db9815dc016924", async() => {
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
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("input", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "ea84887034c5afcf8d10933566a2422db9815dc018380", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.InputTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.InputTypeName = (string)__tagHelperAttribute_5.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_5);
#nullable restore
#line 96 "C:\Users\s-afzalr\Documents\GitHub\Chat-V2\Chat-V2\Chat-V2\Pages\Profile.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For = ModelExpressionProvider.CreateModelExpression(ViewData, __model => __model.JoinInvitationInput.ChatUserID);

#line default
#line hidden
#nullable disable
                __tagHelperExecutionContext.AddTagHelperAttribute("asp-for", __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                BeginWriteTagHelperAttribute();
#nullable restore
#line 96 "C:\Users\s-afzalr\Documents\GitHub\Chat-V2\Chat-V2\Chat-V2\Pages\Profile.cshtml"
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
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("input", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "ea84887034c5afcf8d10933566a2422db9815dc020891", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.InputTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.InputTypeName = (string)__tagHelperAttribute_5.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_5);
#nullable restore
#line 97 "C:\Users\s-afzalr\Documents\GitHub\Chat-V2\Chat-V2\Chat-V2\Pages\Profile.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For = ModelExpressionProvider.CreateModelExpression(ViewData, __model => __model.JoinInvitationInput.InvitationID);

#line default
#line hidden
#nullable disable
                __tagHelperExecutionContext.AddTagHelperAttribute("asp-for", __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                BeginWriteTagHelperAttribute();
#nullable restore
#line 97 "C:\Users\s-afzalr\Documents\GitHub\Chat-V2\Chat-V2\Chat-V2\Pages\Profile.cshtml"
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
                BeginWriteAttribute("value", " value=\"", 5364, "\"", 5382, 1);
#nullable restore
#line 98 "C:\Users\s-afzalr\Documents\GitHub\Chat-V2\Chat-V2\Chat-V2\Pages\Profile.cshtml"
WriteAttributeValue("", 5372, returnUrl, 5372, 10, false);

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
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n                            </div>\r\n                            <div class=\"card-footer text-muted\">");
#nullable restore
#line 101 "C:\Users\s-afzalr\Documents\GitHub\Chat-V2\Chat-V2\Chat-V2\Pages\Profile.cshtml"
                                                            Write(invitation.DateSent.ToString());

#line default
#line hidden
#nullable disable
            WriteLiteral("</div>\r\n                        </div>\r\n");
#nullable restore
#line 103 "C:\Users\s-afzalr\Documents\GitHub\Chat-V2\Chat-V2\Chat-V2\Pages\Profile.cshtml"
                    }

#line default
#line hidden
#nullable disable
#nullable restore
#line 103 "C:\Users\s-afzalr\Documents\GitHub\Chat-V2\Chat-V2\Chat-V2\Pages\Profile.cshtml"
                     
                }

#line default
#line hidden
#nullable disable
            WriteLiteral("            </div>\r\n        </div>\r\n");
#nullable restore
#line 107 "C:\Users\s-afzalr\Documents\GitHub\Chat-V2\Chat-V2\Chat-V2\Pages\Profile.cshtml"
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
