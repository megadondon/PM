# Генерация базы данных для ИС

CREATE DATABASE IF NOT EXISTS `AutoRegDB`;
USE `AutoRegDB`;
# drop database `autoregdb`;

# Генерация таблиц

CREATE TABLE Users (
    UserId INT(4) PRIMARY KEY AUTO_INCREMENT,
    Login VARCHAR(45) UNIQUE NOT NULL,
    Password VARCHAR(255) NOT NULL, -- Увеличена длина поля Password
    Role INT(4),
    Name VARCHAR(45),
    FirstName VARCHAR(45),
    PassportSerial VARCHAR(4),
    PassportNumber VARCHAR(6),
    Email VARCHAR(255) UNIQUE,
    PhoneNumber VARCHAR(20) NOT NULL
);

CREATE TABLE Applications (
    IdApplication INT(4) PRIMARY KEY AUTO_INCREMENT,
    VehiclePassportId INT(4),
    StatusId INT(4),
    SubmissionDate DATETIME NOT NULL,
    ScheduleDate DATETIME NULL,
    DepartamentId INT(4),
    ArchiveDocuments TINYTEXT NULL,
    ProcessingOfficer INT(4) NULL,       -- Сотрудник, обработавший заявку (может быть NULL)
    ClosingDate DATETIME NULL,       -- Дата закрытия заявки (может быть NULL)
    ProcessingResult VARCHAR(255) NULL -- Результат обработки (комментарий) (может быть NULL)
);

CREATE TABLE VehiclePassport (
    IdVehiclePassport INT(4) PRIMARY KEY AUTO_INCREMENT,
    VehicleId INT(4),
    UserId INT(4), --  Владелец ПТС
    RegistrationNumber VARCHAR(20),
    EngineNumber VARCHAR(45),
    BodyNumber VARCHAR(45),
    IssueDate DATE
);

CREATE TABLE Vehicles (
    IdVehicle INT(4) PRIMARY KEY AUTO_INCREMENT,
    VIN VARCHAR(17) UNIQUE NOT NULL,
    ModelId INT(4),
    Year YEAR,
    ColorId INT(4),
    VehicleTypeId INT(4)
);

CREATE TABLE Departaments (
    IdDepartament INT(4) PRIMARY KEY AUTO_INCREMENT,
    DepartmentName VARCHAR(45),
    AddressId INT(4),
    WorkFrom TIME,
    WorkTo TIME,
    HeadOfDepartament INT(4),
    PhoneNumber VARCHAR(20)
);

CREATE TABLE Makes (
    IdMake INT(4) PRIMARY KEY AUTO_INCREMENT,
    Make VARCHAR(45)
);

CREATE TABLE Model (
    IdModel INT(4) PRIMARY KEY AUTO_INCREMENT,
    MakeId INT(4),
    Model VARCHAR(45)
);

CREATE TABLE Addresses (
    IdAddress INT(4) PRIMARY KEY AUTO_INCREMENT,
    RegionId INT(4),
    CityId INT(4),
    DistrictId INT(4),
    Street VARCHAR(45),
    Build VARCHAR(10)
);

CREATE TABLE Regions (
    IdRegion INT(4) PRIMARY KEY AUTO_INCREMENT,
    Region VARCHAR(45)
);

CREATE TABLE Cities (
    IdCity INT(4) PRIMARY KEY AUTO_INCREMENT,
    City VARCHAR(45)
);

CREATE TABLE Districts (
    IdDistrict INT(4) PRIMARY KEY AUTO_INCREMENT,
    District VARCHAR(45)
);

CREATE TABLE Statuses (
    IdStatus INT(4) PRIMARY KEY AUTO_INCREMENT,
    Status VARCHAR(45)
);

CREATE TABLE VehicleTypes (
    idType INT(4) PRIMARY KEY AUTO_INCREMENT,
    Type VARCHAR(45)
);

CREATE TABLE Colors (
    IdColor INT(4) PRIMARY KEY AUTO_INCREMENT,
    Color VARCHAR(45)
);

CREATE TABLE Posts (
    IdPost INT(4) PRIMARY KEY AUTO_INCREMENT,
    Post VARCHAR(45)
);

CREATE TABLE Roles(
    IdRole INT(4) PRIMARY KEY AUTO_INCREMENT,
    Role VARCHAR(45)
);

CREATE TABLE Officers(
    IdOfficer INT(4) PRIMARY KEY AUTO_INCREMENT,
    UserId INT(4) UNIQUE,
    DepartmentId INT(4),  -- Связь с отделением
    PostId INT(4),  -- Связь с должностью
    WorkPhoneNumber VARCHAR(20)
);

# Добавление внешних ключей
ALTER TABLE Users ADD CONSTRAINT FK_Users_Roles FOREIGN KEY (RoleId) REFERENCES Roles(IdRole);

ALTER TABLE Applications 
ADD CONSTRAINT FK_Applications_VehiclePassport FOREIGN KEY (VehiclePassportId) REFERENCES VehiclePassport(IdVehiclePassport),
ADD CONSTRAINT FK_Applications_Statuses FOREIGN KEY (StatusId) REFERENCES Statuses(IdStatus),
ADD CONSTRAINT FK_Applications_Departaments FOREIGN KEY (DepartamentId) REFERENCES Departaments(IdDepartament);

ALTER TABLE VehiclePassport ADD CONSTRAINT FK_VehiclePassport_Vehicles FOREIGN KEY (VehicleId) REFERENCES Vehicles(IdVehicle);
ALTER TABLE VehiclePassport ADD CONSTRAINT FK_VehiclePassport_Users FOREIGN KEY (UserId) REFERENCES Users(UserId);

ALTER TABLE Vehicles ADD CONSTRAINT FK_Vehicles_Model FOREIGN KEY (ModelId) REFERENCES Model(IdModel);
ALTER TABLE Vehicles ADD CONSTRAINT FK_Vehicles_Color FOREIGN KEY (ColorId) REFERENCES Colors(IdColor);
ALTER TABLE Vehicles ADD CONSTRAINT FK_Vehicles_VehicleTypes FOREIGN KEY (VehicleTypeId) REFERENCES VehicleTypes(idType);

ALTER TABLE Departaments ADD CONSTRAINT FK_Departaments_Addresses FOREIGN KEY (AddressId) REFERENCES Addresses(IdAddress);

ALTER TABLE Model ADD CONSTRAINT FK_Model_Makes FOREIGN KEY (MakeId) REFERENCES Makes(IdMake);

ALTER TABLE Addresses ADD CONSTRAINT FK_Addresses_Regions FOREIGN KEY (RegionId) REFERENCES Regions(IdRegion);
ALTER TABLE Addresses ADD CONSTRAINT FK_Addresses_Cities FOREIGN KEY (CityId) REFERENCES Cities(IdCity);
ALTER TABLE Addresses ADD CONSTRAINT FK_Addresses_Districts FOREIGN KEY (DistrictId) REFERENCES Districts(IdDistrict);

ALTER TABLE Officers ADD CONSTRAINT FK_Officers_Users FOREIGN KEY (UserId) REFERENCES Users(UserId);
ALTER TABLE Officers ADD CONSTRAINT FK_Officers_Departments FOREIGN KEY (DepartmentId) REFERENCES Departaments(IdDepartament);
ALTER TABLE Officers ADD CONSTRAINT FK_Officers_Posts FOREIGN KEY (PostId) REFERENCES Posts(IdPost);
