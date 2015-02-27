using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace EnlightFountainControlLibrary.Models
{
    public class QueryControlResponse : ServerBaseModel, IEnlightModelSerializer<QueryControlResponse>
    {
        private int trueQueuePosition;
        private int eta;

        public QueryControlResponse() : this(0, 0)
        {
        }

        [JsonConstructor]
        public QueryControlResponse(int trueQueuePosition, int eta)
        {
            this.trueQueuePosition = trueQueuePosition;
            this.eta = eta;
        }

        public new QueryControlResponse FromJson<QueryControlResponse>(string json)
        {
            return JsonConvert.DeserializeObject<QueryControlResponse>(json, settings);
        }
    }
}
