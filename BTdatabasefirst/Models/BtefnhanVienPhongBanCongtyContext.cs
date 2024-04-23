using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace BTdatabasefirst.Models;

public partial class BtefnhanVienPhongBanCongtyContext : DbContext
{
    public BtefnhanVienPhongBanCongtyContext()
    {
    }

    public BtefnhanVienPhongBanCongtyContext(DbContextOptions<BtefnhanVienPhongBanCongtyContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Congty> Congties { get; set; }

    public virtual DbSet<NhanVien> NhanViens { get; set; }

    public virtual DbSet<Phongban> Phongbans { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Congty>(entity =>
        {
            entity.HasKey(e => e.Idcty).HasName("PK__CONGTY__064FA60D1BBE5638");

            entity.ToTable("CONGTY");

            entity.Property(e => e.Idcty)
                .HasMaxLength(4)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("idcty");
            entity.Property(e => e.Tencty)
                .HasMaxLength(50)
                .HasColumnName("tencty");
        });

        modelBuilder.Entity<NhanVien>(entity =>
        {
            entity.HasKey(e => e.Idnhanvien).HasName("PK__NhanVien__A703FF0134169A93");

            entity.ToTable("NhanVien");

            entity.Property(e => e.Idnhanvien)
                .HasMaxLength(4)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("idnhanvien");
            entity.Property(e => e.Idphongban)
                .HasMaxLength(4)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("idphongban");
            entity.Property(e => e.Tennhanvien)
                .HasMaxLength(50)
                .HasColumnName("tennhanvien");
            entity.Property(e => e.Tuoi).HasColumnName("tuoi");

            entity.HasOne(d => d.IdphongbanNavigation).WithMany(p => p.NhanViens)
                .HasForeignKey(d => d.Idphongban)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__NhanVien__idphon__3C69FB99");
        });

        modelBuilder.Entity<Phongban>(entity =>
        {
            entity.HasKey(e => e.Idphongban).HasName("PK__phongban__B4A47B95258302AA");

            entity.ToTable("phongban");

            entity.Property(e => e.Idphongban)
                .HasMaxLength(4)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("idphongban");
            entity.Property(e => e.Idcongty)
                .HasMaxLength(4)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("idcongty");
            entity.Property(e => e.Tenphongban)
                .HasMaxLength(50)
                .HasColumnName("tenphongban");

            entity.HasOne(d => d.IdcongtyNavigation).WithMany(p => p.Phongbans)
                .HasForeignKey(d => d.Idcongty)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__phongban__idcong__398D8EEE");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
