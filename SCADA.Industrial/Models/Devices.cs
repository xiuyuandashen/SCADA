namespace SCADA.Industrial
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Devices
    {
        [Key]
        public int d_id { get; set; }

        [StringLength(100)]
        public string d_name { get; set; }
    }
}
