using DesktopProject_V3.DataBaseClass;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DexterMobile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Autorization : ContentPage
    {
        private string dbPath;
        public Autorization()
        {
            InitializeComponent();
            dbPath = DependencyService.Get<IPath>().GetDatabasePath(App.DbPath);
        }

        private async void AutButton_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Login.Text) || string.IsNullOrWhiteSpace(Pass.Text))
            {
                DisplayAlert("Пустые поля", "Поле логина/пароля оказалось пустым", "Закрыть");
            }
            else
            {
                using (Model1 db = new Model1(dbPath))
                {
                    foreach (var client in db.Users)
                    {
                        if (Login.Text == client.LoginOfUser)
                        {
                            if (Pass.Text == client.PasswordOfUser)
                            {

                                try
                                {
                                    string role = db.Roles.FirstOrDefault(x => x.LoginOfUsers == client.LoginOfUser).NameOfRole;
                                    Initial.Role = role;
                                }
                                catch
                                {

                                }
                                Initial.login = client.LoginOfUser;
                                Stream ms = new MemoryStream(client.Avatar);
                                Initial.UserAvatar = ImageSource.FromStream(() => ms);
                                await Navigation.PushAsync(new MainPage());
                            }
                            else
                            {
                                DisplayAlert("Неверный логин/пароль", "Неверный логин / пароль", "Закрыть");

                            }
                        }
                    }
                }
            }
        }

        private async void RegButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Registration());
        }
    }
}