CREATE TABLE Rol (
	id UNIQUEIDENTIFIER NOT NULL PRIMARY KEY DEFAULT NEWID(),
    description VARCHAR(150) NOT NULL
);

CREATE TABLE Users (
	id UNIQUEIDENTIFIER NOT NULL PRIMARY KEY DEFAULT NEWID(),
	login VARCHAR(150) NOT NULL,
	password VARCHAR(2000) NOT NULL,
    name NVARCHAR(150) NOT NULL,
    last_name NVARCHAR(150) NOT NULL,
	address NVARCHAR(150) NOT NULL,
	profile_photo_url VARCHAR(150),
	directory_home VARCHAR(150) NOT NULL,
	
	rol_id uniqueidentifier,
	FOREIGN KEY (rol_id) REFERENCES Rol(id)
);

insert into Rol (description)
values('Administrador')

insert into Rol (description)
values('Usuario')

drop table Users
drop table Rol