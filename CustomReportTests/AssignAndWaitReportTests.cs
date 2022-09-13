using Microsoft.VisualStudio.TestTools.UnitTesting;
using CustomReport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomReport.Tests
{
    [TestClass()]
    public class AssignAndWaitReportTests
    {
        [TestMethod()]
        public async Task AssignAndWaitTest()
        {
            MockCustomReportClient client1 = new MockCustomReportClient(1, 1000);
            MockCustomReportClient client2 = new MockCustomReportClient(1, 1000);
            MockCustomReportClient client3 = new MockCustomReportClient(1, 1000);
            List<ICustomReportClient> clients = new List<ICustomReportClient>() { client1, client2, client3 };
            AssignAndWaitReport randomAssignedReport = new AssignAndWaitReport(clients);

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