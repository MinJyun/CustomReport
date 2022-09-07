using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace CustomReport
{
    /// <summary>
    /// 假自訂報表的物件
    /// </summary>
    public class MockCustomReportClient : ICustomReportClient
    {
        /// <summary>
        /// 同時請求數
        /// </summary>
        private int PrivateSimultaneousRequest = 0;

        /// <summary>
        /// 同時請求數
        /// </summary>
        public int SimultaneousRequest { get { return PrivateSimultaneousRequest; } }

        /// <summary>
        /// 最大請求
        /// </summary>
        private int MaxRequest;

        /// <summary>
        /// 平均回應時間
        /// </summary>
        private int AverageResponseTime;

        /// <summary>
        /// 建構子
        /// </summary>
        /// <param name="maxRequest"></param>
        /// <param name="averageResponseTime"></param>
        /// <param name="timePeriod"></param>
        public MockCustomReportClient(int maxRequest, int averageResponseTime)
        {
            MaxRequest = maxRequest;
            AverageResponseTime = averageResponseTime;
        }

        /// <summary>
        /// 假測試方法
        /// </summary>
        /// <param name="dtno"></param>
        /// <param name="ftno"></param>
        /// <param name="params"></param>
        /// <param name="assignSpid"></param>
        /// <param name="keyMap"></param>
        /// <returns></returns>
        public async Task<CustomReportResponse> PostAsync(long dtno, long ftno, string @params, string assignSpid, string keyMap)
        {
            var value = Interlocked.Increment(ref PrivateSimultaneousRequest);
            if (value <= MaxRequest)
            {
                await Task.Delay(AverageResponseTime);
                Interlocked.Decrement(ref PrivateSimultaneousRequest);
                return new CustomReportResponse();
            }
            else
            {
                Interlocked.Decrement(ref PrivateSimultaneousRequest);
                throw new Exception("超過最大請求");
            }
        }
    }
}
