using System.Collections.Concurrent;

namespace CustomReport
{
    /// <summary>
    /// 一個能夠將請求分派給 N 台自訂報表物件的類別
    /// </summary>
    public class AssignAndWaitReport : ICustomReportClient
    {
        /// <summary>
        /// 指定的自訂報表主機群
        /// </summary>
        private ConcurrentQueue<ICustomReportClient> CustomReportClients;

        /// <summary>
        /// 跨執行緒的鎖定
        /// </summary>
        private SemaphoreSlim ThreadLock;

        /// <summary>
        /// 建構子
        /// </summary>
        /// <param name="customReportClients">自訂報表主機</param>
        public AssignAndWaitReport(List<KeyValuePair<int, ICustomReportClient>> assignServers)
        {
            CustomReportClients = new ConcurrentQueue<ICustomReportClient>( 
                assignServers
                .SelectMany( keyValuePair => Enumerable.Range( 0, keyValuePair.Key )
                .Select( _ => keyValuePair.Value ) ) );
            ThreadLock = new SemaphoreSlim(CustomReportClients.Count);
        } 

        /// <summary>
        /// 非同步等待請求
        /// </summary>
        /// <param name="dtno">編號</param>
        /// <param name="ftno">編號</param>
        /// <param name="params">參數</param>
        /// <param name="assignSpid">指定</param>
        /// <param name="keyMap">關鍵字</param>
        /// <returns>回傳內容</returns>
        public async Task<CustomReportResponse> PostAsync(long dtno, long ftno, string @params, string assignSpid, string keyMap)
        {
            await ThreadLock.WaitAsync();
            CustomReportClients.TryDequeue(out ICustomReportClient cstomReportClient);
            try
            {
                var response = await cstomReportClient.PostAsync(dtno, ftno, @params, assignSpid, keyMap);               
                return response;
            }
            finally 
            {
                CustomReportClients.Enqueue(cstomReportClient);
                ThreadLock.Release();
            }
        }
    }
}
