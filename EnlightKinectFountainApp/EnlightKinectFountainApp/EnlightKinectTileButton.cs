using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Kinect.Toolkit.Controls;

namespace EnlightKinectFountainApp
{
    public class EnlightKinectTileButton : KinectTileButton
    {
        public EnlightKinectTileButton(int left, int top) : base()
        {
            // TODO: styling
            this.Height = 100;
            this.Width = 100;
            this.Margin = new System.Windows.Thickness(left, top, 0, 0);
        }
    }
}
