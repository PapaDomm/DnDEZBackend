using System;
using System.Collections.Generic;
using DnDEZBackEnd.Models.Public_Classes;
using Microsoft.EntityFrameworkCore;

namespace DnDEZBackEnd.Models;

public partial class DnDezdbContext : DbContext
{
    public DnDezdbContext()
    {
    }

    public DnDezdbContext(DbContextOptions<DnDezdbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AbilityScore> AbilityScores { get; set; }

    public virtual DbSet<CharAbilityScore> CharAbilityScores { get; set; }

    public virtual DbSet<Character> Characters { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer(Secret.url);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AbilityScore>(entity =>
        {
            entity.HasKey(e => e.Index).HasName("abilityscores_index_pk");

            entity.ToTable("Ability_Scores");

            entity.Property(e => e.Index).HasMaxLength(3);
            entity.Property(e => e.Name).HasMaxLength(3);
        });

        modelBuilder.Entity<CharAbilityScore>(entity =>
        {
            entity.HasKey(e => new { e.CharacterId, e.Index }).HasName("charabilityscores_characterabilityid_pk");

            entity.ToTable("Char_Ability_Scores");

            entity.Property(e => e.Index).HasMaxLength(3);
            entity.Property(e => e.RacialBonus)
                .IsRequired()
                .HasDefaultValueSql("('0')");

            entity.HasOne(d => d.Character).WithMany(p => p.CharAbilityScores)
                .HasForeignKey(d => d.CharacterId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("charabilityscores_characterid_fk");

            entity.HasOne(d => d.IndexNavigation).WithMany(p => p.CharAbilityScores)
                .HasForeignKey(d => d.Index)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("charabilityscores_index_fk");
        });

        modelBuilder.Entity<Character>(entity =>
        {
            entity.HasKey(e => e.CharacterId).HasName("character_characterid_pk");

            entity.ToTable("Character");

            entity.Property(e => e.Class).HasMaxLength(9);
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Race).HasMaxLength(10);

            entity.HasOne(d => d.User).WithMany(p => p.Characters)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("character_userid_fk");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("users_userid_pk");

            entity.Property(e => e.Name).HasMaxLength(40);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
