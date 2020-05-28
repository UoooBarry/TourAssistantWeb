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
    public class AuthorizeAssistant
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var userID = context.HttpContext.Session.GetInt32(nameof(User.UserID));
            if (!userID.HasValue)
            {
                context.Result = new RedirectToActionResult("Privacy", "Home", null);
            }
        }
    }
}
