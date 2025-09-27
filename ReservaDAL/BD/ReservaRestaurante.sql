CREATE DATABASE Restaurante
GO
USE Restaurante 
GO
---tablas---
CREATE TABLE Rol(
Id INT NOT NULL PRIMARY KEY IDENTITY (1,1),
Nombre VARCHAR(50) NOT NULL
);
GO
CREATE TABLE Usuario(
Id INT NOT NULL PRIMARY KEY IDENTITY(1,1),
IdRol INT NOT NULL DEFAULT 2,
Nombre VARCHAR(50) NOT NULL,
Apellido VARCHAR(50) NOT NULL,
Celular VARCHAR(9) NOT NULL,
Cuenta VARCHAR(MAX) NOT NULL,
Contrasenia VARCHAR(MAX) NOT NULL
FOREIGN KEY(IdRol)  REFERENCES Rol(Id)
);
GO
CREATE TABLE Mesa(
Id INT NOT NULL PRIMARY KEY IDENTITY(1,1),
Nombre VARCHAR(50) NOT NULL,
Personas VARCHAR(50) NOT NULL
);
GO 
CREATE TABLE NumeroDeMesa(
Id INT NOT NULL PRIMARY KEY IDENTITY(1,1),
Nombre VARCHAR(50) NOT NULL
);
GO

CREATE TABLE Reservacion(
Id INT NOT NULL PRIMARY KEY IDENTITY(1,1),
IdMesa INT NOT NULL FOREIGN KEY REFERENCES Mesa(Id),
IdNumeroDeMesa INT NOT NULL FOREIGN KEY REFERENCES NumeroDeMesa(Id),
IdUsuario INT NOT NULL FOREIGN KEY REFERENCES Usuario(Id),
FechaDeReservacion DATE NULL
);
--procedimientos almacenados--
GO
CREATE PROCEDURE GuardarRol @Nombre VARCHAR(50)
AS
BEGIN
    INSERT INTO Rol (Nombre) VALUES(@Nombre);
END;
GO

CREATE PROCEDURE EliminarRol @Id INT
AS
BEGIN
    DELETE FROM Rol WHERE Id = @Id;
END;
GO

CREATE PROCEDURE ModificarRol @Id INT, @Nombre VARCHAR(50)
AS
BEGIN
    UPDATE Rol SET Nombre = @Nombre WHERE Id = @Id;
END;
GO

CREATE PROCEDURE MostrarRol @Nombre VARCHAR(50) = NULL
AS
BEGIN
    SELECT Id, Nombre
    FROM Rol
    WHERE (@Nombre IS NULL OR Nombre LIKE '%' + @Nombre + '%');
END;
GO

-- USUARIO
CREATE PROCEDURE GuardarUsuario
    @IdRol INT = 2,
    @Nombre VARCHAR(50),
    @Apellido VARCHAR(50),
    @Celular VARCHAR(9),
    @Cuenta VARCHAR(50),
    @Contrasenia VARCHAR(20)
AS
BEGIN
    INSERT INTO Usuario (IdRol, Nombre, Apellido, Celular, Cuenta, Contrasenia)
    VALUES(@IdRol,@Nombre,@Apellido,@Celular,@Cuenta,@Contrasenia);
END;
GO

CREATE PROCEDURE EliminarUsuario @Id INT
AS
BEGIN
    DELETE FROM Usuario WHERE Id = @Id;
END;
GO

CREATE PROCEDURE ModificarUsuario
    @Id INT,
    @IdRol INT,
    @Nombre VARCHAR(50),
    @Apellido VARCHAR(50),
    @Celular VARCHAR(9),
    @Cuenta VARCHAR(50),
    @Contrasenia VARCHAR(20)
AS
BEGIN
    UPDATE Usuario
    SET IdRol = @IdRol,
        Nombre = @Nombre,
        Apellido = @Apellido,
        Celular = @Celular,
        Cuenta = @Cuenta,
        Contrasenia = @Contrasenia
    WHERE Id = @Id;
END;
GO

CREATE PROCEDURE MostrarUsuario
    @IdRol INT = NULL,
    @Nombre VARCHAR(50) = NULL,
    @Apellido VARCHAR(50) = NULL,
    @Celular VARCHAR(9) = NULL,
    @Cuenta VARCHAR(50) = NULL,
    @Contrasenia VARCHAR(20) = NULL
