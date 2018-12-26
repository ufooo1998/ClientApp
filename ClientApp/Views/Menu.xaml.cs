using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace ClientApp.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Menu : Page
    {
        public Menu()
        {
            this.InitializeComponent();
        }


        private void Nav_Menu_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            TextBlock ItemContent = args.InvokedItem as TextBlock;
            if (ItemContent != null)
            {
                switch (ItemContent.Tag)
                {
                    case "Nav_CaNhan":
                        Content_Header.Text = "Cá Nhân";
                        contentFrame.Navigate(typeof(Views.AccountInformation));
                        break;

                    case "Nav_LopHoc":
                        Content_Header.Text = "Lớp Học";
                        contentFrame.Navigate(typeof(Views.ClassInformation));
                        break;

                    case "Nav_MonHoc":
                        Content_Header.Text = "Môn Học";
                        contentFrame.Navigate(typeof(Views.SubjectInformation));
                        break;
                }
            }
        }

        private async void NavigationViewItem_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ContentDialog noWifiDialog = new ContentDialog
            {
                Title = "Đăng Xuất",
                Content = "Bạn có muốn đăng xuất khỏi ứng dụng?",
                PrimaryButtonText = "OK",
                CloseButtonText = "Cancel"
            };

            ContentDialogResult result = await noWifiDialog.ShowAsync();

            // Delete the file if the user clicked the primary button.
            /// Otherwise, do nothing.
            if (result == ContentDialogResult.Primary)
            {
                StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
                await storageFolder.CreateFileAsync("UserData.txt", CreationCollisionOption.ReplaceExisting);
                await storageFolder.CreateFileAsync("UserLogin.txt", CreationCollisionOption.ReplaceExisting);
                this.Frame.Navigate(typeof(Login));
            }
            else
            {
                // The user clicked the CLoseButton, pressed ESC, Gamepad B, or the system back button.
                // Do nothing.
            }
        }
    }
}
