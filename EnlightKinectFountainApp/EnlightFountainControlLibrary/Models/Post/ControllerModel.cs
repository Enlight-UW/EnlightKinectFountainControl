using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace EnlightFountainControlLibrary.Models.Post
{
    [JsonObject(MemberSerialization.OptIn)]
    public class ControllerModel : PostMessageModel
    {
        [JsonProperty]
        private int controllerId;

        public ControllerModel()
        {
        }

        public ControllerModel(int controllerId)
        {
            this.controllerId = controllerId;
        }

        public int ControllerID
        {
            get { return controllerId; }
            set { controllerId = value; }
        }
    }
}
