using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Controllers.Attribute
{

    /// <summary>
    /// 前台登录验证
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public class UserAuthorizeAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        public const string UserAuthenticationScheme = "UserAuthenticationScheme";//自定义一个默认的登录方案
        public UserAuthorizeAttribute()
        {
            this.AuthenticationSchemes = UserAuthenticationScheme;
        }
        public virtual void OnAuthorization(AuthorizationFilterContext filterContext)
        {
            var cookies = filterContext.HttpContext.Request.Cookies["my.web.cookie"];
            if (string.IsNullOrEmpty(cookies))
            {
                filterContext.Result = new RedirectResult("/website/login");
            }
            return;
        }

    }
}