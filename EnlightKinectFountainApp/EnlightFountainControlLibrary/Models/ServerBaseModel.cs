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
        private bool success;

        public ServerBaseModel() : this(false)
        {
        }

        [JsonConstructor]
        public ServerBaseModel(bool success)
        {
            this.success = success;
        }

        public bool Success
        {
            get { return success; }
        }

        public new ServerBaseModel FromJson<ServerBaseModel>(string json)
        {
            return JsonConvert.DeserializeObject<ServerBaseModel>(json, settings);
        }
    }
}
