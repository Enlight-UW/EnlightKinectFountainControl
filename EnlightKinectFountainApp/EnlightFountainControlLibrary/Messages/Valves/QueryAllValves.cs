using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnlightFountainControlLibrary.Messages.Valves
{
    public class QueryAllValves : Message
    {
        public override string Url
        {
            get { return "/valves"; }
        }
    }
}
