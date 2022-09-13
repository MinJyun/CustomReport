using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        private List<ICustomReportClient> CustomReportClients;

        SemaphoreSlim Sema = new SemaphoreSlim(1);

        /// <summary>
        /// 請求任務
        /// </summary>
        private List<Task<CustomReportResponse>> RequestTasks = new List<Task<CustomReportResponse>>();

        /// <summary>
        /// 建構子
        /// </summary>
        /// <param name="customReportClients"></param>
        public AssignAndWaitReport(List<ICustomReportClient> customReportClients)
        {
            CustomReportClients = customReportClients.ToList();
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
            await Sema.WaitAsync();
            int index;
            if (RequestTasks.Count < CustomReportClients.Count)
            {
                index = RequestTasks.Count;
                RequestTasks.Add(Task.Run(() => CustomReportClients[index].PostAsync(dtno, ftno, @params, assignSpid, keyMap)));
            }
            else
            {
                await Task.WhenAny(RequestTasks);
                index = RequestTasks.FindIndex(x => x.IsCompletedSuccessfully);
                RequestTasks[index] = Task.Run(() => CustomReportClients[index].PostAsync(dtno, ftno, @params, assignSpid, keyMap));
            }
            Sema.Release();
            return await RequestTasks[index];
        }
    }
}
