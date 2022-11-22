use [CSC-CSD-S211_10407554]

DROP TABLE IF EXISTS Customer

CREATE TABLE Customer (
id int PRIMARY KEY IDENTITY (1000,1) NOT NULL,
fname varchar(50) NOT NULL,
lname varchar(50) NOT NULL,
phone varchar(20) NOT NULL,
postalCode int NOT NULL,
city varchar(100) NOT NULL,
[address] varchar(100) NOT NULL,
email nvarchar(256) NOT NULL,
userId nvarchar(450),
userType varchar(40) NOT NULL,

CONSTRAINT FK_CustomerUser 
FOREIGN KEY (userId) REFERENCES AspNetUsers(Id) ON DELETE SET NULL ON UPDATE CASCADE
)