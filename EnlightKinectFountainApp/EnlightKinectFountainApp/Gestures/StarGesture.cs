using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnlightKinectFountainApp.Gestures
{
    class StarGesture : Gesture
    {
        protected override void InitializeGestureSequence()
        {
            sequence = new EnlightKinectTileButton[6];

            sequence[0] = new EnlightKinectTileButton(0, 600);
            sequence[1] = new EnlightKinectTileButton(250, 75);
            sequence[2] = new EnlightKinectTileButton(500, 600);
            sequence[3] = new EnlightKinectTileButton(75, 335);
            sequence[4] = new EnlightKinectTileButton(600, 335);
            sequence[5] = new EnlightKinectTileButton(0, 600);
        }
    }
}
