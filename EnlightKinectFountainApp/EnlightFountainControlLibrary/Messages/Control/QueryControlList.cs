using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using EnlightFountainControlLibrary.Models;

namespace EnlightFountainControlLibrary.Messages.Control
{
    public class QueryControlList : Message
    {
        public override string Url
        {
            get { return "/control/query"; }
        }
    }
}