AS
BEGIN
    SELECT Id, IdRol, Nombre, Apellido, Celular, Cuenta, Contrasenia
    FROM Usuario
    WHERE (@IdRol IS NULL OR IdRol = @IdRol)
      AND (@Nombre IS NULL OR Nombre LIKE '%' + @Nombre + '%')
      AND (@Apellido IS NULL OR Apellido LIKE '%' + @Apellido + '%')
      AND (@Celular IS NULL OR Celular LIKE '%' + @Celular + '%')
      AND (@Cuenta IS NULL OR Cuenta LIKE '%' + @Cuenta + '%')
      AND (@Contrasenia IS NULL OR Contrasenia LIKE '%' + @Contrasenia + '%');
END;
GO
CREATE PROCEDURE MostrarUsuarioNombre
    @IdRol INT = NULL,
    @Nombre VARCHAR(50) = NULL,
    @Apellido VARCHAR(50) = NULL,
    @Celular VARCHAR(9) = NULL,
    @Cuenta VARCHAR(50) = NULL,
    @Contrasenia VARCHAR(20) = NULL
AS
BEGIN
    SELECT 
        u.Id,
		u.IdRol,
        r.Nombre AS Nombre_Rol,
        u.Nombre,
        u.Apellido,
        u.Celular,
        u.Cuenta,
        u.Contrasenia
    FROM Usuario u
    INNER JOIN Rol r ON u.IdRol = r.Id
    WHERE (@IdRol IS NULL OR u.IdRol = @IdRol)
      AND (@Nombre IS NULL OR u.Nombre LIKE '%' + @Nombre + '%')
      AND (@Apellido IS NULL OR u.Apellido LIKE '%' + @Apellido + '%')
      AND (@Celular IS NULL OR u.Celular LIKE '%' + @Celular + '%')
      AND (@Cuenta IS NULL OR u.Cuenta LIKE '%' + @Cuenta + '%')
      AND (@Contrasenia IS NULL OR u.Contrasenia LIKE '%' + @Contrasenia + '%');
END;
GO

---Mesa---
CREATE PROCEDURE GuardarMesa
@Nombre VARCHAR(50),
@Personas VARCHAR(50)
AS
BEGIN
    INSERT INTO Mesa (Nombre, Personas) VALUES(@Nombre,@Personas);
END;
GO

CREATE PROCEDURE EliminarMesa 
@Id INT
AS
BEGIN
    DELETE FROM Mesa WHERE Id = @Id;
END;
GO

CREATE PROCEDURE ModificarMesa
@Id INT, 
@Nombre VARCHAR(50), 
@Personas VARCHAR(50)
AS
BEGIN
    UPDATE Mesa 
	SET 
	Nombre = @Nombre, 
	Personas = @Personas
	WHERE Id = @Id;
END;
GO

CREATE PROCEDURE MostrarMesa 
@Nombre VARCHAR(50) = NULL,
@Personas VARCHAR(50) = NULL
AS
BEGIN
    SELECT Id, Nombre, Personas
    FROM Mesa
    WHERE (@Nombre IS NULL OR Nombre LIKE '%' + @Nombre + '%')
      AND (@Personas IS NULL OR Personas LIKE '%' + @Personas + '%');
END;
GO
---Numero Mesa---
CREATE PROCEDURE GuardarNumeroDeMesa 
@Nombre VARCHAR(50)
AS
BEGIN
    INSERT INTO NumeroDeMesa (Nombre) VALUES(@Nombre);
END;
GO

CREATE PROCEDURE EliminarNumeroDeMesa @Id INT
AS
BEGIN
    DELETE FROM NumeroDeMesa WHERE Id = @Id;
END;
GO

CREATE PROCEDURE ModificarNumeroDeMesa @Id INT,
@Nombre VARCHAR(50)
AS
BEGIN
    UPDATE NumeroDeMesa SET Nombre = @Nombre WHERE Id = @Id;
END;
GO

CREATE PROCEDURE MostrarNumeroDeMesa
@Nombre VARCHAR(50) = NULL
AS
BEGIN
    SELECT Id, Nombre
    FROM NumeroDeMesa
    WHERE (@Nombre IS NULL OR Nombre LIKE '%' + @Nombre + '%');
