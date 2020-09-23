using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace SiliverSun.encryptionTool
{
    /// <summary>
    /// 取自https://www.cnblogs.com/weiweixiang/p/10102950.html
    /// </summary>
    public class Des3Helper
    {
        /// <summary>
        /// 密钥长度
        /// </summary>
        private static readonly string _key = "3dSXKs1oBSzbG@t!AIo#D5cx";
        /// <summary>
        /// 偏移量
        /// </summary>
        private static readonly string _Vector = "Q9Fvi$SO";

        /// <summary>
        /// 3DES加密
        /// 密码：默认
        /// 偏移向量：默认
        /// 模式：CBC
        /// 填充：PKCS7
        /// 输出：Base64
        /// 编码：UTF8
        /// </summary>
        /// <param name="plaintext">明文</param>
        /// <returns>大写的密文</returns>
        //public static string Encrypt(string plaintext):base() {
        //    return Encrypt(plaintext, _key, _Vector);
        //}

        /// <summary>
        /// 3DES加密
        /// 密码：默认
        /// 偏移量：默认
        /// 模式：CBC
        /// 填充：PKCS7
        /// 输出：自定义
        /// 编码：UTF8
        /// </summary>
        /// <param name="plaintext">明文</param>
        /// <param name="method">输出编码方式</param>
        /// <returns>大写的密文</returns>
        //public static string Encrypt(string plaintext, OutputMethod method) {
        //    return Encrypt(plaintext, _key, _Vector, method);
        //}

    //    public static string Encrypt(string plaintext, string key) {
    //        return null;
    //    }
    //}
}
