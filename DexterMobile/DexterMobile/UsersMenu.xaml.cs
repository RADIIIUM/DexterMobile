using DesktopProject_V3.DataBaseClass;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DexterMobile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]


    public partial class UsersMenu : ContentPage
    {
        private string dbPath;
        public ObservableCollection<US> NP { get; set; }
        public UsersMenu()
        {
            InitializeComponent();
            dbPath = DependencyService.Get<IPath>().GetDatabasePath(App.DbPath);
            NP = new ObservableCollection<US>();

            using (Model1 db = new Model1(dbPath))
            {
                foreach (var user in db.Users)
                {
                    if (user != null)
                    {
                        Stream ms = new MemoryStream(user.Avatar);
                        US u = new US(ImageSource.FromStream(() => ms), user.LoginOfUser, user.NameOfUser);
                            NP.Add(u);
                    }
                }
                if(NP != null) ListUsers.ItemsSource = NP;
            }
        }

        private async void Open_Clicked(object sender, EventArgs e)
        {

            using (Model1 db = new Model1(dbPath))
            {
                if (Initial.OldLogin == null)
                {
                    Initial.OldLogin = Initial.login;
                }
                US u = (US)ListUsers.SelectedItem;
                Initial.login = u.Login;
                Initial.ShowProfile = true;
                await Navigation.PushModalAsync(new Profile());
            }
            
        }

        private void Search_TextChanged(object sender, TextChangedEventArgs e)
        {
            using(Model1 db = new Model1(dbPath))
            {
                NP = new ObservableCollection<US>();
                if (!string.IsNullOrEmpty(Search.Text))
                {
                    var list = from t in db.Users
                               where t.LoginOfUser.ToLower().IndexOf(Search.Text.ToLower()) >= 0 || t.NameOfUser.ToLower().IndexOf(Search.Text.ToLower()) >= 0
                               select t;
                    foreach (var user in list)
                    {
                        if (user != null)
                        {
                            Stream ms = new MemoryStream(user.Avatar);
                            US u = new US(ImageSource.FromStream(() => ms), user.LoginOfUser, user.NameOfUser);
                            NP.Add(u);
                        }
                    }
                    if (NP != null) ListUsers.ItemsSource = NP;
                }
                else
                {
                    foreach (var user in db.Users)
                    {
                        if (user != null)
                        {
                            Stream ms = new MemoryStream(user.Avatar);
                            US u = new US(ImageSource.FromStream(() => ms), user.LoginOfUser, user.NameOfUser);
                            NP.Add(u);
                        }
                    }
                    if (NP != null) ListUsers.ItemsSource = NP;
                }
                            
            }
        }
    }
}