using System;

namespace SiliverSun.TcpTool
{
    /// <summary>
    /// 创建自定义的Session结构
    /// </summary>
    internal struct Session
    {
        /// <summary>
        /// 状态ID
        /// </summary>
        public string SessionId { get; set; }

        /// <summary>
        /// 最后一次通过时间
        /// </summary>
        public DateTime LastAccessTime { get; set; }
    }
}
