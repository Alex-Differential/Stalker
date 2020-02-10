using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace StalkerApplication
{
    public partial class DBStalkerContext : DbContext
    {
        public DBStalkerContext()
        {
        }

        public DBStalkerContext(DbContextOptions<DBStalkerContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Equipment> Equipment { get; set; }
        public virtual DbSet<GroupSg> GroupSg { get; set; }
        public virtual DbSet<GroupWp> GroupWp { get; set; }
        public virtual DbSet<Grouping> Grouping { get; set; }
        public virtual DbSet<ProducingCountry> ProducingCountry { get; set; }
        public virtual DbSet<SeriesGame> SeriesGame { get; set; }
        public virtual DbSet<TypeEquipment> TypeEquipment { get; set; }
        public virtual DbSet<TypeWeapons> TypeWeapons { get; set; }
        public virtual DbSet<Weapons> Weapons { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=DESKTOP-VOUJJQ0; Database=DBStalker; Trusted_Connection=True; ");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Equipment>(entity =>
            {
                entity.HasKey(e => e.EqId);

                entity.ToTable("EQUIPMENT");

                entity.Property(e => e.EqId).HasColumnName("EQ_ID");

                entity.Property(e => e.EqName)
                    .IsRequired()
                    .HasColumnName("EQ_NAME")
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.EqTe).HasColumnName("EQ_TE");

                entity.HasOne(d => d.EqTeNavigation)
                    .WithMany(p => p.Equipment)
                    .HasForeignKey(d => d.EqTe)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EQUIPMENT_TYPE_EQUIPMENT");
            });

            modelBuilder.Entity<GroupSg>(entity =>
            {
                entity.HasKey(e => new { e.GsGrid, e.GsSgid });

                entity.ToTable("GROUP_SG");

                entity.Property(e => e.GsGrid).HasColumnName("GS_GRID");

                entity.Property(e => e.GsSgid).HasColumnName("GS_SGID");

                entity.HasOne(d => d.GsGr)
                    .WithMany(p => p.GroupSg)
                    .HasForeignKey(d => d.GsGrid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_GROUP_SG_GROUPING");

                entity.HasOne(d => d.GsSg)
                    .WithMany(p => p.GroupSg)
                    .HasForeignKey(d => d.GsSgid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_GROUP_SG_SERIES_GAME");
            });

            modelBuilder.Entity<GroupWp>(entity =>
            {
                entity.HasKey(e => new { e.GwGrid, e.GwWpid })
                    .HasName("PK_GR_WP");

                entity.ToTable("GROUP_WP");

                entity.Property(e => e.GwGrid).HasColumnName("GW_GRID");

                entity.Property(e => e.GwWpid).HasColumnName("GW_WPID");

                entity.HasOne(d => d.GwGr)
                    .WithMany(p => p.GroupWp)
                    .HasForeignKey(d => d.GwGrid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_GROUP_WP_GROUPING");

                entity.HasOne(d => d.GwGrNavigation)
                    .WithMany(p => p.GroupWp)
                    .HasForeignKey(d => d.GwGrid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_GROUP_WP_WEAPONS");
            });

            modelBuilder.Entity<Grouping>(entity =>
            {
                entity.HasKey(e => e.GrId);

                entity.ToTable("GROUPING");

                entity.Property(e => e.GrId).HasColumnName("GR_ID");

                entity.Property(e => e.GrEq).HasColumnName("GR_EQ");

                entity.Property(e => e.GrInfo)
                    .HasColumnName("GR_INFO")
                    .HasColumnType("text");

                entity.Property(e => e.GrName)
                    .IsRequired()
                    .HasColumnName("GR_NAME")
                    .HasMaxLength(20)
                    .IsFixedLength();

                entity.Property(e => e.GrSg).HasColumnName("GR_SG");

                entity.HasOne(d => d.GrEqNavigation)
                    .WithMany(p => p.Grouping)
                    .HasForeignKey(d => d.GrEq)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_GROUPING_EQUIPMENT");
            });

            modelBuilder.Entity<ProducingCountry>(entity =>
            {
                entity.HasKey(e => e.PcId);

                entity.ToTable("PRODUCING_COUNTRY");

                entity.Property(e => e.PcId).HasColumnName("PC_ID");

                entity.Property(e => e.PcName)
                    .HasColumnName("PC_NAME")
                    .HasMaxLength(10)
                    .IsFixedLength();
            });

            modelBuilder.Entity<SeriesGame>(entity =>
            {
                entity.HasKey(e => e.SgId);

                entity.ToTable("SERIES_GAME");

                entity.Property(e => e.SgId).HasColumnName("SG_ID");

                entity.Property(e => e.SgName)
                    .IsRequired()
                    .HasColumnName("SG_NAME")
                    .HasMaxLength(10)
                    .IsFixedLength();
            });

            modelBuilder.Entity<TypeEquipment>(entity =>
            {
                entity.HasKey(e => e.TeId);

                entity.ToTable("TYPE_EQUIPMENT");

                entity.Property(e => e.TeId).HasColumnName("TE_ID");

                entity.Property(e => e.TeName)
                    .HasColumnName("TE_NAME")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<TypeWeapons>(entity =>
            {
                entity.HasKey(e => e.TwId);

                entity.ToTable("TYPE_WEAPONS");

                entity.Property(e => e.TwId).HasColumnName("TW_ID");

                entity.Property(e => e.TwName)
                    .HasColumnName("TW_NAME")
                    .HasMaxLength(10)
                    .IsFixedLength();
            });

            modelBuilder.Entity<Weapons>(entity =>
            {
                entity.HasKey(e => e.WpId);

                entity.ToTable("WEAPONS");

                entity.Property(e => e.WpId).HasColumnName("WP_ID");

                entity.Property(e => e.WpMg)
                    .HasColumnName("WP_MG")
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.WpName)
                    .IsRequired()
                    .HasColumnName("WP_NAME")
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.WpPc).HasColumnName("WP_PC");

                entity.Property(e => e.WpRn)
                    .HasColumnName("WP_RN")
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.WpTf)
                    .HasColumnName("WP_TF")
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.WpTw).HasColumnName("WP_TW");

                entity.Property(e => e.WpWg)
                    .IsRequired()
                    .HasColumnName("WP_WG")
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.HasOne(d => d.WpPcNavigation)
                    .WithMany(p => p.Weapons)
                    .HasForeignKey(d => d.WpPc)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_WEAPONS_PRODUCING_COUNTRY");

                entity.HasOne(d => d.WpTwNavigation)
                    .WithMany(p => p.Weapons)
                    .HasForeignKey(d => d.WpTw)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_WEAPONS_TYPE_WEAPONS");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
