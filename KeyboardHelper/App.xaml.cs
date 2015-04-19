using System.Windows;
using System.Windows.Input;
using System.Diagnostics;
using System.Linq;
using System.Threading;

namespace KeyboardHelper
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static MainWindow Window { get; set; }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            TrayIcon.Show();

            if (Process.GetProcessesByName(Application.ResourceAssembly.GetName().Name).Length > 1)
            {
                var processes = Process.GetProcessesByName(Application.ResourceAssembly.GetName().Name);
                foreach (var p in processes.Where(p => p != Process.GetCurrentProcess()))
                    p.Kill();
            }

            Window = new MainWindow();

            AppleKeyboardHID2.Start();
            AppleKeyboardHID2.KeyDown += AppleKeyboardHID_KeyDown;
            AppleKeyboardHID2.KeyUp += AppleKeyboardHID_KeyUp;

            KeyboardListener.HookedKeys.Add(Key.F1);
            KeyboardListener.HookedKeys.Add(Key.F2);
            KeyboardListener.HookedKeys.Add(Key.F3);
            KeyboardListener.HookedKeys.Add(Key.F4);
            KeyboardListener.HookedKeys.Add(Key.F7);
            KeyboardListener.HookedKeys.Add(Key.F8);
            KeyboardListener.HookedKeys.Add(Key.F9);
            KeyboardListener.HookedKeys.Add(Key.F10);
            KeyboardListener.HookedKeys.Add(Key.F11);
            KeyboardListener.HookedKeys.Add(Key.F12);
            KeyboardListener.HookedKeys.Add(Key.Back);
            KeyboardListener.HookedKeys.Add(Key.Up);
            KeyboardListener.HookedKeys.Add(Key.Left);
            KeyboardListener.HookedKeys.Add(Key.Right);
            KeyboardListener.HookedKeys.Add(Key.Down);
            KeyboardListener.HookedKeys.Add(Key.Enter);
            KeyboardListener.Register();
            KeyboardListener.KeyDown += KeyboardListener_KeyDown;

            Microsoft.Win32.SystemEvents.PowerModeChanged += SystemEvents_PowerModeChanged;

            BrightnessControl.BrightnessControl.Initialize();
        }

        void SystemEvents_PowerModeChanged(object sender, Microsoft.Win32.PowerModeChangedEventArgs e)
        {
            if (e.Mode != Microsoft.Win32.PowerModes.Resume) return;
            Thread.Sleep(16000);
            AppleKeyboardHID2.Shutdown();
            AppleKeyboardHID2.Start();
        }

        void AppleKeyboardHID_KeyUp(AppleKeyboardSpecialKeys key)
        {
            if (key == AppleKeyboardSpecialKeys.Fn)
                KeyboardListener.ModifierFn = false;
        }

        void AppleKeyboardHID_KeyDown(AppleKeyboardSpecialKeys key)
        {
            switch (key)
            {
                case AppleKeyboardSpecialKeys.Fn:
                    KeyboardListener.ModifierFn = true;
                    break;
                case AppleKeyboardSpecialKeys.Eject:
                    KeyboardHandler.HandleEject();
                    break;
            }
        }

        bool KeyboardListener_KeyDown(KeyboardListener.KeyHookEventArgs e)
        {
            return KeyboardHandler.HandleKeyDown(e);
        }

        private void Application_Exit(object sender, ExitEventArgs e)
        {
            KeyboardListener.UnRegister();
            AppleKeyboardHID2.Shutdown();
        }
    }
}
