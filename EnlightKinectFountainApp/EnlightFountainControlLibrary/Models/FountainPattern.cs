using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace EnlightFountainControlLibrary.Models
{
    [JsonObject(MemberSerialization.OptIn)]
    public class FountainPattern : BaseModel, IEnlightModelSerializer<FountainPattern>
    {
        [JsonProperty]
        private int ID;

        [JsonProperty]
        private string name;

        [JsonProperty]
        private int active;

        public FountainPattern() : this(0, null, 0)
        {
        }

        [JsonConstructor]
        public FountainPattern(int ID, string name, int active)
        {
            this.ID = ID;
            this.name = name;
            this.active = active;
        }

        public int PatternID
        {
            get { return ID; }
        }

        public string Name
        {
            get { return name; }
        }

        public bool Active
        {
            get { return active == 1; }
        }
    }
}
