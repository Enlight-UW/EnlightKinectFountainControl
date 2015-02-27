using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace EnlightFountainControlLibrary.Models
{
    public class RequestControlResponse : ServerBaseModel, IEnlightModelSerializer<RequestControlResponse>
    {
        private int ttl;
        private int controllerID;

        public RequestControlResponse() : this(0, 0)
        {
        }

        [JsonConstructor]
        public RequestControlResponse(int ttl, int controllerID)
        {
            this.ttl = ttl;
            this.controllerID = controllerID;
        }

        public int TimeToLive
        {
            get { return ttl; }
        }

        public int ControllerID
        {
            get { return controllerID; }
        }

        public new RequestControlResponse FromJson<RequestControlResponse>(string json)
        {
            return JsonConvert.DeserializeObject<RequestControlResponse>(json, settings);
        }
    }
}