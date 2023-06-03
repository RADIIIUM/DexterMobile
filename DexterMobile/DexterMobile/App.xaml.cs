using DexterMobile.Services;
using DexterMobile.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.IO;
using System.Reflection;
using DesktopProject_V3.DataBaseClass;

namespace DexterMobile
{
    public partial class App : Application
    {
        public const string DbPath = "Dexter.db";
        public App()
        {
            InitializeComponent();
            string dbPath = DependencyService.Get<IPath>().GetDatabasePath(DbPath);
            using (var db = new Model1(dbPath))
            {
                // Создаем бд, если она отсутствует
                db.Database.EnsureCreated();
            }
            MainPage = new NavigationPage(new Goods());
            MainPage = new NavigationPage(new Registration());
            MainPage = new NavigationPage(new MainPage());
            MainPage = new NavigationPage(new Product());
            MainPage = new NavigationPage(new CartPage());
            MainPage = new NavigationPage(new Autorization());
        }

        protected override void OnStart()
        {
        }
        protected override void OnSleep()
        {
        }
        protected override void OnResume()
        {
        }
    }
}