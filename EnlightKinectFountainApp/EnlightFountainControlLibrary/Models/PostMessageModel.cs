using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace EnlightFountainControlLibrary.Models
{
    [JsonObject(MemberSerialization.OptIn)]
    public abstract class PostMessageModel : BaseModel
    {
        [JsonProperty]
        private string apikey;

        public PostMessageModel() : this(null)
        {
        }

        public PostMessageModel(string apikey)
        {
            this.apikey = apikey;
        }

        public string ApiKey
        {
            get { return apikey; }
            set { apikey = value; }
        }
    }
}
