using System;
using System.Collections.Generic;
using System.Text;

namespace Entity
{
    public class PagingModel
    {
        /// <summary>
        /// 总记录数 OUTPUT
        /// </summary>
        public int TotalCount { get; set; }
        /// <summary>
        /// 总页数 OUTPUT
        /// </summary>
        public int TotalPage { get; set; }
        /// <summary>
        /// 查询的表名（可多表，例如：Person p LEFT JOIN TE a ON a.PID=p.Id ）
        /// </summary>
        public string Table { get; set; }
        /// <summary>
        /// 查询的字段，可多列或者为
        /// </summary>
        public string Column { get; set; }
        /// <summary>
        /// 每页记录数
        /// </summary>
        public int PageSize { get; set; }
        /// <summary>
        /// 排序字段
        /// </summary>
        public string OrderColumn { get; set; }
        /// <summary>
        /// 当前页数
        /// </summary>
        public int CurrentPage { get; set; }
        /// <summary>
        /// 是否使用分组，否是
        /// </summary>
        public int Group { get; set; } =0;
        /// <summary>
        /// 分组字段
        /// </summary>
        public string GroupColumn { get; set; }
        /// <summary>
        /// 查询条件（注意：若这时候为多表查询，这里也可以跟条件，例如：a.pid=2）
        /// </summary>
        public string Condition { get; set; }
    }
    public class BasePageList<T>
    {
        /// <summary>
        /// 总记录数
        /// </summary>
        public int Totalcount { get; set; }

        public int TotalPage { get; set; }
        /// <summary>
        /// 数据实体列表
        /// </summary>
        public IEnumerable<T> aaData { get; set; }
    }
}
