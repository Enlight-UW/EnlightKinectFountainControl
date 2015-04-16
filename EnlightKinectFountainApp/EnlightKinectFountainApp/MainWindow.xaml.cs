using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using Microsoft.Kinect;
using Microsoft.Kinect.Toolkit;
using Microsoft.Kinect.Toolkit.Interaction;
using Microsoft.Kinect.Toolkit.Controls;

using EnlightFountainControlLibrary;
using EnlightFountainControlLibrary.Messages;

using EnlightKinectFountainApp.Gestures;

namespace EnlightKinectFountainApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region constants
        private const string ENLIGHT_WEBSERVER_URL = "";
        private const string ENLIGHT_API_KEY = "";
        private static readonly string[] MODE_COMMAND_TEXT =
        {
            "Check", "Line", "Triangle", 
            "Wave", "Z",
            "Star", "W"
        };
        #endregion

        #region class variables
        private KinectSensorChooser sensorChooser;

        private Gesture currentGesture;

        /// <summary>
        /// Width of output drawing
        /// </summary>
        private const float RenderWidth = 640.0f;

        /// <summary>
        /// Height of our output drawing
        /// </summary>
        private const float RenderHeight = 480.0f;

        /// <summary>
        /// Thickness of drawn joint lines
        /// </summary>
        private const double JointThickness = 3;

        /// <summary>
        /// Thickness of body center ellipse
        /// </summary>
        private const double BodyCenterThickness = 10;

        /// <summary>
        /// Thickness of clip edge rectangles
        /// </summary>
        private const double ClipBoundsThickness = 10;

        /// <summary>
        /// Brush used to draw skeleton center point
        /// </summary>
        private readonly Brush centerPointBrush = Brushes.Blue;

        /// <summary>
        /// Bitmap that will hold color information
        /// </summary>
        private WriteableBitmap colorBitmap;

        /// <summary>
        /// Intermediate storage for the color data received from the camera
        /// </summary>
        private byte[] colorPixels;

        /// <summary>
        /// Brush used for drawing joints that are currently tracked
        /// </summary>
        private readonly Brush trackedJointBrush = new SolidColorBrush(Color.FromArgb(255, 68, 192, 68));

        /// <summary>
        /// Brush used for drawing joints that are currently inferred
        /// </summary>        
        private readonly Brush inferredJointBrush = Brushes.Yellow;

        /// <summary>
        /// Pen used for drawing bones that are currently tracked
        /// </summary>
        private readonly Pen trackedBonePen = new Pen(Brushes.Green, 6);

        /// <summary>
        /// Pen used for drawing bones that are currently inferred
        /// </summary>        
        private readonly Pen inferredBonePen = new Pen(Brushes.Gray, 1);

        /// <summary>
        /// Active Kinect sensor
        /// </summary>
        private KinectSensor sensor;

        /// <summary>
        /// Drawing group for skeleton rendering output
        /// </summary>
        private DrawingGroup drawingGroup;

        /// <summary>
        /// Drawing image that we will display
        /// </summary>
        /// <remarks>
        /// Image source is another way that the image element of the window can be accessed.  
        /// This is a little dangerous due to the fact that it has higher precidence that the drawing context, 
        /// or so it would appear.
        /// </remarks>
        private DrawingImage imageSource;

        private EnlightFountainService enlightFountainService;

        #endregion

        private Color[] brushes = { Colors.AliceBlue, Colors.Chartreuse, Colors.DodgerBlue, Colors.MistyRose, Colors.Pink, Colors.Salmon, Colors.Thistle, Colors.Yellow, Colors.WhiteSmoke};
        private int brushCounter;

        public MainWindow()
        {
            InitializeComponent();
            brushCounter = 0;
        }

        private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            enlightFountainService = EnlightFountainService.GetInstance(ENLIGHT_WEBSERVER_URL, ENLIGHT_API_KEY);

            this.sensorChooser = new KinectSensorChooser();
            this.sensorChooser.KinectChanged += SensorChooserOnKinectChanged;
            this.sensorChooserUi.KinectSensorChooser = this.sensorChooser;
            this.sensorChooser.Start();
            this.FillScrollContent();

            // Create the drawing group we'll use for drawing
            this.drawingGroup = new DrawingGroup();

            // Create an image source that we can use in our image control
            this.imageSource = new DrawingImage(this.drawingGroup);

            // Display the drawing using our image control
            Image.Source = this.imageSource;

            this.sensor = sensorChooser.Kinect;

            if (sensor != null)
            {
                // Turn on the skeleton stream to receive skeleton frames
                this.sensor.SkeletonStream.Enable();

                // Add an event handler to be called whenever there is new color frame data
                this.sensor.SkeletonFrameReady += this.SensorSkeletonFrameReady;

                // Turn on the color stream to receive color frames
                this.sensor.ColorStream.Enable(ColorImageFormat.RgbResolution640x480Fps30);

                // Allocate space to put the pixels we'll receive
                this.colorPixels = new byte[this.sensor.ColorStream.FramePixelDataLength];

                // This is the bitmap we'll display on-screen
                this.colorBitmap = new WriteableBitmap(this.sensor.ColorStream.FrameWidth, this.sensor.ColorStream.FrameHeight, 96.0, 96.0, PixelFormats.Bgr32, null);

                /* The above line of code was what was removed to get the images to work together.  This is the more direct way to write to the screen
                 * therefore it can cause issues (drawing multiple images) when this is directly written to, like in the line above.
                 */

                // Add an event handler to be called whenever there is new color frame data
                this.sensor.ColorFrameReady += this.SensorColorFrameReady;
            }

        }

        private void OnClosed(object sender, EventArgs e)
        {
            if (this.sensor != null && this.sensorChooser != null)
                this.sensorChooser.Stop();
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
                KinectRegion.KinectSensor = args.NewSensor;
        }

        private void FillScrollContent()
        {
            ModeSelectorPanel.Children.Clear();

            KinectTileButton[] modeButtons = new KinectTileButton[MODE_COMMAND_TEXT.Length];

            // fill scroll content
            for (int i = 0; i < MODE_COMMAND_TEXT.Length; i++)
            {
                modeButtons[i] = new KinectTileButton
                {
                    Label = MODE_COMMAND_TEXT[i],
                    Height = 150
                };

                ModeSelectorPanel.Children.Add(modeButtons[i]);
            }

            modeButtons[0].Click += delegate(object sender, RoutedEventArgs e)
            {
                if (currentGesture != null)
                    return;

                currentGesture = new CheckGesture();
                currentGesture.SequenceUpdated += HandleSequenceUpdatedEvent;
            };

            modeButtons[1].Click += delegate(object sender, RoutedEventArgs e)
            {
                if (currentGesture != null)
                    return;

                currentGesture = new LineGesture();
                currentGesture.SequenceUpdated += HandleSequenceUpdatedEvent;
            };

            modeButtons[2].Click += delegate(object sender, RoutedEventArgs e)
            {
                if (currentGesture != null)
                    return;

                currentGesture = new TriangleGesture();
                currentGesture.SequenceUpdated += HandleSequenceUpdatedEvent;
            };

            modeButtons[3].Click += delegate(object sender, RoutedEventArgs e)
            {
                if (currentGesture != null)
                    return;

                currentGesture = new WaveGesture();
                currentGesture.SequenceUpdated += HandleSequenceUpdatedEvent;
            };

            modeButtons[4].Click += delegate(object sender, RoutedEventArgs e)
            {
                if (currentGesture != null)
                    return;

                currentGesture = new ZGesture();
                currentGesture.SequenceUpdated += HandleSequenceUpdatedEvent;
            };

            modeButtons[5].Click += delegate(object sender, RoutedEventArgs e)
            {
                if (currentGesture != null)
                    return;

                currentGesture = new StarGesture();
                currentGesture.SequenceUpdated += HandleSequenceUpdatedEvent;
            };

            modeButtons[6].Click += delegate(object sender, RoutedEventArgs e)
            {
                if (currentGesture != null)
                    return;

                currentGesture = new WGesture();
                currentGesture.SequenceUpdated += HandleSequenceUpdatedEvent;
            };
        }

        private void HandleSequenceUpdatedEvent(object sender, SequenceEventArgs args)
        {
            // ensure that the origin is our current gesture
            if (sender != currentGesture)
            {
                Gesture g = sender as Gesture;

                if (g != null)
                    g.Dispose();

                return;
            }

            // remove any buttons on the screen
            Action removeStale = new Action(() => AppCanvas.Children.OfType<EnlightKinectTileButton>().ToList().ForEach(b => AppCanvas.Children.Remove(b)));
            this.Dispatcher.Invoke(removeStale);

            // if done
            if (args.SequenceComplete)
            {
                Action changeColor = new Action(() =>  AppCanvas.Background = new SolidColorBrush(brushes[brushCounter]));

                brushCounter++;
                brushCounter %= brushes.Length;

                this.Dispatcher.Invoke(changeColor);
                    
                // clean up the current gesture, and reset
                currentGesture.Dispose();
                currentGesture = null;
                return;
            }

            // add new child to window
            Action addAction = new Action(() => AppCanvas.Children.Add(args.NextButton));
            this.Dispatcher.Invoke(addAction);
        }

        /// <summary>
        /// Event handler for Kinect sensor's SkeletonFrameReady event
        /// </summary>
        /// <param name="sender">object sending the event</param>
        /// <param name="e">event arguments</param>
        private void SensorSkeletonFrameReady(object sender, SkeletonFrameReadyEventArgs e)
        {
            //Fetching the first skeleton from the skeleton array in the sensor data
            Skeleton[] skeletons = new Skeleton[0];

            using (SkeletonFrame skeletonFrame = e.OpenSkeletonFrame())
            {
                if (skeletonFrame != null)
                {
                    skeletons = new Skeleton[skeletonFrame.SkeletonArrayLength];
                    skeletonFrame.CopySkeletonDataTo(skeletons);
                }
            }

            using (DrawingContext dc = this.drawingGroup.Open())
            {
                // Draw a transparent background to set the render size
                dc.DrawRectangle(Brushes.Black, null, new Rect(0.0, 0.0, RenderWidth, RenderHeight));

                //This is where the magic happens. The video image from the kinect is moved to the screen here
                dc.DrawImage(colorBitmap, new Rect(0.0, 0.0, RenderWidth, RenderHeight));

                //This is where the spooky skeleton dark magic happens. Bones happen and get drawn to the screen and stuff I guess
                if (skeletons.Length != 0)
                {
                    foreach (Skeleton skel in skeletons)
                    {
                        if (skel.TrackingState == SkeletonTrackingState.Tracked)
                        {
                            this.DrawBonesAndJoints(skel, dc);
                        }
                        else if (skel.TrackingState == SkeletonTrackingState.PositionOnly)
                        {
                            dc.DrawEllipse(
                            this.centerPointBrush,
                            null,
                            this.SkeletonPointToScreen(skel.Position),
                            BodyCenterThickness,
                            BodyCenterThickness);
                        }
                    }
                }

                // prevent drawing outside of our render area
                this.drawingGroup.ClipGeometry = new RectangleGeometry(new Rect(0.0, 0.0, RenderWidth, RenderHeight));
            }
        }

        /// <summary>
        /// Event handler for Kinect sensor's ColorFrameReady event
        /// </summary>
        /// <param name="sender">object sending the event</param>
        /// <param name="e">event arguments</param>
        private void SensorColorFrameReady(object sender, ColorImageFrameReadyEventArgs e)
        {
            using (ColorImageFrame colorFrame = e.OpenColorImageFrame())
            {
                if (colorFrame != null)
                {
                    // Copy the pixel data from the image to a temporary array
                    colorFrame.CopyPixelDataTo(this.colorPixels);

                    // Write the pixel data into our bitmap
                    this.colorBitmap.WritePixels(
                        new Int32Rect(0, 0, this.colorBitmap.PixelWidth, this.colorBitmap.PixelHeight),
                        this.colorPixels,
                        this.colorBitmap.PixelWidth * sizeof(int),
                        0);

                }
            }
        }

        /// <summary>
        /// Draws a skeleton's bones and joints
        /// </summary>
        /// <param name="skeleton">skeleton to draw</param>
        /// <param name="drawingContext">drawing context to draw to</param>
        private void DrawBonesAndJoints(Skeleton skeleton, DrawingContext drawingContext)
        {
            // Left Arm
            this.DrawBone(skeleton, drawingContext, JointType.ElbowLeft, JointType.WristLeft);
            this.DrawBone(skeleton, drawingContext, JointType.WristLeft, JointType.HandLeft);

            // Right Arm
            this.DrawBone(skeleton, drawingContext, JointType.ElbowRight, JointType.WristRight);
            this.DrawBone(skeleton, drawingContext, JointType.WristRight, JointType.HandRight);

            // Left Leg
            this.DrawBone(skeleton, drawingContext, JointType.KneeLeft, JointType.AnkleLeft);
            this.DrawBone(skeleton, drawingContext, JointType.AnkleLeft, JointType.FootLeft);

            // Right Leg
            this.DrawBone(skeleton, drawingContext, JointType.KneeRight, JointType.AnkleRight);
            this.DrawBone(skeleton, drawingContext, JointType.AnkleRight, JointType.FootRight);

            // Render Joints
            foreach (Joint joint in skeleton.Joints)
            {
                Brush drawBrush = null;

                if (joint.TrackingState == JointTrackingState.Tracked)
                {
                    drawBrush = this.trackedJointBrush;
                }
                else if (joint.TrackingState == JointTrackingState.Inferred)
                {
                    drawBrush = this.inferredJointBrush;
                }

                if (drawBrush != null)
                {
                    drawingContext.DrawEllipse(drawBrush, null, this.SkeletonPointToScreen(joint.Position), JointThickness, JointThickness);
                }
            }
        }

        /// <summary>
        /// Maps a SkeletonPoint to lie within our render space and converts to Point
        /// </summary>
        /// <param name="skelpoint">point to map</param>
        /// <returns>mapped point</returns>
        private Point SkeletonPointToScreen(SkeletonPoint skelpoint)
        {
            // Convert point to depth space.  
            // We are not using depth directly, but we do want the points in our 640x480 output resolution.
            DepthImagePoint depthPoint = this.sensor.CoordinateMapper.MapSkeletonPointToDepthPoint(skelpoint, DepthImageFormat.Resolution640x480Fps30);
            return new Point(depthPoint.X, depthPoint.Y);
        }

        private void DrawBone(Skeleton skeleton, DrawingContext drawingContext, JointType jointType0, JointType jointType1)
        {
            Joint joint0 = skeleton.Joints[jointType0];

            // If we can't find either of these joints, exit
            if (joint0.TrackingState == JointTrackingState.NotTracked)
            {
                return;
            }

            // Don't draw if both points are inferred(?)
            if (joint0.TrackingState == JointTrackingState.Inferred)
            {
                return;
            }

            //We default or drawPen to be specifically the one for inferred bones unless specified otherwise.
            Pen drawPen = this.inferredBonePen;
            if (joint0.TrackingState == JointTrackingState.Tracked)
            {
                drawPen = this.trackedBonePen;
            }

            //Note: the only values here that should be explicitly changed in the method would be the values of 10.
            //Also note, the only thing this method needs in the file in which it is present is a trackedBonePen field,
            //and an inferredBonePen. These are the only "dependencies" of this method, i.e., the necessary parts that aren't passed as arguments.
            drawingContext.DrawEllipse(this.trackedJointBrush, drawPen, this.SkeletonPointToScreen(joint0.Position), 10, 10);
        }
    }
}