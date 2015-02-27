using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace EnlightFountainControlLibrary.Models
{
    [JsonObject(MemberSerialization.OptIn)]
    public class FountainPatternList : ServerBaseModel, IEnlightModelSerializer<FountainPatternList>
    {
        [JsonProperty]
        private List<FountainPattern> items;

        public FountainPatternList() : this(null)
        {
        }

        [JsonConstructor]
        public FountainPatternList(List<FountainPattern> items)
        {
            this.items = items;
        }

        public List<FountainPattern> Items
        {
            get { return items; }
        }

        public new FountainPatternList FromJson<FountainPatternList>(string json)
        {
            return JsonConvert.DeserializeObject<FountainPatternList>(json, settings);
        }
    }
}
