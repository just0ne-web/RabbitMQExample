using System;
using RabbitMq.Common;
using RabbitMq.Helper;
using RabbitMq.Model;

namespace RabbitMq.Send
{
    class Program
    {
        static void Main(string[] args)
        {
            #region  一对一确认模式

            //System.Timers.Timer timer = new System.Timers.Timer(2000);
            //timer.Elapsed += (o, s) =>
            //{
            //    RabbitMQSend.PushMsgToMq(new Date { Message = "Hellow", Time = DateTime.Now }, ConstDefind.RABBITMQNAME_FIRST);
            //};
            //timer.Enabled = true;
            //timer.Start();

            #endregion

            #region  发布订阅

            System.Timers.Timer timer = new System.Timers.Timer(1000);
            timer.Elapsed += (o, s) =>
            {
                RabbitMQSend.ReleaseToMq(new Date { Message = "Hellow", Time = DateTime.Now }, ConstDefind.RABBITMQEXANGE_FIRST);
            };
            timer.Enabled = true;
            timer.Start();

            #endregion

            Console.Read();
        }
    }


}
