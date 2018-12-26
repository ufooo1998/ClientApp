using ClientApp.Entities;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace ClientApp.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AccountInformation : Page
    {
        StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
        private API_URL link;
        private SinhVien currentSV;
        private AccessInfo currentInfo;
        public AccountInformation()
        {
            this.InitializeComponent();
            GetLocalData();
            GetData();
            this.currentSV = new SinhVien();
            this.link = new API_URL();
            this.currentInfo = new AccessInfo();
        }
        public async void GetLocalData()
        {
            StorageFile userFile = await storageFolder.GetFileAsync("UserLogin.txt");
            string localFile = await FileIO.ReadTextAsync(userFile);
            AccessInfo currentInfo = JsonConvert.DeserializeObject<AccessInfo>(localFile);
        }
        public async void GetData()
        {
            StorageFile UserFile = await storageFolder.GetFileAsync("UserData.txt");
            string textFile = await FileIO.ReadTextAsync(UserFile);
            Debug.WriteLine(textFile);
            if (textFile == "")
            {
                HttpClient httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", currentInfo.accessToken);
                var response = httpClient.GetAsync(link.GetInfo);
                var contents = await response.Result.Content.ReadAsStringAsync();
                Debug.WriteLine(contents);



            //    StorageFile sampleFile = await storageFolder.CreateFileAsync("UserData.txt", CreationCollisionOption.ReplaceExisting);
            //    await FileIO.WriteTextAsync(sampleFile, contents);
            }
            string data = await FileIO.ReadTextAsync(UserFile);
            SinhVien currentSV = JsonConvert.DeserializeObject<SinhVien>(data);
            //HoTen.Text = currentSV.hoTen;
            //MaSinhVien.Text = currentSV.maSinhVien.ToString();
            //NgaySinh.Text = currentSV.ngaySinh;
            //QueQuan.Text = currentSV.queQuan;
            //SoDienThoai.Text = currentSV.soDienThoai;
            //Email.Text = currentSV.email;
            //GioiTinh.Text = currentSV.gioiTinh;
            //Lop.Text = currentSV.lopHoc;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
