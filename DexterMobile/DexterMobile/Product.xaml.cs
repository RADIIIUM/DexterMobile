using DesktopProject_V3.DataBaseClass;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Shapes;
using Xamarin.Forms.Xaml;

namespace DexterMobile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Product : ContentPage
    {
        public byte[] avatar;
        private string dbPath;
        public Product()
        {
            InitializeComponent();
            dbPath = DependencyService.Get<IPath>().GetDatabasePath(App.DbPath);
            if (Initial.IDProduct != -1)
            {
                AddGood.IsVisible = false;
                using (Model1 db = new Model1(dbPath))
                {
                    Products prd = db.Products.FirstOrDefault(x => x.ID_Product == Initial.IDProduct);
                    string tp = null;
                    try
                    {
                        tp = db.TypesOfProducts.FirstOrDefault(x => x.ID_Type == db.Types_Products.FirstOrDefault(y => y.ID_Product == Initial.IDProduct).ID_Type).NameOfType;

                        Name.IsReadOnly = true;
                        Name.Text = prd.NameOfProduct;
                        Cena.IsReadOnly = true;
                        Name.Text = prd.Price.ToString();
                        Akcia.IsReadOnly = true;
                        Akcia.Text = prd.Discount.ToString();
                        Desc.IsReadOnly = true;
                        Desc.Text = prd.DescriptionOfProduct;
                        Spec.IsReadOnly = true;
                        Spec.Text = prd.Specifications;
                        Type.IsReadOnly = true;
                        Type.Text = tp;
                        AvaDonwloadBtn.IsVisible = false;
                        AvaBtn.IsVisible = false;
                    }
                    catch
                    {
                        tp = null;
                    }



                }
            }
            else
            {
                AddGood.IsVisible = true;
                Name.IsReadOnly = false;
                Cena.IsReadOnly = false;
                Akcia.IsReadOnly = false;
                Desc.IsReadOnly = false;
                Spec.IsReadOnly = false;
                Type.IsReadOnly = false;
                AvaDonwloadBtn.IsVisible = true;
                AvaBtn.IsVisible = true;
            }
        }

        private void AddGood_Clicked(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(Name.Text) ||
                string.IsNullOrEmpty(Cena.Text) ||
                (string.IsNullOrEmpty(Akcia.Text) || Akcia.Text != "0") ||
                string.IsNullOrEmpty(Desc.Text) || string.IsNullOrEmpty(Spec.Text) ||
                string.IsNullOrEmpty(Type.Text) || this.avatar == null)
            {
                DisplayAlert("Неправильные данные", "Ни одно из полей не должно быть пустым\nТакже проверьте загрузили ли вы фото", "ОК");
            }
            else
            {
                using(Model1 db = new Model1(dbPath))
                {
                    Products prd = new Products(Name.Text, this.avatar, int.Parse(Cena.Text), Desc.Text, int.Parse(Akcia.Text), Spec.Text);
                    TypesOfProducts tp = new TypesOfProducts(Type.Text);
                    Types_Products t_p = new Types_Products(prd.ID_Product, tp.ID_Type);
                    db.Products.Add(prd);
                    db.SaveChanges();
                    db.TypesOfProducts.Add(tp);
                    db.SaveChanges();
                    db.Types_Products.Add(t_p);
                    db.SaveChanges();
                    DisplayAlert("Готово!", "Товар был добавлен", "ОК");

                }
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
                var newFile = System.IO.Path.Combine(FileSystem.AppDataDirectory, photo.FileName);
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