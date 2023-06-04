using DesktopProject_V3.DataBaseClass;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        public ObservableCollection<string> NP { get; set; }
        public MessangerMenu ()
		{
			InitializeComponent ();
            dbPath = DependencyService.Get<IPath>().GetDatabasePath(App.DbPath);
            NP = new ObservableCollection<string>();

            using (Model1 db = new Model1(dbPath))
            {
                if(Initial.OldLogin != null) Initial.login = Initial.OldLogin;
                var userlist = from t in db.Messages
                           where (t.Sender == Initial.login || t.Recipient == Initial.login) && (t.Sender != t.Recipient)
                           orderby t.Sender
                           select t;
                var list = userlist.Where(x => x.Recipient != Initial.login || x.Sender != Initial.login).Distinct().ToArray();
                foreach (var user in list)
                {
                        if (user.Recipient == Initial.login) {
                            NP.Add($"{user.Sender}");
                        }
                        else {
                            NP.Add($"{user.Recipient}");
                        }
                }
                    if (NP != null) ListMessages.ItemsSource = NP;
            }
        }

        private async void Open_Clicked(object sender, EventArgs e)
        {
            Initial.Receiver = ListMessages.SelectedItem.ToString().Trim();
            await Navigation.PushAsync(new Messanger());
        }

        private void Search_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Remove_Clicked(object sender, EventArgs e)
        {

        }
    }
}