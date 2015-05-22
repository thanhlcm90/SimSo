namespace SimSo.Models.App
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SimType")]
    public partial class SimType
    {
        public int ID { get; set; }

        public int? IDParent { get; set; }

        [StringLength(150)]
        public string Name { get; set; }

        [StringLength(1000)]
        public string Condition { get; set; }

        public bool? isActive { get; set; }

        public bool? isDeleted { get; set; }
        [StringLength(200)]

        public string Title { get; set; }
        [StringLength(1000)]
        public string Description { get; set; }
        [StringLength(1000)]
        public string Keyword { get; set; }
    }
}
