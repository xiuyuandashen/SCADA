using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace SCADA.Industrial.Models
{
    public partial class SCADAModels : DbContext
    {
        public SCADAModels()
            : base("name=SCADAModels")
        {
        }

        public virtual DbSet<Devices> Devices { get; set; }
        public virtual DbSet<MonitorValues> MonitorValues { get; set; }
        public virtual DbSet<StorageArea> StorageArea { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StorageArea>()
                .Property(e => e.func_code)
                .IsUnicode(false);
        }
    }
}
