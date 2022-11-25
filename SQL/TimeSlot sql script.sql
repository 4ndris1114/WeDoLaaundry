use [CSC-CSD-S211_10407554]

drop TABLE if exists TimeSlots

CREATE table TimeSlots (
    [date] date not null,
	slot varchar(10) not null,
	slotCounter int not null,
);