using DexterMobile.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace DexterMobile.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}