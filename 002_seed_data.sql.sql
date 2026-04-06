USE ActivosTI_DB;
GO

-- =========================
-- DATOS INICIALES
-- =========================

IF NOT EXISTS (SELECT 1 FROM dbo.EstadoActivo)
BEGIN
    INSERT INTO dbo.EstadoActivo (Nombre) VALUES 
    ('Disponible'),
    ('Asignado'),
    ('Dañado'),
    ('Baja');
END
GO

IF NOT EXISTS (SELECT 1 FROM dbo.TipoPropiedad)
BEGIN
    INSERT INTO dbo.TipoPropiedad (Nombre) VALUES
    ('Propio'),
    ('Rentado');
END
GO

IF NOT EXISTS (SELECT 1 FROM dbo.Rol)
BEGIN
    INSERT INTO dbo.Rol (Nombre) VALUES
    ('Admin'),
    ('Soporte'),
    ('Auditor');
END
GO

IF NOT EXISTS (SELECT 1 FROM dbo.EstadoFisico)
BEGIN
    INSERT INTO dbo.EstadoFisico (Nombre) VALUES
    ('Nuevo'),
    ('Bueno'),
    ('Regular'),
    ('Dañado');
END
GO