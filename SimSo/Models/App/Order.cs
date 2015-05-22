namespace SimSo.Models.App
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Order")]
    public partial class Order
    {
        [Key]
        public int ID { get; set; }

        //[Required]
        //public int SIM_ID { get; set; }

        [StringLength(50)]
        public string Number { get; set; }

        [StringLength(150)]
        [Required(ErrorMessage = "Vui lòng nhập tên của bạn.")]
        public string FullName { get; set; }

        [StringLength(250)]
        [Required(ErrorMessage = "Vui lòng nhập địa chỉ của bạn.")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập số điện thoại của bạn.")]
        [StringLength(50)]
        public string Mobile { get; set; }

        [StringLength(50)]
        [EmailAddress]
        public string Email { get; set; }

        [StringLength(850)]
        public string Description { get; set; }

        [StringLength(500)]
        public string Note { get; set; }

        public decimal? Price { get; set; }

        [StringLength(20)]
        public string UserBusiness { get; set; }

        [StringLength(50)]
        public string CreateBy { get; set; }

        public DateTime? CreateDate { get; set; }

        [StringLength(50)]
        public string UpdateBy { get; set; }

        public DateTime? LastUpdate { get; set; }

        public int? Status { get; set; }

        [Column(TypeName = "money")]

        public decimal? Price_Sup { get; set; }

        public int? Sup_ID { get; set; }
        public int? Network_ID { get; set; }
        public int? SimType_ID { get; set; }
    }
}
