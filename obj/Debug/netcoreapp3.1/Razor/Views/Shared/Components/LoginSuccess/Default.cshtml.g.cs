#pragma checksum "D:\OneDrive\Gaurav_websites\GauravRanaTaskEM\EmployeeManagement\EmployeeManagement\Views\Shared\Components\LoginSuccess\Default.cshtml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "8482a59d6ab4ecb8f88ca28bb82340ccd324c6e75a953f856e05e71dfa75278c"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCoreGeneratedDocument.Views_Shared_Components_LoginSuccess_Default), @"mvc.1.0.view", @"/Views/Shared/Components/LoginSuccess/Default.cshtml")]
namespace AspNetCoreGeneratedDocument
{
    #line default
    using global::System;
    using global::System.Collections.Generic;
    using global::System.Linq;
    using global::System.Threading.Tasks;
    using global::Microsoft.AspNetCore.Mvc;
    using global::Microsoft.AspNetCore.Mvc.Rendering;
    using global::Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "D:\OneDrive\Gaurav_websites\GauravRanaTaskEM\EmployeeManagement\EmployeeManagement\Views\_ViewImports.cshtml"
using EmployeeManagement.Models

#nullable disable
    ;
#nullable restore
#line 2 "D:\OneDrive\Gaurav_websites\GauravRanaTaskEM\EmployeeManagement\EmployeeManagement\Views\_ViewImports.cshtml"
using EmployeeManagement.ViewModels

#line default
#line hidden
#nullable disable
    ;
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"Sha256", @"8482a59d6ab4ecb8f88ca28bb82340ccd324c6e75a953f856e05e71dfa75278c", @"/Views/Shared/Components/LoginSuccess/Default.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"Sha256", @"74552029520681211639b34e2be28890c2d2b192f172d9a9c608452d85bead7d", @"/Views/_ViewImports.cshtml")]
    #nullable restore
    internal sealed class Views_Shared_Components_LoginSuccess_Default : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    #nullable disable
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 1 "D:\OneDrive\Gaurav_websites\GauravRanaTaskEM\EmployeeManagement\EmployeeManagement\Views\Shared\Components\LoginSuccess\Default.cshtml"
  
    var username = User.Identity.Name;
    var SuccessMessage = "Login Successful!";


#line default
#line hidden
#nullable disable

            WriteLiteral("\r\n\r\n    <div class=\"alert alert-success\">\r\n        ");
            Write(
#nullable restore
#line 9 "D:\OneDrive\Gaurav_websites\GauravRanaTaskEM\EmployeeManagement\EmployeeManagement\Views\Shared\Components\LoginSuccess\Default.cshtml"
         SuccessMessage

#line default
#line hidden
#nullable disable
            );
            WriteLiteral("\r\n    </div>\r\n    <p><b>Welcome, ");
            Write(
#nullable restore
#line 11 "D:\OneDrive\Gaurav_websites\GauravRanaTaskEM\EmployeeManagement\EmployeeManagement\Views\Shared\Components\LoginSuccess\Default.cshtml"
                    username

#line default
#line hidden
#nullable disable
            );
            WriteLiteral("! You are successfully logged in.</b></p>");
        }
        #pragma warning restore 1998
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; } = default!;
        #nullable disable
    }
}
#pragma warning restore 1591
