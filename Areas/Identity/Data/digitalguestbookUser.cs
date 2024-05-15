using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;

namespace digitalguestbook.Areas.Identity.Data;

// Add profile data for application users by adding properties to the digitalguestbookUser class
public class digitalguestbookUser : IdentityUser
{
    [PersonalData]
    [Column(TypeName = "nvarchar(100)")]
    public string FirstName { get; set; }
    [PersonalData]
    [Column(TypeName = "nvarchar(100)")]
    public string LastName { get; set; }
    [PersonalData]
    [Column(TypeName = "nvarchar(255)")]
    public string CompanyName { get; set; }
}

