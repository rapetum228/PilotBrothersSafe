using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using ToggleState = PilotBrothersSafe.Models.ToggleState;

namespace PilotBrothersSafe.Controls
{
    public class Toggle : ContentControl
    {
        private const int ToggleImageSize = 20;
        private const string PathToImage = "../Images/toggle.png";

        public static readonly DependencyProperty CurrentToggleStateProperty =
            DependencyProperty.Register(
                nameof(CurrentToggleState),
                typeof(ToggleState),
                typeof(Toggle),
                new PropertyMetadata(ToggleState.Horizontal, ToggleStateChanged));
        public Toggle(ToggleState stateInit)
        {
            DataContext = this;
            CurrentToggleState = stateInit;
        }

        public ToggleState CurrentToggleState
        {
            get
            {
                return (ToggleState)GetValue(CurrentToggleStateProperty);
            }

            private set
            {
                SetValue(CurrentToggleStateProperty, value);
            }
        }

        private static void ToggleStateChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args)
        {
            if (args.OldValue != args.NewValue)
            {
                ((Toggle)dependencyObject).SetToggleImage(); //rotate
            }
        }
        private void SetToggleImage()
        {
            Content = GetImage();

        }

        private Image GetImage()
        {
            BitmapImage bi = new BitmapImage();
            bi.BeginInit();
            bi.UriSource = new Uri(PathToImage, UriKind.Relative);
            bi.EndInit();

            int angle = (int)CurrentToggleState;
            RotateTransform rotateTransform = new RotateTransform(angle);
            rotateTransform.CenterX = ToggleImageSize / 2;
            rotateTransform.CenterY = ToggleImageSize / 2;

            return new Image
            {
                Source = bi,
                Width = ToggleImageSize,
                Height = ToggleImageSize,
                RenderTransform = rotateTransform,
            };
        }
        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);
            SetToggleImage();

        }

        public int ChangeState()
        {
            if (CurrentToggleState == ToggleState.Horizontal)
            {
                CurrentToggleState = ToggleState.Vertical;
                return (int)ToggleState.Vertical;
            }
            else
            {
                CurrentToggleState = ToggleState.Horizontal;
                return -(int)ToggleState.Vertical;
            }
        }
    }
}
