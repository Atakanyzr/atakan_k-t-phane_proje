create database library


create table loginTable(
id int not null IDENTITY(1,1) primary key,
username varchar(50) not null,
pass varchar(50) not null
)

insert into loginTable (username,pass) values ('atakan','123');

select * from loginTable