CREATE DATABASE [bdolimpiada]
GO
USE [bdolimpiada]
GO

CREATE TABLE T_USUARIO
(
	usuario_id INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	usuario VARCHAR(100) NULL,
	clave VARCHAR(100) NULL,
);

CREATE TABLE T_SEDEOLIMPICA
(
	sede_id INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	nombre VARCHAR(100) NULL,
	complejo_numero INT NULL,
	presupuesto DECIMAL(10,3) NULL
);

CREATE TABLE T_COMPLEJODEPORTIVO
(
	deportivo_id INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	localizacion VARCHAR(100) NULL,
	jefe_organizacion VARCHAR(150) NULL,
	area_total VARCHAR(150) NULL,
	sede_id INT NOT NULL,
FOREIGN KEY(sede_id) REFERENCES T_SEDEOLIMPICA(sede_id)
);

CREATE TABLE T_COMPLEJOPOLIDEPORTIVO
(
	polideportivo_id INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	localizacion VARCHAR(50) NULL,
	area VARCHAR(150) NULL,
	deportivo_id INT NOT NULL,
FOREIGN KEY(deportivo_id) REFERENCES T_COMPLEJODEPORTIVO(deportivo_id)
);

CREATE TABLE T_COMPLEJOUNIDEPORTIVO
(
	unideportivo_id INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	nombre VARCHAR(150) NULL,
	localizacion VARCHAR(50) NULL,
	area VARCHAR(150) NULL,
	deportivo_id INT NOT NULL,
FOREIGN KEY(deportivo_id) REFERENCES T_COMPLEJODEPORTIVO(deportivo_id)
);

CREATE TABLE T_EVENTO
(
   evento_id INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
   nombre VARCHAR(100) NULL,
   lugar VARCHAR(100) NULL,
   fecha VARCHAR(100) NULL,
   duracion VARCHAR(100) NULL,
   numero_participantes INT NULL,
   cataegoria VARCHAR(50) NULL,
   polideportivo_id INT NOT NULL,
   unideportivo_id INT NOT NULL,
   FOREIGN KEY(polideportivo_id) REFERENCES T_COMPLEJOPOLIDEPORTIVO(polideportivo_id),
   FOREIGN KEY(unideportivo_id) REFERENCES T_COMPLEJOUNIDEPORTIVO(unideportivo_id)
);

CREATE TABLE T_COMISARIO
(
   comisario_id INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
   nombre VARCHAR(100) NULL,
   tipo VARCHAR(100) NULL,
   evento_id INT NOT NULL,
   FOREIGN KEY(evento_id) REFERENCES T_EVENTO(evento_id)
);

GO

-- PROCEDIMIENTOS --

CREATE PROCEDURE [SP_SEDE_CREAR]
(
@nombre VARCHAR(100),
@complejo_numero INT,
@presupuesto DECIMAL(10,3)
)
AS
BEGIN

INSERT INTO T_SEDEOLIMPICA 
(
	nombre,
	complejo_numero,
	presupuesto
)
VALUES 
(
	@nombre,
	@complejo_numero,
	@presupuesto
)
END

GO

CREATE PROCEDURE [SP_SEDE_LISTAR]
(
@tipolistado INT,
@sede_id INT
)
AS
BEGIN

IF (@tipolistado = 0)
	BEGIN

SELECT
	sede_id,
	nombre, 
	complejo_numero, 
	presupuesto 
FROM T_SEDEOLIMPICA

	END
ELSE
	BEGIN
SELECT 
	sede_id,
	nombre, 
	complejo_numero, 
	presupuesto 
FROM T_SEDEOLIMPICA WHERE sede_id = @sede_id
	END

END

GO

CREATE PROCEDURE [SP_SEDE_ACTUALIZAR]
(
@sede_id INT,
@nombre VARCHAR(100),
@complejo_numero INT,
@presupuesto DECIMAL(10,3)
)
AS
BEGIN

UPDATE T_SEDEOLIMPICA 
SET 
	nombre = @nombre,
	complejo_numero = @complejo_numero, 
	presupuesto = @presupuesto
WHERE sede_id = @sede_id

END

GO

CREATE PROCEDURE [SP_SEDE_ELIMINAR]
(
@sede_id INT
)
AS
BEGIN

DELETE FROM T_SEDEOLIMPICA WHERE sede_id = @sede_id

END

GO

CREATE PROCEDURE [SP_DEPORTIVO_CREAR]
(
@localizacion VARCHAR(100),
@jefe_organizacion VARCHAR(150),
@area_total VARCHAR(150),
@sede_id INT
)
AS
BEGIN

INSERT INTO T_COMPLEJODEPORTIVO 
(
	localizacion,
	jefe_organizacion,
	area_total,
	sede_id
)
VALUES 
(
	@localizacion,
	@jefe_organizacion,
	@area_total,
	@sede_id
)
END

GO

CREATE PROCEDURE [SP_DEPORTIVO_LISTAR]
(
@tipolistado INT,
@deportivo_id INT
)
AS
BEGIN

IF (@tipolistado = 0)
	BEGIN

SELECT
	cd.deportivo_id,
	cd.localizacion,
	cd.jefe_organizacion,
	cd.area_total,
	sd.sede_id,
	sd.nombre
FROM T_COMPLEJODEPORTIVO cd INNER JOIN T_SEDEOLIMPICA sd ON cd.sede_id = sd.sede_id

	END
ELSE
	BEGIN
SELECT
	cd.deportivo_id,
	cd.localizacion,
	cd.jefe_organizacion,
	cd.area_total,
	sd.sede_id,
	sd.nombre
FROM T_COMPLEJODEPORTIVO cd INNER JOIN T_SEDEOLIMPICA sd ON cd.sede_id = sd.sede_id
WHERE cd.deportivo_id = @deportivo_id
	END

END

GO

CREATE PROCEDURE [SP_DEPORTIVO_ACTUALIZAR]
(
@deportivo_id INT,
@localizacion VARCHAR(100),
@jefe_organizacion VARCHAR(150),
@area_total VARCHAR(150),
@sede_id INT
)
AS
BEGIN

UPDATE T_COMPLEJODEPORTIVO 
SET
	localizacion = @localizacion,
	jefe_organizacion = @jefe_organizacion,
	area_total = @area_total,
	sede_id = @sede_id
WHERE deportivo_id = @deportivo_id

END

GO

CREATE PROCEDURE [SP_DEPORTIVO_ELIMINAR]
(
@deportivo_id INT
)
AS
BEGIN

DELETE FROM T_COMPLEJODEPORTIVO WHERE deportivo_id = @deportivo_id

END

GO

CREATE PROCEDURE [SP_USUARIO_CREAR]
(
@usuario VARCHAR(100),
@clave VARCHAR(100)
)
AS
BEGIN

IF EXISTS(SELECT * FROM T_USUARIO WHERE usuario = @usuario)
BEGIN 

SELECT 0

END
ELSE
BEGIN

INSERT INTO T_USUARIO 
(
	usuario,
	clave
)
VALUES 
(
	@usuario,
	@clave
)

SELECT SCOPE_IDENTITY()

END
END

GO

CREATE PROCEDURE [SP_USUARIO_LISTAR]
(
@usuario VARCHAR(100),
@clave VARCHAR(100)
)
AS
BEGIN

SELECT
	usuario_id,
	usuario
FROM T_USUARIO WHERE usuario = @usuario AND clave = @clave 

END

GO