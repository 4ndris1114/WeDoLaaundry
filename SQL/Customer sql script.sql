use [CSC-CSD-S211_10407554]

DROP TABLE IF EXISTS Customer

CREATE TABLE Customer (
id int PRIMARY KEY IDENTITY (1000,1) NOT NULL,
fname varchar(40) NOT NULL,
lname varchar(40) NOT NULL,
phone varchar(15) NOT NULL,
postalCode int NOT NULL,
city varchar(100) NOT NULL,
[address] varchar(100) NOT NULL,
email varchar(50) NOT NULL,
password_hash varchar(100) NOT NULL,
userType varchar(40) NOT NULL
)