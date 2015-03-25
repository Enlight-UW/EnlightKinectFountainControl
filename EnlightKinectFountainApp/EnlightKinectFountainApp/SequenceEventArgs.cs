using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Kinect.Toolkit.Controls;

namespace EnlightKinectFountainApp
{
    public class SequenceEventArgs : EventArgs
    {
        public SequenceEventArgs(EnlightKinectTileButton button, bool complete)
        {
            this.NextButton = button;
            this.SequenceComplete = complete;
        }

        public EnlightKinectTileButton NextButton
        {
            get;
            set;
        }

        public bool SequenceComplete
        {
            get;
            set;
        }
    }
}
