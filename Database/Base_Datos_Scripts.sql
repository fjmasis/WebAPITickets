create database VisorTickets;
go

use VisorTickets;
go

create table Roles
(
ro_identificador int primary key identity (1,1),
ro_descripcion nvarchar(125) not null,
ro_fecha_adicion datetime default getdate() not null,
ro_adicionado_por nvarchar(10) not null,
ro_fecha_modificacion datetime,
ro_modificado_por nvarchar(10)
);

insert into roles values ('Soporte',GETDATE(), 'admin',null,null),
                         ('Analista', GETDATE(), 'admin',null ,  null)

drop table Usuarios;

create table Usuarios
(
us_identificador int primary key identity (1,1),
us_nombre_completo nvarchar(150) not null,
us_correo nvarchar(150) not null,
us_clave nvarchar(150) not null,
us_ro_identificador int foreign key (us_ro_identificador) references Roles(ro_identificador),
us_estado nvarchar(1) not null,
us_fecha_adicion datetime default getdate() not null,
us_adicionado_por nvarchar(10) not null,
us_fecha_modificacion datetime,
us_modificado_por nvarchar(10)
);
insert into Usuarios values ('Karla Brenes', 'kbrenesc@castrocarazo.ac.cr','123',1,'A',getdate(),'admin',null,null)
insert into Usuarios values ('Joshua Esteban', 'jEsteban@castrocarazo.ac.cr','123',2,'A',getdate(),'admin',null,null)

select * from Usuarios;

select ro_identificador, us_ro_identificador , us_correo
from Usuarios, Roles
where ro_identificador = us_ro_identificador;

drop table Tiquetes;
create table Tiquetes
(
ti_identificador int primary key identity (1,1),
ti_asunto nvarchar(150) not null,
ti_categoria nvarchar(150) not null,
ti_us_id_asigna int foreign key (ti_us_id_asigna) references Usuarios(us_identificador),
ti_urgencia nvarchar(150) not null,
ti_importancia nvarchar(150) not null,
ti_estado nvarchar(150) not null,
ti_solucion           nvarchar(255),
ti_fecha_adicion datetime default getdate() not null,
ti_adicionado_por nvarchar(10) not null,
ti_fecha_modificacion datetime,
ti_modificado_por nvarchar(10)
);

insert into Tiquetes (ti_asunto, ti_categoria, ti_us_id_asigna, ti_urgencia, ti_importancia, ti_estado, ti_fecha_adicion, ti_adicionado_por )
values ('Problemas de red', 'Redes',2,'Alta','Alta', 'A', getdate(),'admin');

insert into Tiquetes (ti_asunto, ti_categoria, ti_us_id_asigna, ti_urgencia, ti_importancia, ti_estado, ti_fecha_adicion, ti_adicionado_por )
values ('Problemas de compu', 'Hardware',2,'Alta','Alta', 'A', getdate(),'admin');
