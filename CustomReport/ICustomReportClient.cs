using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomReport
{
    /// <summary>
    /// 呼叫自訂報表的介面
    /// </summary>
    public interface ICustomReportClient
    {
        /// <summary>
        /// 取得自訂報表值的方法
        /// </summary>
        /// <param name="dtno"></param>
        /// <param name="ftno"></param>
        /// <param name="params"></param>
        /// <param name="assignSpid"></param>
        /// <param name="keyMap"></param>
        /// <returns></returns>
        public Task<CustomReportResponse> PostAsync(long dtno, long ftno, string @params, string assignSpid, string keyMap);
    }
}
