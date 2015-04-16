using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnlightKinectFountainApp.Gestures
{
    public class WGesture : Gesture
    {
        protected override void InitializeGestureSequence()
        {
            sequence = new EnlightKinectTileButton[5];

            sequence[0] = new EnlightKinectTileButton(75, 75);
            sequence[1] = new EnlightKinectTileButton(150, 600);
            sequence[2] = new EnlightKinectTileButton(300, 75);
            sequence[3] = new EnlightKinectTileButton(450, 600);
            sequence[4] = new EnlightKinectTileButton(600, 75);
        }
    }
}
