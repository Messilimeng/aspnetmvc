using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using IDao.Lib;
using Newtonsoft.Json;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.AspNetCore.Authentication;
using System;
using System.Security.Claims;
using System.Collections.Generic;
using Controllers.Attribute;

namespace Controllers
{
    public class WebSiteController : Controller
    {
        //public IExampleDao _iExampleDao { get; set; }
        //public IDistributedCache _icache { get; set; }
        //public WebSiteController(IExampleDao iExampleDao)
        //{
        //    _iExampleDao = iExampleDao;

        //}
        public async Task<IActionResult> Login(string UserName, string UserPwd)
        {
            UserName = "11111";
            UserPwd = "22222";

            try
            {
                var user = new ClaimsPrincipal(
                       new ClaimsIdentity(new[]
                       {
                             new Claim("UserName",UserName),
                             new Claim("UserPwd",UserPwd),
                       }, UserAuthorizeAttribute.UserAuthenticationScheme)
                 );
                await HttpContext.SignInAsync(
                UserAuthorizeAttribute.UserAuthenticationScheme,
                user,
                new AuthenticationProperties
                {
                    ExpiresUtc = DateTime.UtcNow.AddMinutes(60),// 有效时间  //ExpiresUtc = DateTimeOffset.Now.Add(TimeSpan.FromDays(7)), // 有效时间
                    IsPersistent = true,
                    AllowRefresh = false
                });
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }

            return new RedirectResult("~/Home/Index");
        }
        public async Task<IActionResult> Logout(string returnurl)
        {
            await HttpContext.SignOutAsync(UserAuthorizeAttribute.UserAuthenticationScheme);
            return Redirect(returnurl ?? "~/");
        }

    }
}
