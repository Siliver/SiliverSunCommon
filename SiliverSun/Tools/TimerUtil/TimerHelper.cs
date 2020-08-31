using SiliverSun.Tools.TimerModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;

namespace SiliverSun.Tool.TimerUtil
{
    /// <summary>
    /// Timer辅助类
    /// </summary>
    public class TimerHelper
    {
        /// <summary>
        /// 创建私有的Timer对象
        /// </summary>
        private Timer _timer;

        /// <summary>
        /// 公有的GET方法
        /// </summary>
        public Timer timer
        {
            get => _timer ?? new Timer();
        }

        /// <summary>
        /// 初始化构造函数
        /// </summary>
        public TimerHelper() {
            if (_timer == null)
            {
                _timer = new Timer();
            }
        }

        /// <summary>
        /// 默认计时器时间间隔1秒
        /// </summary>
        private double DefaultTimerInterval = 1 * 1000;

        /// <summary>
        /// 设置多个循环周期
        /// </summary>
        private List<TimeCycle> TimeCycleList { get; set; }

        /// <summary>
        /// 更新Timer执行间隔
        /// </summary>
        /// <param name="newinterval"></param>
        public void UpdateTimeInterval(double newinterval, bool isFirstStart = false)
        {
            //判断Timer对象和新的执行时间间隔
            if (_timer != null && newinterval > 0)
            {
                //停止计时器
                _timer.Stop();
                //修改计时器的执行时间
                if(_timer.Interval!= newinterval)
                {
                    _timer.Interval = newinterval;
                }
                
                //定时器事件添加(定时器可以自行执行，目前不添加执行方法)
                if (isFirstStart) {
                    _timer.Elapsed += new ElapsedEventHandler(CustomElapsedEventHandler);
                }
               
                //设置自动执行
                _timer.AutoReset = true;
                //启动计时器
                _timer.Start();
            }
        }

        /// <summary>
        /// 事件初始化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void CustomElapsedEventHandler(object sender, ElapsedEventArgs e)
        {
            //初始化循环周期
            List<TimeCycle> currentTimeCycleList = new List<TimeCycle>();

            DateTime now = DateTime.Now;

            foreach (var item in TimeCycleList) {
                //转换开始时间
                if (!DateTime.TryParse(item.BeginTime, out DateTime cycleBeginTime))
                {
                    break;
                }
                cycleBeginTime = now.Date.AddHours(cycleBeginTime.Hour).AddMinutes(cycleBeginTime.Minute).AddSeconds(cycleBeginTime.Second);

                if(!DateTime.TryParse(item.EndTime, out DateTime cycleEndTime))
                {
                    break;
                }
                //转换结束时间
                cycleEndTime = Convert.ToDateTime(item.EndTime);
                cycleEndTime = now.Date.AddHours(cycleEndTime.Hour).AddMinutes(cycleEndTime.Minute).AddSeconds(cycleEndTime.Second);

                //进行起始结束时间的判断(不符合结束时间加一天)
                if (cycleBeginTime < cycleEndTime) {
                    cycleEndTime.AddDays(1);
                }

                //进行当前时间的判断
                if (now >= cycleBeginTime && now <= cycleEndTime) {
                    //判断最大执行次数限制 或 没有限制
                    if (item.ActionExeccutionTimes < item.MaxActionTimes || item.MaxActionTimes == 0)
                    {
                        TimeSpan timeSpan = now - cycleBeginTime;
                        bool isCanAction = (int)timeSpan.TotalSeconds % item.ActionSeconds == 0;
                        //是否进行动作
                        if (isCanAction)
                        {
                            item.ActionExeccutionTimes++;
                            currentTimeCycleList.Add(item);
                        }
                    }
                }
                else
                {
                    //不在计时周期内，已执行的清零
                    item.ActionExeccutionTimes = 0;
                }
            }

            //找到当前循环周期后，执行周期内动作
            if (currentTimeCycleList.Count > 0)
            {
                currentTimeCycleList.ForEach(item =>
                {
                    //使用多线程执行任务，让代码快速执行
                    Task.Run(() => item.Action());
                });
            }
        }

        /// <summary>
        /// 开启计时器
        /// </summary>
        /// <param name="timeCycleArray"></param>
        public void Start(params TimeCycle[] timeCycleArray)
        {
            if (timeCycleArray != null && timeCycleArray.Length > 0)
            {
                if (TimeCycleList == null)
                {
                    TimeCycleList = new List<TimeCycle>(100);
                }
                TimeCycleList = timeCycleArray.ToList();

                //设置首次计时器周期（首次动作执行，是在计时器启动后在设置的时间间隔后做出的动作）
                UpdateTimeInterval(DefaultTimerInterval, true);
            }
        }

        /// <summary>
        /// 结束计时器
        /// </summary>
        public void Stop()
        {
            _timer.Stop();
        }
    }
}
