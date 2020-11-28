using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ReportTracker.Data
{
    public partial class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<FileTrackerInfo> FileTrackerInfos { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Department>(entity =>
            {
                entity.ToTable("Department");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(250);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<FileTrackerInfo>(entity =>
            {
                entity.ToTable("FileTrackerInfo");

                entity.Property(e => e.BarCode)
                    .IsRequired()
                    .HasColumnType("text");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.DeptFrom).HasColumnName("Dept_From");

                entity.Property(e => e.DeptTo).HasColumnName("Dept_To");

                entity.Property(e => e.FileName)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.HasOne(d => d.DeptFromNavigation)
                    .WithMany(p => p.FileTrackerInfoDeptFromNavigation)
                    .HasForeignKey(d => d.DeptFrom)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FileTracker_Department_From");

                entity.HasOne(d => d.DeptToNavigation)
                    .WithMany(p => p.FileTrackerInfoDeptToNavigation)
                    .HasForeignKey(d => d.DeptTo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FileTracker_Department_To");
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
