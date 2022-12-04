use [CSC-CSD-S211_10407554]

drop TABLE if exists TimeSlots

CREATE table TimeSlots (
	id int PRIMARY KEY IDENTITY(1000,1) NOT NULL,
    [date] date not null,
	slot varchar(10) not null,
	availability int not null,
);

INSERT INTO TimeSlots([date], slot, [availability]) VALUES ('2023-01-01', '12-15', 1000);
INSERT INTO TimeSlots([date], slot, [availability]) VALUES ('2023-01-01', '15-18', 10);
INSERT INTO TimeSlots([date], slot, [availability]) VALUES ('2023-01-01', '18-21', 10);
INSERT INTO TimeSlots([date], slot, [availability]) VALUES ('2023-01-02', '12-15', 1000);
INSERT INTO TimeSlots([date], slot, [availability]) VALUES ('2023-01-02', '15-18', 10);
INSERT INTO TimeSlots([date], slot, [availability]) VALUES ('2023-01-02', '18-21', 10);
INSERT INTO TimeSlots([date], slot, [availability]) VALUES ('2023-01-03', '12-15', 1000);
INSERT INTO TimeSlots([date], slot, [availability]) VALUES ('2023-01-03', '15-18', 10);
INSERT INTO TimeSlots([date], slot, [availability]) VALUES ('2023-01-03', '18-21', 10);