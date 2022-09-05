using Newtonsoft.Json;
using System.Text;

namespace CustomReport
{
    /// <summary>
    /// 自訂報表
    /// </summary>
    public class CustomReportClient
    {
        /// <summary>
        /// 自訂報表IP
        /// </summary>
        private readonly string ReportUrl;

        /// <summary>
        /// 建構子
        /// </summary>
        /// <param name="reportUrl">網址</param>
        public CustomReportClient(string reportUrl)
        {
            ReportUrl = reportUrl;
        }

        /// <summary>
        /// 呼叫的非同步方法
        /// </summary>
        /// <param name="url"></param>
        /// <param name="para"></param>
        /// <param name="postData"></param>
        /// <returns></returns>
        public async Task<CustomReportResponse> PostAsync(long dtno, long ftno, string @params, string assignSpid, string keyMap)
        {
            HttpClient client = new HttpClient() { BaseAddress = new Uri(ReportUrl) };
            //傳入值轉為物件
            CustomReportRequest requestJson = new CustomReportRequest() { Dtno = dtno, Ftno = ftno, Params = @params, AssignSpid = assignSpid, KeyMap = keyMap} ;
            // 將 data 轉為 json
            string json = JsonConvert.SerializeObject(requestJson);
            // 將轉為 string 的 json 依編碼並指定 content type 存為 httpcontent
            HttpContent contentPost = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync(ReportUrl, contentPost);
            string reponseContent = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<CustomReportResponse>(reponseContent);
        }
    }
}