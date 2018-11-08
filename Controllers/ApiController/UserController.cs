using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using IDao.Lib;
using Newtonsoft.Json;


namespace Controllers.ApiController
{
    [Route("api/[controller]/[action]")]
    public class UserController: Controller
    {
        public IExampleDao _iExampleDao { get; set; }
        public UserController(IExampleDao iExampleDao)
        {
            _iExampleDao = iExampleDao;
        }

        [HttpGet]
        public async Task<dynamic> Get()
        {
            var s = await _iExampleDao.GetUser();
            return s;
        }
        [HttpGet]
        public async Task<dynamic> PageList()
        {
            var s = await _iExampleDao.PageList();
            return s;
        }
    }
}
