using CustomReport;

MockCustomReportClient client = new MockCustomReportClient( 1, 1000 );

var sum = ( await Task.WhenAll( Enumerable.Range( 0, 100 ).Select( async _ =>
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
} ) ) ).Sum();

Console.WriteLine( sum );