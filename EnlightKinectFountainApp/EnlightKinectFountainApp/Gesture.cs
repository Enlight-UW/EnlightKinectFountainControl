using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Input;

using Microsoft.Kinect.Toolkit.Controls;

namespace EnlightKinectFountainApp
{
    public abstract class Gesture : IDisposable
    {
        private const int RESET_THRESHOLD = 5; // sec

        protected EnlightKinectTileButton[] sequence;
        private int sequence_index;
        private Timer resetTimer;

        private EventHandler<SequenceEventArgs> sequenceUpdated;

        public Gesture()
        {
            sequence_index = 0;
            resetTimer = new Timer(RESET_THRESHOLD * 1000);
            resetTimer.Elapsed += Reset;
            resetTimer.AutoReset = false;

            InitializeGestureSequence();
            SetupHandPointerEnteredEvents();
        }

        public EventHandler<SequenceEventArgs> SequenceUpdated
        {
            get { return sequenceUpdated; }
            set
            {
                sequenceUpdated = value;
                this.Reset(this, null);
            }
        }

        protected abstract void InitializeGestureSequence();

        public void Reset(object source, ElapsedEventArgs args)
        {
            // stop the timer - resetting
            resetTimer.Stop();

            sequence_index = 0;
            this.SequenceUpdated(this, new SequenceEventArgs(sequence[0], false));
        }

        protected void SetupHandPointerEnteredEvents()
        {
            foreach (EnlightKinectTileButton button in sequence)
            {
                KinectRegion.AddHandPointerEnterHandler(button, HandPointerEntered);
            }
        }

        // what happens a button is entered
        private void HandPointerEntered(object sender, HandPointerEventArgs event_args)
        {
            EnlightKinectTileButton button = sender as EnlightKinectTileButton;

            // sender cast check
            if (button == null)
                return;

            // reference check or equals() check? both *should* be OK
            if (sequence[sequence_index] != button)
                return;

            // reset timer
            resetTimer.Stop();
            resetTimer.Start();

            // move the index up
            sequence_index++;

            // fire event to window to display the next in sequence
            // if complete, send null + true
            if (sequence_index == sequence.Length)
                this.SequenceUpdated(this, new SequenceEventArgs(null, true));
            else
                this.SequenceUpdated(this, new SequenceEventArgs(sequence[sequence_index], false));
        }

        public void Dispose()
        {
            this.Dispose(true);
        }

        private void Dispose(bool dispose)
        {
            GC.SuppressFinalize(this);
            resetTimer.Dispose();
        }
    }
}
