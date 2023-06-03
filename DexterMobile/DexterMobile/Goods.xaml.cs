using DesktopProject_V3.DataBaseClass;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DexterMobile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public class Prod
    {
        public Prod(int id, string name, string desc, string spec, int cena, string akcia, ImageSource img, int count, string type)
        {
            this.ID = id;
            this.Name = name;
            this.Description = desc;
            this.Specification = spec;
            this.Cena = cena;
            this.Akcia = akcia;
            this.Count = count;
            this.im = img;
            this.Type = type;
        }

        public Prod() { }

        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Specification { get; set; }
        public int Cena { get; set; }
        public string Akcia { get; set; }

        public ImageSource im { get; set; }
        public string Type { get; set; }

        public int Count { get; set; }

    }
    public partial class Goods : ContentPage
    {
        public ObservableCollection<Prod> NP { get; set; }
        public string ctg { get; set; }

        private string dbPath;
        public Goods()
        {
            InitializeComponent();
            dbPath = DependencyService.Get<IPath>().GetDatabasePath(App.DbPath);
            Ava.Source = Initial.UserAvatar;
            Avatar avat = new Avatar();
            using (Model1 db = new Model1(dbPath)) 
                    {
                try
                {
                    string role = db.Roles.First(x => x.LoginOfUsers == Initial.login).NameOfRole;
                    if ((role == "Администратор"))
                    {
                        AddProduct.IsVisible = true;
                    }
                    else
                    {
                        AddProduct.IsVisible = false;
                    }
                }
                catch
                {
                    string role = "Тест";
                }
                try
                {
                    NP = new ObservableCollection<Prod>();
                    switch (Initial.NameOfCategoryOfProduct)
                    {
                        case "Computers":
                           
                            var type = db.TypesOfProducts.Where(x => x.NameOfType.IndexOf("_CMP") >= 0).Select(x => x.ID_Type).ToList();
                            var typesearch = db.TypesOfProducts.Where(x => x.NameOfType.IndexOf("_CMP") >= 0).Select(x => x) ?? null;
                            this.ctg = "_CMP";

                            foreach (var t in type)
                            {
                                foreach (var tp in db.Types_Products)
                                {
                                    if (t == tp.ID_Type)
                                    {
                                        foreach (var prod in db.Products)
                                        {
                                            if (prod.ID_Product == tp.ID_Product)
                                            {

                                                if (prod.Discount > 0)
                                                {
                                                    int cena = prod.Discount ?? 0;
                                                    int idtp = db.Types_Products.First(x => x.ID_Product == prod.ID_Product).ID_Type ?? 0;
                                                    int count;
                                                    string tip = (db.TypesOfProducts.First(x => x.ID_Type == idtp).NameOfType).Split('_')[0];
                                                    try
                                                    {
                                                        count = db.Warehouses.First(x => x.ID_Product == prod.ID_Product).CountOfProduct ?? 0;
                                                    }
                                                    catch (System.InvalidOperationException ex)
                                                    {
                                                        count = 0;
                                                    }
                                                    Stream ms = new MemoryStream(prod.AvatarOfProduct);
                                                    Prod p = new Prod(prod.ID_Product, prod.NameOfProduct, prod.DescriptionOfProduct,
                                                    prod.Specifications, prod.Price, cena.ToString(), ImageSource.FromStream(() => ms), int.Parse(count.ToString()), tip);
                                                    NP.Add(p);
                                                }
                                                else
                                                {
                                                    int cena = prod.Price;
                                                    int idtp = db.Types_Products.First(x => x.ID_Product == prod.ID_Product).ID_Type ?? 0;
                                                    int count;
                                                    string tip = (db.TypesOfProducts.First(x => x.ID_Type == idtp).NameOfType).Split('_')[0];
                                                    try
                                                    {
                                                        count = db.Warehouses.First(x => x.ID_Product == prod.ID_Product).CountOfProduct ?? 0;
                                                    }
                                                    catch (System.InvalidOperationException ex)
                                                    {
                                                        count = 0;
                                                    }
                                                    Stream ms = new MemoryStream(prod.AvatarOfProduct);
                                                    Prod p = new Prod(prod.ID_Product, prod.NameOfProduct, prod.DescriptionOfProduct,
                                                        prod.Specifications, prod.Price, "", ImageSource.FromStream(() => ms), int.Parse(count.ToString()), tip);
                                                    NP.Add(p);
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            if (NP != null) ProductList.ItemsSource = NP;
                            break;

                        case "Notebook":
                            var type1 = db.TypesOfProducts.Where(x => x.NameOfType.IndexOf("_NTB") >= 0).Select(x => x.ID_Type).ToList();
                            var typesearch1 = db.TypesOfProducts.Where(x => x.NameOfType.IndexOf("_NTB") >= 0).Select(x => x);
                            this.ctg = "_NTB";

                            foreach (var t in type1)
                            {
                                foreach (var tp in db.Types_Products)
                                {
                                    if (t == tp.ID_Type)
                                    {
                                        foreach (var prod in db.Products)
                                        {
                                            if (prod.ID_Product == tp.ID_Product)
                                            {
                                                if (prod.Discount > 0)
                                                {
                                                    int cena = prod.Discount ?? 0;
                                                    int count;
                                                    int idtp = db.Types_Products.First(x => x.ID_Product == prod.ID_Product).ID_Type ?? 0;
                                                    string tip = (db.TypesOfProducts.First(x => x.ID_Type == idtp).NameOfType).Split('_')[0];
                                                    try
                                                    {
                                                        count = db.Warehouses.First(x => x.ID_Product == prod.ID_Product).CountOfProduct ?? 0;
                                                    }
                                                    catch (System.InvalidOperationException ex)
                                                    {
                                                        count = 0;
                                                    }
                                                    Stream ms = new MemoryStream(prod.AvatarOfProduct);
                                                    Prod p = new Prod(prod.ID_Product, prod.NameOfProduct, prod.DescriptionOfProduct,
                                                        prod.Specifications, cena, prod.Price.ToString(), ImageSource.FromStream(() => ms), int.Parse(count.ToString()), tip);
                                                    NP.Add(p);
                                                }
                                                else
                                                {
                                                    int cena = prod.Price;
                                                    int count;
                                                    int idtp = db.Types_Products.First(x => x.ID_Product == prod.ID_Product).ID_Type ?? 0;
                                                    string tip = (db.TypesOfProducts.First(x => x.ID_Type == idtp).NameOfType).Split('_')[0];
                                                    try
                                                    {
                                                        count = db.Warehouses.First(x => x.ID_Product == prod.ID_Product).CountOfProduct ?? 0;
                                                    }
                                                    catch (System.InvalidOperationException ex)
                                                    {
                                                        count = 0;
                                                    }
                                                    Stream ms = new MemoryStream(prod.AvatarOfProduct);

                                                    Prod p = new Prod(prod.ID_Product, prod.NameOfProduct, prod.DescriptionOfProduct,
                                                        prod.Specifications, prod.Price, "", ImageSource.FromStream(() => ms), int.Parse(count.ToString()), tip);
                                                    NP.Add(p);
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            if (NP != null) ProductList.ItemsSource = NP;
                            break;

                        case "Accessory":
                            var type2 = db.TypesOfProducts.Where(x => x.NameOfType.IndexOf("_ACS") >= 0).Select(x => x.ID_Type).ToList();
                            var typesearch2 = db.TypesOfProducts.Where(x => x.NameOfType.IndexOf("_ACS") >= 0).Select(x => x);
                            this.ctg = "_ACS";
                            foreach (var t in type2)
                            {
                                foreach (var tp in db.Types_Products)
                                {
                                    if (t == tp.ID_Type)
                                    {
                                        foreach (var prod in db.Products)
                                        {
                                            if (prod.ID_Product == tp.ID_Product)
                                            {
                                                if (prod.Discount > 0)
                                                {
                                                    int cena = prod.Discount ?? 0;
                                                    int count;
                                                    int idtp = db.Types_Products.First(x => x.ID_Product == prod.ID_Product).ID_Type ?? 0;
                                                    string tip = (db.TypesOfProducts.First(x => x.ID_Type == idtp).NameOfType).Split('_')[0];
                                                    try
                                                    {
                                                        count = db.Warehouses.First(x => x.ID_Product == prod.ID_Product).CountOfProduct ?? 0;
                                                    }
                                                    catch (System.InvalidOperationException ex)
                                                    {
                                                        count = 0;
                                                    }
                                                    Stream ms = new MemoryStream(prod.AvatarOfProduct);
                                                    Prod p = new Prod(prod.ID_Product, prod.NameOfProduct, prod.DescriptionOfProduct,
                                                        prod.Specifications, cena, prod.Price.ToString(), ImageSource.FromStream(() => ms), int.Parse(count.ToString()), tip);
                                                    NP.Add(p);
                                                }
                                                else
                                                {
                                                    int cena = prod.Price;
                                                    int count;
                                                    int idtp = db.Types_Products.First(x => x.ID_Product == prod.ID_Product).ID_Type ?? 0;
                                                    string tip = (db.TypesOfProducts.First(x => x.ID_Type == idtp).NameOfType).Split('_')[0];
                                                    try
                                                    {
                                                        count = db.Warehouses.First(x => x.ID_Product == prod.ID_Product).CountOfProduct ?? 0;
                                                    }
                                                    catch (System.InvalidOperationException ex)
                                                    {
                                                        count = 0;
                                                    }
                                                    Stream ms = new MemoryStream(prod.AvatarOfProduct);
                                                    Prod p = new Prod(prod.ID_Product, prod.NameOfProduct, prod.DescriptionOfProduct,
                                                        prod.Specifications, prod.Price, "", ImageSource.FromStream(() => ms), int.Parse(count.ToString()), tip);
                                                    NP.Add(p);
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            if (NP != null) ProductList.ItemsSource = NP;
                            break;

                        case "Complect":
                            var type3 = db.TypesOfProducts.Where(x => x.NameOfType.IndexOf("_CKT") >= 0).Select(x => x.ID_Type).ToList();
                            var typesearch3 = db.TypesOfProducts.Where(x => x.NameOfType.IndexOf("_CKT") >= 0).Select(x => x);
                            this.ctg = "_CKT";
                            foreach (var t in type3)
                            {
                                foreach (var tp in db.Types_Products)
                                {
                                    if (t == tp.ID_Type)
                                    {
                                        foreach (var prod in db.Products)
                                        {
                                            if (prod.ID_Product == tp.ID_Product)
                                            {
                                                if (prod.Discount > 0)
                                                {
                                                    int cena = prod.Discount ?? 0;
                                                    int count;
                                                    int idtp = db.Types_Products.First(x => x.ID_Product == prod.ID_Product).ID_Type ?? 0;
                                                    string tip = (db.TypesOfProducts.First(x => x.ID_Type == idtp).NameOfType).Split('_')[0];
                                                    try
                                                    {
                                                        count = db.Warehouses.First(x => x.ID_Product == prod.ID_Product).CountOfProduct ?? 0;
                                                    }
                                                    catch (System.InvalidOperationException ex)
                                                    {
                                                        count = 0;
                                                    }
                                                    Stream ms = new MemoryStream(prod.AvatarOfProduct);
                                                    Prod p = new Prod(prod.ID_Product, prod.NameOfProduct, prod.DescriptionOfProduct,
                                                        prod.Specifications, cena, prod.Price.ToString(), ImageSource.FromStream(() => ms), int.Parse(count.ToString()), tip);
                                                    NP.Add(p);
                                                }
                                                else
                                                {
                                                    int cena = prod.Price;
                                                    int count;
                                                    int idtp = db.Types_Products.First(x => x.ID_Product == prod.ID_Product).ID_Type ?? 0;
                                                    string tip = (db.TypesOfProducts.First(x => x.ID_Type == idtp).NameOfType).Split('_')[0];
                                                    try
                                                    {
                                                        count = db.Warehouses.First(x => x.ID_Product == prod.ID_Product).CountOfProduct ?? 0;
                                                    }
                                                    catch (System.InvalidOperationException ex)
                                                    {
                                                        count = 0;
                                                    }
                                                    Stream ms = new MemoryStream(prod.AvatarOfProduct);
                                                    Prod p = new Prod(prod.ID_Product, prod.NameOfProduct, prod.DescriptionOfProduct,
                                                        prod.Specifications, prod.Price, "", ImageSource.FromStream(() => ms), int.Parse(count.ToString()), tip);
                                                    NP.Add(p);
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            if(NP != null) ProductList.ItemsSource = NP;
                            break;

                        default:
                            break;
                    }
                }
                catch
                {

                }
            }
        }

        private async void AddProduct_Clicked(object sender, EventArgs e)
        {
            Initial.IDProduct = -1;
            await Navigation.PushAsync(new Product());
        }

        private void Store_Clicked(object sender, EventArgs e)
        {

        }

        private void UserProfileBtn_Clicked(object sender, EventArgs e)
        {

        }
    }
}