using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Asset.API.Models;

public partial class AssetDbContext : DbContext
{
    public AssetDbContext()
    {
    }

    public AssetDbContext(DbContextOptions<AssetDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ActaDetalle> ActaDetalles { get; set; }

    public virtual DbSet<Activo> Activos { get; set; }

    public virtual DbSet<ActivoAtributo> ActivoAtributos { get; set; }

    public virtual DbSet<ActivoComponente> ActivoComponentes { get; set; }

    public virtual DbSet<Actum> Acta { get; set; }

    public virtual DbSet<Atributo> Atributos { get; set; }

    public virtual DbSet<BajaActivo> BajaActivos { get; set; }

    public virtual DbSet<Contrato> Contratos { get; set; }

    public virtual DbSet<Departamento> Departamentos { get; set; }

    public virtual DbSet<Empleado> Empleados { get; set; }

    public virtual DbSet<EstadoActivo> EstadoActivos { get; set; }

    public virtual DbSet<EstadoFisico> EstadoFisicos { get; set; }

    public virtual DbSet<HistorialAsignacion> HistorialAsignacions { get; set; }

    public virtual DbSet<Marca> Marcas { get; set; }

    public virtual DbSet<Modelo> Modelos { get; set; }

    public virtual DbSet<Proveedor> Proveedors { get; set; }

    public virtual DbSet<Rol> Rols { get; set; }

    public virtual DbSet<TipoActivo> TipoActivos { get; set; }

    public virtual DbSet<TipoPropiedad> TipoPropiedads { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    public virtual DbSet<UsuarioRol> UsuarioRols { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ActaDetalle>(entity =>
        {
            entity.HasKey(e => e.IdActaDetalle).HasName("PK__ActaDeta__44CAC8543756DC79");

            entity.ToTable("ActaDetalle");

            entity.HasOne(d => d.IdActaNavigation).WithMany(p => p.ActaDetalles)
                .HasForeignKey(d => d.IdActa)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ActaDetalle_Acta");

            entity.HasOne(d => d.IdActivoNavigation).WithMany(p => p.ActaDetalles)
                .HasForeignKey(d => d.IdActivo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ActaDetalle_Activo");

            entity.HasOne(d => d.IdEstadoEntregaNavigation).WithMany(p => p.ActaDetalleIdEstadoEntregaNavigations)
                .HasForeignKey(d => d.IdEstadoEntrega)
                .HasConstraintName("FK_ActaDetalle_EstadoEntrega");

            entity.HasOne(d => d.IdEstadoRecepcionNavigation).WithMany(p => p.ActaDetalleIdEstadoRecepcionNavigations)
                .HasForeignKey(d => d.IdEstadoRecepcion)
                .HasConstraintName("FK_ActaDetalle_EstadoRecepcion");
        });

        modelBuilder.Entity<Activo>(entity =>
        {
            entity.HasKey(e => e.IdActivo).HasName("PK__Activo__146481C0D2A7384E");

            entity.ToTable("Activo");

            entity.HasIndex(e => e.CodigoInventario, "UQ__Activo__D5EC1A77F55858CC").IsUnique();

            entity.Property(e => e.CodigoInventario).HasMaxLength(100);
            entity.Property(e => e.NombreActivo).HasMaxLength(150);
            entity.Property(e => e.Observaciones).HasMaxLength(500);
            entity.Property(e => e.Serie).HasMaxLength(150);

            entity.HasOne(d => d.IdContratoNavigation).WithMany(p => p.Activos)
                .HasForeignKey(d => d.IdContrato)
                .HasConstraintName("FK_Activo_Contrato");

            entity.HasOne(d => d.IdEstadoActivoNavigation).WithMany(p => p.Activos)
                .HasForeignKey(d => d.IdEstadoActivo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Activo_Estado");

            entity.HasOne(d => d.IdModeloNavigation).WithMany(p => p.Activos)
                .HasForeignKey(d => d.IdModelo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Activo_Modelo");

            entity.HasOne(d => d.IdTipoActivoNavigation).WithMany(p => p.Activos)
                .HasForeignKey(d => d.IdTipoActivo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Activo_TipoActivo");

            entity.HasOne(d => d.IdTipoPropiedadNavigation).WithMany(p => p.Activos)
                .HasForeignKey(d => d.IdTipoPropiedad)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Activo_TipoPropiedad");
        });

        modelBuilder.Entity<ActivoAtributo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ActivoAt__3214EC075C8D8F1F");

            entity.ToTable("ActivoAtributo");

            entity.Property(e => e.Valor).HasMaxLength(150);

            entity.HasOne(d => d.IdActivoNavigation).WithMany(p => p.ActivoAtributos)
                .HasForeignKey(d => d.IdActivo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ActivoAtributo_Activo");

            entity.HasOne(d => d.IdAtributoNavigation).WithMany(p => p.ActivoAtributos)
                .HasForeignKey(d => d.IdAtributo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ActivoAtributo_Atributo");
        });

        modelBuilder.Entity<ActivoComponente>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ActivoCo__3214EC077494CEE2");

            entity.ToTable("ActivoComponente");

            entity.Property(e => e.FechaAsignacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.FechaRemocion).HasColumnType("datetime");

            entity.HasOne(d => d.ActivoHijo).WithMany(p => p.ActivoComponenteActivoHijos)
                .HasForeignKey(d => d.ActivoHijoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Componente_Hijo");

            entity.HasOne(d => d.ActivoPadre).WithMany(p => p.ActivoComponenteActivoPadres)
                .HasForeignKey(d => d.ActivoPadreId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Componente_Padre");
        });

        modelBuilder.Entity<Actum>(entity =>
        {
            entity.HasKey(e => e.IdActa).HasName("PK__Acta__280A76B9421BF659");

            entity.Property(e => e.Fecha)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Observaciones).HasMaxLength(500);
            entity.Property(e => e.Tipo).HasMaxLength(50);

            entity.HasOne(d => d.IdEmpleadoNavigation).WithMany(p => p.Acta)
                .HasForeignKey(d => d.IdEmpleado)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Acta_Empleado");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Acta)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Acta_Usuario");
        });

        modelBuilder.Entity<Atributo>(entity =>
        {
            entity.HasKey(e => e.IdAtributo).HasName("PK__Atributo__E1FA3A0864DFD15A");

            entity.ToTable("Atributo");

            entity.HasIndex(e => e.Nombre, "UQ__Atributo__75E3EFCF7A5E1F6F").IsUnique();

            entity.Property(e => e.Nombre).HasMaxLength(100);
        });

        modelBuilder.Entity<BajaActivo>(entity =>
        {
            entity.HasKey(e => e.IdBaja).HasName("PK__BajaActi__3EA5C6096D73DDF5");

            entity.ToTable("BajaActivo");

            entity.Property(e => e.Fecha)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Motivo).HasMaxLength(500);

            entity.HasOne(d => d.IdActivoNavigation).WithMany(p => p.BajaActivos)
                .HasForeignKey(d => d.IdActivo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Baja_Activo");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.BajaActivos)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Baja_Usuario");
        });

        modelBuilder.Entity<Contrato>(entity =>
        {
            entity.HasKey(e => e.IdContrato).HasName("PK__Contrato__8569F05AF31B5E30");

            entity.ToTable("Contrato");

            entity.Property(e => e.CostoMensual).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Observaciones).HasMaxLength(500);

            entity.HasOne(d => d.IdProveedorNavigation).WithMany(p => p.Contratos)
                .HasForeignKey(d => d.IdProveedor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Contrato_Proveedor");
        });

        modelBuilder.Entity<Departamento>(entity =>
        {
            entity.HasKey(e => e.IdDepartamento).HasName("PK__Departam__787A433D8166AAC4");

            entity.ToTable("Departamento");

            entity.HasIndex(e => e.Nombre, "UQ__Departam__75E3EFCF91FBCC1A").IsUnique();

            entity.Property(e => e.Nombre).HasMaxLength(100);
        });

        modelBuilder.Entity<Empleado>(entity =>
        {
            entity.HasKey(e => e.IdEmpleado).HasName("PK__Empleado__CE6D8B9EE074EB5A");

            entity.ToTable("Empleado");

            entity.Property(e => e.Email).HasMaxLength(150);
            entity.Property(e => e.Estado)
                .HasMaxLength(50)
                .HasDefaultValue("Activo");
            entity.Property(e => e.Nombre).HasMaxLength(150);

            entity.HasOne(d => d.IdDepartamentoNavigation).WithMany(p => p.Empleados)
                .HasForeignKey(d => d.IdDepartamento)
                .HasConstraintName("FK_Empleado_Departamento");
        });

        modelBuilder.Entity<EstadoActivo>(entity =>
        {
            entity.HasKey(e => e.IdEstadoActivo).HasName("PK__EstadoAc__FBCF33C829D4CFAC");

            entity.ToTable("EstadoActivo");

            entity.HasIndex(e => e.Nombre, "UQ__EstadoAc__75E3EFCFC2BE924D").IsUnique();

            entity.Property(e => e.Nombre).HasMaxLength(50);
        });

        modelBuilder.Entity<EstadoFisico>(entity =>
        {
            entity.HasKey(e => e.IdEstadoFisico).HasName("PK__EstadoFi__E1450A3E8C7ACF5E");

            entity.ToTable("EstadoFisico");

            entity.HasIndex(e => e.Nombre, "UQ__EstadoFi__75E3EFCF2B5E7D72").IsUnique();

            entity.Property(e => e.Nombre).HasMaxLength(50);
        });

        modelBuilder.Entity<HistorialAsignacion>(entity =>
        {
            entity.HasKey(e => e.IdHistorial).HasName("PK__Historia__9CC7DBB426F428D6");

            entity.ToTable("HistorialAsignacion");

            entity.Property(e => e.FechaAsignacion).HasColumnType("datetime");
            entity.Property(e => e.FechaDevolucion).HasColumnType("datetime");

            entity.HasOne(d => d.IdActivoNavigation).WithMany(p => p.HistorialAsignacions)
                .HasForeignKey(d => d.IdActivo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Historial_Activo");

            entity.HasOne(d => d.IdEmpleadoNavigation).WithMany(p => p.HistorialAsignacions)
                .HasForeignKey(d => d.IdEmpleado)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Historial_Empleado");
        });

        modelBuilder.Entity<Marca>(entity =>
        {
            entity.HasKey(e => e.IdMarca).HasName("PK__Marca__4076A887E3E36C5A");

            entity.ToTable("Marca");

            entity.HasIndex(e => e.Nombre, "UQ__Marca__75E3EFCF5759C86F").IsUnique();

            entity.Property(e => e.Nombre).HasMaxLength(100);
        });

        modelBuilder.Entity<Modelo>(entity =>
        {
            entity.HasKey(e => e.IdModelo).HasName("PK__Modelo__CC30D30CE0500254");

            entity.ToTable("Modelo");

            entity.Property(e => e.NombreModelo).HasMaxLength(150);

            entity.HasOne(d => d.IdMarcaNavigation).WithMany(p => p.Modelos)
                .HasForeignKey(d => d.IdMarca)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Modelo_Marca");

            entity.HasOne(d => d.IdTipoActivoNavigation).WithMany(p => p.Modelos)
                .HasForeignKey(d => d.IdTipoActivo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Modelo_TipoActivo");
        });

        modelBuilder.Entity<Proveedor>(entity =>
        {
            entity.HasKey(e => e.IdProveedor).HasName("PK__Proveedo__E8B631AF2055FF22");

            entity.ToTable("Proveedor");

            entity.Property(e => e.Contacto).HasMaxLength(150);
            entity.Property(e => e.Email).HasMaxLength(150);
            entity.Property(e => e.Nombre).HasMaxLength(150);
            entity.Property(e => e.Telefono).HasMaxLength(50);
        });

        modelBuilder.Entity<Rol>(entity =>
        {
            entity.HasKey(e => e.IdRol).HasName("PK__Rol__2A49584CE439887D");

            entity.ToTable("Rol");

            entity.HasIndex(e => e.Nombre, "UQ__Rol__75E3EFCF5F9EB6E0").IsUnique();

            entity.Property(e => e.Nombre).HasMaxLength(100);
        });

        modelBuilder.Entity<TipoActivo>(entity =>
        {
            entity.HasKey(e => e.IdTipoActivo).HasName("PK__TipoActi__10ABF115D009B0EE");

            entity.ToTable("TipoActivo");

            entity.HasIndex(e => e.Nombre, "UQ__TipoActi__75E3EFCF1DB873E5").IsUnique();

            entity.Property(e => e.Nombre).HasMaxLength(100);
        });

        modelBuilder.Entity<TipoPropiedad>(entity =>
        {
            entity.HasKey(e => e.IdTipoPropiedad).HasName("PK__TipoProp__A0781E346606EBB8");

            entity.ToTable("TipoPropiedad");

            entity.HasIndex(e => e.Nombre, "UQ__TipoProp__75E3EFCF80E998BA").IsUnique();

            entity.Property(e => e.Nombre).HasMaxLength(50);
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PK__Usuario__5B65BF97BBF1FD79");

            entity.ToTable("Usuario");

            entity.HasIndex(e => e.Username, "UQ__Usuario__536C85E49FD0289A").IsUnique();

            entity.Property(e => e.Activo).HasDefaultValue(true);
            entity.Property(e => e.Email).HasMaxLength(150);
            entity.Property(e => e.PasswordHash).HasMaxLength(255);
            entity.Property(e => e.Username).HasMaxLength(100);

            entity.HasOne(d => d.IdEmpleadoNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.IdEmpleado)
                .HasConstraintName("FK_Usuario_Empleado");
        });

        modelBuilder.Entity<UsuarioRol>(entity =>
        {
            entity.HasKey(e => e.IdUsuarioRol).HasName("PK__UsuarioR__6806BF4ADA3F2542");

            entity.ToTable("UsuarioRol");

            entity.HasIndex(e => new { e.IdUsuario, e.IdRol }, "UQ_UsuarioRol").IsUnique();

            entity.HasOne(d => d.IdRolNavigation).WithMany(p => p.UsuarioRols)
                .HasForeignKey(d => d.IdRol)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UsuarioRol_Rol");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.UsuarioRols)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UsuarioRol_Usuario");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
