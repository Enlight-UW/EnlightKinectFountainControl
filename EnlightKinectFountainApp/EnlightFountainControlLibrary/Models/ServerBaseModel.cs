using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace EnlightFountainControlLibrary.Models
{
    [JsonObject(MemberSerialization.OptIn)]
    public class ServerBaseModel : BaseModel, IEnlightModelSerializer<ServerBaseModel>
    {
        [JsonProperty]
        protected bool success;

        public ServerBaseModel() : this(false)
        {
        }

        [JsonConstructor]
        public ServerBaseModel(bool success)
        {
            this.success = success;
        }

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }

        public EnlightBaseModel FromJson<EnlightBaseModel>(string json)
        {
            return JsonConvert.DeserializeObject<EnlightBaseModel>(json, settings);
        }
    }

}
