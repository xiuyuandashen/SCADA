namespace SCADA.Industrial.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("StorageArea")]
    public partial class StorageArea
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [StringLength(10)]
        public string id { get; set; }

        public int? slave_addr { get; set; }

        [StringLength(100)]
        public string func_code { get; set; }

        public int? start_req { get; set; }

        public int? length { get; set; }
    }
}
