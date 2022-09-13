namespace CustomReport
{
    /// <summary>
    /// 回傳的類別
    /// </summary>
    public class CustomReportResponse
    {
        /// <summary>
        /// 是否完成
        /// </summary>
        public bool IsCompleted { get; set; }

        /// <summary>
        /// 是否錯誤
        /// </summary>
        public bool IsFaulted { get; set; }

        /// <summary>
        /// 簽章
        /// </summary>
        public string Signature { get; set; }

        /// <summary>
        /// 例外
        /// </summary>
        public string Exception { get; set; }

        /// <summary>
        /// 結果
        /// </summary>
        public string Result { get; set; }
    }
}
