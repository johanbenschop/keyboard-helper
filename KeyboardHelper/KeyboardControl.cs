using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KeyboardHelper
{
    public static class KeyboardControl
    {
        public static void Send(Keys vKey, KeyboardEvent e = KeyboardEvent.Both)
        {
            switch (e)
            {
                case KeyboardEvent.Both:
                    {
                        keybd_event((byte)vKey, 0, 0, 0);
                        keybd_event((byte)vKey, 0, 2, 0);
                    }
                    break;
                case KeyboardEvent.Down:
                    keybd_event((byte)vKey, 0, 0, 0);
                    break;
                case KeyboardEvent.Up:
                    keybd_event((byte)vKey, 0, 2, 0);
                    break;
            }
        }

        public static void SendInsert()
        {
            Task.Factory.StartNew(() => Send(Keys.Insert));
        }

        public static void SendDelete()
        {
            Task.Factory.StartNew(() => Send(Keys.Delete));
        }

        public static void SendPrintScreen()
        {
            Task.Factory.StartNew(() => Send(Keys.PrintScreen));
        }

        public static void SendVolumeUp()
        {
            Task.Factory.StartNew(() => Send(Keys.VolumeUp));
        }

        public static void SendVolumeDown()
        {
            Task.Factory.StartNew(() => Send(Keys.VolumeDown));
        }

        public static void SendVolumeMute()
        {
            Task.Factory.StartNew(() => Send(Keys.VolumeMute));
        }

        public static void SendPlayPause()
        {
            Task.Factory.StartNew(() => Send(Keys.MediaPlayPause));
        }

        public static void SendNextTrack()
        {
            Task.Factory.StartNew(() => Send(Keys.MediaNextTrack));
        }

        public static void SendPreviousTrack()
        {
            Task.Factory.StartNew(() => Send(Keys.MediaPreviousTrack));
        }

        public static void OpenTaskManager()
        {
            Task.Factory.StartNew(() =>
            {
                var taskmgr = Process.GetProcessesByName(@"taskmgr.exe").FirstOrDefault();
                if (taskmgr != null)
                    SetForegroundWindow(taskmgr.MainWindowHandle);
                else
                    Process.Start(@"taskmgr.exe");
                NotificationCenter.NotifyTaskManager();
            });
        }

        #region PInvoke
        [DllImport("user32.dll")]
        static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, int dwExtraInfo);

        public enum KeyboardEvent
        {
            Down = 0, Up = 2, Both
        }
        #endregion

        public static void SendPageUp()
        {
            Task.Factory.StartNew(() => Send(Keys.PageUp));
        }
        public static void SendPageDown()
        {
            Task.Factory.StartNew(() => Send(Keys.PageDown));
        }
        public static void SendHome()
        {
            Task.Factory.StartNew(() => Send(Keys.Home));
        }
        public static void SendEnd()
        {
            Task.Factory.StartNew(() => Send(Keys.End));
        }

        public static void SendF3()
        {
            Task.Factory.StartNew(() => Send(Keys.F3));
        }

        public static void SendBrightnessUp()
        {
            Task.Factory.StartNew(BrightnessControl.BrightnessControl.IncreaseBrightness);
            NotificationCenter.Slider();
        }

        public static void SendBrightnessDown()
        {
            Task.Factory.StartNew(BrightnessControl.BrightnessControl.DecreaseBrightness);
            NotificationCenter.Slider();
        }
    }
}
