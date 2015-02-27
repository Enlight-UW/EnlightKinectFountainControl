using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace EnlightFountainControlLibrary.Models.Post
{
    [JsonObject(MemberSerialization.OptIn)]
    public class PatternControlModel : ControllerModel
    {
        [JsonProperty]
        private bool setCurrent;

        public PatternControlModel(int controllerId, bool enable) : base(controllerId)
        {
            setCurrent = enable;
        }

        public bool Enable
        {
            get { return setCurrent; }
        }
    }
}
