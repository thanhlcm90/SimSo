namespace SimSo.Models.App
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SIM_CLIENT")]
    public partial class Sim_Client
    {

        [StringLength(20)]
        [Key]
        public string Number { get; set; }

        public int? NetWork_ID { get; set; }

        public int? SimType_ID { get; set; }

        public int? Supplier_ID { get; set; }

        [Column(TypeName = "money")]
        public decimal? Price { get; set; }
        [Column(TypeName = "money")]
        public decimal? Price_Sup { get; set; }
    }
}
