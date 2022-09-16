using Microsoft.VisualStudio.TestTools.UnitTesting;
using CustomReport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomReport.Tests
{
    /// <summary>
    /// 指定數量分派的單元測試
    /// </summary>
    [TestClass()]
    public class AssignAndWaitReportTests
    {
        /// <summary>
        /// 測試指定其中一台最大請求數為2
        /// </summary>
        /// <returns>任務</returns>
        [TestMethod()]
        public async Task AssignAndWaitTest()
        {
            MockCustomReportClient client1 = new MockCustomReportClient(2, 1000);
            MockCustomReportClient client2 = new MockCustomReportClient(1, 1000);
            MockCustomReportClient client3 = new MockCustomReportClient(1, 1000);
            List<KeyValuePair<int, ICustomReportClient>> assignServers = new List<KeyValuePair<int, ICustomReportClient>>()
            {
                new KeyValuePair<int, ICustomReportClient>(2, client1),
                new KeyValuePair<int, ICustomReportClient>(1, client2),
                new KeyValuePair<int, ICustomReportClient>(1, client3)
            };
            AssignAndWaitReport randomAssignedReport = new AssignAndWaitReport(assignServers);

            var sum = (await Task.WhenAll(Enumerable.Range(0, 15).Select(async _ =>
            {
                try
                {
                    await randomAssignedReport.PostAsync(0, 0, string.Empty, string.Empty, string.Empty);
                    return 1;
                }
                catch
                {
                    return 0;
                }
            }))).Sum();

            Assert.AreEqual(15, sum);
        }
    }
}