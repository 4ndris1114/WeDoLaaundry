use [CSC-CSD-S211_10407554]

DROP TABLE IF EXISTS Bookings

CREATE TABLE Bookings (

id int PRIMARY KEY IDENTITY (1000,1) NOT NULL,
customerId int,
driverId int,
pickUpTime datetime NOT NULL,
returnTime datetime NOT NULL,
pickUpAddress varchar(100) NOT NULL,
returnAddress varchar(100) NOT NULL,
[status] varchar(40) NOT NULL,
noOfBags int NOT NULL,
invoiceId int NOT NULL,

CONSTRAINT Fk_CustomerBooking
FOREIGN KEY (customerId) REFERENCES Customer(id) ON DELETE SET NULL ON UPDATE CASCADE
)