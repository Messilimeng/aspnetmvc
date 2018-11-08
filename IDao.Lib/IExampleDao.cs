using Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IDao.Lib
{
    public interface IExampleDao
    {
        Task<dynamic> GetUser();
        Task<BasePageList<dynamic>> PageList();
    }
}
