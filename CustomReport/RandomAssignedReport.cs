namespace CustomReport
{
    /// <summary>
    /// 隨機指派自訂報表的類別
    /// </summary>
    public class RandomAssignedReport : ICustomReportClient
    {
        /// <summary>
        /// 指定的自訂報表主機群
        /// </summary>
        private List<ICustomReportClient> CustomReportClients;

        /// <summary>
        /// 亂數物件
        /// </summary>
        private Random RandomNumber = new Random();

        /// <summary>
        /// 建構子
        /// </summary>
        /// <param name="customReportClients">指派的自訂報表</param>
        public RandomAssignedReport(List<ICustomReportClient> customReportClients)
        {
            CustomReportClients = customReportClients.ToList();
        }

        /// <summary>
        /// 隨機指派PostData給自訂報表的方法
        /// </summary>
        /// <param name="dtno">編號</param>
        /// <param name="ftno">編號</param>
        /// <param name="params">參數</param>
        /// <param name="assignSpid">指定</param>
        /// <param name="keyMap">關鍵字</param>
        /// <returns>回傳內容</returns>
        public async Task<CustomReportResponse> PostAsync(long dtno, long ftno, string @params, string assignSpid, string keyMap)
        {
            int randomNumber = RandomNumber.Next(0, CustomReportClients.Count);
            ICustomReportClient client = CustomReportClients[randomNumber];
            return await client.PostAsync(dtno, ftno, @params, assignSpid, keyMap);
        }
    }
}
