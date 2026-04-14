using System.Diagnostics;
using System.IO;
using System;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using praktika21_30_.Pages;

namespace praktika21_30_.Classes
{
    public class Functions
    {
        public static void SetNotification(Label LNameUser, string message, SolidColorBrush color)
        {
            LNameUser.Content = message;
            LNameUser.Foreground = color;
        }

        public static void Animation(Label LNameUser, Image IUser)
        {
            SetNotification(LNameUser, "Hi, " + MainWindow.mainWindow.UserLogIn.Name, Brushes.Black);
            try
            {
                BitmapImage biImg = new BitmapImage();
                MemoryStream ms = new MemoryStream(MainWindow.mainWindow.UserLogIn.Image);
                biImg.BeginInit();
                biImg.StreamSource = ms;
                biImg.EndInit();
                ImageSource imgSrc = biImg;
                DoubleAnimation StartAnimation = new DoubleAnimation();
                StartAnimation.From = 1;
                StartAnimation.To = 0;
                StartAnimation.Duration = TimeSpan.FromSeconds(0.6);
                StartAnimation.Completed += delegate
                {
                    IUser.Source = imgSrc;
                    DoubleAnimation EndAnimation = new DoubleAnimation();
                    EndAnimation.From = 0;
                    EndAnimation.To = 1;
                    EndAnimation.Duration = TimeSpan.FromSeconds(1.2);
                    IUser.BeginAnimation(Image.OpacityProperty, EndAnimation);
                };
                IUser.BeginAnimation(Image.OpacityProperty, StartAnimation);
            }
            catch (Exception exp)
            {
                Debug.WriteLine(exp.Message);
            }
        }

        public static void Animation(Label LNameUser, Image IUser, string source)
        {
            LNameUser.Content = "";
            DoubleAnimation StartAnimation = new DoubleAnimation();
            StartAnimation.From = 1;
            StartAnimation.To = 0;
            StartAnimation.Duration = TimeSpan.FromSeconds(0.6);
            StartAnimation.Completed += delegate
            {
                IUser.Source = new BitmapImage(new Uri("pack://application:,,,/Images/profile.png"));
                DoubleAnimation EndAnimation = new DoubleAnimation();
                EndAnimation.From = 0;
                EndAnimation.To = 1;
                EndAnimation.Duration = TimeSpan.FromSeconds(1.2);
                IUser.BeginAnimation(Image.OpacityProperty, EndAnimation);
            };
            IUser.BeginAnimation(Image.OpacityProperty, StartAnimation);
        }
    }
}
