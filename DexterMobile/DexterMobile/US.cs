using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace DexterMobile
{
    public class US
    {
        public US()
        {

        }

        public US(ImageSource image, string Login, string Name)
        {
            this.img = image;
            this.Login = Login;
            this.Name = Name;
        }
        public ImageSource img { get; set; }
        public string Login { get; set; }
        public string Name { get; set; }
    }
}
