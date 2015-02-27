using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace EnlightFountainControlLibrary.Models
{
    [JsonObject(MemberSerialization.OptIn)]
    public class FountainValveStatusList : ServerBaseModel, IEnlightModelSerializer<FountainValveStatusList>
    {
        [JsonProperty]
        private List<FountainValveStatusList> items;

        public FountainValveStatusList() : this(null)
        {
        }

        [JsonConstructor]
        public FountainValveStatusList(List<FountainValveStatusList> items)
        {
            this.items = items;
        }

        public List<FountainValveStatusList> Items
        {
            get { return items; }
        }

        public new FountainValveStatusList FromJson<FountainValveStatusList>(string json)
        {
            return JsonConvert.DeserializeObject<FountainValveStatusList>(json, settings);
        }
    }
}
