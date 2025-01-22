create database gostinka;
use gostinka;

create table passports(
	IdPassport int auto_increment primary key,
    Serial varchar(4),
    Number varchar(6),
    Address varchar(100),
    WhoIssue varchar(100),
    IssueDate date
);

create table statuses(
	IdStatus int auto_increment primary key,
    StatusName varchar(30)
);

create table users(
	IdUser int auto_increment primary key,
    Firstname varchar(50),
    Lastname varchar(50),
    Patronymic varchar(50),
    DateOfBirth date,
    PassportId int, foreign key(PassportId) references passports(IdPassport),
    Phone varchar(15),
    Email varchar(256),
    Username varchar(50),
    Password varchar(50),
	Role enum("Руководитель", "Администратор", 'Клиент', "Сотрудник", "Гость")
);

create table categories(
	IdCategory int auto_increment primary key,
    CategoryName varchar(100),
    PricePerDay decimal(10,2),
    Description tinytext
);

create table rooms(
	IdRoom int auto_increment primary key,
    RoomNumber int,
    CategoryId int, foreign key (CategoryId) references categories(IdCategory),
    Floor int,
    StatusId int, foreign key(StatusId) references statuses(IdStatus)
);

create table services(
	IdService int auto_increment primary key,
    ServiceName varchar(50),
    Description tinytext,
    Price decimal(10,2),
    InHour bool
);

create table bookings(
	IdBooking int auto_increment primary key,
    RoomId int, foreign key(RoomId) references rooms(IdRoom),
    ClientId int, foreign key(ClientId) references users(IdUser),
    ArrivalDate date,
    DepartureDate date,
    Amount decimal(10,2)
);

create table bookings_services(
	ServiceId int primary key, foreign key(ServiceId) references services(IdService),
    BookingId int primary key, foreign key(BookingId) references bookings(IdBooking)
);

create table cleaning_schedule(
	IdCleaning int auto_increment primary key,
	CleaningDate date,
    Floor int,
    CleanerId int, foreign key(CleanerId) references users(IdUser)
);