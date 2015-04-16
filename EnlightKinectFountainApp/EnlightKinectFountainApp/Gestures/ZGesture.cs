using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnlightKinectFountainApp.Gestures
{
    class ZGesture : Gesture
    {
        protected override void InitializeGestureSequence()
        {
            sequence = new EnlightKinectTileButton[5];

            sequence[0] = new EnlightKinectTileButton(0, 150);
            sequence[1] = new EnlightKinectTileButton(600, 150);
            sequence[2] = new EnlightKinectTileButton(300, 300);
            sequence[3] = new EnlightKinectTileButton(0, 450);
            sequence[4] = new EnlightKinectTileButton(600, 450);
        }
    }
}
