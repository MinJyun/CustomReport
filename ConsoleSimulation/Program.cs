using CustomReport;

MockCustomReportClient client1 = new MockCustomReportClient(1, 1000);
MockCustomReportClient client2 = new MockCustomReportClient(2, 1000);
MockCustomReportClient client3 = new MockCustomReportClient(5, 1000);
//CustomReportClient client3 = new CustomReportClient("http://192.168.10.146:5000/api/CustomReport");
List<ICustomReportClient> clients = new List<ICustomReportClient>() { client1, client2, client3 };
List<ICustomReportClient> clients2 = new List<ICustomReportClient>();
RandomAssignedReport randomAssignedReport = new RandomAssignedReport(clients);
RandomAssignedReport randomAssignedReport2 = new RandomAssignedReport(clients2);
//var sum = ( await Task.WhenAll( Enumerable.Range( 0, 100 ).Select( async _ =>
//{
//    try
//    {
//        await randomAssignedReport.GetResponseAsync(0, 0, string.Empty, string.Empty, string.Empty);
//        return 1;
//    }
//    catch
//    {
//        return 0;
//    }
//} ) ) ).Sum();

var test = await randomAssignedReport2.GetResponseAsync(0, 0, string.Empty, string.Empty, string.Empty);
int i = 1;
//Console.WriteLine( sum );