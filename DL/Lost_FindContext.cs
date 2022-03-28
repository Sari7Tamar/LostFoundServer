using System;
using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace DL
{
    public partial class Lost_FindContext : DbContext
    {
        public Lost_FindContext()
        {
        }

        public Lost_FindContext(DbContextOptions<Lost_FindContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Address> Addresses { get; set; }
        public virtual DbSet<Adjustment> Adjustments { get; set; }
        public virtual DbSet<LostFound> LostFounds { get; set; }
        public virtual DbSet<PublicTransport> PublicTransports { get; set; }
        public virtual DbSet<Rating> Ratings { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=srv2\\pupils; Database=Lost_Find;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Hebrew_CI_AS");

            modelBuilder.Entity<Address>(entity =>
            {
                entity.ToTable("ADDRESS");

                entity.Property(e => e.LFId).HasColumnName("L_F_Id");

                entity.HasOne(d => d.LF)
                    .WithMany(p => p.Addresses)
                    .HasForeignKey(d => d.LFId)
                    .HasConstraintName("FK_Address_Lost_found");

                entity.Property(e => e.CityName).HasMaxLength(50);

                entity.Property(e => e.StreetName).HasMaxLength(50);

            });

            modelBuilder.Entity<Adjustment>(entity =>
            {
                entity.ToTable("ADJUSTMENTS");

                entity.HasOne(d => d.Found)
                    .WithMany(p => p.AdjustmentFounds)
                    .HasForeignKey(d => d.FoundId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ADJUSTMENTS_Lost_found1");

                entity.HasOne(d => d.Lost)
                    .WithMany(p => p.AdjustmentLosts)
                    .HasForeignKey(d => d.LostId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ADJUSTMENTS_Lost_found");
            });

            modelBuilder.Entity<LostFound>(entity =>
            {
                entity.ToTable("Lost_found");

                entity.Property(e => e.Date).HasColumnType("date");

                entity.Property(e => e.Description).HasMaxLength(50);

                entity.Property(e => e.Image).HasColumnType("image");

                entity.Property(e => e.LocationType).HasMaxLength(20);

                entity.Property(e => e.AddedDate).HasColumnType("date");

            });

            modelBuilder.Entity<PublicTransport>(entity =>
            {
                entity.ToTable("PUBLIC_TRANSPORT");

                entity.Property(e => e.BoardingStation).HasMaxLength(20);

                entity.Property(e => e.Company).HasMaxLength(20);

                entity.Property(e => e.DropStation).HasMaxLength(20);

                entity.Property(e => e.LFId).HasColumnName("L_F_Id");

                entity.HasOne(d => d.LF)
                    .WithMany(p => p.PublicTransports)
                    .HasForeignKey(d => d.LFId)
                    .HasConstraintName("FK_PUBLIC_TRANSPORT_Lost_found");
            });

            modelBuilder.Entity<Rating>(entity =>
            {
                entity.ToTable("RATING");

                entity.Property(e => e.RatingId).HasColumnName("RATING_ID");

                entity.Property(e => e.Host)
                    .HasMaxLength(50)
                    .HasColumnName("HOST");

                entity.Property(e => e.Method)
                    .HasMaxLength(10)
                    .HasColumnName("METHOD")
                    .IsFixedLength(true);

                entity.Property(e => e.Path)
                    .HasMaxLength(50)
                    .HasColumnName("PATH");

                entity.Property(e => e.RecordDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Record_Date");

                entity.Property(e => e.Referer)
                    .HasMaxLength(100)
                    .HasColumnName("REFERER");

                entity.Property(e => e.UserAgent).HasColumnName("USER_AGENT");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("USERS");

                entity.Property(e => e.Email).HasMaxLength(30);

                entity.Property(e => e.Fhone).HasMaxLength(10);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(10);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
