namespace DesktopProject_V3.DataBaseClass
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class Orders
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Orders()
        {
            Orders_Products = new HashSet<Orders_Products>();
            Orders_Users = new HashSet<Orders_Users>();
        }

        public Orders(string Status, string typep, string typed, DateTime data, string adress)
        {
            this.StatusOfOrder = Status;
            this.TypeOfPayment = typep;
            this.TypeOfDelivery = typed;
            this.DataOrder = data;
            this.DeliveryAdress = adress;
        }

        [Key]
        public int ID_Order { get; set; }

        [Required]
        [StringLength(45)]
        public string StatusOfOrder { get; set; }

        [Required]
        [StringLength(45)]
        public string TypeOfPayment { get; set; }

        [Required]
        [StringLength(45)]
        public string TypeOfDelivery { get; set; }

        public DateTime DataOrder { get; set; }

        [Required]
        [StringLength(100)]
        public string DeliveryAdress { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Orders_Products> Orders_Products { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Orders_Users> Orders_Users { get; set; }
    }
}
