using ClientApp.Entities;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
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
        private Student studentInfo;
        private AccessInfo currentInfo;
        private EditInfo editInfo;
        public AccountInformation()
        {
            this.InitializeComponent();
            GetData();
            this.studentInfo = new Student();
            this.link = new API_URL();
            this.currentInfo = new AccessInfo();
            this.editInfo = new EditInfo();
        }
        public async void GetData()
        {
            StorageFile userFile = await storageFolder.GetFileAsync("UserLogin.txt");
            string localFile = await FileIO.ReadTextAsync(userFile);
            AccessInfo currentInfo = JsonConvert.DeserializeObject<AccessInfo>(localFile);

            StorageFile UserFile = await storageFolder.GetFileAsync("UserData.txt");
            string textFile = await FileIO.ReadTextAsync(UserFile);
            if (textFile == "")
            {
                HttpClient httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", "Basic " + currentInfo.accessToken);
                var response = httpClient.GetAsync(link.GetInfo);
                var contents = await response.Result.Content.ReadAsStringAsync();
                Student studentInfo = JsonConvert.DeserializeObject<Student>(contents);
                StorageFile sampleFile = await storageFolder.CreateFileAsync("UserData.txt", CreationCollisionOption.ReplaceExisting);
                await FileIO.WriteTextAsync(sampleFile, contents);
                FullName.Text = studentInfo.firstName + studentInfo.lastName;
                RollNumber.Text = studentInfo.accountId.ToString();
                DateOfBirth.Text = studentInfo.bod.ToString("yyyy-MM-dd");
                Address.Text = studentInfo.address;
                Phone.Text = studentInfo.phone;
                Gender.Text = studentInfo.gender.ToString();
                Grade.Text = studentInfo.account.gradeStudents[0].grade.gradeName;
            }
            else
            {
                Student studentInfo = JsonConvert.DeserializeObject<Student>(textFile);
                FullName.Text = studentInfo.firstName + " " + studentInfo.lastName;
                RollNumber.Text = studentInfo.accountId.ToString();
                DateOfBirth.Text = studentInfo.bod.ToString("yyyy-MM-dd");
                Address.Text = studentInfo.address;
                Phone.Text = studentInfo.phone;
                Email.Text = studentInfo.account.email;
                
                switch (studentInfo.gender.ToString())
                {
                    case "0":
                        Gender.Text = "Nữ";
                        break;
                    case "1":
                        Gender.Text = "Nam";
                        break;
                    case "2":
                        Gender.Text = "Khác";
                        break;
                }
                Grade.Text = studentInfo.account.gradeStudents[0].grade.gradeName;
                //Edit Form
                firstName.Text = studentInfo.firstName;
                lastName.Text = studentInfo.lastName;
                editAddress.Text = studentInfo.address;
                editPhone.Text = studentInfo.phone;
                editAvatar.Text = studentInfo.avatar;
                GenderComboBox.SelectedIndex = studentInfo.gender;
                DoB.Date = studentInfo.bod;
            }

        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ShowInfo.Visibility = Visibility.Collapsed;
            EditInfo.Visibility = Visibility.Visible;
        }
        private async void Button_Edit_Click(object sender, RoutedEventArgs e)
        {
            ShowInfo.Visibility = Visibility.Visible;
            EditInfo.Visibility = Visibility.Collapsed;
            editInfo.firstName = firstName.Text;
            editInfo.lastName = lastName.Text;
            editInfo.bod = DoB.Date;
            editInfo.phone = editPhone.Text;
            editInfo.address = editAddress.Text;
            editInfo.avatar = editAvatar.Text;
            switch (GenderComboBox.SelectedValue.ToString())
            {
                case "Nữ":
                    editInfo.gender = "0";
                    break;
                case "Nam":
                    editInfo.gender = "1";
                    break;
                case "Khác":
                    editInfo.gender = "2";
                    break;
            }

            StorageFile userFile = await storageFolder.GetFileAsync("UserLogin.txt");
            string localFile = await FileIO.ReadTextAsync(userFile);
            AccessInfo currentInfo = JsonConvert.DeserializeObject<AccessInfo>(localFile);
            string SendData = JsonConvert.SerializeObject(editInfo);
            var content = new StringContent(SendData, Encoding.UTF8, "application/json");
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", "Basic " + currentInfo.accessToken);
            var response = httpClient.PostAsync(link.EditInfo, content);
            var contents = await response.Result.Content.ReadAsStringAsync();
            if (response.Result.StatusCode == HttpStatusCode.OK)
            {
                StorageFile sampleFile = await storageFolder.CreateFileAsync("UserData.txt", CreationCollisionOption.ReplaceExisting);
                Nontification.Text = "Cập Nhật Thành Công, Tải Lại Hiển Thị!";
                Nontification.Visibility = Visibility.Visible;
            }
        }

        private void Button_Cancel_Click(object sender, RoutedEventArgs e)
        {
            ShowInfo.Visibility = Visibility.Visible;
            EditInfo.Visibility = Visibility.Collapsed;
        }
    }
}
