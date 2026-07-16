/*=============================================================
    DVLD Database
    Driving & Vehicle License Department

    Author : Eslam Elmasry
    Description:
    Database schema for the Driving & Vehicle License
    Department (DVLD) Management System.

=============================================================*/

--============================================================
-- Create Database
--============================================================

USE master;
CREATE DATABASE DVLD;

USE DVLD;

--============================================================
-- Table: People
--============================================================

CREATE TABLE People
(
    PersonID int NOT NULL,
    NationalNumber nvarchar(20) NULL,
    FirstName nvarchar(200),
    SecondName nvarchar(200),
    ThirdName nvarchar(200),
    LastName nvarchar(200),
    DataOfBirth date,
    Address nvarchar(400),
    PhoneNumber nvarchar(20),
    EmailAddress nvarchar(200),
    NationalityCountryID int,
    PersonalPhoto nvarchar(max)
);

ALTER TABLE peable
ADD PRIMARY KEY (PersonID);

EXEC sp_rename 'peable','people';

--============================================================
-- Table: Countries
--============================================================

CREATE TABLE Countries
(
    CountryID int NOT NULL,
    CountryName nvarchar(100),

    PRIMARY KEY (CountryID)

    --CONSTRAINT FK_NationalityCountry
    --FOREIGN KEY (CountryID)
    --REFERENCES People(NationalityCountryID)
);

ALTER TABLE People
ADD CONSTRAINT FK_NationalityCountry
FOREIGN KEY (NationalityCountryID)
REFERENCES Countries(CountryID);

--============================================================
-- Table: Applications
--============================================================

CREATE TABLE Applications
(
    ApplicationID int PRIMARY KEY,
    PersonId int,
    ApplicationType int,
    ApplicationDate date,
    ApplicationStatuse tinyint,
    CreateByUser int,
    LastStatusDate date
);

--============================================================
-- Table: ApplicationType
--============================================================

CREATE TABLE ApplicationType
(
    ApplicationTypeID int PRIMARY KEY,
    ApplicationTitle nvarchar(20),
    applicationFees smallmoney
);

--============================================================
-- Table: Users
--============================================================

CREATE TABLE Users
(
    UserID int PRIMARY KEY,
    PersonID int,
    UserName nvarchar(200),
    Password nvarchar(200),
    IsActive bit
);

--============================================================
-- Table: LocalDrivingLicenseApplication
--============================================================

CREATE TABLE LocalDrivingLicenseApplication
(
    LocalDrivingLicenseApplicationID int PRIMARY KEY,
    ApplicationID int,
    LisenceClassID int
);

--============================================================
-- Table: Tests
--============================================================

CREATE TABLE Tests
(
    TestID int PRIMARY KEY,
    TestAppointmentID int,
    TestResult bit,
    Notes nvarchar(max),
    createdByUser int
);

--============================================================
-- Table: TestAppointment
--============================================================

CREATE TABLE TestAppointment
(
    TestAppointmentID int PRIMARY KEY,
    LocalDrivingLicenseApplicationID int,
    TestTypeID int,
    PaidFees smallmoney,
    AppointmentDate date,
    CreateByUser int,
    isLocked bit
);

--============================================================
-- Table: TestTypes
--============================================================

CREATE TABLE TestTypes
(
    TestTypeID int PRIMARY KEY,
    TestName nvarchar(200),
    TestFees smallmoney,
    TestDescribtion nvarchar(max)
);
--============================================================
-- Table: LisenceClass
--============================================================

CREATE TABLE LisenceClass
(
    LisenceClassID int PRIMARY KEY,
    ClassName nvarchar(400),
    MinimumAllowedAge tinyint,
    validityLength tinyint,
    ClassFees smallmoney
);

--============================================================
-- Table: Lisences
--============================================================

CREATE TABLE Lisences
(
    LisenceID int PRIMARY KEY,

    ApplicationID int,

    DriverID int,

    LisenceClass int,

    IssueDate date,

    ExpiryDate date,

    Notes nvarchar(MAX),

    PaidFees smallmoney,

    IsActive bit,

    IssueReason nvarchar(200),

    CreatedByUser int
);

--============================================================
-- Table: DetainedLicenses
--============================================================

CREATE TABLE DetainedLicenses
(
    DetainID int PRIMARY KEY,

    LisencelD int,

    DetainDate date,

    FindFees smallmoney,

    CreatedByUser int,

    IsReleased bit,

    ReleaseDate date,

    ReleasedByUser int,

    ReleaseApplicationID int
);

--============================================================
-- Table: internationalLicenses
--============================================================

CREATE TABLE internationalLicenses
(
    InternationalLicenselD int PRIMARY KEY,

    ApplicationID int,

    LisencelD int,

    DriverID int,

    IssueDate date,

    ExpirationDate date,

    isActive bit,

    CreatedByUser int
);

--============================================================
-- Table: Drivers
--============================================================

