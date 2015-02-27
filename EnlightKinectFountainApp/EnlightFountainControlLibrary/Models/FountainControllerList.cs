using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace EnlightFountainControlLibrary.Models
{
    [JsonObject(MemberSerialization.OptIn)]
    public class FountainControllerList : ServerBaseModel, IEnlightModelSerializer<FountainControllerList>
    {
        [JsonProperty]
        private List<FountainController> items;

        public FountainControllerList() : this(null)
        {
        }

        [JsonConstructor]
        public FountainControllerList(List<FountainController> items)
        {
            this.items = items;
        }

        public List<FountainController> Items
        {
            get { return items; }
        }

        public new EnlightControllerList FromJson<EnlightControllerList>(string json)
        {
            return JsonConvert.DeserializeObject<EnlightControllerList>(json, settings);
        }
    }
}
