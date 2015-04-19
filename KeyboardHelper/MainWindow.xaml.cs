using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;

namespace KeyboardHelper
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly DoubleAnimationUsingKeyFrames _fade;

        public MainWindow()
        {
            InitializeComponent();
            _fade = new DoubleAnimationUsingKeyFrames { Duration = new Duration(TimeSpan.FromSeconds(3)) };
            _fade.KeyFrames.Add(new LinearDoubleKeyFrame(1, KeyTime.FromPercent(0)));
            _fade.KeyFrames.Add(new LinearDoubleKeyFrame(1, KeyTime.FromPercent(0.75)));
            _fade.KeyFrames.Add(new LinearDoubleKeyFrame(0, KeyTime.FromPercent(1)));
        }
        public void ShowOff(Control glyph, bool valueBar = false, int value = 0)
        {
            //this.DataContext = new { Glyph = glyph };

            //if (valueBar) ValueBar.Visibility = System.Windows.Visibility.Visible;
            //else ValueBar.Visibility = System.Windows.Visibility.Collapsed;

            //MakeValue(value);

            Opacity = 1;
            Slider.Value = BrightnessControl.BrightnessControl.GetHighestBrightness();
            this.Show();
            BeginAnimation(OpacityProperty, _fade);
        }

        public void ShowOff(bool valueBar = false, int value = 0)
        {
            //this.DataContext = new { Glyph = glyph };

            //if (valueBar) ValueBar.Visibility = System.Windows.Visibility.Visible;
            //else ValueBar.Visibility = System.Windows.Visibility.Collapsed;

            //MakeValue(value);

            Opacity = 1;
            Slider.Value = BrightnessControl.BrightnessControl.GetHighestBrightness();
            this.Show();
            BeginAnimation(OpacityProperty, _fade);
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            BrightnessControl.BrightnessControl.SetBrightness((int)Slider.Value);
        }

        private void Window_MouseEnter(object sender, MouseEventArgs e)
        {
            BeginAnimation(OpacityProperty, null);
            Opacity = 1;
        }

        private void Window_MouseLeave(object sender, MouseEventArgs e)
        {
            BeginAnimation(OpacityProperty, _fade);
        }
        //public void MakeValue(int value)
        //{
        //    ValueBar.Children.Clear();

        //    for (int i = 0; i <= value; i++)
        //    {
        //        Rectangle rect = new Rectangle();
        //        rect.Fill = new SolidColorBrush(Colors.White);
        //        rect.Width = 6;
        //        rect.Height = 6;
        //        rect.Margin = new Thickness(0, 0, 3, 0);
        //        ValueBar.Children.Add(rect);
        //    }
        //}
    }
}
