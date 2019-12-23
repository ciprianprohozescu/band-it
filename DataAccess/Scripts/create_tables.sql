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
	ID int auto_increment,
	Username varchar(225) NOT NULL,
	Email varchar(225) NOT NULL,
	Password varchar(225) NOT NULL,
	Salt varchar(225) NOT NULL,
	FirstName varchar(225),
	LastName varchar(225),
	Description longtext,
	Longitude decimal(9,6),
	Latitude decimal(9,6),
	ProfilePicture varchar(225),
	Deleted datetime(3),
    
    primary key (ID)
);

create table Bands (
	ID int auto_increment,
	Name varchar(225) NOT NULL UNIQUE,
	Description varchar(225), 
	Latitude decimal(9,6),
	Longitude decimal(9,6),
	ProfilePicture varchar(225),
	InviteMessage longtext,
	RowVersion binary(8),
	Deleted datetime(3),
    
    primary key (ID)
);

create table Permissions (
	ID int auto_increment,
	UserID int,
	Name varchar(225) NOT NULL,
	Description varchar(225),
	Deleted datetime,
    
    primary key (ID),
    foreign key (UserID) references Users(ID)
);

create table BandUsers (
     ID int auto_increment,
	 PermisionID int,
	 UserID int,
	 BandID int,
     
     primary key (ID),
     foreign key (PermisionID) references Permissions(ID),
     foreign key (UserID) references Users(ID),
     foreign key (BandID) references Bands(ID)
);

create table Files (
	ID int,
	Name varchar(225) NOT NULL,
	Deleted datetime(3),
    
    primary key (ID)
);

create table Blocks (
	ID int auto_increment,
	BlockerID int NOT NULL,
	ReceiverID int NOT NULL,
	Blocked datetime NOT NULL,
	Deleted datetime,
    
    primary key (ID),
    foreign key (BlockerID) references Users(ID),
    foreign key (ReceiverID) references Users(ID)
);

create table Messages (
	ID int auto_increment,
	SenderID int NOT NULL,
	Sent datetime NOT NULL,
	Deleted datetime,
    
    primary key (ID),
    foreign key (SenderID) references Users(ID)
);

create table UserMessages (
	UserID int,
	MessageID int,
    
	primary key (UserID, MessageID),
    foreign key (UserID) references Users(ID),
    foreign key (MessageID) references Messages(ID)
);

create table BandMessages (
	BandID int,
	MessageID int,
    
	primary key (BandID, MessageID),
    foreign key (BandID) references Bands(ID),
    foreign key (MessageID) references Messages(ID)
);

create table UserFiles (
	UserID int,
	FileID int,
    
	primary key (UserID, FileID),
    foreign key (UserID) references Users(ID) on delete cascade,
    foreign key (FileID) references Files(ID) on delete cascade
);

create table Genres (
	ID int auto_increment,
	Name varchar(225) NOT NULL, 
	Deleted datetime(3),
    
    primary key (ID)
);

create table BandGenres(
	BandID int,
	GenreID int,
    
	primary key (BandID, GenreID),
    foreign key (BandID) references Bands(ID),
    foreign key (GenreID) references Genres(ID)
);


create table Applications (
	ID int auto_increment,
	BandID int NOT NULL,
	UserID int NOT NULL,
	Sent datetime NOT NULL,
	Message text NOT NULL,
	Result bit,
	Deleted datetime,
    
    primary key (ID),
    foreign key (BandID) references Bands(ID),
    foreign key (UserID) references Users(ID)
);

create table Invites (
	ID int auto_increment,
	BandID int NOT NULL,
	UserID int NOT NULL,
	Sent datetime NOT NULL,
	Result bit,
	Deleted datetime,
    
    primary key (ID),
    foreign key (BandID) references Bands(ID),
    foreign key (UserID) references Users(ID)
);

create table NotificationTypes (
	ID int auto_increment,
	Name varchar(255) NOT NULL,
	Message longtext NOT NULL,
	Deleted datetime(3),
    
    primary key (ID)
);

create table Notifications (
	ID int auto_increment,
	BandID int,
	UserID int,
	TypeID int NOT NULL,
	Sent datetime NOT NULL,
	Status bit NOT NULL,
	Deleted datetime,
    
    primary key (ID),
    foreign key (BandID) references Bands(ID),
    foreign key (UserID) references Users(ID),
    foreign key (TypeID) references NotificationTypes(ID)
);

create table Skills (
	ID int auto_increment,
	Name varchar(255) NOT NULL,
	Deleted datetime(3),
    
    primary key (ID)
);

create table UserSkills (
	UserID int,
	SkillID int,
    
	primary key (UserID, SkillID),
    foreign key (UserID) references Users(ID) on delete cascade,
    foreign key (SkillID) references Skills(ID) on delete cascade
);

create table BandUserSkills (
	BandUserID int,
	SkillID int,
    
	primary key (BandUserID, SkillID),
    foreign key (BandUserID) references BandUsers(ID),
    foreign key (SkillID) references Skills(ID)
);
