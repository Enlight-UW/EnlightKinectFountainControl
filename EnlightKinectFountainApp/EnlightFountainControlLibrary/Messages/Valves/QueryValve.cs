using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnlightFountainControlLibrary.Messages.Valves
{
    public class QueryValve : Message
    {
        private int valveId;

        public QueryValve(int valveId)
        {
            this.valveId = valveId;
        }

        public override string Url
        {
            get { return "/valves/" + valveId; }
        }
    }
}
