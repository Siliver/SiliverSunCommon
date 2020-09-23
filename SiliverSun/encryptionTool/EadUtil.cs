using System;
using System.Text;

namespace SiliverSun.encryptionTool
{
    internal class EadUtil
    {
        /// <summary>
        /// 根据字节数组和加密方式返回字符串
        /// </summary>
        /// <param name="bytes">字节数组</param>
        /// <param name="method">加密方法</param>
        /// <returns></returns>
        public static string Output(byte[] bytes,OutputMethod method=OutputMethod.Base64) {
            return method switch
            {
                OutputMethod.Base64 => Base64(bytes),
                OutputMethod.Hex => Hex(bytes),
                _ => "",
            };
        }

        /// <summary>
        /// 输入要加密的字符串
        /// </summary>
        /// <param name="ciphertext"></param>
        /// <param name="method"></param>
        /// <returns></returns>
        public static byte[] Input(string ciphertext,OutputMethod method=OutputMethod.Base64) {
            return method switch
            {
                OutputMethod.Base64 => Base64(ciphertext),
                OutputMethod.Hex => Hex(ciphertext),
                _ => null,
            };
        }

        /// <summary>
        /// 转换字节数组到Base64字符串
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        private static string Base64(byte[] bytes) {
            return Convert.ToBase64String(bytes);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        private static string Hex(byte[] bytes) {
            StringBuilder builder = new StringBuilder();
            foreach (var num in bytes) {
                builder.AppendFormat("{0:X2}", num);
            }
            return builder.ToString();
        }

        /// <summary>
        /// 字符串转换为64位字节数组
        /// </summary>
        /// <param name="ciphertext"></param>
        /// <returns></returns>
        private static byte[] Base64(string ciphertext) {
            return Convert.FromBase64String(ciphertext);
        }

        /// <summary>
        /// 字符转为16位字符数组
        /// </summary>
        /// <param name="ciphertext"></param>
        /// <returns></returns>
        private static byte[] Hex(string ciphertext) {
            byte[] buffer = new byte[ciphertext.Length / 2];
            for (int i = 0; i < (ciphertext.Length / 2); i++) {
                int num2 = Convert.ToInt32(ciphertext.Substring(i * 2, 2), 0x10);
                buffer[i] = (byte)num2;
            }
            return buffer;
        }
    }
}
