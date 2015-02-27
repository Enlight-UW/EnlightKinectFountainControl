using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace EnlightFountainControlLibrary.Models
{
    [JsonObject(MemberSerialization.OptIn)]
    public class FountainValveStatus : BaseModel, IEnlightModelSerializer<FountainValveStatus>
    {
        [JsonProperty]
        private int ID;

        [JsonProperty]
        private string name;

        [JsonProperty]
        private int spraying;

        [JsonProperty]
        private int enabled;

        public FountainValveStatus() : this(0, null, 0, 0)
        {
        }
        
        [JsonConstructor]
        public FountainValveStatus(int ID, string name, int spraying, int enabled)
        {
            this.ID = ID;
            this.name = name;
            this.spraying = spraying;
            this.enabled = enabled;
        }

        public int ValveID
        {
            get { return ID; }
        }

        public string Name
        {
            get { return name; }
        }

        public bool Spraying
        {
            get { return spraying == 1; }
        }

        public bool Enabled
        {
            get { return enabled == 1; }
        }

        public new FountainValveStatus FromJson<FountainValveStatus>(string json)
        {
            return JsonConvert.DeserializeObject<FountainValveStatus>(json, settings);
        }
    }
}
