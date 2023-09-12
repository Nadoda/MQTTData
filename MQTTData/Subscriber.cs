using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace MQTTData
{
    public class Subscriber
    {
        private readonly MqttClient client;
        public Subscriber()
        {
            
            client = new MqttClient("test.mosquitto.org");
            string clientId = Guid.NewGuid().ToString();
            client.Connect(clientId);
            client.Subscribe(new string[] { "goto_topic" }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });

            client.MqttMsgPublishReceived += FromUI;
        }

        public void FromUI(object sender, MqttMsgPublishEventArgs e)
        {
            var message = Encoding.UTF8.GetString(e.Message);
            var jsonObject = JsonConvert.DeserializeObject<dynamic>(message);
            Console.WriteLine(jsonObject);
            var NewPosition = new TrolleyData()
            {
                Identifier = jsonObject[4],
                X = jsonObject[6],
                Y = jsonObject[7],
                Z = jsonObject[8]
            };
            client.Publish("Position_topic", Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(NewPosition)));
            
        }
    }
}
