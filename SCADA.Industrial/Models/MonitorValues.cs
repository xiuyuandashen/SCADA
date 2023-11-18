namespace SCADA.Industrial.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class MonitorValues
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int d_id { get; set; }

        [StringLength(100)]
        public string value_id { get; set; }

        [StringLength(100)]
        public string value_name { get; set; }
        [StringLength(10)]
        public string s_area_id { get; set; }

        public int? address { get; set; }

        [StringLength(100)]
        public string data_type { get; set; }

        [StringLength(10)]
        public string is_alarm { get; set; }

        [StringLength(100)]
        public string unit { get; set; }

        [StringLength(100)]
        public string alarm_lolo { get; set; }

        [StringLength(100)]
        public string alarm_low { get; set; }

        [StringLength(100)]
        public string alarm_high { get; set; }

        [StringLength(100)]
        public string alarm_hihi { get; set; }

        [StringLength(100)]
        public string description { get; set; }
    }
}
