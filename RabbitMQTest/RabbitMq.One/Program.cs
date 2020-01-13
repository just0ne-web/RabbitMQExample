using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMq.Common;
using RabbitMq.Helper;
using Newtonsoft.Json;
using RabbitMq.Model;

namespace RabbitMq.Receive
{
    class Program
    {
        static void Main(string[] args)
        {
            #region 一对一确认模式

            //var query = new RabbitMQReceive();
            //query.BindReceiveMqMsg<Date>(Perating, ConstDefind.RABBITMQNAME_FIRST);

            #endregion

            #region  发布订阅模式

            var query = new RabbitMQReceive();
            query.SubscriptionMqMsg<Date>(ConsoleWirte, ConstDefind.RABBITMQEXANGE_FIRST);

            #endregion

            Console.Read();
        }

        static bool Perating(Date data)
        {

            Console.WriteLine(JsonConvert.SerializeObject(data));
            return true;
        }


        static void ConsoleWirte(Date data)
        {
            Console.WriteLine("接收到订阅消息:"+ JsonConvert.SerializeObject(data));
        }
    }



}
