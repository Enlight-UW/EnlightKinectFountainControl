using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using EnlightFountainControlLibrary.Models.Post;

namespace EnlightFountainControlLibrary.Messages.Control
{
    public class ReleaseControl : PostMessage
    {
        public ReleaseControl(int controllerId)
        {
            model = new ControllerModel(controllerId);
        }

        public override string Url
        {
            get { return "/control/release"; }
        }
    }
}
