-- ========================================
-- CREAR BASE DE DATOS
-- ========================================

IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = 'ActivosTI_DB')
BEGIN
    CREATE DATABASE ActivosTI_DB;
END
GO

USE ActivosTI_DB;
GO

-- ========================================
-- CONFIGURACIÓN
-- ========================================

SET ANSI_NULLS ON;
GO

SET QUOTED_IDENTIFIER ON;
GO

-- =========================
-- CATÁLOGOS
-- =========================

CREATE TABLE Marca (
    IdMarca INT IDENTITY PRIMARY KEY,
    Nombre NVARCHAR(100) NOT NULL UNIQUE
);

CREATE TABLE TipoActivo (
    IdTipoActivo INT IDENTITY PRIMARY KEY,
    Nombre NVARCHAR(100) NOT NULL UNIQUE
);

CREATE TABLE EstadoActivo (
    IdEstadoActivo INT IDENTITY PRIMARY KEY,
    Nombre NVARCHAR(50) NOT NULL UNIQUE
);

CREATE TABLE TipoPropiedad (
    IdTipoPropiedad INT IDENTITY PRIMARY KEY,
    Nombre NVARCHAR(50) NOT NULL UNIQUE -- Propio / Rentado
);

CREATE TABLE Departamento (
    IdDepartamento INT IDENTITY PRIMARY KEY,
    Nombre NVARCHAR(100) NOT NULL UNIQUE
);

-- =========================
-- PROVEEDORES Y CONTRATOS
-- =========================

CREATE TABLE Proveedor (
    IdProveedor INT IDENTITY PRIMARY KEY,
    Nombre NVARCHAR(150) NOT NULL,
    Contacto NVARCHAR(150),
    Telefono NVARCHAR(50),
    Email NVARCHAR(150)
);

CREATE TABLE Contrato (
    IdContrato INT IDENTITY PRIMARY KEY,
    IdProveedor INT NOT NULL,
    FechaInicio DATE,
    FechaFin DATE,
    CostoMensual DECIMAL(10,2),
    Observaciones NVARCHAR(500),

    CONSTRAINT FK_Contrato_Proveedor FOREIGN KEY (IdProveedor) REFERENCES Proveedor(IdProveedor)
);

-- =========================
-- MODELO
-- =========================

CREATE TABLE Modelo (
    IdModelo INT IDENTITY PRIMARY KEY,
    NombreModelo NVARCHAR(150) NOT NULL,
    IdMarca INT NOT NULL,
    IdTipoActivo INT NOT NULL,

    CONSTRAINT FK_Modelo_Marca FOREIGN KEY (IdMarca) REFERENCES Marca(IdMarca),
    CONSTRAINT FK_Modelo_TipoActivo FOREIGN KEY (IdTipoActivo) REFERENCES TipoActivo(IdTipoActivo)
);

-- =========================
-- EMPLEADOS
-- =========================

CREATE TABLE Empleado (
    IdEmpleado INT IDENTITY PRIMARY KEY,
    Nombre NVARCHAR(150) NOT NULL,
    Email NVARCHAR(150),
    IdDepartamento INT,
    Estado NVARCHAR(50) DEFAULT 'Activo',

    CONSTRAINT FK_Empleado_Departamento FOREIGN KEY (IdDepartamento) REFERENCES Departamento(IdDepartamento)
);

-- =========================
-- USUARIOS (LOGIN)
-- =========================

CREATE TABLE Usuario (
    IdUsuario INT IDENTITY PRIMARY KEY,
    Username NVARCHAR(100) NOT NULL UNIQUE,
    PasswordHash NVARCHAR(255) NOT NULL,
    Email NVARCHAR(150),
    Activo BIT DEFAULT 1,
    IdEmpleado INT NULL,

    CONSTRAINT FK_Usuario_Empleado FOREIGN KEY (IdEmpleado) REFERENCES Empleado(IdEmpleado)
);

CREATE TABLE Rol (
    IdRol INT IDENTITY PRIMARY KEY,
    Nombre NVARCHAR(100) NOT NULL UNIQUE
);

CREATE TABLE UsuarioRol (
    IdUsuarioRol INT IDENTITY PRIMARY KEY,
    IdUsuario INT NOT NULL,
    IdRol INT NOT NULL,

    CONSTRAINT FK_UsuarioRol_Usuario FOREIGN KEY (IdUsuario) REFERENCES Usuario(IdUsuario),
    CONSTRAINT FK_UsuarioRol_Rol FOREIGN KEY (IdRol) REFERENCES Rol(IdRol),
    CONSTRAINT UQ_UsuarioRol UNIQUE (IdUsuario, IdRol)
);

-- =========================
-- ACTIVOS
-- =========================

