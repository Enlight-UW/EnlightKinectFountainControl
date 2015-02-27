using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace EnlightFountainControlLibrary.Models.Post
{
    [JsonObject(MemberSerialization.OptIn)]
    public class ValveControlModel : ControllerModel
    {
        [JsonProperty]
        private int spraying;

        public ValveControlModel(int controllerId, bool spraying) : base(controllerId)
        {
            Spraying = spraying;
        }

        public bool Spraying
        {
            get { return spraying == 1; }
            set { spraying = (value) ? 1 : 0; }
        }
    }
}
