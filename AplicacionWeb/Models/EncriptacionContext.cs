using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace AplicacionWeb.Models;

public partial class EncriptacionContext : DbContext
{
    public EncriptacionContext()
    {
    }

    public EncriptacionContext(DbContextOptions<EncriptacionContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PK__USUARIO__5B65BF976335BF2F");

            entity.ToTable("USUARIO");

            entity.Property(e => e.Correo)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("correo");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.Nombreusuario)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombreusuario");
            entity.Property(e => e.Password)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("password");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
