use [CSC-CSD-S211_10407554]

DROP TABLE IF EXISTS Drivers

CREATE TABLE Drivers (
id int PRIMARY KEY IDENTITY (1000,1) NOT NULL,
fname varchar(50) NOT NULL,
lname varchar(50) NOT NULL,
phone varchar(20) NOT NULL,
postalCode int NOT NULL,
city varchar(100) NOT NULL,
[address] varchar(100) NOT NULL,
email nvarchar(256) NOT NULL,
salary int NOT NULL,
)