using KeyboardHelper.FocusCheck;
using System.Windows.Input;

namespace KeyboardHelper
{
    public static class KeyboardHandler
    {
        public static int PowerButtonPressCount { get; set; }

        static KeyboardHandler()
        {
            PowerButtonPressCount = 0;
        }

        public static bool FMode
        {
            get
            {
                return Properties.Settings.Default.FMode;
            }
            set
            {
                if (Properties.Settings.Default.FMode == value) return;

                Properties.Settings.Default.FMode = value;
                if (value)
                    NotificationCenter.NotifyOn();
                else NotificationCenter.NotifyOff();
            }
        }

        public static void HandleEject()
        {
            if (!AppleKeyboardHID2.FnDown)
                FMode = !FMode;
            else IoControl.EjectAllMedia();
        }

        public static bool HandleKeyDown(KeyboardListener.KeyHookEventArgs e)
        {
            var title = Focus.GetActiveWindowTitle();
            if (title != null && title.Contains("EVE"))
            {
                return false;
            }
            
            var f = (FMode || e.ModifierFn) && !e.ModifierAnyNative;

            switch (e.Key)
            {
                case Key.F1:
                    if (f)
                    {
                        KeyboardControl.SendBrightnessDown();
                        return true;
                    }
                    break;
                case Key.F2:
                    if (f)
                    {
                        KeyboardControl.SendBrightnessUp();
                        return true;
                    }
                    break;
                case Key.F3:
                    if (f)
                    {
                        KeyboardControl.SendPrintScreen();
                        return true;
                    }
                    break;
                case Key.F4:
                    if (f)
                    {
                        KeyboardControl.OpenTaskManager();
                        return true;
                    }
                    break;
                case Key.F7:
                    if (f)
                    {
                        KeyboardControl.SendPreviousTrack();
                        return true;
                    }
                    break;
                case Key.F8:
                    if (f)
                    {
                        KeyboardControl.SendPlayPause();
                        return true;
                    }
                    break;
                case Key.F9:
                    if (f)
                    {
                        KeyboardControl.SendNextTrack();
                        return true;
                    }
                    break;
                case Key.F10:
                    if (f)
                    {
                        KeyboardControl.SendVolumeMute();
                        return true;
                    }
                    break;
                case Key.F11:
                    if (f)
                    {
                        KeyboardControl.SendVolumeDown();
                        return true;
                    }
                    break;
                case Key.F12:
                    if (f)
                    {
                        KeyboardControl.SendVolumeUp();
                        return true;
                    }
                    break;
                case Key.Back:
                    if (e.ModifierFn)
                    {
                        KeyboardControl.SendDelete();
                        return true;
                    }
                    break;
                case Key.Up:
                    if (e.ModifierFn)
                    {
                        KeyboardControl.SendPageUp();
                        return true;
                    }
                    break;
                case Key.Down:
                    if (e.ModifierFn)
                    {
                        KeyboardControl.SendPageDown();
                        return true;
                    }
                    break;
                case Key.Left:
                    if(e.ModifierFn)
                    {
                        KeyboardControl.SendHome();
                        return true;
                    }
                    break;
                case Key.Right:
                    if (e.ModifierFn)
                    {
                        KeyboardControl.SendEnd();
                        return true;
                    } 
                    break;
                case Key.Enter:
                    if (e.ModifierFn)
                    {
                        KeyboardControl.SendInsert();
                        return true;
                    }
                    break;
            }

            if (!e.ModifierAnyAlt || (!FMode && !e.ModifierFn)) return false;
            if (e.Key != Key.F3) return false;
            KeyboardControl.SendPrintScreen();
            return true;
        }
    }
}
