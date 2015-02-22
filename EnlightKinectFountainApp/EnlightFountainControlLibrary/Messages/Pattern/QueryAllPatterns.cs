using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using EnlightFountainControlLibrary.Models;

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
