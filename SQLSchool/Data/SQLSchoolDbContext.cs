using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using SQLSchool.Models;

namespace SQLSchool.Data;

public partial class SQLSchoolDbContext : DbContext
{
    public SQLSchoolDbContext()
    {
    }

    public SQLSchoolDbContext(DbContextOptions<SQLSchoolDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Befattning> Befattnings { get; set; }

    public virtual DbSet<Betyg> Betygs { get; set; }

    public virtual DbSet<Elever> Elevers { get; set; }

    public virtual DbSet<Klasser> Klassers { get; set; }

    public virtual DbSet<Kurser> Kursers { get; set; }

    public virtual DbSet<Personal> Personals { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=DANIELS-ZENBOOK;Database=SQLSchool; Integrated Security=True;Trust Server Certificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Befattning>(entity =>
        {
            entity.HasKey(e => e.BefattningsId).HasName("PK__Befattni__7CA584DA6DADEB96");

            entity.ToTable("Befattning");

            entity.Property(e => e.BefattningsId).HasColumnName("BefattningsID");
            entity.Property(e => e.Befattning1)
                .HasMaxLength(50)
                .HasColumnName("Befattning");
        });

        modelBuilder.Entity<Betyg>(entity =>
        {
            entity.HasKey(e => e.BetygId).HasName("PK__Betyg__E90ED0485D2E69D5");

            entity.ToTable("Betyg");

            entity.Property(e => e.BetygId).HasColumnName("BetygID");
            entity.Property(e => e.Betyg1)
                .HasMaxLength(5)
                .HasColumnName("Betyg");
            entity.Property(e => e.ElevId).HasColumnName("ElevID");
            entity.Property(e => e.KursId).HasColumnName("KursID");
            entity.Property(e => e.LärareId).HasColumnName("LärareID");

            entity.HasOne(d => d.Elev).WithMany(p => p.Betygs)
                .HasForeignKey(d => d.ElevId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Betyg__ElevID__44FF419A");

            entity.HasOne(d => d.Kurs).WithMany(p => p.Betygs)
                .HasForeignKey(d => d.KursId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Betyg__KursID__45F365D3");

            entity.HasOne(d => d.Lärare).WithMany(p => p.Betygs)
                .HasForeignKey(d => d.LärareId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Betyg__LärareID__46E78A0C");
        });

        modelBuilder.Entity<Elever>(entity =>
        {
            entity.HasKey(e => e.ElevId).HasName("PK__Elever__4AE80D03305C2965");

            entity.ToTable("Elever");

            entity.HasIndex(e => e.Personnummer, "UQ__Elever__245B03C1037BA678").IsUnique();

            entity.Property(e => e.ElevId).HasColumnName("ElevID");
            entity.Property(e => e.KlassId).HasColumnName("KlassID");
            entity.Property(e => e.Namn).HasMaxLength(100);
            entity.Property(e => e.Personnummer).HasMaxLength(12);

            entity.HasOne(d => d.Klass).WithMany(p => p.Elevers)
                .HasForeignKey(d => d.KlassId)
                .HasConstraintName("FK__Elever__KlassID__403A8C7D");
        });

        modelBuilder.Entity<Klasser>(entity =>
        {
            entity.HasKey(e => e.KlassId).HasName("PK__Klasser__CF47A60D12E1A567");

            entity.ToTable("Klasser");

            entity.Property(e => e.KlassId).HasColumnName("KlassID");
            entity.Property(e => e.LärarId).HasColumnName("LärarID");
            entity.Property(e => e.Namn).HasMaxLength(100);

            entity.HasOne(d => d.Lärar).WithMany(p => p.Klassers)
                .HasForeignKey(d => d.LärarId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Klasser__LärarID__3C69FB99");
        });

        modelBuilder.Entity<Kurser>(entity =>
        {
            entity.HasKey(e => e.KursId).HasName("PK__Kurser__BCCFFF3BC21055D5");

            entity.ToTable("Kurser");

            entity.Property(e => e.KursId).HasColumnName("KursID");
            entity.Property(e => e.Namn).HasMaxLength(100);
        });

        modelBuilder.Entity<Personal>(entity =>
        {
            entity.HasKey(e => e.PersonalId).HasName("PK__Personal__283437133833432B");

            entity.ToTable("Personal");

            entity.Property(e => e.PersonalId).HasColumnName("PersonalID");
            entity.Property(e => e.BefattningId).HasColumnName("BefattningID");
            entity.Property(e => e.Namn).HasMaxLength(100);

            entity.HasOne(d => d.Befattning).WithMany(p => p.Personals)
                .HasForeignKey(d => d.BefattningId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Personal__Befatt__398D8EEE");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
