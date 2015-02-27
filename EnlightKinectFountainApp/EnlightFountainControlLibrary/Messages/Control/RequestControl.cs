using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using EnlightFountainControlLibrary.Models;

namespace EnlightFountainControlLibrary.Messages.Control
{
    public class RequestControl : PostMessage
    {
        public RequestControl(int requestedLength)
        {
            model = new RequestControlModel(requestedLength);
        }

        public override string Url
        {
            get { return "/control/request"; }
        }
    }
}
