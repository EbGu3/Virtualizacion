CREATE DATABASE chat_plus;


CREATE TABLE usuario 
(
	Id 			SERIAL 		PRIMARY KEY	,
	Usuario		TEXT		UNIQUE		,
	Nombre 		VARCHAR(20)	NOT NULL    ,
	Apellido 	VARCHAR(20)	NOT NULL	,
	Edad 		INT			NOT NULL	,
	email		TEXT		NOT NULL
);


SELECT * FROM usuario;

INSERT INTO usuario (Usuario, Nombre, Apellido, Edad, email) VALUES ('ivanna', 'Ivanna', 'Guzman', '18', 'ivaGuz@gmail.com');
INSERT INTO usuario (Usuario, Nombre, Apellido, Edad, email) VALUES ('eber', 'Jared', 'Guerra', '18', 'eguerra@gmail.com');

DROP TABLE usuario;