using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace SmallSimpleOA.Models
{
    public partial class SmallSimpleOAContext : DbContext
    {
        public SmallSimpleOAContext()
        {
        }

        public SmallSimpleOAContext(DbContextOptions<SmallSimpleOAContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AskForLeave> AskForLeave { get; set; }
        public virtual DbSet<Attendance> Attendance { get; set; }
        public virtual DbSet<Department> Department { get; set; }
        public virtual DbSet<Message> Message { get; set; }
        public virtual DbSet<TodoTask> TodoTask { get; set; }
        public virtual DbSet<Uzer> Uzer { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=localhost,1433;Database=SmallSimpleOA;User ID=sa;Password=SSOAPassw0rd;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AskForLeave>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AppTime).HasColumnType("datetime");

                entity.Property(e => e.Reason).HasColumnType("text");

                entity.Property(e => e.Valid).HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<Attendance>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ActionTime).HasColumnType("datetime");

                entity.Property(e => e.UzerId).HasColumnName("UzerID");

                entity.Property(e => e.Valid).HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<Department>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Valid).HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<Message>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Content).HasColumnType("text");

                entity.Property(e => e.MsgTime).HasColumnType("datetime");

                entity.Property(e => e.Valid).HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<TodoTask>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Content).HasColumnType("text");

                entity.Property(e => e.CreateTime).HasColumnType("datetime");

                entity.Property(e => e.DeadLine).HasColumnType("datetime");

                entity.Property(e => e.ModifyTime).HasColumnType("datetime");

                entity.Property(e => e.Title).HasColumnType("text");

                entity.Property(e => e.Valid).HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<Uzer>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Email)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.LastLogin).HasColumnType("datetime");

                entity.Property(e => e.LastName)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Salt)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Valid).HasDefaultValueSql("((1))");
            });
        }
    }
}
