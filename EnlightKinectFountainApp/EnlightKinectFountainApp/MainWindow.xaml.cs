using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using Microsoft.Kinect;
using Microsoft.Kinect.Toolkit;
using Microsoft.Kinect.Toolkit.Interaction;
using Microsoft.Kinect.Toolkit.Controls;

namespace EnlightKinectFountainApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private KinectSensorChooser sensorChooser;

        public MainWindow()
        {
            InitializeComponent();
            Loaded += OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            this.sensorChooser = new KinectSensorChooser();
            this.sensorChooser.KinectChanged += SensorChooserOnKinectChanged;
            this.sensorChooserUi.KinectSensorChooser = this.sensorChooser;
            this.sensorChooser.Start();
            this.fillScrollContent();
        }

        private void SensorChooserOnKinectChanged(object sender, KinectChangedEventArgs args)
        {
            bool error = false;
            if (args.OldSensor != null)
            {
                try
                {
                    args.OldSensor.DepthStream.Range = DepthRange.Default;
                    args.OldSensor.SkeletonStream.EnableTrackingInNearRange = false;
                    args.OldSensor.DepthStream.Disable();
                    args.OldSensor.SkeletonStream.Disable();
                }
                catch (InvalidOperationException)
                {
                    error = true;
                }
            }

            if (args.NewSensor != null)
            {
                try
                {
                    args.NewSensor.DepthStream.Enable(DepthImageFormat.Resolution640x480Fps30);
                    args.NewSensor.SkeletonStream.Enable();

                    try
                    {
                        args.NewSensor.DepthStream.Range = DepthRange.Default;
                        args.NewSensor.SkeletonStream.EnableTrackingInNearRange = true;
                        args.NewSensor.SkeletonStream.TrackingMode = SkeletonTrackingMode.Default;
                    }
                    catch (InvalidOperationException)
                    {
                        args.NewSensor.DepthStream.Range = DepthRange.Default;
                        args.NewSensor.SkeletonStream.EnableTrackingInNearRange = false;
                        error = true;
                    }
                }
                catch (InvalidOperationException)
                {
                    error = true;
                }
            }

            if (!error)
                mykinectRegion.KinectSensor = args.NewSensor;
        }

        private void fillScrollContent()
        {
            String[] commands = new String[10];
            commands[0] = "One jet";
            commands[1] = "Multiple jets";
            commands[2] = "Random jet";
            commands[3] = "All one side";
            commands[4] = "whooosh";
            commands[5] = "Many jets";
            commands[6] = "jump";
            commands[7] = "more words";
            commands[8] = "even more";
            commands[9] = "free mode";

            //fill scroll content
            for (int i = 1; i < 10; i++)
            {
                var button = new KinectTileButton
                {
                    Label = commands[i - 1],
                    Height = 100
                };

                int i1 = i;
                button.Click += delegate(System.Object o, RoutedEventArgs e)
                {
                    MessageBox.Show("You have selected command: " + button.Label);
                };

                scrollContent.Children.Add(button);
            }
        }

        private void ButtonOnClick(object sender, RoutedEventArgs e)
        {
            // put something more interesting here, delegate function is now handling in 
            // fillScrollContent function
        }
    }
}