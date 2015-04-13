using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Input;

using System.Diagnostics;

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

        public EventHandler<RoutedEventArgs> RightHandEntered
        {
            get;
            set;
        }

        public void CheckIfRightHandEntered(object sender, RightHandEventArgs args)
        {
            // check the point
            // internal point check
            Point rightHand = args.RightHandPoint;
            Thickness buttonMargin = this.Margin;
            int buttonHeight = (int)this.Height;
            int buttonWidth = (int)this.Width;

             // check if right hand within button
            if ((buttonMargin.Left < rightHand.X) && (buttonMargin.Left + buttonWidth) > rightHand.X)
            {
                if ((buttonMargin.Top < rightHand.Y) && (buttonMargin.Top + buttonHeight) > rightHand.Y)
                {
                    this.RightHandEntered(this, new RoutedEventArgs());
                }
            }
        }
        
    }

}
