namespace SBAdmin.Models.App
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Data.Entity.Core.Objects;
    using System.Data.Entity.Infrastructure;

    public partial class AppDbContext : DbContext
    {
        public AppDbContext()
            : base("name=AppDbContext")
        {
        }

        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<NetWork> NetWorks { get; set; }
        public virtual DbSet<SIM> SIMs { get; set; }
        public virtual DbSet<SimType> SimTypes { get; set; }
        public virtual DbSet<Supplier> Suppliers { get; set; }
        public virtual DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>()
                .Property(e => e.Mobile)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.CreateBy)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.UpdateBy)
                .IsUnicode(false);

            modelBuilder.Entity<NetWork>()
                .Property(e => e.Number)
                .IsUnicode(false);

            modelBuilder.Entity<NetWork>()
                .Property(e => e.image)
                .IsUnicode(false);

            modelBuilder.Entity<NetWork>()
                .Property(e => e.CreateBy)
                .IsUnicode(false);

            modelBuilder.Entity<NetWork>()
                .Property(e => e.UpdateBy)
                .IsUnicode(false);

            modelBuilder.Entity<SIM>()
                .Property(e => e.Number)
                .IsUnicode(false);

            modelBuilder.Entity<SIM>()
                .Property(e => e.Price)
                .HasPrecision(19, 4);

            modelBuilder.Entity<SIM>()
                .Property(e => e.CreateBy)
                .IsUnicode(false);

            modelBuilder.Entity<SIM>()
                .Property(e => e.UpdateBy)
                .IsUnicode(false);

            modelBuilder.Entity<SimType>()
                .Property(e => e.Condition)
                .IsUnicode(false);

            modelBuilder.Entity<Supplier>()
                .Property(e => e.UserName)
                .IsUnicode(false);

            modelBuilder.Entity<Supplier>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<Supplier>()
                .Property(e => e.Mobile)
                .IsUnicode(false);

            modelBuilder.Entity<Supplier>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<Supplier>()
                .Property(e => e.CreateBy)
                .IsUnicode(false);

            modelBuilder.Entity<Supplier>()
                .Property(e => e.UpdateBy)
                .IsUnicode(false);

            modelBuilder.Entity<Order>()
              .Property(e => e.Email)
              .IsUnicode(false);

            modelBuilder.Entity<Order>()
              .Property(e => e.UserBussiness)
              .IsUnicode(false);
        }

        public virtual ObjectResult<Nullable<int>> SimTypeGetTypeBySim(string simNumber)
        {
            var simNumberParameter = simNumber != null ?
                new ObjectParameter("SimNumber", simNumber) :
                new ObjectParameter("SimNumber", typeof(string));

            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<int>>("SimTypeGetTypeBySim", simNumberParameter);
        }
    }
}
