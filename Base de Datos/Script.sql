CREATE DATABASE Encriptacion;

USE Encriptacion;

CREATE TABLE USUARIO(
	IdUsuario int PRIMARY KEY IDENTITY(1,1) NOT NULL,
	nombre varchar(50) NOT NULL,
	nombreusuario varchar(50) NOT NULL,
	correo varchar(50) NOT NULL,
	password varchar(100) NOT NULL);

delete from USUARIO;

SELECT * FROM USUARIO;

delete from USUARIO where IdUsuario = '14';