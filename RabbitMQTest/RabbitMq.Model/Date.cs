using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMq.Model
{
    public class Date
    {

        [JsonProperty(PropertyName = "t")]
        public DateTime Time { get; set; }

        [JsonProperty(PropertyName = "m")]
        public string Message { get; set; }
    }
}
