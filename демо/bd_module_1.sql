create database gostinka;
use gostinka;

-- drop database gostinka;

create table users(
	IdUser int auto_increment primary key,
    Firstname varchar(50),
    Lastname varchar(50),
    Patronymic varchar(50),
    Username varchar(50),
    Password varchar(50),
	Role enum("Руководитель", "Администратор", 'Клиент', "Сотрудник", "Гость")
);

create table categories(
	IdCategory int auto_increment primary key,
    CategoryName varchar(100)
);

create table floors(
	IdFloor int auto_increment primary key,
    FloorName varchar(100)
);

create table rooms(
	IdRoom int auto_increment primary key,
    RoomNumber int,
    CategoryId int,
    FloorId int,
    Status enum("Занят", "Чистый", "Назначен к уборке", "Грязный"),
    foreign key (CategoryId) references categories(IdCategory),
    foreign key (FloorId) references floors(IdFloor)
);

create table payments(
	IdPayment int auto_increment primary key,
    ClientId int,
    CategoryId int,
    EntryDate date,
    DepartureDate date,
    Amount decimal(10, 2),
    Payed decimal(10,2),
    foreign key (ClientId) references users(IdUser),
    foreign key (CategoryId) references categories(IdCategory)
);

create table services(
	IdService int auto_increment primary key,
    ServiceName varchar (50),
    Description tinytext,
    Price decimal(10,2),
    InHour bool
);

create table payments_services(
	ServiceId int,
    PaymentId int,
    foreign key(ServiceId) references services(IdService),
    foreign key(PaymentId) references payments(IdPayment)
);

create table cleaning_schedule(
	IdCleaning int auto_increment primary key,
	CleaningDate date,
    FloorId int,
    CleanerId int,
    foreign key(FloorId ) references floors(IdFloor),
    foreign key(CleanerId) references users(IdUser)
);

