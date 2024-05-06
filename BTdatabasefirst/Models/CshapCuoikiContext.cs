using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace BTdatabasefirst.Models;

public partial class CshapCuoikiContext : DbContext
{
    public CshapCuoikiContext()
    {
    }

    public CshapCuoikiContext(DbContextOptions<CshapCuoikiContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Chucvu> Chucvus { get; set; }

    public virtual DbSet<Luong> Luongs { get; set; }

    public virtual DbSet<Nhanvien> Nhanviens { get; set; }

    public virtual DbSet<Phongban> Phongbans { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Chucvu>(entity =>
        {
            entity.HasKey(e => e.Idcv).HasName("PK__chucvu__9DB7B274A3B1CBF9");

            entity.ToTable("chucvu");

            entity.Property(e => e.Idcv)
                .HasMaxLength(4)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("idcv");
            entity.Property(e => e.Mota)
                .HasMaxLength(50)
                .HasColumnName("mota");
            entity.Property(e => e.Tenchucvu)
                .HasMaxLength(50)
                .HasColumnName("tenchucvu");
        });

        modelBuilder.Entity<Luong>(entity =>
        {
            entity.HasKey(e => new { e.Idnhanvien, e.Thoigian }).HasName("PK__luong__40AEAD418E0AE83D");

            entity.ToTable("luong");

            entity.Property(e => e.Idnhanvien)
                .HasMaxLength(4)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("idnhanvien");
            entity.Property(e => e.Thoigian)
                .HasMaxLength(4)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("thoigian");
            entity.Property(e => e.Luong1).HasColumnName("luong");

            entity.HasOne(d => d.IdnhanvienNavigation).WithMany(p => p.Luongs)
                .HasForeignKey(d => d.Idnhanvien)
                .HasConstraintName("FK__luong__idnhanvie__3F466844");
        });

        modelBuilder.Entity<Nhanvien>(entity =>
        {
            entity.HasKey(e => e.Idnv).HasName("PK__nhanvien__9DB74554051BF9E4");

            entity.ToTable("nhanvien");

            entity.Property(e => e.Idnv)
                .HasMaxLength(4)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("idnv");
            entity.Property(e => e.Idcv)
                .HasMaxLength(4)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("idcv");
            entity.Property(e => e.Idpb)
                .HasMaxLength(4)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("idpb");
            entity.Property(e => e.Ngaysinh)
                .HasColumnType("datetime")
                .HasColumnName("ngaysinh");
            entity.Property(e => e.Sdt)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("sdt");
            entity.Property(e => e.Tennhanvien)
                .HasMaxLength(50)
                .HasColumnName("tennhanvien");

            entity.HasOne(d => d.IdcvNavigation).WithMany(p => p.Nhanviens)
                .HasForeignKey(d => d.Idcv)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__nhanvien__idcv__3C69FB99");

            entity.HasOne(d => d.IdpbNavigation).WithMany(p => p.Nhanviens)
                .HasForeignKey(d => d.Idpb)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__nhanvien__idpb__3B75D760");
        });

        modelBuilder.Entity<Phongban>(entity =>
        {
            entity.HasKey(e => e.Idpb).HasName("PK__phongban__9DB755026952F1C4");

            entity.ToTable("phongban");

            entity.Property(e => e.Idpb)
                .HasMaxLength(4)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("idpb");
            entity.Property(e => e.Mota)
                .HasMaxLength(50)
                .HasColumnName("mota");
            entity.Property(e => e.Tenphongban)
                .HasMaxLength(50)
                .HasColumnName("tenphongban");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
