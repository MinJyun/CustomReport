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
    public class MockCustomReportClientTests
    {
        /// <summary>
        /// 一次發100個請求方法測試
        /// </summary>
        /// <returns>任務</returns>
        [TestMethod()]
        public async Task RequestNumberTest()
        {
            MockCustomReportClient client = new MockCustomReportClient(5, 1000);

            var sum = await SumAsync(client);

            Assert.AreEqual(5, sum);
        }

        /// <summary>
        /// 連續發2次，檢測是否2次測驗都回傳1
        /// </summary>
        /// <returns>任務</returns>
        [TestMethod()]
        public async Task RequestTwiceTest()
        {
            MockCustomReportClient client = new MockCustomReportClient(1, 1000);

            var sum1 = await SumAsync(client);
            var sum2 = await SumAsync(client);

            Assert.AreEqual(2, sum1 + sum2);
        }

        /// <summary>
        /// 同時發100個請求的方法
        /// </summary>
        /// <param name="client">請求端</param>
        /// <returns>數值</returns>
        private async Task<int> SumAsync(MockCustomReportClient client)
        {
            return (await Task.WhenAll(Enumerable.Range(0, 100).Select(async _ =>
            {
                try
                {
                    await client.PostAsync(0, 0, string.Empty, string.Empty, string.Empty);
                    return 1;
                }
                catch
                {
                    return 0;
                }
            }))).Sum();
        }
    }
}