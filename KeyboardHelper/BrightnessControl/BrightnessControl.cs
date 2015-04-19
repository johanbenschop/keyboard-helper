using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KeyboardHelper.BrightnessControl
{
    public class BrightnessControl
    {
        public static List<Display> Displays { get; private set; }
        private const int StepSize = 10;

        public static void Initialize()
        {
            Displays = GetDisplays();
        }

        public async static void IncreaseBrightness()
        {
            foreach (var monitor in Displays.SelectMany(display => display.Monitors))
            {
                if (monitor.CurrentBrightness + StepSize > monitor.MaximumBrightness) break;
                 monitor.CurrentBrightness += StepSize;
                 await Task.Factory.StartNew(() => setBrightness(monitor));
            }
        }

        public async static void DecreaseBrightness()
        {
            foreach (var monitor in Displays.SelectMany(display => display.Monitors))
            {
                if (monitor.CurrentBrightness - StepSize < monitor.MinimumBrightness) break;
                monitor.CurrentBrightness -= StepSize;
                await Task.Factory.StartNew(() => setBrightness(monitor));
            }
        }

        public static async void SetBrightness(int newBrightness)
        {
            foreach (var monitor in Displays.SelectMany(display => display.Monitors))
            {
                if (newBrightness < monitor.MinimumBrightness || newBrightness > monitor.MaximumBrightness) break;
                monitor.CurrentBrightness = newBrightness;
                await Task.Factory.StartNew(() => setBrightness(monitor));
            }
        }

        private static void setBrightness(Monitor monitor)
        {
            NativeMethods.SetMonitorBrightness(monitor.hPhysicalMonitor, (uint)monitor.CurrentBrightness);
        }

        public static int GetHighestBrightness()
        {
            return (from display in Displays from monitor in display.Monitors select monitor.CurrentBrightness).Concat(new[] {0}).Max();
        }

        public static List<Display> GetDisplays()
        {
            var displays = new List<Display>();

            NativeMethods.EnumDisplayMonitors(IntPtr.Zero, IntPtr.Zero,
                delegate(IntPtr hMonitor, IntPtr hdcMonitor, ref NativeStructures.Rect lprcMonitor, IntPtr dwData)
                {
                    var display = new Display
                    {
                        ScreenWidth = (lprcMonitor.right - lprcMonitor.left),
                        ScreenHeight = (lprcMonitor.bottom - lprcMonitor.top),
                        hMonitor = hMonitor
                    };
                    
                    var pPhysicalMonitorArray = GetMonitors(hMonitor);
                    foreach (var physicalMonitor in pPhysicalMonitorArray)
                    {
                        uint pdwMonitorCapabilities = 0, pdwSupportedColorTemperatures = 0u;
                        var caps = NativeMethods.GetMonitorCapabilities(physicalMonitor.hPhysicalMonitor,
                            ref pdwMonitorCapabilities, ref pdwSupportedColorTemperatures);

                        var monitor = new Monitor {
                            Capabilities = (MonitorCapabilities) pdwMonitorCapabilities,
                            hPhysicalMonitor = physicalMonitor.hPhysicalMonitor
                        };
                        UpdateCurrentBrightness(monitor);
                        display.Monitors.Add(monitor);
                    }
                    
                    displays.Add(display);
                    return true;
                }, IntPtr.Zero);

            return displays;
        }

        private static IEnumerable<NativeStructures.PHYSICAL_MONITOR> GetMonitors(IntPtr hMonitor)
        {
            var pdwNumberOfPhysicalMonitors = 0u;
            var numberOfPhysicalMonitorsFromHmonitor = NativeMethods.GetNumberOfPhysicalMonitorsFromHMONITOR(hMonitor, ref pdwNumberOfPhysicalMonitors);

            var pPhysicalMonitorArray = new NativeStructures.PHYSICAL_MONITOR[pdwNumberOfPhysicalMonitors];
            var physicalMonitorsFromHmonitor = NativeMethods.GetPhysicalMonitorsFromHMONITOR(hMonitor, pdwNumberOfPhysicalMonitors, pPhysicalMonitorArray);

            return pPhysicalMonitorArray;
        }

        private static void UpdateCurrentBrightness(Monitor monitor)
        {
            uint pdwMinimumBrightness = 0, pdwCurrentBrightness = 0, pdwMaximumBrightness = 0u;
            var caps = NativeMethods.GetMonitorBrightness(monitor.hPhysicalMonitor,
                            ref pdwMinimumBrightness, ref pdwCurrentBrightness, ref pdwMaximumBrightness);
            if (!caps) return;
            monitor.CurrentBrightness = (int)pdwCurrentBrightness;
            monitor.MinimumBrightness = (int)pdwMinimumBrightness;
            monitor.MaximumBrightness = (int)pdwMaximumBrightness;
        }

        private static bool CleanupMonitors(uint pdwNumberOfPhysicalMonitors, NativeStructures.PHYSICAL_MONITOR[] pPhysicalMonitorArray)
        {
            return NativeMethods.DestroyPhysicalMonitors(pdwNumberOfPhysicalMonitors, pPhysicalMonitorArray);
        }

        private static bool SetMonitorBrightness(Monitor monitor, int newBrightness)
        {
            return false;
        }
    }
}