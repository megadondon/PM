create database gostinka;
use gostinka;

-- drop database gostinka;

create table passports(
	IdPassport int auto_increment primary key,
    SerialPassport varchar(4),
    NumberPassport varchar(6),
	PlaceOfBirth tinytext,
    DateOfBirth date,
    DateOfIssue date,
    WhoIssued varchar(100)
);

create table users(
	IdUser int auto_increment primary key,
    Firstname varchar(50),
    Lastname varchar(50),
    Patronymic varchar(50),
    PhoneNumber varchar(50),
    Email varchar(256),
    PassportId int,
    foreign key (PassportId) references passports(IdPassport),
    Username varchar(50),
    Password varchar(50),
	Role enum("Руководитель", "Администратор", 'Клиент', "Сотрудник", "Гость")
);

create table categories(
	IdCategory int auto_increment primary key,
    CategoryName varchar(100)
);

create table rooms(
	IdRoom int auto_increment primary key,
    RoomNumber int,
    CategoryId int,
    Floor int,
    Status enum("Занят", "Чистый", "Назначен к уборке", "Грязный"),
    foreign key (CategoryId) references categories(IdCategory)
);

create table bookings(
	IdBooking int auto_increment primary key,
    ClientId int,
    RoomId int,
    EntryDate date,
    DepartureDate date,
    Amount decimal(10, 2),
    Payed decimal(10,2),
    foreign key (RoomId) references rooms (IdRoom),
    foreign key (ClientId) references users(IdUser)
);

create table services(
	IdService int auto_increment primary key,
    ServiceName varchar (50),
    Description tinytext,
    Price decimal(10,2),
    InHour bool
);

create table bookings_services(
	ServiceId int,
    BookingId int,
    foreign key(ServiceId) references services(IdService),
    foreign key(BookingId) references bookings(IdBooking)
);

create table cleaning_schedules(
	IdCleaning int auto_increment primary key,
	CleaningDate date,
    Floor int,
    CleanerId int,
    foreign key(CleanerId) references users(IdUser)
);
