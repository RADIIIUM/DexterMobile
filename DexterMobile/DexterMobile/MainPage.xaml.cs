using DesktopProject_V3.DataBaseClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DexterMobile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        private string dbPath;
        public MainPage()
        {
            InitializeComponent();
            dbPath = DependencyService.Get<IPath>().GetDatabasePath(App.DbPath);
            UserAva.Source = Initial.UserAvatar;
            using (Model1 db = new Model1(dbPath))
            {

            }
        }

        private async void Computers_Clicked(object sender, EventArgs e)
        {
            Initial.NameOfCategoryOfProduct = "Computers";
            await Navigation.PushAsync(new Goods());

        }

        private async void Notebook_Clicked(object sender, EventArgs e)
        {
            Initial.NameOfCategoryOfProduct = "Notebook";
            await Navigation.PushAsync(new Goods());
        }

        private async void Acsesuari_Clicked(object sender, EventArgs e)
        {
            Initial.NameOfCategoryOfProduct = "Accessory";
            await Navigation.PushAsync(new Goods());

        }

        private async void Comblect_Clicked(object sender, EventArgs e)
        {
            Initial.NameOfCategoryOfProduct = "Complect";
            await Navigation.PushAsync(new Goods());
        }


        private void UserProfileBtn_Clicked(object sender, EventArgs e)
        {

        }

        private void Store_Clicked(object sender, EventArgs e)
        {

        }
    }
}