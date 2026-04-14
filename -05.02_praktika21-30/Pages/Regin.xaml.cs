using Microsoft.Win32;
using System;
using System.IO;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Aspose.Imaging;
using praktika21_30_.Classes;

namespace praktika21_30_.Pages
{
    /// <summary>
    /// Логика взаимодействия для Regin.xaml
    /// </summary>
    public partial class Regin : Page
    {

        OpenFileDialog FileDialogImage = new OpenFileDialog();
        bool bCorrectLogin = false;
        bool BCorrectPassword = false;
        bool BCorrectConfirmPassword = false;
        bool BSetImages = false;

        public Regin()
        {
            InitializeComponent();

            MainWindow.mainWindow.UserLogIn.HandlerCorrectLogin += CorrectLogin;
            MainWindow.mainWindow.UserLogIn.HandlerInCorrectLogin += InCorrectLogin;
            FileDialogImage.Filter = "PNG (*.png)|*.png|JPG (*.jpg)|*.jpg";
            FileDialogImage.RestoreDirectory = true;
            FileDialogImage.Title = "Choose a photo for ypur avatar";
        }

        private void CorrectLogin()
        {
            Functions.SetNotification(LNameUser, "Login already in use", Brushes.Red);
            bCorrectLogin = false;
        }

        private void InCorrectLogin() => Functions.SetNotification(LNameUser, "", Brushes.Black);

        private void SetLogin(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) SetLogin();
        }

        private void SetLogin(object sender, System.Windows.RoutedEventArgs e) {
            SetLogin();
        }

        public void SetLogin()
        {
            if (RegexMath.Math(bCorrectLogin, @"([a-zA-Z0-9._-]{4,}@[a-zA-Z0-9._-]{2,}\.[a-zA-Z0-9_-]{2,})", TbLogin.Text) == true)
            {
                Functions.SetNotification(LNameUser, "", Brushes.Black);
                MainWindow.mainWindow.UserLogIn.GetUserLogin(TbLogin.Text);
            }
            else Functions.SetNotification(LNameUser, "Invalid login", Brushes.Red);
            OnRegin();
        }

        #region SetPassword

        private void SetPassword(object sender, System.Windows.RoutedEventArgs e) => SetPassword();

        private void SetPassword(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) SetPassword();
        }

        public void SetPassword()
        {
            if (RegexMath.Math(BCorrectPassword, @"(?=.*[0-9])(?=.*[!@#$%^&?*\-_=])(?=.*[a-z])(?=.*[A-Z])[0-9a-zA-Z!@#$%^&?*\-_=]{10,}", TbPassword.Password) == true)
            {
                Functions.SetNotification(LNameUser, "", Brushes.Black);
                if (TbConfirmPassword.Password.Length > 0)
                    ConfirmPassword(true);
                OnRegin();
            }
            else Functions.SetNotification(LNameUser, "Invalid password", Brushes.Red);
        }

        #endregion

        #region SetConfirmPassword

        private void ConfirmPassword(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) ConfirmPassword();
        }
        
        private void ConfirmPassword(object sender, System.Windows.RoutedEventArgs e) =>
            ConfirmPassword();

        public void ConfirmPassword(bool Pass = false)
        {
            BCorrectConfirmPassword = TbConfirmPassword.Password == TbPassword.Password;

            if (TbConfirmPassword.Password != TbPassword.Password)
            {
                Functions.SetNotification(LNameUser, "Passwords do not match", Brushes.Red);
            }
            else
            {
                Functions.SetNotification(LNameUser, "", Brushes.Black);
                if (!Pass) SetPassword();
            }
        }

        #endregion

        void OnRegin()
        {
            if (!bCorrectLogin) return;
            if (TbName.Text.Length == 0) return;
            if (!BCorrectPassword) return;
            if (!BCorrectConfirmPassword) return;
            MainWindow.mainWindow.UserLogIn.Login = TbLogin.Text;
            MainWindow.mainWindow.UserLogIn.Password = TbPassword.Password;
            MainWindow.mainWindow.UserLogIn.Name = TbName.Text;

            if (BSetImages)
                MainWindow.mainWindow.UserLogIn.Image = File.ReadAllBytes(Directory.GetCurrentDirectory() +
                    @"\IUser.jpg");

            MainWindow.mainWindow.UserLogIn.DateUpdate = DateTime.Now;
            MainWindow.mainWindow.UserLogIn.DateCreate = DateTime.Now;
            MainWindow.mainWindow.OpenPage(new Confirmation(Confirmation.TypeConfirmation.Regin));
        }

        private void SetName(object sender, TextCompositionEventArgs e) =>
            e.Handled = !(Char.IsLetter(e.Text, 0));

        private void SelectImage(object sender, MouseButtonEventArgs e)
        {
            if (FileDialogImage.ShowDialog() == true)
            {
                using (Aspose.Imaging.Image image = Aspose.Imaging.Image.Load(FileDialogImage.FileName))
                {
                    int NewWidth = 0;
                    int NewHeight = 0;
                    if (image.Width > image.Height) {                     
                        NewWidth = (int)(image.Width * (256f / image.Height));
                        NewHeight = 256;
                    }
                    else
                    {
                        NewWidth = 256;
                        NewHeight = (int)(image.Height * (256f / image.Width));
                    }

                    image.Resize(NewWidth, NewHeight);
                    image.Save("IUser.jpg");
                }

                using (RasterImage rasterImage = (RasterImage)Aspose.Imaging.Image.Load("IUser.jpg"))
                {
                    if (!rasterImage.IsCached) rasterImage.CacheData();

                    int X = 0;
                    int Width = 256;
                    int Y = 0;
                    int Height = 256;

                    if (rasterImage.Width > rasterImage.Height)
                        X = (int)((rasterImage.Height - 256f) / 2);
                    else
                        Y = (int)((rasterImage.Height - 256f) / 2);

                    Rectangle rectangle = new Aspose.Imaging.Rectangle(X, Y, Width, Height);
                    rasterImage.Crop(rectangle);

                    rasterImage.Save("IUser.jpg");
                }

                Functions.Animation(LNameUser, IUser, Directory.GetCurrentDirectory() + @"\IUser.jpg");
                BSetImages = true;
            }
            else BSetImages = false;
        }

        private void OpenLogin(object sender, MouseButtonEventArgs e)
        {
            MainWindow.mainWindow.OpenPage(new Login());
        }
    }
}
