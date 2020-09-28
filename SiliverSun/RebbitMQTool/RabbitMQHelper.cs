using Microsoft.EntityFrameworkCore.Metadata.Internal;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;

namespace SiliverSun.RebbitMQTool
{
    /// <summary>
    /// RabbitMQ 帮助类
    /// </summary>
    public class RabbitMQHelper
    {
        private ConnectionFactory factory = new ConnectionFactory();

        private string _hostname = "localhost";
        private string _username = "guest";
        private string _password = "guest";

        /// <summary>
        /// 进行帮助类的创建
        /// </summary>
        /// <param name="hostname">服务器地址</param>
        /// <param name="username">用户名称</param>
        /// <param name="password">用户密码</param>
        public RabbitMQHelper(string hostname,string username,string password) {
            factory.HostName = hostname ?? _hostname;
            factory.UserName = username ?? _username;
            factory.Password = password ?? _password;
        }

        /// <summary>
        /// 像消息队列中发送消息
        /// </summary>
        public void SendMessage() {
            //连接服务器，即创建终节点
            using var connection = factory.CreateConnection();
            //创建一个channel通道，包含多个队列Queue
            using var channel = connection.CreateModel();
            //声明一个名为testQueue的消息队列
            channel.QueueDeclare("testQueue", false, false, false, null);
            //构造一个完全空的内容头，以便与基本内容类一起使用
            var properties = channel.CreateBasicProperties();
            //配置信息 1 非持久性 2持久性
            properties.DeliveryMode = 1;
            //要传递的消息内容
            string message = "test RabbitMQ connection";
            channel.BasicPublish("", "testQueue", false, properties, Encoding.UTF8.GetBytes(message));
        }

        /// <summary>
        /// 获取RabbitMQ队列中的数据
        /// </summary>
        /// <returns></returns>
        public string GetMessage() {
            //连接服务器，即创建终节点
            using var connection = factory.CreateConnection();
            //创建一个channel通道，包含多个队列Queue
            using var channel = connection.CreateModel();
            //声明一个名为testQueue的消息队列
            channel.QueueDeclare("testQueue", false, false, false, null);
            //创建一个消费者，=》委托事件
            var consumer = new EventingBasicConsumer(channel);
            //创建要输出的信息
            string message = String.Empty;

            consumer.Received += (model, ea) =>
            {
                var body = ea.Body;
                message = Encoding.UTF8.GetString(body.ToArray());
            };

            return message;
        }
    }
}
