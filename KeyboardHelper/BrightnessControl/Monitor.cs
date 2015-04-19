using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KeyboardHelper.BrightnessControl
{
    public class Monitor
    {
        private int _currentBrightness;
        public MonitorCapabilities Capabilities { get; set; }
        // ReSharper disable once InconsistentNaming
        public IntPtr hPhysicalMonitor { get; set; }
        public int CurrentBrightness
        {
            get { return _currentBrightness; }
            set
            {
                if (value < 0 || value > 100) throw new ArgumentOutOfRangeException();
                _currentBrightness = value;

            }
        }
        public int MinimumBrightness { get; set; }
        public int MaximumBrightness { get; set; }
    }
}
