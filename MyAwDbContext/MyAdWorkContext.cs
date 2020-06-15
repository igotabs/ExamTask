using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ExamTask.MyAwDbContext
{
    public partial class MyAdWorkContext : DbContext
    {
        public MyAdWorkContext()
        {
        }

        public MyAdWorkContext(DbContextOptions<MyAdWorkContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Orders> Orders { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=contsqlbuilder;Initial Catalog=MyAdWork;TrustServerCertificate=True;Integrated Security=false;User ID=sa;Password=P@ssword!;MultipleActiveResultSets=False;Encrypt=True;Connection Timeout=30;persist security info=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Orders>(entity =>
            {
                entity.Property(e => e.SalesOrderId).HasColumnName("SalesOrderID");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
