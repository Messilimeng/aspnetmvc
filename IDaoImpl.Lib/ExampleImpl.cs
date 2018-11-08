using Dapper;
using IDao.Lib;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Entity;
using System.Data;

namespace IDaoImpl.Lib
{
    public class ExampleImpl : IExampleDao
    {
        public async Task<dynamic> GetUser()
        {
            var Eid = "10000025";
            var s = await MSSQL.QueryFirstOrDefaultAsync<dynamic>($"select * from System_UserInfo where EID='{Eid}'");
            return s;
        }

        public async Task<BasePageList<dynamic>> PageList()
        {
            var querymodel = new PagingModel
            {
                PageSize=10,
                CurrentPage =1,
                Table= "System_UserInfo",
                Column="*",
                Condition="",
                OrderColumn= "EID desc",
            };
            var outParam = new DynamicParameters();
            outParam.Add("TotalCount", dbType: DbType.Int32, direction: ParameterDirection.Output);
            outParam.Add("TotalPage", dbType: DbType.Int32, direction: ParameterDirection.Output);
            var mainval = new BasePageList<dynamic>
            {
                aaData =  await MSSQL.QuerySPAsync<dynamic>("Com_Pagination", querymodel, outParam),
                Totalcount = outParam.Get<int>("TotalCount"),
                TotalPage = outParam.Get<int>("TotalPage")
            };
            return mainval;
        }
    }
}
