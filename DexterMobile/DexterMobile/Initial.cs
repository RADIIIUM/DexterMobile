using DesktopProject_V3.DataBaseClass;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace DexterMobile
{
    public class Initial
    {
        public static ImageSource UserAvatar { get; set; }
        public static string login { get; set; }

        public static List<Products> Cart { get; set; } = new List<Products>();

        public static string OldLogin { get; set; }

        public static bool ShowProfile { get; set; }

        public static int NumberOfNews { get; set; }

        public static string NameOfCategoryOfProduct { get; set; }

        public static int IDProduct { get; set; }

        public static int NumberOfOrder { get; set; }
    }
}
