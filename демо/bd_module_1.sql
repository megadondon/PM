create database gostinka;
use gostinka;

create table statuses(
	IdStatus int auto_increment primary key,
    StatusName varchar(30)
);

create table categories(
	IdCategory int auto_increment primary key,
    CategoryName varchar(100),
    PricePerDay decimal(10,2),
    Description tinytext
);

create table users(
	IdUser int auto_increment primary key,
    Firstname varchar(50),
    Lastname varchar(50),
    Patronymic varchar(50),
	Role enum("Руководитель", "Администратор", 'Клиент', "Сотрудник", "Гость")
);

create table rooms(
	IdRoom int auto_increment primary key,
    RoomNumber int,
    CategoryId int, foreign key (CategoryId) references categories(IdCategory),
    Floor int
);

create table rooms_statuses(
	IdRoomStatus int primary key auto_increment,
	RoomId int, foreign key(RoomId) references rooms(IdRoom),
    StatusId int, foreign key(StatusId) references statuses(IdStatus),
    StatusDate date
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
	IdBookingService int auto_increment primary key,
	ServiceId int, foreign key(ServiceId) references services(IdService),
    BookingId int, foreign key(BookingId) references bookings(IdBooking),
    CountHour int default(1)
);

create table cleaning_schedule(
	IdCleaning int auto_increment primary key,
	CleaningDate date,
    CleanerId int, foreign key(CleanerId) references users(IdUser),
    Floor int
);