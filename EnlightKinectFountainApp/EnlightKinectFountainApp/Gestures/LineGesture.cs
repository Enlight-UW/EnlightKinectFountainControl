using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnlightKinectFountainApp.Gestures
{
    class LineGesture : Gesture
    {
        protected override void InitializeGestureSequence()
        {
            sequence = new EnlightKinectTileButton[5];

            sequence[0] = new EnlightKinectTileButton(0, 600);
            sequence[1] = new EnlightKinectTileButton(0, 300);
            sequence[2] = new EnlightKinectTileButton(0, 0);
            sequence[3] = new EnlightKinectTileButton(300, 0);
            sequence[4] = new EnlightKinectTileButton(600, 0);
        }
    }
}
