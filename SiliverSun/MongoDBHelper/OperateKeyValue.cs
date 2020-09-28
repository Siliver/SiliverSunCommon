using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace SiliverSun.MongoDBHelper
{
    /// <summary>
    /// 创建MongoDB条件帮助类
    /// </summary>
    /// <typeparam name="Tkey">操作类型</typeparam>
    /// <typeparam name="TValue">操作键值对</typeparam>
    public class OperateKeyValue : Dictionary<string, Dictionary<MongoCnd, object>>, ICloneable
    {
        /// <summary>
        /// 索引器
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public Object this[int index] {
            get { return Get(index); }
            set {
                Set(index, value);
            }
        }

        /// <summary>
        /// 默认的构造函数
        /// </summary>
        public OperateKeyValue() { }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="key"></param>
        /// <param name="cnd"></param>
        /// <param name="obj"></param>
        public OperateKeyValue(string key, MongoCnd cnd, object obj) {
            var mongodata = new Dictionary<MongoCnd, object>();
            mongodata.Add(cnd, obj);
            this.Set(key, mongodata);
        }

        /// <summary>
        /// 重写Get
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public Dictionary<MongoCnd, object> Get(int index) {
            int i = 0;
            foreach (var key in Keys) {
                if (i.Equals(index))
                {
                    return this[key];
                }
                else {
                    i++;
                }
            }
            return null;
        }

        /// <summary>
        /// 重写Set
        /// </summary>
        /// <param name="index"></param>
        /// <param name="obj"></param>
        public void Set(int index, object obj) {
            int i = 0;
            foreach (string key in Keys) {
                if (i.Equals(index))
                {
                    this[key] = (Dictionary<MongoCnd, object>)obj;
                }
                else
                {
                    i++;
                }
            }
        }

        /// <summary>
        /// 重写Set
        /// </summary>
        /// <param name="key"></param>
        /// <param name="obj"></param>
        public void Set(string key, object obj) {
            this[key] = (Dictionary<MongoCnd, object>)obj;
        }

        /// <summary>
        /// 进行深拷贝
        /// </summary>
        /// <returns></returns>
        public object Clone()
        {
            BinaryFormatter Formatter = new BinaryFormatter(null, new StreamingContext(StreamingContextStates.Clone));
            MemoryStream stream = new MemoryStream();
            Formatter.Serialize(stream, this);
            stream.Position = 0;
            object clonedObj = Formatter.Deserialize(stream);
            stream.Close();
            return clonedObj;
        }
    }
}
