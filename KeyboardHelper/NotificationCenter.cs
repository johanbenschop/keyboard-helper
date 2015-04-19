using System;

namespace KeyboardHelper
{
    public static class NotificationCenter
    {
        public static void NotifyTaskManager()
        {
            //App.Window.Dispatcher.Invoke((Action)(() => App.Window.ShowOff(new Glyphs.TaskManager())));
        }
        public static void NotifyOn()
        {
           // App.Window.Dispatcher.Invoke((Action)(() => App.Window.ShowOff(new Glyphs.On())));
        }
        public static void NotifyOff()
        {
            //App.Window.Dispatcher.Invoke((Action)(() => App.Window.ShowOff(new Glyphs.Off())));
        }

        internal static void NotifyEject()
        {
            //App.Window.Dispatcher.Invoke((Action)(() => App.Window.ShowOff(new Glyphs.Eject())));
        }

        public static void Slider()
        {
            App.Window.Dispatcher.Invoke(() => App.Window.ShowOff());
        }
    }
}
