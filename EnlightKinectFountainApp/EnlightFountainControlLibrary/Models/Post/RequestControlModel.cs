using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace EnlightFountainControlLibrary.Models
{
    [JsonObject(MemberSerialization.OptIn)]
    public class RequestControlModel : PostMessageModel, IEnlightModelSerializer<RequestControlModel>
    {
        [JsonProperty]
        private int requestedLength;

        public RequestControlModel() : this(0)
        {
        }

        public RequestControlModel(int requestedLength) : base(null)
        {
            this.requestedLength = requestedLength;
        }

        public int RequestedLength
        {
            get { return requestedLength; }
            set { requestedLength = value; }
        }
    }
}
