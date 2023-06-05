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
        }

        private async void Messanger_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MessangerMenu());
        }

        private async void Users_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new UsersMenu());
        }

        private async void UserProfileBtn_Clicked(object sender, EventArgs e)
        {
            if(Initial.OldLogin != null) Initial.login = Initial.OldLogin;
            await Navigation.PushAsync(new Profile());
        }
    }
}