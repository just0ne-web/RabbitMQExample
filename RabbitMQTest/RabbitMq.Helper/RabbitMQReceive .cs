using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMq.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using RabbitMQ.Client.Events;

namespace RabbitMq.Helper
{

    public class RabbitMQReceive : IDisposable
    {
        IConnection connection = null;
        IModel channel = null;

        public void Dispose()
        {
            if (channel != null)
                channel.Close();
            if (connection != null)
                connection.Close();
        }

        #region  简单模式(路由模式)

        /// <summary>
        /// 监控队列事件
        /// </summary>
        /// <typeparam name="T">添加进队列的值</typeparam>
        /// <param name="func">事件</param>
        /// <param name="log">日志</param>
        /// <param name="queueName">对列名</param>
        public void BindReceiveMqMsg<T>(Func<T, bool> func, string queueName)
        {
            connection = RabbitMQConnect.factory.CreateConnection();
            channel = connection.CreateModel();
            channel.QueueDeclare(queue: queueName,
                             durable: true,
                             exclusive: false,
                             autoDelete: false,
                             arguments: null);

            channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body;
                var message = Encoding.UTF8.GetString(body);
                var item = JsonConvert.DeserializeObject<T>(message);
                var result = func(item);
                if (result)
                    channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false); //手动确认
                else
                    channel.BasicNack(deliveryTag: ea.DeliveryTag, multiple: false, requeue: false);//手动确认 requeue 是否重新发送
            };
            channel.BasicConsume(queue: queueName,
                                 autoAck: false, //是否自动确认
                                 consumer: consumer);
        }



        #endregion

        #region  发布订阅模式

        /// <summary>
        /// 订阅消息
        /// 订阅不需要服务端确认
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="action">执行的事件</param>
        /// <param name="exchange">交换机名称(频道)</param>
        public void SubscriptionMqMsg<T>(Action<T> action, string exchange)
        {
            connection = RabbitMQConnect.factory.CreateConnection();
            channel = connection.CreateModel();
            channel.ExchangeDeclare(exchange: exchange, type: "fanout", durable: true);
            string queueName = channel.QueueDeclare().QueueName;//自动挂载一个队列名称
            channel.QueueBind(queueName, exchange, "");//绑定到频道
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body;
                var message = Encoding.UTF8.GetString(body);
                T data = JsonConvert.DeserializeObject<T>(message);
                action(data);
            };
            channel.BasicConsume(queue: queueName,
                                 autoAck: true,//自动确认
                                 consumer: consumer);
        }


        #endregion
    }

}
