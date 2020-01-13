using Newtonsoft.Json.Converters;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMq.Helper
{
    public class RabbitMQSend
    {
        static IsoDateTimeConverter dtConverter = new IsoDateTimeConverter { DateTimeFormat = "yyyy-MM-dd HH:mm:ss" };
        static IConnection connection = null;
        static RabbitMQSend()
        {
            var factory = RabbitMQConnect.factory;
            connection = factory.CreateConnection();
        }

        #region  简单模式(路由模式)

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        /// <param name="queueName"></param>
        public static void PushMsgToMq<T>(T item, string queueName)
        {
            var msg = Newtonsoft.Json.JsonConvert.SerializeObject(item, dtConverter);
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: queueName,
                                     durable: true, //是否持久化
                                     exclusive: false,//是否排他 设置为是 队列仅对首次声明它的连接可见，并在连接断开时自动删除
                                     autoDelete: false,//是否自动删除
                                     arguments: null);//其他参数 
                var properties = channel.CreateBasicProperties();
                properties.Persistent = true;
                var body = Encoding.UTF8.GetBytes(msg);
                channel.BasicPublish(exchange: "", //交换机
                                     routingKey: queueName,//队列名
                                     basicProperties: properties,//未知 rpc 
                                     body: body);//内容
            }
        }

        #endregion

        #region  发布订阅模式

        /// <summary>
        /// 发布消息
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="result">消息内容</param>
        /// <param name="exchange">绑定的交换机(相当于频道)</param>
        public static void ReleaseToMq<T>(T result, string exchange)
        {
            var msg = Newtonsoft.Json.JsonConvert.SerializeObject(result, dtConverter);//消息
            using (var channel = connection.CreateModel())
            {
                channel.ExchangeDeclare(
                      exchange: exchange, // 交换机 名称
                      type: "fanout", //类型 默认为发布订阅模式
                      durable: true, //是否持久化
                      autoDelete: false, //是否自动删除
                      arguments: null);//拓展参数
                Console.WriteLine("发布消息:" + msg);
                channel.BasicPublish(exchange, "", null, Encoding.UTF8.GetBytes(msg));//发布消息
            }
        }

        #endregion
    }
}
