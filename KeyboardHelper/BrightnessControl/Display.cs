using System;
using System.Collections.Generic;
using System.Windows.Documents;

namespace KeyboardHelper.BrightnessControl
{
    public class Display
    {
        public int ScreenWidth { get; set; }
        public int ScreenHeight { get; set; }
        public int MonitorArea { get; set; }
        public int WorkArea { get; set; }
        public bool Availability { get; set; }
        // ReSharper disable once InconsistentNaming
        public IntPtr hMonitor { get; set; }
        public List<Monitor> Monitors { get; set; }

        public Display()
        {
            Monitors = new List<Monitor>();
        }
    }
}
