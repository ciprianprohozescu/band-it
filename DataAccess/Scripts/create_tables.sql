use BandIt;

drop table if exists Invites;
drop table if exists Notifications;
drop table if exists NotificationTypes;
drop table if exists UserSkills;
drop table if exists BandUserSkills;
drop table if exists Skills;

drop table if exists BandGenres;
drop table if exists Genres;
drop table if exists Applications;

drop table if exists Blocks;
drop table if exists UserMessages;
drop table if exists BandMessages;
drop table if exists Messages;
drop table if exists UserFiles;


drop table if exists BandUsers;
drop table if exists Files;
drop table if exists Bands;
drop table if exists Permissions;
drop table if exists Users;



create table Users (
	ID int identity(1,1) primary key,
	Username varchar(225) NOT NULL UNIQUE,
	Email varchar(225) NOT NULL UNIQUE,
	Password varchar(225) NOT NULL,
	Salt varchar(225) NOT NULL,
	FirstName varchar(225),
	LastName varchar(225),
	Description text,
	Longitude decimal(9,6),
	Latitude decimal(9,6),
	ProfilePicture varchar(225),
	Deleted datetime
);

create table Bands (
	ID int identity(1,1) primary key,
	Name varchar(225) NOT NULL UNIQUE,
	Description varchar(225), 
	Latitude decimal(9,6),
	Longitude decimal(9,6),
	ProfilePicture varchar(225),
	InviteMessage text,
	RowVersion timestamp,
	Deleted datetime
);

create table Permissions (
	ID int identity(1,1) primary key,
	UserID int foreign key references Users(ID),
	Name varchar(225) NOT NULL,
	Description varchar(225),
	Deleted datetime
);

create table BandUsers (
     ID int identity(1,1) primary key,
	 PermisionID int foreign key references Permissions(ID),
	 UserID int foreign key references Users(ID),
	 BandID int foreign key references Bands(ID),
);

create table Files (
	ID int identity(1,1) primary key,
	Name varchar(225) NOT NULL,
	Deleted datetime,
);

create table Blocks (
	ID int identity (1,1) primary key,
	BlockerID int foreign key references Users(ID) NOT NULL,
	ReceiverID int foreign key references Users(ID) NOT NULL,
	Blocked datetime NOT NULL,
	Deleted datetime
);

create table Messages (
	ID int identity (1,1) primary key,
	SenderID int foreign key references Users(ID) NOT NULL,
	Sent datetime NOT NULL,
	Deleted datetime
);

create table UserMessages (
	UserID int foreign key references Users(ID),
	MessageID int foreign key references Messages(ID),
	primary key (UserID, MessageID)
);

create table BandMessages (
	BandID int foreign key references Bands(ID),
	MessageID int foreign key references Messages(ID),
	primary key (BandID, MessageID)
);

create table UserFiles (
	UserID int foreign key references Users(ID),
	FileID int foreign key references Files(ID),
	primary key (UserID, FileID)
);

create table Genres (
	ID int identity(1,1) primary key,
	Name varchar(225) NOT NULL, 
	Deleted datetime
);

create table BandGenres(
	BandID int foreign key references Bands(ID),
	GenreID int foreign key references Genres(ID),
	primary key (BandID, GenreID)
);


create table Applications (
	ID int identity(1,1) primary key,
	BandID int foreign key references Bands(ID) NOT NULL,
	UserID int foreign key references Users(ID) NOT NULL,
	Sent datetime NOT NULL,
	Message text NOT NULL,
	Result bit,
	Deleted datetime
);

create table Invites (
	ID int identity(1, 1) primary key,
	BandID int foreign key references Bands(ID) NOT NULL,
	UserID int foreign key references Users(ID) NOT NULL,
	Sent datetime NOT NULL,
	Result bit,
	Deleted datetime
);

create table NotificationTypes (
	ID int identity(1, 1) primary key,
	Name varchar(255) NOT NULL,
	Message text NOT NULL,
	Deleted datetime
);

create table Notifications (
	ID int identity(1, 1) primary key,
	BandID int foreign key references Bands(ID),
	UserID int foreign key references Users(ID),
	TypeID int foreign key references NotificationTypes(ID) NOT NULL,
	Sent datetime NOT NULL,
	Status bit NOT NULL,
	Deleted datetime
);

create table Skills (
	ID int identity(1, 1) primary key,
	Name varchar(255) NOT NULL,
	Deleted datetime
);

create table UserSkills (
	UserID int foreign key references Users(ID) on delete cascade,
	SkillID int foreign key references Skills(ID) on delete cascade,
	primary key (UserID, SkillID)
);

create table BandUserSkills (
	BandUserID int foreign key references BandUsers(ID),
	SkillID int foreign key references Skills(ID),
	primary key (BandUserID, SkillID)
);