CREATE TABLE Activo (
    IdActivo INT IDENTITY PRIMARY KEY,
    CodigoInventario NVARCHAR(100) NOT NULL UNIQUE,
    NombreActivo NVARCHAR(150) NOT NULL,
    IdTipoActivo INT NOT NULL,
    IdModelo INT NOT NULL,
    Serie NVARCHAR(150),
    IdEstadoActivo INT NOT NULL,
    IdTipoPropiedad INT NOT NULL,
    IdContrato INT NULL,
    FechaAdquisicion DATE,
    Observaciones NVARCHAR(500),

    CONSTRAINT FK_Activo_TipoActivo FOREIGN KEY (IdTipoActivo) REFERENCES TipoActivo(IdTipoActivo),
    CONSTRAINT FK_Activo_Modelo FOREIGN KEY (IdModelo) REFERENCES Modelo(IdModelo),
    CONSTRAINT FK_Activo_Estado FOREIGN KEY (IdEstadoActivo) REFERENCES EstadoActivo(IdEstadoActivo),
    CONSTRAINT FK_Activo_TipoPropiedad FOREIGN KEY (IdTipoPropiedad) REFERENCES TipoPropiedad(IdTipoPropiedad),
    CONSTRAINT FK_Activo_Contrato FOREIGN KEY (IdContrato) REFERENCES Contrato(IdContrato)
);

-- =========================
-- ACTAS (ASIGNACIÓN / DEVOLUCIÓN)
-- =========================

CREATE TABLE Acta (
    IdActa INT IDENTITY PRIMARY KEY,
    Tipo NVARCHAR(50) NOT NULL, -- Asignacion / Devolucion
    IdEmpleado INT NOT NULL,
    IdUsuario INT NOT NULL,
    Fecha DATETIME DEFAULT GETDATE(),
    Observaciones NVARCHAR(500),

    CONSTRAINT FK_Acta_Empleado FOREIGN KEY (IdEmpleado) REFERENCES Empleado(IdEmpleado),
    CONSTRAINT FK_Acta_Usuario FOREIGN KEY (IdUsuario) REFERENCES Usuario(IdUsuario)
);

CREATE TABLE EstadoFisico (
    IdEstadoFisico INT IDENTITY PRIMARY KEY,
    Nombre NVARCHAR(50) NOT NULL UNIQUE
);


CREATE TABLE ActaDetalle (
    IdActaDetalle INT IDENTITY PRIMARY KEY,
    IdActa INT NOT NULL,
    IdActivo INT NOT NULL,
    IdEstadoEntrega INT NULL,
    IdEstadoRecepcion INT NULL,

    CONSTRAINT FK_ActaDetalle_Acta FOREIGN KEY (IdActa) REFERENCES Acta(IdActa),
    CONSTRAINT FK_ActaDetalle_Activo FOREIGN KEY (IdActivo) REFERENCES Activo(IdActivo),
    CONSTRAINT FK_ActaDetalle_EstadoEntrega FOREIGN KEY (IdEstadoEntrega) REFERENCES EstadoFisico(IdEstadoFisico),
    CONSTRAINT FK_ActaDetalle_EstadoRecepcion FOREIGN KEY (IdEstadoRecepcion) REFERENCES EstadoFisico(IdEstadoFisico)
);

-- =========================
-- HISTORIAL
-- =========================

CREATE TABLE HistorialAsignacion (
    IdHistorial INT IDENTITY PRIMARY KEY,
    IdActivo INT NOT NULL,
    IdEmpleado INT NOT NULL,
    FechaAsignacion DATETIME NOT NULL,
    FechaDevolucion DATETIME NULL,

    CONSTRAINT FK_Historial_Activo FOREIGN KEY (IdActivo) REFERENCES Activo(IdActivo),
    CONSTRAINT FK_Historial_Empleado FOREIGN KEY (IdEmpleado) REFERENCES Empleado(IdEmpleado)
);

-- =========================
-- BAJA DE ACTIVOS
-- =========================

CREATE TABLE BajaActivo (
    IdBaja INT IDENTITY PRIMARY KEY,
    IdActivo INT NOT NULL,
    Fecha DATETIME DEFAULT GETDATE(),
    Motivo NVARCHAR(500),
    IdUsuario INT NOT NULL,

    CONSTRAINT FK_Baja_Activo FOREIGN KEY (IdActivo) REFERENCES Activo(IdActivo),
    CONSTRAINT FK_Baja_Usuario FOREIGN KEY (IdUsuario) REFERENCES Usuario(IdUsuario)
);

-- =========================
-- COMPONENTES (PRO)
-- =========================

CREATE TABLE ActivoComponente (
    Id INT IDENTITY PRIMARY KEY,
    ActivoPadreId INT NOT NULL,
    ActivoHijoId INT NOT NULL,
    FechaAsignacion DATETIME DEFAULT GETDATE(),
    FechaRemocion DATETIME NULL,

    CONSTRAINT FK_Componente_Padre FOREIGN KEY (ActivoPadreId) REFERENCES Activo(IdActivo),
    CONSTRAINT FK_Componente_Hijo FOREIGN KEY (ActivoHijoId) REFERENCES Activo(IdActivo)
);

-- =========================
-- ATRIBUTOS DINÁMICOS
-- =========================

CREATE TABLE Atributo (
    IdAtributo INT IDENTITY PRIMARY KEY,
    Nombre NVARCHAR(100) NOT NULL UNIQUE
);

CREATE TABLE ActivoAtributo (
    Id INT IDENTITY PRIMARY KEY,
    IdActivo INT NOT NULL,
    IdAtributo INT NOT NULL,
    Valor NVARCHAR(150),

    CONSTRAINT FK_ActivoAtributo_Activo FOREIGN KEY (IdActivo) REFERENCES Activo(IdActivo),
    CONSTRAINT FK_ActivoAtributo_Atributo FOREIGN KEY (IdAtributo) REFERENCES Atributo(IdAtributo)
);