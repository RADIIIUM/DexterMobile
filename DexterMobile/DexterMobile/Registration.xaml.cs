using DesktopProject_V3.DataBaseClass;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DexterMobile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Registration : ContentPage
    {
        public byte[] avatar;
        string dbPath;
        public Registration()
        {
            InitializeComponent();
            dbPath = DependencyService.Get<IPath>().GetDatabasePath(App.DbPath);
            Role.Items.Add("Пользователь");
            Role.Items.Add("Модератор");
            Role.Items.Add("Администратор");
        }


        private void Button_Clicked(object sender, EventArgs e)
        {
            if (RepeatePass.Text == Pass.Text && Role.SelectedItem != null
                && !(string.IsNullOrEmpty(RepeatePass.Text)) && !(string.IsNullOrEmpty(Pass.Text))
                && !(string.IsNullOrEmpty(Login.Text)) && this.avatar != null)
            {
                using (Model1 db = new Model1(dbPath))
                {

                    try
                    {
                        string login = db.Users.First(x => x.LoginOfUser == Login.Text).LoginOfUser;
                        if (login == null)
                        {
                            Users us = new Users(Login.Text, Pass.Text, Email.Text, this.avatar);
                            db.Users.Add(us);
                            DisplayAlert("Успешно!", "Пользователь зарегестрирован", "OK");
                            db.SaveChanges();
                        }
                        else
                        {
                            DisplayAlert("Ошибка!", "Данный пользователь уже существует", "OK");
                        }
                    }
                    catch
                    {
                        string login = null;
                        if (login == null)
                        {
                            Users us = new Users(Login.Text, Pass.Text, Email.Text, this.avatar);
                            Roles role = new Roles(Role.SelectedItem.ToString(), Login.Text);
                            db.Roles.Add(role);
                            db.Users.Add(us);
                            DisplayAlert("Успешно!", "Пользователь зарегестрирован", "OK");
                            db.SaveChanges();
                        }
                        else
                        {
                            DisplayAlert("Ошибка!", "Данный пользователь уже существует", "OK");
                        }
                    }

                }
                DisplayAlert("Успешная регистрация!", "Вы успешно зарегестрировались", "Принять");
            }
            else
            {
                DisplayAlert("Ошибка!", "Проверьте правильность введенных паролей/полей", "ОК");
            }

        }

        private void P2_Clicked(object sender, EventArgs e)
        {
            if (RepeatePass.IsPassword == false)
            {
                RepeatePass.IsPassword = true;
            }
            else
            {
                RepeatePass.IsPassword = false;
            }
        }

        private void P1_Clicked(object sender, EventArgs e)
        {
            if (Pass.IsPassword == false)
            {
                Pass.IsPassword = true;
            }
            else
            {
                Pass.IsPassword = false;
            }
        }

        private async void ToSign_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private async void AvaBtn_Clicked(object sender, EventArgs e)
        {
            try
            {
                var photo = await MediaPicker.CapturePhotoAsync(new MediaPickerOptions
                {
                    Title = $"xamarin.{DateTime.Now.ToString("dd.MM.yyyy_hh.mm.ss")}.png"
                });

                // для примера сохраняем файл в локальном хранилище
                var newFile = Path.Combine(FileSystem.AppDataDirectory, photo.FileName);
                using (var stream = await photo.OpenReadAsync())
                using (var newStream = File.OpenWrite(newFile))
                    await stream.CopyToAsync(newStream);

                // загружаем в ImageView
                Ava.Source = ImageSource.FromFile(photo.FullPath);
                this.avatar = File.ReadAllBytes(photo.FullPath);
            }
            catch (Exception ex)
            {
                await DisplayAlert("Сообщение об ошибке", ex.Message, "OK");
            }
        }

        private async void AvaDonwloadBtn_Clicked(object sender, EventArgs e)
        {
            try
            {
                // выбираем фото
                var photo = await MediaPicker.PickPhotoAsync();
                // загружаем в ImageView
                Ava.Source = ImageSource.FromFile(photo.FullPath);
                this.avatar = File.ReadAllBytes(photo.FullPath);
            }
            catch (Exception ex)
            {
                await DisplayAlert("Сообщение об ошибке", ex.Message, "OK");
            }
        }

    }
}