using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using IDao.Lib;
using Newtonsoft.Json;
using Microsoft.Extensions.Caching.Distributed;
using Controllers.Attribute;

namespace Controllers
{
    [UserAuthorize]
    public class HomeController : Controller
    {
        public IExampleDao _iExampleDao { get; set; }
        public HomeController(IExampleDao iExampleDao)
        {
            _iExampleDao = iExampleDao;

        }
        public async Task<IActionResult> Index()
        {
            //var s = await _iExampleDao.GetUser();
   
             await HttpContext.Session.LoadAsync();
            var sp = this.HttpContext.Session.GetString("user");
            if (string.IsNullOrEmpty(sp)){
                  HttpContext.Session.SetString("user", "word");
                await HttpContext.Session.CommitAsync();
        
            }
            
           // var s1 = await _iExampleDao.PageList();
           // ViewBag.User = JsonConvert.SerializeObject(s1);
            return View(); ;
        }
        public async Task<IActionResult> Word()
        {
            var s = await _iExampleDao.GetUser();
            return View();
        }

    }
}
