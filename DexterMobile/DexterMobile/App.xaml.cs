using DexterMobile.Services;
using DexterMobile.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.IO;
using System.Reflection;
using DesktopProject_V3.DataBaseClass;

[assembly: ExportFont("MaterialIcons-Regular.ttf", Alias = "RegularMaterial")]
[assembly: ExportFont("MaterialIconsTwoTone-Regular.otf", Alias = "TwoToneMaterial")]
[assembly: ExportFont("MaterialIconsOutlined-Regular.otf", Alias = "OutlinedMaterial")]

namespace DexterMobile
{
    public partial class App : Application
    {
        public const string DbPath = "DexterDB.db";
        public App()
        {
            InitializeComponent();
            string dbPath = DependencyService.Get<IPath>().GetDatabasePath(DbPath);
            using (var db = new Model1(dbPath))
            {
                // Создаем бд, если она отсутствует
                db.Database.EnsureCreated();
            }
            MainPage = new NavigationPage(new Registration());
            MainPage = new NavigationPage(new MainPage());
            MainPage = new NavigationPage(new Messanger());
            MainPage = new NavigationPage(new MessangerMenu());
            MainPage = new NavigationPage(new UsersMenu());
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