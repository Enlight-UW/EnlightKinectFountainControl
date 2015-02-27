using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace EnlightFountainControlLibrary.Models
{
    [JsonObject(MemberSerialization.OptIn)]
    public class FountainController : BaseModel, IEnlightModelSerializer<FountainController>
    {
        [JsonProperty]
        private int controllerID;

        [JsonProperty]
        private int acquire;

        [JsonProperty]
        private int ttl;

        [JsonProperty]
        private int priority;

        [JsonProperty]
        private int queuePosition;

        public FountainController() : this(0, 0, 0, 0, 0)
        {
        }

        [JsonConstructor]
        public FountainController(int controllerID, int acquire, int ttl, int priority, int queuePosition)
        {
            this.controllerID = controllerID;
            this.acquire = acquire;
            this.ttl = ttl;
            this.priority = priority;
            this.queuePosition = queuePosition;
        }

        public int ControllerID
        {
            get { return controllerID; }
        }

        public int Acquire
        {
            get { return acquire; }
        }

        public int TimeToLive
        {
            get { return ttl; }
        }

        public int Priority
        {
            get { return priority; }
        }

        public int QueuePosition
        {
            get { return queuePosition; }
        }

        public new EnlightController FromJson<EnlightController>(string json)
        {
            return JsonConvert.DeserializeObject<EnlightController>(json, settings);
        }
    }
}
