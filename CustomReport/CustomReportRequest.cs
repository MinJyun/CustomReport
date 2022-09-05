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
        public long Dtno { get; set; }

        public long Ftno { get; set; }

        public string Params { get; set; }

        public string AssignSpid { get; set; }

        public string KeyMap { get; set; }
    }
}
