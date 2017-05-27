using Newtonsoft.Json;
using OrdersCollector.Utils.Serialization;

namespace OrdersCollector.EventStore.IntegrationTests
{
    public class TestJsonSerializer : IJsonSerializer
    {
        public T Deserialize<T>(string json)
        {
            return (T)JsonConvert.DeserializeObject(json, typeof(TestEvent));
        }

        public string Serialize(object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }
    }
}