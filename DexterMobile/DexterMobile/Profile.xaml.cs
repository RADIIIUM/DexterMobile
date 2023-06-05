using DesktopProject_V3.DataBaseClass;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DexterMobile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Profile : ContentPage
    {
        private string dbPath;
        public byte[] avatar;
        public Profile()
        {
            InitializeComponent();
            Role.Items.Add("Пользователь");
            Role.Items.Add("Модератор");
            Role.Items.Add("Администратор");
            dbPath = DependencyService.Get<IPath>().GetDatabasePath(App.DbPath);
            using (Model1 db = new Model1(dbPath))
            {
                try {
                    Users us = db.Users.FirstOrDefault(x => x.LoginOfUser == Initial.login);
                    Stream ms = new MemoryStream(us.Avatar);
                    this.avatar = us.Avatar;
                    Ava.Source = ImageSource.FromStream(() => ms);
                    Name.Text = us.NameOfUser;
                    string rol = db.Roles.FirstOrDefault(x => x.LoginOfUsers == Initial.login).NameOfRole;
                    RoleLabel.Text = rol;
                    Role.SelectedIndex = Role.Items.IndexOf(rol);
                    Email.Text = us.Email;

                    if(rol == "Заблокированный")
                    {
                        ButtonBan.Text = "Разблокировать";
                    }
                    else
                    {
                        ButtonBan.Text = "Заблокировать";
                    }

                    if (Initial.Role == "Модератор" || Initial.Role == "Администратор")
                    {
                        ButtonBan.IsVisible = true;
                    }
                    else
                    {
                        ButtonBan.IsVisible = false;
                    }

                    if (Initial.ShowProfile == false || Initial.login == Initial.OldLogin)
                    {
                        AvaBtn.IsVisible = true;
                        AvaDonwloadBtn.IsVisible = true;
                        Name.IsReadOnly = false;
                        Email.IsReadOnly = false;
                        ChangePass.IsVisible = true;
                        ButtonDelete.IsVisible = true;
                        RoleLabel.IsVisible = true;
                        ButtonBan.IsVisible = false;
                        ButtonMessage.IsVisible = false;
                        ButtonSave.IsVisible = true;
                    }
                    else
                    {
                        ButtonSave.IsVisible = false;
                        AvaBtn.IsVisible = false;
                        AvaDonwloadBtn.IsVisible = false;
                        Name.IsReadOnly = true;
                        Email.IsReadOnly = true;
                        RoleLabel.IsVisible = true;
                        ChangePass.IsVisible = false;
                        ButtonDelete.IsVisible = false;
                        ButtonMessage.IsVisible = true;
                    }
                    if (Initial.Role.Replace(" ", "") == "Администратор")
                    {
                        Role.IsVisible = true;
                        RoleLabel.IsVisible = false;
                        ButtonSave.IsVisible = true;
                    }
                    else
                    {
                        RoleLabel.IsVisible = true;
                        Role.IsVisible = false;
                        ButtonSave.IsVisible = false;
                    }
                }
                catch
                {

                }
                }
        }

        private void ButtonSave_Clicked(object sender, EventArgs e)
        {
            using(Model1 db = new Model1(dbPath))
            {
                if(string.IsNullOrEmpty(Name.Text) || string.IsNullOrEmpty(Email.Text) || this.avatar == null)
                {
                    DisplayAlert("Ошибка", "Ни одно из полей не должно быть пустым", "OK");
                    return;
                }
                Users us = db.Users.FirstOrDefault(x => x.LoginOfUser == Initial.login);
                Roles rol = db.Roles.FirstOrDefault(x => x.LoginOfUsers == Initial.login);
                us.Avatar = this.avatar;
                us.NameOfUser = Name.Text;
                us.Email = Email.Text;
                rol.NameOfRole = Role.SelectedItem.ToString();
                db.SaveChanges();
                DisplayAlert("Изменения сохранены", "Ваши изменения были сохранены", "OK");
            }
        }

        private async void ButtonDelete_Clicked(object sender, EventArgs e)
        {
            using (Model1 db = new Model1(dbPath))
            {
                Users us = db.Users.FirstOrDefault(x => x.LoginOfUser == Initial.login);
                bool result = await DisplayAlert("Подтвердить действие", "Вы хотите удалить аккаунт?", "Да", "Нет");
                if(result)
                {
                    db.Remove(us);
                    db.SaveChanges();
                    DisplayAlert("Готово", "Аккаунт был удален", "OK");
                    Initial.login = null;
                    Initial.OldLogin = null;
                    Initial.UserAvatar = null;
                    await Navigation.PushAsync(new Autorization());
                }
                
            }
        }

        private async void ButtonBan_Clicked(object sender, EventArgs e)
        {
            using (Model1 db = new Model1(dbPath))
            {
                Roles rol = db.Roles.FirstOrDefault(x => x.LoginOfUsers == Initial.login);
                if (Initial.login == Initial.OldLogin || rol.NameOfRole == "Администратор" || (Initial.Role == "Модератор" && rol.NameOfRole == "Модератор"))
                {
                    DisplayAlert("Отмена", "Блокировка самого себя/администратора/модератора запрещена", "ОТМЕНА");
                }
                else
                {
                    if(rol.NameOfRole == "Заблокированный")
                    {
                        bool result1 = await DisplayAlert("Подтвердить действие", "Вы хотите РАЗБЛОКИРОВАТЬ аккаунт?", "Да", "Нет");
                        if (result1)
                        {
                            rol.NameOfRole = "Пользователь";
                            db.SaveChanges();
                            DisplayAlert("Готово", "Аккаунт был РАЗБЛОКИРОВАН", "OK");
                        }
                        return;
                    }

                    bool result2 = await DisplayAlert("Подтвердить действие", "Вы хотите ЗАБЛОКИРОВАТЬ аккаунт?", "Да", "Нет");
                    if (result2)
                    {
                        rol.NameOfRole = "Заблокированный";
                        db.SaveChanges();
                        DisplayAlert("Готово", "Аккаунт был ЗАБЛОКИРОВАН", "OK");
                    }
                    return;
                }
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

        private void ButtonChangePass_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Pass.Text) || string.IsNullOrEmpty(RepeatePass.Text))
            {
                DisplayAlert("Пустые поля", "Ни одно из полей не должно быть пустым", "OK");
                return;
            }
            if(Pass.Text != RepeatePass.Text)
            {
                DisplayAlert("Пароли не совпадают", "Пароли не совпадают", "OK");
                return;
            }
            else
            {
                using (Model1 db = new Model1(dbPath))
                {
                    Users us = db.Users.FirstOrDefault(x => x.LoginOfUser == Initial.login);
                    us.PasswordOfUser = Pass.Text;
                    db.SaveChanges();
                    DisplayAlert("Пароль изменен", "Ваш пароль сохранен", "OK");
                }
            }
        }

        private async void ButtonMessage_Clicked(object sender, EventArgs e)
        {
            using(Model1 db = new Model1(dbPath))
            {
                Messages msg = db.Messages.FirstOrDefault(x => x.Sender == Initial.OldLogin || Initial.OldLogin == x.Recipient) ?? null;
                if (msg == null)
                {
                    bool result = await DisplayAlert("Подтвердить действие", "Вы хотите начать переписку?", "Да", "Нет");
                    if (result)
                    {
                        Messages m = new Messages(Initial.OldLogin, Initial.login);
                        db.Messages.Add(m);
                        db.SaveChanges();
                        DisplayAlert("Готово", "Перейдите в мэсснеджер", "OK");
                    }
                    return;
                }
            }
        }
    }
}