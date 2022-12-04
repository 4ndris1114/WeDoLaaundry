use [CSC-CSD-S211_10407554]

DROP TABLE IF EXISTS Bookings

CREATE TABLE Bookings (

id int PRIMARY KEY IDENTITY (1000,1) NOT NULL,
customerId int,
driverId int,
pickUpTimeId int,
returnTimeId int,
pickUpAddress varchar(100) NOT NULL,
returnAddress varchar(100) NOT NULL,
[status] varchar(40) NOT NULL,
noOfBags int NOT NULL,
invoiceId int NOT NULL,

CONSTRAINT Fk_CustomerBooking
FOREIGN KEY (customerId) REFERENCES Customers(id) ON DELETE SET NULL ON UPDATE CASCADE,

CONSTRAINT Fk_BookingPickUpTime
FOREIGN KEY (pickUpTimeId) REFERENCES TimeSlots(id) on delete set null,

CONSTRAINT Fk_BookingReturnTime
FOREIGN KEY (returnTimeId) REFERENCES TimeSlots(id)
)