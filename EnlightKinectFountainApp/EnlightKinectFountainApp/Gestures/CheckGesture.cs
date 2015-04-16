using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnlightKinectFountainApp.Gestures
{
    class CheckGesture :Gesture
    {
        protected override void InitializeGestureSequence()
        {
            sequence = new EnlightKinectTileButton[4];

            sequence[0] = new EnlightKinectTileButton(0, 350);
            sequence[1] = new EnlightKinectTileButton(150, 600);
            sequence[2] = new EnlightKinectTileButton(450, 400);
            sequence[3] = new EnlightKinectTileButton(750, 200);
        }
    }
}
