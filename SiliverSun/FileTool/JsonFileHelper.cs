using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;

namespace SiliverSun.FileTool
{
    /// <summary>
    /// JSON文件帮助类
    /// </summary>
    public class JsonFileHelper
    {
        /// <summary>
        /// Json文件读取
        /// </summary>
        /// <param name="filepath"></param>
        public ConcurrentDictionary<string, object> ReaderJsonFile(string filepath, string filename)
        {

            //判断要寻找的文件名称是不是空的
            if (string.IsNullOrWhiteSpace(filename))
            {
                return null;
            }

            #region 进行文件目录的获取
            //添加目录组
            List<string> paths = new List<string>();

            //添加
            if (!string.IsNullOrWhiteSpace(filepath))
            {
                paths.Add(filepath);
            }
            else {
                //如果地址为空，添加当前工作目录和应用根目录
                paths.Add(Path.Combine(Directory.GetCurrentDirectory(), filename));
                //添加根目录
                paths.Add(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, filename));
            }

            //判断文件路径是否正确，并获取文件
            if (!JudgeHasFile(out string truepath, paths.ToArray())) {
                return null;
            }

            #endregion

            #region 进行JSON文件的获取
            using StreamReader textReader = File.OpenText(truepath);
            using JsonTextReader jsonReader = new JsonTextReader(textReader) { CloseInput = true };
            //为确保获取到的文件是线程安全的 使用ConcurrentDictionary 类进行获取
            ConcurrentDictionary<string, object> resultdictionary = JsonConvert.DeserializeObject<ConcurrentDictionary<string, object>>(jsonReader.ReadAsString());
            return resultdictionary;
            #endregion
        }

        /// <summary>
        /// 通过传入的路径组，判断文件是否存在，并返回正确的文件路径
        /// </summary>
        /// <param name="truepath"></param>
        /// <param name="paths"></param>
        /// <returns></returns>
        private bool JudgeHasFile(out string truepath, params string[] paths) {
            truepath = string.Empty;
            if (paths != null && paths.Length > 0)
            {
                foreach (var item in paths) {
                    if (File.Exists(item)) {
                        truepath = item;
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
