using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnlightKinectFountainApp.Gestures
{
    class WaveGesture : Gesture
    {
        protected override void InitializeGestureSequence()
        {
            sequence = new EnlightKinectTileButton[5];

            sequence[0] = new EnlightKinectTileButton(0, 350);
            sequence[1] = new EnlightKinectTileButton(150, 150);
            sequence[2] = new EnlightKinectTileButton(300, 0);
            sequence[3] = new EnlightKinectTileButton(450, 150);
            sequence[4] = new EnlightKinectTileButton(600, 300);
        }
    }
}
