create database Tem

use tem

create table Users(
UserId int IDENTITY NOT NULL PRIMARY KEY,
UserFullName varchar(255),
UserEmail varchar(255),
UserPhone int,
UserBirthday datetime,
UserGender nvarchar(10),
UserPassword varchar(255),
UserCreatedAt datetime
)

