using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace EnlightKinectFountainApp
{
    class GestureButtonClickHandler
    {
        private Gesture gesture;

        public GestureButtonClickHandler(ref Gesture g)
        {
            this.gesture = g;
        }

        public void HandleButtonClick(object sender, RoutedEventArgs e)
        {
            if (gesture != null)
                return;

        }
    }
}
