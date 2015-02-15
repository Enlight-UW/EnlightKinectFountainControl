using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnlightFountainControlLibrary.Messages.Pattern
{
    public class QueryAllPatterns : Message
    {
        public override string Url
        {
            get { return "/patterns"; }
        }
    }
}
