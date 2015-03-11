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
                var button = new KinectCircleButton
                {
                    Content = i,
                    Label = commands[i - 1],
                    Height = 200,
                    Width = 200

                };

                int i1 = i;
                button.Click += delegate(System.Object o, RoutedEventArgs e){

                    MessageBox.Show("You have selected command: " +  button.Label + "!");
                };
                button.MouseEnter += delegate(System.Object o, MouseEventArgs e){
                   // MessageBox.Show("You have hovered over command: " + button.Label + "!");

                };
                 //   ButtonOnClick;

                scrollContent.Children.Add(button);
            }
            
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
                    // KinectSensor might enter an invalid state while enabling/disabling streams or stream features.
                    // E.g.: sensor might be abruptly unplugged.
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
                        args.NewSensor.DepthStream.Range = DepthRange.Near;
                        args.NewSensor.SkeletonStream.EnableTrackingInNearRange = true;
                        args.NewSensor.SkeletonStream.TrackingMode = SkeletonTrackingMode.Seated;
                    }
                    catch (InvalidOperationException)
                    {
                        // Non Kinect for Windows devices do not support Near mode, so reset back to default mode.
                        args.NewSensor.DepthStream.Range = DepthRange.Default;
                        args.NewSensor.SkeletonStream.EnableTrackingInNearRange = false;
                        error = true;
                    }
                }
                catch (InvalidOperationException)
                {
                    error = true;
                    // KinectSensor might enter an invalid state while enabling/disabling streams or stream features.
                    // E.g.: sensor might be abruptly unplugged.
                }
            }

            if (!error)
                kinectRegion.KinectSensor = args.NewSensor;
        }
    }

    }
