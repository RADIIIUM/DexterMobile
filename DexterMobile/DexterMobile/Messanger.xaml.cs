using DesktopProject_V3.DataBaseClass;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Xsl;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DexterMobile
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Messanger : ContentPage
	{
		private string dbPath;
		public Messanger()
		{
			InitializeComponent();
			dbPath = DependencyService.Get<IPath>().GetDatabasePath(App.DbPath);
			MessagesStack.Children.Clear();

            using (Model1 db = new Model1(dbPath))
			{
				var list = db.Messages.Where(x => x.Sender == Initial.login || Initial.login == x.Recipient);

				foreach (var message in list)
				{

						if (string.IsNullOrEmpty(message.Message)) continue;
						if (message.Sender.Replace(" ", "") == Initial.login.Replace(" ", "") && Initial.Receiver == message.Recipient)
						{
							Label sen = new Label()
							{
								Text = message.Message

							};
							sen.TextColor = Color.White;
							sen.Padding = 10;

							StackLayout senstack = new StackLayout();
							senstack.Margin = new Thickness(30, 10, 30, 0);
							senstack.Background = new SolidColorBrush(Color.RoyalBlue);
							senstack.Children.Add(sen);

							MessagesStack.Children.Add(senstack);
						}
						if (message.Recipient.Replace(" ", "") == Initial.login.Replace(" ", "") && Initial.Receiver == message.Sender)
                    {
							Label sen = new Label()
							{
								Text = message.Message

							};
							sen.TextColor = Color.White;
							sen.Padding = 10;

							StackLayout senstack = new StackLayout();
							senstack.Margin = new Thickness(30, 10, 30, 0);
							senstack.Background = new SolidColorBrush(Color.Purple);
							senstack.Children.Add(sen);

							MessagesStack.Children.Add(senstack);
						}
					}
				
				}
			}

        private void Send_Clicked(object sender, EventArgs e)
        {
			if (string.IsNullOrEmpty(MSG.Text)) return;
			else
			{
				Label sen = new Label()
				{
					Text = MSG.Text

                };
                sen.TextColor = Color.White;
                sen.Padding = 10;

                StackLayout senstack = new StackLayout();
                senstack.Margin = new Thickness(30, 10, 30, 0);
                senstack.Background = new SolidColorBrush(Color.RoyalBlue);
                senstack.Children.Add(sen);

                MessagesStack.Children.Add(senstack);
				using(Model1 db = new Model1(dbPath))
				{
					Messages msg = new Messages(Initial.login, Initial.Receiver, MSG.Text);
					db.Messages.Add(msg);
					db.SaveChanges();
				}
            }
        }
    }
}
