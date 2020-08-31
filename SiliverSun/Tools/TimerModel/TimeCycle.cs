using System;

namespace SiliverSun.Tools.TimerModel
{
    /// <summary>
    /// 计时周期类
    /// </summary>
    public class TimeCycle
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// 计划开始时间
        /// </summary>
        public string BeginTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public string EndTime { get; set; }

        /// <summary>
        /// 最大执行次数
        /// </summary>
        public int MaxActionTimes { get; set; }

        /// <summary>
        /// 计时周期内执行的动作(动作会到达开始事件后的)
        /// </summary>
        public Action Action { get; set; }

        /// <summary>
        /// 动作执行时间间隔(秒)
        /// </summary>
        public int ActionSeconds { get; set; }

        /// <summary>
        /// 方法执行次数
        /// </summary>
        public int ActionExeccutionTimes { get; set; }


        public TimeCycle(int id, Action action, int actionSeconds) : this(id, "00:00:00", action, actionSeconds) { }


        public TimeCycle(int id, string beginTime, Action action, int actionSeconds) : this(id, beginTime, action, actionSeconds, 0) { }


        public TimeCycle(int id, string beginTime, Action action, int actionSeconds, int maxActionTimes) : this(id, beginTime, "23:59:59", action, actionSeconds, maxActionTimes) { }

        /// <summary>
        /// 基本构造器
        /// </summary>
        /// <param name="id">唯一标识</param>
        /// <param name="beginTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="action">要执行的任务</param>
        /// <param name="actionSeconds">任务执行时间间隔</param>
        /// <param name="maxActionTimes">最大执行次数</param>
        public TimeCycle(int id, string beginTime, string endTime, Action action, int actionSeconds, int maxActionTimes)
        {
            this.ID = id;
            this.BeginTime = beginTime;
            this.EndTime = endTime;
            this.Action = action;
            this.ActionSeconds = actionSeconds;
            this.MaxActionTimes = maxActionTimes;
        }
    }
}
