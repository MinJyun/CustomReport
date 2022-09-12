using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomReport
{
    /// <summary>
    /// 請求的類別
    /// </summary>
    internal class CustomReportRequest
    {
        /// <summary>
        /// 編號
        /// </summary>
        public long Dtno { get; set; }

        /// <summary>
        /// 編號
        /// </summary>
        public long Ftno { get; set; }

        /// <summary>
        /// 參數
        /// </summary>
        public string Params { get; set; }

        /// <summary>
        /// 指定
        /// </summary>
        public string AssignSpid { get; set; }

        /// <summary>
        /// 關鍵字
        /// </summary>
        public string KeyMap { get; set; }
    }
}