END;
GO
---Reservacion---
CREATE PROCEDURE GuardarReservacion
    @IdMesa INT,
    @IdNumeroDeMesa INT,
    @IdUsuario INT,
    @FechaDeReservacion DATE
    
AS
BEGIN
    INSERT INTO Reservacion(IdMesa,IdNumeroDeMesa,IdUsuario,FechaDeReservacion)
    VALUES (@IdMesa,@IdNumeroDeMesa,@IdUsuario,@FechaDeReservacion);
END;
GO

CREATE PROCEDURE EliminarReservacion @Id INT
AS
BEGIN
    DELETE FROM Reservacion WHERE Id = @Id;
END;
GO

CREATE PROCEDURE ModificarReservacion
    @Id INT,
    @IdMesa INT,
    @IdNumeroDeMesa INT,
    @IdUsuario INT,
    @FechaDeReservacion DATE
    
AS
BEGIN
    UPDATE Reservacion
    SET IdMesa=@IdMesa,
	IdNumeroDeMesa=@IdNumeroDeMesa,
	IdUsuario=@IdUsuario,
	FechaDeReservacion=@FechaDeReservacion
    WHERE Id = @Id;
END;
GO

CREATE PROCEDURE MostrarReservacion
    
    @IdMesa INT =NULL,
    @IdNumeroDeMesa INT=NULL,
    @IdUsuario INT=NULL,
    @FechaDeReservacion DATE=NULL
AS
BEGIN
    SELECT Id,IdMesa,IdNumeroDeMesa, IdUsuario,FechaDeReservacion
    FROM Reservacion
    WHERE (@IdMesa IS NULL OR IdMesa = @IdMesa)
      AND (@IdNumeroDeMesa IS NULL OR IdNumeroDeMesa = @IdNumeroDeMesa)
      AND (@IdUsuario IS NULL OR IdUsuario = @IdUsuario)
      AND (@FechaDeReservacion IS NULL OR FechaDeReservacion = @FechaDeReservacion)
END;
GO
CREATE PROCEDURE MostrarReservacionNombre
    @IdMesa INT = NULL,
    @IdNumeroDeMesa INT = NULL,
    @IdUsuario INT = NULL,
    @FechaDeReservacion DATE = NULL
  
AS
BEGIN
    SELECT 
        r.Id,
		r.IdMesa,
        m.Nombre AS Nombre_Mesa,
		r.IdNumeroDeMesa,
        n.Nombre AS Nombre_NumeroDeMesa,
		r.IdUsuario,
        u.Nombre AS Nombre_Usuario,
		u.Apellido AS Apellido_Usuario,
		u.Celular AS Numero_Celular,
        r.FechaDeReservacion
       
    FROM Reservacion r
    INNER JOIN Mesa m ON r.IdMesa = m.Id
    INNER JOIN NumeroDeMesa n ON r.IdNumeroDeMesa = n.Id
    INNER JOIN Usuario u ON r.IdUsuario = u.Id
    WHERE (@IdMesa IS NULL OR r.IdMesa = @IdMesa)
      AND (@IdNumeroDeMesa IS NULL OR r.IdNumeroDeMesa = @IdNumeroDeMesa)
      AND (@IdUsuario IS NULL OR r.IdUsuario = @IdUsuario)
      AND (@FechaDeReservacion IS NULL OR r.FechaDeReservacion = @FechaDeReservacion)
      
END;
GO

EXEC GuardarRol Administrador
EXEC GuardarRol Cliente
EXEC GuardarUsuario @IdRol='1',@Nombre='Diego',@Apellido='Reyes',@Celular='79053370',@Cuenta='Diego@gmail.com',@Contrasenia='Diego'
EXEC GuardarUsuario @Nombre='Michel',@Apellido='Rivas',@Celular='77020277',@Cuenta='Michel@gmail.com',@Contrasenia='Michel'
EXEC GuardarMesa @Nombre='Mesa VIP',@Personas='Espacio para 10 Personas'
EXEC GuardarNumeroDeMesa @Nombre='1'
EXEC GuardarReservacion @IdMesa='1',@IdNumeroDeMesa='1',@IdUsuario='2',@FechaDeReservacion='2025-09-26'
EXEC MostrarReservacion