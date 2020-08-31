using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SiliverSun.CustomAttribute
{
    public class AnalyseCustomAttribute
    {
        /// <summary>
        /// 根据自定义的描述进行更新语句的拼接
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        /// <returns></returns>
        public static string UpdateModelToString<T>(T item)
        {
            var type = item.GetType();
            var assembly = Assembly.GetAssembly(type);
            Attribute supportAttribute = assembly.GetCustomAttribute(type);
            //反射获取该类型所有的内容
            var typeinfos = type.GetTypeInfo();
            try
            {
                // 进行反射获取内容对象
                List<Attribute> hotelmodelattributes = typeinfos.GetCustomAttributes()?.ToList();

                if (hotelmodelattributes.Count() == 0)
                {
                    return pingjie(typeinfos);
                }
                else
                {
                    //找到酒店模板描述
                    var onehotelattr = hotelmodelattributes.Find(t => t.GetType() == typeof(ModelAttribute)) as ModelAttribute;
                    if (onehotelattr.VerifiedClass)
                    {
                        return pingjiewithattr(typeinfos);
                    }
                    else
                    {
                        return pingjie(typeinfos);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }

            //进行字符串拼接的本地函数
            string pingjiewithattr(TypeInfo typeinfo2)
            {
                Dictionary<string,object> result = new Dictionary<string, object>();
                foreach (var item2 in typeinfo2.GetProperties())
                {
                    if (item2.GetCustomAttributes().ToList()?.Find(t => t.GetType() == typeof(ModelAttribute)) is ModelAttribute proattr)
                    {
                        if (proattr.Canupdate)
                        {
                            result.Add(item2.Name, item2.GetValue(item));
                        }
                    }
                    else
                    {
                        result.Add(item2.Name, item2.GetValue(item));
                    }
                }
                return result.ToString();
            }

            string pingjie(TypeInfo typeinfo2)
            {
                Dictionary<string, object> result = new Dictionary<string, object>();
                foreach (var item2 in typeinfo2.GetProperties())
                {
                    result.Add(item2.Name, item2.GetValue(item));
                }
                return result.ToString();
            }
        }

        /// <summary>
        /// 进行实体不能为空字段的判断
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        /// <returns></returns>
        public static bool JudgeEmpty<T>(T item, out string message)
        {
            var type = item.GetType();
            var assembly = Assembly.GetAssembly(type);
            Attribute supportAttribute = assembly.GetCustomAttribute(typeof(ModelAttribute));
            //反射获取该类型所有的内容
            var typeinfos = type.GetTypeInfo();
            //进行反射获取内容对象
            List<Attribute> hotelmodelattributes = typeinfos.GetCustomAttributes().ToList();

            if (hotelmodelattributes.Count() == 0)
            {
                message = "不用检测";
                return true;
            }
            else
            {
                //找到酒店模板描述
                var onehotelattr = hotelmodelattributes.Find(t => t.GetType() == typeof(ModelAttribute)) as ModelAttribute;
                if (!onehotelattr.VerifiedClass)
                {
                    message = "不用检测";
                    return true;
                }
                else
                {
                    message = string.Empty;
                    string returnmessage = string.Empty;
                    message = judge(typeinfos, returnmessage);
                    return string.IsNullOrWhiteSpace(message);
                }
            }

            string judge(TypeInfo typeinfo2, string returnmessage)
            {
                var allflag = true;
                Dictionary<string, object> result = new Dictionary<string, object>();
                foreach (var item2 in typeinfo2.GetProperties())
                {
                    if (item2.GetCustomAttributes().ToList()?.Find(t => t.GetType() == typeof(ModelAttribute)) is ModelAttribute proattr)
                    {
                        //进行列表循环判断
                        if (item2.PropertyType.IsArray)
                        {
                            if (item2.GetValue(item) != null)
                            {
                                var arraytype = item2.GetValue(item).GetType();
                                if (arraytype.IsClass)
                                {
                                    var flag2 = judge(arraytype.GetTypeInfo(), returnmessage);
                                    allflag = string.IsNullOrWhiteSpace(flag2) && allflag;
                                }
                            }
                        }
                        else if (item2.PropertyType.IsPrimitive)//进行属性类型为class时的嵌套验证
                        {
                            var flag2 = judge(item2.PropertyType.GetTypeInfo(), returnmessage);
                            allflag = string.IsNullOrWhiteSpace(flag2) && allflag;
                        }
                        if (!proattr.Canempty && item2.GetValue(item) == null)
                        {
                            returnmessage = !string.IsNullOrWhiteSpace(proattr.Message) ? $"{item2.Name}不能为空！" : "有信息为空！";
                            return returnmessage;
                        }
                    }
                }
                if (allflag)
                {
                    return returnmessage;
                }
                else
                {
                    return null;
                }
            }

        }
    }
}
