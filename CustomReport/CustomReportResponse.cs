using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomReport
{
    /// <summary>
    /// 回傳的類別
    /// </summary>
    public class CustomReportResponse
    {
        public bool IsCompleted { get; set; }

        public bool IsFaulted { get; set; }

        public string Signature { get; set; }

        public string Exception { get; set; }

        public string Result { get; set; }
    }
}
