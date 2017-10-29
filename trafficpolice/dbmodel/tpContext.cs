using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace trafficpolice.dbmodel
{
    public partial class tpContext : DbContext
    {
        public virtual DbSet<Dataitem> Dataitem { get; set; }
        public virtual DbSet<Reportlog> Reportlog { get; set; }
        public virtual DbSet<Summarized> Summarized { get; set; }
        public virtual DbSet<Template> Template { get; set; }
        public virtual DbSet<Unit> Unit { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<Userlog> Userlog { get; set; }
        public virtual DbSet<Videoreport> Videoreport { get; set; }
        public virtual DbSet<Weeksummarized> Weeksummarized { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseMySql("Server=47.93.226.74;User Id=blah;Password=ycl1mail@A;Database=tp");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Dataitem>(entity =>
            {
                entity.ToTable("dataitem");

                entity.HasIndex(e => e.Id)
                    .HasName("id_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.Name)
                    .HasName("name_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)")
                    .ValueGeneratedNever();

                entity.Property(e => e.Centerdisplay)
                    .HasColumnName("centerdisplay")
                    .HasColumnType("smallint(1)")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.Comment)
                    .HasColumnName("comment")
                    .HasMaxLength(146);

                entity.Property(e => e.Datatype)
                    .HasColumnName("datatype")
                    .HasColumnType("smallint(2)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Deleted)
                    .HasColumnName("deleted")
                    .HasColumnType("smallint(1)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Inputtype)
                    .HasColumnName("inputtype")
                    .HasColumnType("smallint(2)")
                    .HasDefaultValueSql("'2'");

                entity.Property(e => e.Mandated)
                    .HasColumnName("mandated")
                    .HasColumnType("smallint(1)")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(45);

                entity.Property(e => e.Seconditem)
                    .HasColumnName("seconditem")
                    .HasMaxLength(500);

                entity.Property(e => e.Time)
                    .HasColumnName("time")
                    .HasColumnType("datetime");

                entity.Property(e => e.Unitdisplay)
                    .HasColumnName("unitdisplay")
                    .HasColumnType("smallint(1)")
                    .HasDefaultValueSql("'1'");
            });

            modelBuilder.Entity<Reportlog>(entity =>
            {
                entity.HasKey(e => new { e.Date, e.Unitid });

                entity.ToTable("reportlog");

                entity.HasIndex(e => e.Unitid)
                    .HasName("reportlogunitid_idx");

                entity.Property(e => e.Date)
                    .HasColumnName("date")
                    .HasMaxLength(10);

                entity.Property(e => e.Unitid)
                    .HasColumnName("unitid")
                    .HasMaxLength(50);

                entity.Property(e => e.Content)
                    .IsRequired()
                    .HasColumnName("content")
                    .HasMaxLength(4500);

                entity.Property(e => e.Declinereason)
                    .HasColumnName("declinereason")
                    .HasMaxLength(450);

                entity.Property(e => e.Draft)
                    .HasColumnName("draft")
                    .HasColumnType("smallint(2)")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.Time)
                    .HasColumnName("time")
                    .HasColumnType("datetime");

                entity.HasOne(d => d.Unit)
                    .WithMany(p => p.Reportlog)
                    .HasForeignKey(d => d.Unitid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("reportlogunitid");
            });

            modelBuilder.Entity<Summarized>(entity =>
            {
                entity.HasKey(e => e.Date);

                entity.ToTable("summarized");

                entity.HasIndex(e => e.Date)
                    .HasName("date_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Date)
                    .HasColumnName("date")
                    .HasMaxLength(10)
                    .ValueGeneratedNever();

                entity.Property(e => e.Comment)
                    .HasColumnName("comment")
                    .HasMaxLength(450);

                entity.Property(e => e.Content)
                    .IsRequired()
                    .HasColumnName("content")
                    .HasMaxLength(4500);

                entity.Property(e => e.Draft)
                    .HasColumnName("draft")
                    .HasColumnType("smallint(2)")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.Time)
                    .HasColumnName("time")
                    .HasColumnType("datetime");
            });

            modelBuilder.Entity<Template>(entity =>
            {
                entity.ToTable("template");

                entity.HasIndex(e => e.Id)
                    .HasName("id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasMaxLength(50)
                    .ValueGeneratedNever();

                entity.Property(e => e.File)
                    .IsRequired()
                    .HasColumnName("file")
                    .HasMaxLength(45);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(145);

                entity.Property(e => e.Path)
                    .IsRequired()
                    .HasColumnName("path")
                    .HasMaxLength(450);
            });

            modelBuilder.Entity<Unit>(entity =>
            {
                entity.ToTable("unit");

                entity.HasIndex(e => e.Id)
                    .HasName("id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasMaxLength(50)
                    .ValueGeneratedNever();

                entity.Property(e => e.Ip)
                    .IsRequired()
                    .HasColumnName("ip")
                    .HasMaxLength(45);

                entity.Property(e => e.Level)
                    .HasColumnName("level")
                    .HasColumnType("smallint(1)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(145);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("user");

                entity.HasIndex(e => e.Id)
                    .HasName("id_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.Unitid)
                    .HasName("unitid_idx");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasMaxLength(50)
                    .ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(45);

                entity.Property(e => e.Pass)
                    .IsRequired()
                    .HasColumnName("pass")
                    .HasMaxLength(45);

                entity.Property(e => e.Token)
                    .HasColumnName("token")
                    .HasMaxLength(45);

                entity.Property(e => e.Unitid)
                    .IsRequired()
                    .HasColumnName("unitid")
                    .HasMaxLength(50);

                entity.HasOne(d => d.Unit)
                    .WithMany(p => p.User)
                    .HasForeignKey(d => d.Unitid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("unitid");
            });

            modelBuilder.Entity<Userlog>(entity =>
            {
                entity.HasKey(e => e.Ordinal);

                entity.ToTable("userlog");

                entity.HasIndex(e => e.Ordinal)
                    .HasName("ordinal_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.Userid)
                    .HasName("userid_idx");

                entity.Property(e => e.Ordinal)
                    .HasColumnName("ordinal")
                    .HasColumnType("int(11)")
                    .ValueGeneratedNever();

                entity.Property(e => e.Content)
                    .HasColumnName("content")
                    .HasMaxLength(450);

                entity.Property(e => e.Ip)
                    .HasColumnName("ip")
                    .HasMaxLength(45);

                entity.Property(e => e.Time)
                    .HasColumnName("time")
                    .HasColumnType("datetime");

                entity.Property(e => e.Userid)
                    .IsRequired()
                    .HasColumnName("userid")
                    .HasMaxLength(50);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Userlog)
                    .HasForeignKey(d => d.Userid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("userid");
            });

            modelBuilder.Entity<Videoreport>(entity =>
            {
                entity.HasKey(e => new { e.Date, e.Unitid });

                entity.ToTable("videoreport");

                entity.HasIndex(e => e.Date)
                    .HasName("date_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.Unitid)
                    .HasName("reportlogunitid_idx");

                entity.Property(e => e.Date)
                    .HasColumnName("date")
                    .HasMaxLength(10);

                entity.Property(e => e.Unitid)
                    .HasColumnName("unitid")
                    .HasMaxLength(50);

                entity.Property(e => e.Comment)
                    .HasColumnName("comment")
                    .HasMaxLength(450);

                entity.Property(e => e.Content)
                    .IsRequired()
                    .HasColumnName("content")
                    .HasMaxLength(4500);

                entity.Property(e => e.Declinereason)
                    .HasColumnName("declinereason")
                    .HasMaxLength(450);

                entity.Property(e => e.Draft)
                    .HasColumnName("draft")
                    .HasColumnType("smallint(2)")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.Signtype)
                    .HasColumnName("signtype")
                    .HasColumnType("smallint(2)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Time)
                    .HasColumnName("time")
                    .HasColumnType("datetime");

                entity.HasOne(d => d.Unit)
                    .WithMany(p => p.Videoreport)
                    .HasForeignKey(d => d.Unitid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("videoreportunitid");
            });

            modelBuilder.Entity<Weeksummarized>(entity =>
            {
                entity.HasKey(e => new { e.Startdate, e.Enddate });

                entity.ToTable("weeksummarized");

                entity.Property(e => e.Startdate)
                    .HasColumnName("startdate")
                    .HasMaxLength(10);

                entity.Property(e => e.Enddate)
                    .HasColumnName("enddate")
                    .HasMaxLength(10);

                entity.Property(e => e.Comment)
                    .HasColumnName("comment")
                    .HasMaxLength(450);

                entity.Property(e => e.Content)
                    .IsRequired()
                    .HasColumnName("content")
                    .HasMaxLength(4500);

                entity.Property(e => e.Draft)
                    .HasColumnName("draft")
                    .HasColumnType("smallint(2)")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.Time)
                    .HasColumnName("time")
                    .HasColumnType("datetime");
            });
        }
    }
}
