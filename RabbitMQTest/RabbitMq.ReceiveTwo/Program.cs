using System;
using RabbitMq.Common;
using RabbitMq.Helper;
using Newtonsoft.Json;
using RabbitMq.Model;
namespace RabbitMq.ReceiveTwo
{
    class Program
    {
        static void Main(string[] args)
        {

            #region  发布订阅模式

            var query = new RabbitMQReceive();
            query.SubscriptionMqMsg<Date>(ConsoleWirte, ConstDefind.RABBITMQEXANGE_FIRST);

            #endregion

            Console.Read();
        }

        static void ConsoleWirte(Date data)
        {
            Console.WriteLine("接收到订阅消息:" + JsonConvert.SerializeObject(data));
        }
    }
}
