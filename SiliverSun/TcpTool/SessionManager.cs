using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using static SiliverSun.TcpTool.CustomPool;

namespace SiliverSun.TcpTool
{
    internal class SessionManager
    {
        /// <summary>
        /// Session记录字典
        /// </summary>
        private readonly ConcurrentDictionary<string, Session> _sessions = new ConcurrentDictionary<string, Session>();

        /// <summary>
        /// Session记录数据字典
        /// </summary>
        private readonly ConcurrentDictionary<string, Dictionary<string, string>> _sessionData = new ConcurrentDictionary<string, Dictionary<string, string>>();

        /// <summary>
        /// 创建Session方法
        /// </summary>
        /// <returns></returns>
        public string CreateSession() {
            string sessionId = Guid.NewGuid().ToString();
            if (_sessions.TryAdd(sessionId, new Session { SessionId = sessionId, LastAccessTime = DateTime.UtcNow }))
            {
                return sessionId;
            }
            else {
                return string.Empty;
            }
        }

        /// <summary>
        /// 清除所有的失效的Session
        /// </summary>
        public void CleanupAllSession() {
            foreach (var session in _sessions) {
                if (session.Value.LastAccessTime + SessionTimeout >= DateTime.UtcNow) {
                    CleanupSession(session.Key);
                }
            }
        }

        /// <summary>
        /// 清除一个Session
        /// </summary>
        /// <param name="sessionId"></param>
        public void CleanupSession(string sessionId) {
            if (_sessionData.TryRemove(sessionId, out Dictionary<string, string> removed)) { 

            }

            if (_sessions.TryRemove(sessionId,out Session header)) { 
            
            }
        }

        /// <summary>
        /// 更新会话的LastAccessTime
        /// </summary>
        /// <param name="sessionId"></param>
        /// <returns></returns>
        public bool TouchSession(string sessionId) {
            if (!_sessions.TryGetValue(sessionId, out Session oldHeader)) {
                return false;
            }
            Session updateHeader = oldHeader;
            updateHeader.LastAccessTime = DateTime.UtcNow;
            _sessions.TryUpdate(sessionId, updateHeader, oldHeader);
            return true;
        }

        /// <summary>
        /// 解析Session数据
        /// </summary>
        /// <param name="sessionId"></param>
        /// <param name="requestAction"></param>
        /// <returns></returns>
        public string ParseSessionData(string sessionId,string requestAction) {
            string[] sessionData = requestAction.Split(",");
            if (sessionData.Length != 2) {
                return STATUSUNKNOWN;
            }
            string key = sessionData[0];
            string value = sessionData[1];
            SetSessionData(sessionId, key, value);
            return $"{key}={value}";
        }

        /// <summary>
        /// 设置SessionData数据
        /// </summary>
        /// <param name="sessionId">sessionid</param>
        /// <param name="key">键名</param>
        /// <param name="value">键值</param>
        public void SetSessionData(string sessionId, string key,string value) {
            if (!_sessionData.TryGetValue(sessionId, out Dictionary<string, string> data))
            {
                data = new Dictionary<string, string>();
                data.Add(key, value);
                _sessionData.TryAdd(sessionId, data);
            }
            else {
                if (data.TryGetValue(key, out string val)) {
                    data.Remove(key);
                }
                data.Add(key, value);
            }
        }

        /// <summary>
        /// 获取Session
        /// </summary>
        /// <param name="sessionid"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public string GetSessionData(string sessionid,string key) {
            if (_sessionData.TryGetValue(sessionid, out Dictionary<string, string> data)) {
                if (data.TryGetValue(key, out string value)) {
                    return value;
                }
            }
            return STATUSNOTFOUND;
        }
    }
}
