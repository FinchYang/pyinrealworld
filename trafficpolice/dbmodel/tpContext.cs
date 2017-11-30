using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace trafficpolice.dbmodel
{
    public partial class tpContext : DbContext
    {
        public virtual DbSet<Dataitem> Dataitem { get; set; }
        public virtual DbSet<Moban> Moban { get; set; }
        public virtual DbSet<Reportlog> Reportlog { get; set; }
        public virtual DbSet<Reports> Reports { get; set; }
        public virtual DbSet<Reportsdata> Reportsdata { get; set; }
        public virtual DbSet<Summarized> Summarized { get; set; }
        public virtual DbSet<Unit> Unit { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<Userlog> Userlog { get; set; }
        public virtual DbSet<Weeksummarized> Weeksummarized { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
            // optionsBuilder.UseMySql("Server=localhost;User Id=blah;Password=ycl1mail@A;Database=tp");
             optionsBuilder.UseMySql("Server=localhost;port=5678;User Id=blah;Password=ycl1mail@A;Database=tp");
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

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasMaxLength(50)
                    .ValueGeneratedNever();

                entity.Property(e => e.Comment)
                    .HasColumnName("comment")
                    .HasMaxLength(450);

                entity.Property(e => e.Defaultvalue)
                    .HasColumnName("defaultvalue")
                    .HasMaxLength(450);

                entity.Property(e => e.Deleted)
                    .HasColumnName("deleted")
                    .HasColumnType("smallint(2)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Hassecond)
                    .HasColumnName("hassecond")
                    .HasColumnType("smallint(2)");

                entity.Property(e => e.Index)
                    .HasColumnName("index")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Inputtype)
                    .HasColumnName("inputtype")
                    .HasColumnType("smallint(2)");

                entity.Property(e => e.Lastdata)
                    .HasColumnName("lastdata")
                    .HasColumnType("smallint(2)");

                entity.Property(e => e.Mandated)
                    .HasColumnName("mandated")
                    .HasColumnType("smallint(2)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(150);

                entity.Property(e => e.Seconditem)
                    .HasColumnName("seconditem")
                    .HasMaxLength(5000);

                entity.Property(e => e.Statisticstype)
                    .IsRequired()
                    .HasColumnName("statisticstype")
                    .HasMaxLength(600);

                entity.Property(e => e.Sumunits)
                    .HasColumnName("sumunits")
                    .HasMaxLength(300);

                entity.Property(e => e.Tabletype)
                    .IsRequired()
                    .HasColumnName("tabletype")
                    .HasMaxLength(100);

                entity.Property(e => e.Time)
                    .HasColumnName("time")
                    .HasColumnType("datetime");

                entity.Property(e => e.Units)
                    .IsRequired()
                    .HasColumnName("units")
                    .HasMaxLength(300);
            });

            modelBuilder.Entity<Moban>(entity =>
            {
                entity.HasKey(e => e.Name);

                entity.ToTable("moban");

                entity.HasIndex(e => e.Name)
                    .HasName("name_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(150)
                    .ValueGeneratedNever();

                entity.Property(e => e.Comment)
                    .IsRequired()
                    .HasColumnName("comment")
                    .HasMaxLength(450);

                entity.Property(e => e.Deleted)
                    .HasColumnName("deleted")
                    .HasColumnType("smallint(2)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Filename)
                    .IsRequired()
                    .HasColumnName("filename")
                    .HasMaxLength(450);

                entity.Property(e => e.Tabletype)
                    .IsRequired()
                    .HasColumnName("tabletype")
                    .HasMaxLength(100);

                entity.Property(e => e.Time)
                    .HasColumnName("time")
                    .HasColumnType("datetime");
            });

            modelBuilder.Entity<Reportlog>(entity =>
            {
                entity.HasKey(e => new { e.Date, e.Unitid });

                entity.ToTable("reportlog");

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

            modelBuilder.Entity<Reports>(entity =>
            {
                entity.HasKey(e => e.Name);

                entity.ToTable("reports");

                entity.HasIndex(e => e.Name)
                    .HasName("name_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(100)
                    .ValueGeneratedNever();

                entity.Property(e => e.Comment)
                    .HasColumnName("comment")
                    .HasMaxLength(600);

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasColumnName("type")
                    .HasMaxLength(100);

                entity.Property(e => e.Units)
                    .IsRequired()
                    .HasColumnName("units")
                    .HasMaxLength(600);
            });

            modelBuilder.Entity<Reportsdata>(entity =>
            {
                entity.HasKey(e => new { e.Date, e.Unitid, e.Rname });

                entity.ToTable("reportsdata");

                entity.HasIndex(e => e.Unitid)
                    .HasName("reportsdataunitid_idx");

                entity.Property(e => e.Date)
                    .HasColumnName("date")
                    .HasMaxLength(10);

                entity.Property(e => e.Unitid)
                    .HasColumnName("unitid")
                    .HasMaxLength(50);

                entity.Property(e => e.Rname)
                    .HasColumnName("rname")
                    .HasMaxLength(100);

                entity.Property(e => e.Comment)
                    .HasColumnName("comment")
                    .HasMaxLength(450);

                entity.Property(e => e.Content)
                    .IsRequired()
                    .HasColumnName("content")
                    .HasColumnType("varchar(20000)");

                entity.Property(e => e.Declinereason)
                    .HasColumnName("declinereason")
                    .HasMaxLength(450);

                entity.Property(e => e.Draft)
                    .HasColumnName("draft")
                    .HasColumnType("smallint(2)");

                entity.Property(e => e.Signtype)
                    .HasColumnName("signtype")
                    .HasColumnType("smallint(2)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Submittime)
                    .HasColumnName("submittime")
                    .HasColumnType("datetime");

                entity.Property(e => e.Time)
                    .HasColumnName("time")
                    .HasColumnType("datetime");
            });

            modelBuilder.Entity<Summarized>(entity =>
            {
                entity.HasKey(e => new { e.Date, e.Reportname });

                entity.ToTable("summarized");

                entity.Property(e => e.Date)
                    .HasColumnName("date")
                    .HasMaxLength(10);

                entity.Property(e => e.Reportname)
                    .HasColumnName("reportname")
                    .HasMaxLength(100);

                entity.Property(e => e.Comment)
                    .HasColumnName("comment")
                    .HasMaxLength(450);

                entity.Property(e => e.Content)
                    .IsRequired()
                    .HasColumnName("content")
                    .HasColumnType("varchar(21000)");

                entity.Property(e => e.Draft)
                    .HasColumnName("draft")
                    .HasColumnType("smallint(2)");

                entity.Property(e => e.Time)
                    .HasColumnName("time")
                    .HasColumnType("datetime");
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
                    .HasColumnType("smallint(1)");

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

                entity.Property(e => e.Disabled)
                    .HasColumnName("disabled")
                    .HasColumnType("smallint(2)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Level)
                    .HasColumnName("level")
                    .HasColumnType("smallint(2)")
                    .HasDefaultValueSql("'1'");

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

                entity.Property(e => e.Unitclass)
                    .HasColumnName("unitclass")
                    .HasColumnType("smallint(2)");

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

            modelBuilder.Entity<Weeksummarized>(entity =>
            {
                entity.HasKey(e => new { e.Startdate, e.Enddate, e.Reportname });

                entity.ToTable("weeksummarized");

                entity.Property(e => e.Startdate)
                    .HasColumnName("startdate")
                    .HasMaxLength(10);

                entity.Property(e => e.Enddate)
                    .HasColumnName("enddate")
                    .HasMaxLength(10);

                entity.Property(e => e.Reportname)
                    .HasColumnName("reportname")
                    .HasMaxLength(100);

                entity.Property(e => e.Comment)
                    .HasColumnName("comment")
                    .HasMaxLength(450);

                entity.Property(e => e.Content)
                    .IsRequired()
                    .HasColumnName("content")
                    .HasColumnType("varchar(21000)");

                entity.Property(e => e.Draft)
                    .HasColumnName("draft")
                    .HasColumnType("smallint(2)");

                entity.Property(e => e.Time)
                    .HasColumnName("time")
                    .HasColumnType("datetime");
            });
        }
    }
}
