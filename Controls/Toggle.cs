using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows;
using PilotBrothersSafe.Models;
using ToggleState = PilotBrothersSafe.Models.ToggleState;
using System.Windows.Media;

namespace PilotBrothersSafe.Controls
{
    public class Toggle : ContentControl
    {
        private const int ToggleImageSize = 20;

        

        public static readonly DependencyProperty CurrentToggleStateProperty =
            DependencyProperty.Register(
                nameof(CurrentToggleState),
                typeof(ToggleState),
                typeof(Toggle),
                new PropertyMetadata(ToggleState.Horizontal, CellTypeChanged));
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
        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);
            SetToggleImage();

        }

        private static void CellTypeChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args)
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
            BitmapImage bi3 = new BitmapImage();
            bi3.BeginInit();
            bi3.UriSource = new Uri("../Images/toggle.png", UriKind.Relative);
            bi3.EndInit();

            int angle = (int)CurrentToggleState;
            RotateTransform rotateTransform = new RotateTransform(angle);
            rotateTransform.CenterX = ToggleImageSize/2;
            rotateTransform.CenterY = ToggleImageSize/2;

            return new Image
            {
                Source = bi3,
                Width = ToggleImageSize,
                Height = ToggleImageSize,
                RenderTransform = rotateTransform,
            };
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
