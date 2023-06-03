namespace DesktopProject_V3.DataBaseClass
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class News
    {
        public News()
        {

        }
        public News(string pr, string desc, byte[] img, bool fix)
        {
            this.Paragraph = pr;
            this.DescriptionOfNews = desc;
            this.MainImage = img;
            this.Fix = fix;
        }
        [Key]
        public int ID_New { get; set; }

        [Required]
        [StringLength(50)]
        public string Paragraph { get; set; }

        [Required]
        public string DescriptionOfNews { get; set; }

        public byte[] MainImage { get; set; }

        public bool? Fix { get; set; }
    }
}
