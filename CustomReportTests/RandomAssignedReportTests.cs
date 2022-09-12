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
    /// 測試類別
    /// </summary>
    [TestClass()]
    public class RandomAssignedReportTests
    {
        /// <summary>
        /// 測試是否有隨機指定
        /// </summary>
        /// <returns>任務</returns>
        [TestMethod()]
        public async Task RandomRequestSumTest()
        {
            MockCustomReportClient client1 = new MockCustomReportClient(1, 1000);
            MockCustomReportClient client2 = new MockCustomReportClient(2, 1000);
            MockCustomReportClient client3 = new MockCustomReportClient(5, 1000);
            List<ICustomReportClient> clients = new List<ICustomReportClient>() { client1, client2, client3 };
            RandomAssignedReport randomAssignedReport = new RandomAssignedReport(clients);

            var sum = (await Task.WhenAll(Enumerable.Range(0, 100).Select(async _ =>
            {
                try
                {
                    await randomAssignedReport.GetResponseAsync(0, 0, string.Empty, string.Empty, string.Empty);
                    return 1;
                }
                catch
                {
                    return 0;
                }
            }))).Sum();

            Assert.AreEqual(8, sum);
        }
    }
}