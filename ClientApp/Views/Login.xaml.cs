using ClientApp.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Text.RegularExpressions;
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
    public sealed partial class Login : Page
    {
        StorageFolder storageFolder = ApplicationData.Current.LocalFolder;

        private DangNhap currentDN;
        private API_URL link;
        public Login()
        {
            this.InitializeComponent();
            checkLogin();
            this.currentDN = new DangNhap();
            this.link = new API_URL();
        }

        private void HyperlinkButton_Click(object sender, RoutedEventArgs e)
        {
            
        }

        public async void checkLogin()
        {
            StorageFile data = await storageFolder.GetFileAsync("UserLogin.txt");
            string stringdata = await FileIO.ReadTextAsync(data);
            if (stringdata != "")
            {
                AccessInfo accessInfo = JsonConvert.DeserializeObject<AccessInfo>(stringdata);
                string formatDateTime = accessInfo.expireAt.Remove(19);
                formatDateTime = formatDateTime.Replace("T", " ");
                DateTime ExpiredTime = DateTime.ParseExact(formatDateTime, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
                if (ExpiredTime > DateTime.Now)
                {
                    this.Frame.Navigate(typeof(Menu));
                }
            }
        }
        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            if (Regex.IsMatch(Email.Text, "[@]") && Regex.IsMatch(Email.Text, "[.]") && Password.Password != null)
            {
                currentDN.Email = this.Email.Text;
                currentDN.Password = this.Password.Password;
                string jsonString = JsonConvert.SerializeObject(currentDN);

                HttpClient httpClient = new HttpClient();
                var jsonContent = new StringContent(jsonString, Encoding.UTF8, "application/json");
                var response = httpClient.PostAsync(new Uri(link.Login), jsonContent);
                var contents = await response.Result.Content.ReadAsStringAsync();
                if (response.Result.StatusCode == HttpStatusCode.OK)
                {
                    StorageFile sampleFile = await storageFolder.CreateFileAsync("UserLogin.txt", CreationCollisionOption.ReplaceExisting);
                    await FileIO.WriteTextAsync(sampleFile, contents);
                    this.Frame.Navigate(typeof(Menu));
                }
            }
            this.Nontification.Text = "* Email hoặc mật khẩu không hợp lệ!";
            this.Nontification.Visibility = Visibility.Visible;
        }
    }
}
