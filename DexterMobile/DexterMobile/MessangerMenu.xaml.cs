using DesktopProject_V3.DataBaseClass;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DexterMobile
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MessangerMenu : ContentPage
	{
        private string dbPath;
        public ObservableCollection<US> NP { get; set; }
        public MessangerMenu ()
		{
			InitializeComponent ();
            dbPath = DependencyService.Get<IPath>().GetDatabasePath(App.DbPath);
            NP = new ObservableCollection<US>();

            using (Model1 db = new Model1(dbPath))
            {
                if(Initial.OldLogin != null) Initial.login = Initial.OldLogin;
                    foreach(var user in db.Messages)
                    {
                    if(user.Sender == Initial.login)
                    {
                        try
                        {
                            if (NP == null || NP.FirstOrDefault(x => x.Name == user.Recipient).Name != null) continue;
                            else
                            {
                                Users us = db.Users.FirstOrDefault(x => x.LoginOfUser == user.Recipient);
                                Stream ms = new MemoryStream(us.Avatar);
                                US u = new US(ImageSource.FromStream(() => ms), us.LoginOfUser, us.NameOfUser);
                                NP.Add(u);
                                continue;
                            }
                        }
                        catch
                        {
                            Users us = db.Users.FirstOrDefault(x => x.LoginOfUser == user.Recipient);
                            Stream ms = new MemoryStream(us.Avatar);
                            US u = new US(ImageSource.FromStream(() => ms), us.LoginOfUser, us.NameOfUser);
                            NP.Add(u);
                            continue;
                        }
                    }
                    if(user.Recipient == Initial.login)
                    {
                        try
                        {


                            if (NP == null || NP.FirstOrDefault(x => x.Name == user.Sender).Name != null) continue;
                            else
                            {
                                Users us = db.Users.FirstOrDefault(x => x.LoginOfUser == user.Sender);
                                Stream ms = new MemoryStream(us.Avatar);
                                US u = new US(ImageSource.FromStream(() => ms), us.LoginOfUser, us.NameOfUser);
                                NP.Add(u);
                                continue;
                            }
                        }
                        catch
                        {
                            Users us = db.Users.FirstOrDefault(x => x.LoginOfUser == user.Sender);
                            Stream ms = new MemoryStream(us.Avatar);
                            US u = new US(ImageSource.FromStream(() => ms), us.LoginOfUser, us.NameOfUser);
                            NP.Add(u);
                            continue;
                        }
                    }
                    }
                var u1 = NP.FirstOrDefault(x => x.Login == Initial.login);
                NP.Remove(u1);
                if (NP != null)
                {
                    var N = NP.Distinct();
                    ListMessages.ItemsSource = N;
                }
            }
        }

        private async void Open_Clicked(object sender, EventArgs e)
        {
            US u = (US)ListMessages.SelectedItem;
            Initial.Receiver = u.Login;
            await Navigation.PushAsync(new Messanger());
        }

        private void Remove_Clicked(object sender, EventArgs e)
        {
            using(Model1 db = new Model1(dbPath))
            {
                US u = (US)ListMessages.SelectedItem;
                Initial.Receiver = u.Login;
                foreach (var message in db.Messages) 
                {
                    if (message.Sender == Initial.Receiver) db.Messages.Remove(message);
                    if(message.Recipient == Initial.Receiver) db.Messages.Remove(message);
                }
                db.SaveChanges();
                DisplayAlert("Переписка удалена", "Вы покинули переписку", "OK");
            }
        }
    }
}