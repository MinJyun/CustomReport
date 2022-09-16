using CustomReport;

MockCustomReportClient client1 = new MockCustomReportClient(2, 1000);
MockCustomReportClient client2 = new MockCustomReportClient(1, 1000);
MockCustomReportClient client3 = new MockCustomReportClient(1, 1000);
List<KeyValuePair<int, ICustomReportClient>> assignServers = new List<KeyValuePair<int, ICustomReportClient>>()
{
  new KeyValuePair<int, ICustomReportClient>(2, client1),
  new KeyValuePair<int, ICustomReportClient>(1, client2),
  new KeyValuePair<int, ICustomReportClient>(1, client3)
};
//CustomReportClient client3 = new CustomReportClient("http://192.168.10.146:5000/api/CustomReport");
List<ICustomReportClient> clients = new List<ICustomReportClient>() { client1, client1, client2, client3 };
List<ICustomReportClient> clients2 = new List<ICustomReportClient>();
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

Console.WriteLine(sum);