CREATE TABLE Drivers
(
    DriverID int PRIMARY KEY,

    PersonID int,

    CreatedDate date,

    CreatedByUser int
);

--============================================================
-- Relationships
-- Applications
--============================================================

ALTER TABLE Applications
ADD CONSTRAINT FK_Applications_ApplicationType
FOREIGN KEY (ApplicationType)
REFERENCES ApplicationType(ApplicationTypeID),

CONSTRAINT FK_Applications_User
FOREIGN KEY (CreateByUser)
REFERENCES Users(UserID),

CONSTRAINT FK_Applications_People
FOREIGN KEY (PersonId)
REFERENCES People(PersonID);

--============================================================
-- Relationships
-- Users
--============================================================

ALTER TABLE Users
ADD CONSTRAINT FK_User_People
FOREIGN KEY (PersonID)
REFERENCES People(PersonID);

--============================================================
-- Relationships
-- Local Driving License Application
--============================================================

ALTER TABLE LocalDrivingLicenseApplication
ADD CONSTRAINT FK_LocalDrivingLicenseApplication_Applications
FOREIGN KEY (ApplicationID)
REFERENCES Applications(ApplicationID),

CONSTRAINT FK_LocalDrivingLicenseApplication_LisenceClass
FOREIGN KEY (LisenceClassID)
REFERENCES LisenceClass(LisenceClassID);
--============================================================
-- Relationships
-- Tests
--============================================================

ALTER TABLE Tests
ADD CONSTRAINT FK_Tests_TestAppointment
FOREIGN KEY (TestAppointmentID)
REFERENCES TestAppointment(TestAppointmentID),

CONSTRAINT FK_Tests_Users
FOREIGN KEY (createdByUser)
REFERENCES Users(UserID);

--============================================================
-- Relationships
-- Test Appointment
--============================================================

ALTER TABLE TestAppointment
ADD CONSTRAINT FK_TestAppointment_LocalDrivingLicenseApplication
FOREIGN KEY (LocalDrivingLicenseApplicationID)
REFERENCES LocalDrivingLicenseApplication(LocalDrivingLicenseApplicationID),

CONSTRAINT FK_TestAppointment_TestTypes
FOREIGN KEY (TestTypeID)
REFERENCES TestTypes(TestTypeID),

CONSTRAINT FK_TestAppointment_Users
FOREIGN KEY (CreateByUser)
REFERENCES Users(UserID);

--============================================================
-- Relationships
-- Drivers
--============================================================

ALTER TABLE Drivers
ADD CONSTRAINT FK_Drivers_People
FOREIGN KEY (PersonID)
REFERENCES People(PersonID),

CONSTRAINT FK_Drivers_Users
FOREIGN KEY (CreatedByUser)
REFERENCES Users(UserID);

--============================================================
-- Relationships
-- Lisences
--============================================================

ALTER TABLE Lisences
ADD CONSTRAINT FK_Lisences_Applications
FOREIGN KEY (ApplicationID)
REFERENCES Applications(ApplicationID),

CONSTRAINT FK_Lisences_Drivers
FOREIGN KEY (DriverID)
REFERENCES Drivers(DriverID),

CONSTRAINT FK_Lisences_LisenceClass
FOREIGN KEY (LisenceClass)
REFERENCES LisenceClass(LisenceClassID),

CONSTRAINT FK_Lisences_Users
FOREIGN KEY (CreatedByUser)
REFERENCES Users(UserID);

--============================================================
-- Relationships
-- Detained Licenses
--============================================================

ALTER TABLE DetainedLicenses
ADD CONSTRAINT FK_DetainedLicenses_Lisences
FOREIGN KEY (LisencelD)
REFERENCES Lisences(LisenceID),

CONSTRAINT FK_DetainedLicenses_Users1
FOREIGN KEY (CreatedByUser)
REFERENCES Users(UserID),

CONSTRAINT FK_DetainedLicenses_Users2
FOREIGN KEY (ReleasedByUser)
REFERENCES Users(UserID),

CONSTRAINT FK_DetainedLicenses_Applications
FOREIGN KEY (ReleaseApplicationID)
REFERENCES Applications(ApplicationID);

--============================================================
-- Relationships
-- International Licenses
--============================================================

ALTER TABLE internationalLicenses
ADD CONSTRAINT FK_InternationalLicenses_Applications
FOREIGN KEY (ApplicationID)
REFERENCES Applications(ApplicationID),

CONSTRAINT FK_InternationalLicenses_Lisences
FOREIGN KEY (LisencelD)
REFERENCES Lisences(LisenceID),

CONSTRAINT FK_InternationalLicenses_Drivers
FOREIGN KEY (DriverID)
REFERENCES Drivers(DriverID),

CONSTRAINT FK_InternationalLicenses_Users
FOREIGN KEY (CreatedByUser)
REFERENCES Users(UserID);

--============================================================
-- End of Database Schema
--============================================================
