using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TourWebApp.Models;

namespace TourWebApp.Attributes
{
    public class AuthorizeAdmin : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var userID = context.HttpContext.Session.GetInt32(nameof(User.UserID));
            var userRole = context.HttpContext.Session.GetString(nameof(User.Role));
            if (!userID.HasValue && userRole != "Admin")
            {
                context.Result = new RedirectToActionResult("Index", "Home", null);
            }
        }
    }
}
