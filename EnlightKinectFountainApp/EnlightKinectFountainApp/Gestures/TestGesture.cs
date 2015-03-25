using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnlightKinectFountainApp.Gestures
{
    public class TestGesture : Gesture
    {
        protected override void InitializeGestureSequence()
        {
            sequence = new EnlightKinectTileButton[2];

            sequence[0] = new EnlightKinectTileButton(100, 50);
            sequence[1] = new EnlightKinectTileButton(400, 100);
        }
    }
}
