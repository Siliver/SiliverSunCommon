using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace SiliverSun.Tools.TimerUtil
{
    /// <summary>
    /// 自定义Timer 事件组
    /// </summary>
    public class TimerEventsHandler
    {
        /// <summary>
        /// 私有Timer事件列表
        /// </summary>
        private EventHandlerList _timereventhandlist;

        /// <summary>
        /// Timer事件列表
        /// </summary>
        public EventHandlerList timereventhandlist { get => _timereventhandlist ?? new EventHandlerList(); set => _timereventhandlist = value; }

        /// <summary>
        /// 默认的初始化句柄
        /// </summary>
        public TimerEventsHandler(){
            if (_timereventhandlist == null) {
                _timereventhandlist= new EventHandlerList();
            }
        }

        /// <summary>
        /// 添加事件，重复则覆盖
        /// </summary>
        /// <param name="key"></param>
        /// <param name="func"></param>
        public void PutEvents(string key, Delegate func){
            if (func != null && !string.IsNullOrWhiteSpace(key))
            {
                //判断是否为空
                if (_timereventhandlist == null)
                {
                    _timereventhandlist.AddHandler(key, func);
                }
                else {
                    if (_timereventhandlist[key] != null)
                    {
                        _timereventhandlist.RemoveHandler(key, _timereventhandlist[key]);
                        
                    }
                    _timereventhandlist.AddHandler(key, func);
                }
            }
        }
    }
}
