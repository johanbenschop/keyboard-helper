using System;
using System.Windows.Forms;
using System.Drawing;
using KeyboardHelper.Views;

namespace KeyboardHelper
{
    public static class TrayIcon
    {
        static TrayIcon()
        {
            NotifyIcon icon = new NotifyIcon();
            icon.Text = "AppleWirelessKeyboard";
            icon.Icon = new Icon(App.GetResourceStream(new Uri(@"pack://application:,,,/Gnome-Preferences-Desktop-Keyboard-Shortcuts.ico")).Stream);
            icon.Visible = true;

            var menuItems = new[] { 
                new MenuItem("Configure", TriggerConfigure),
                new MenuItem("Restart", TriggerRestart),
                new MenuItem("Refresh", TriggerRefresh),
                new MenuItem("Exit", TriggerExit)
            };

            ContextMenu menu = new ContextMenu(menuItems);
            icon.ContextMenu = menu;
        }

        public static void Show() { }

        private static void TriggerRestart(object sender, EventArgs e)
        {
            Application.Restart();
        }
        private static void TriggerConfigure(object sender, EventArgs e)
        {
            (new Configuration()).Show();
        }
        private static void TriggerExit(object sender, EventArgs e)
        {
            //Application.Exit();
            Environment.Exit(0);
        }

        public static void TriggerRefresh(object sender, EventArgs e)
        {
            if (AppleKeyboardHID2.Registered)
                AppleKeyboardHID2.Shutdown();
            AppleKeyboardHID2.Start();
        }
    }
}
