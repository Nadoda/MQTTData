using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using uPLibrary.Networking.M2Mqtt;

namespace MQTTData
{
    public class Publish
    {
       public static MqttClient? client;
        public static string? topic { get; set; } = "Position_topic";
        public Publish()
        {
            client = new uPLibrary.Networking.M2Mqtt.MqttClient("test.mosquitto.org");
            string clientId = Guid.NewGuid().ToString();
            client.Connect(clientId);
            //topic = "Position_topic";
        }

        public void PublishToUI(object[] data)
        {
            client.Publish(topic, Encoding.UTF8.GetBytes(data.ToString()));
        }
    }
}
