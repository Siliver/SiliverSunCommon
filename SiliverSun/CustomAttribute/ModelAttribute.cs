using System;

namespace SiliverSun.CustomAttribute
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class ModelAttribute : Attribute
    {
        /// <summary>
        /// 用于验证读取后字段是否可以为null
        /// </summary>
        public bool Canempty { get; set; } = false;

        /// <summary>
        /// 用于判断更新的时候该字段是否作为更新的额字段出现
        /// </summary>
        public bool Canupdate { get; set; } = true;

        /// <summary>
        /// 是否需要验证的实体类
        /// </summary>
        public bool VerifiedClass { get; set; } = false;

        /// <summary>
        /// 是否验证值类型是否可以为0
        /// </summary>
        public bool Canzero { get; set; }

        /// <summary>
        /// 实体字段注释
        /// </summary>
        public string Message { get; set; }
    }
}
