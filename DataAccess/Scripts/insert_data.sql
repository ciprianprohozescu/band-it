use BandIt;

delete from Applications;
delete from Users;
delete from Skills;
delete from UserSkills;
delete from Bands;


insert into Users (Username, Email, Password, Salt, FirstName, LastName, Latitude, Longitude) 
values ('greg79', 'greglarrison@yahoo.com', 'greg', 'ihow5ihesto', 'Greg', 'Larrison', 56.982860, 9.638097);

insert into Users (Username, Email, Password, Salt, FirstName, LastName, Latitude, Longitude) 
values ('bjornthebear', 'bjornohara@yahoo.com', 'jiogwrhgoi', 'ihow5ihesto', 'Bjorn', 'O''Hara', 57.729286, 10.564563);

insert into Users (Username, Email, Password, Salt, FirstName, LastName, Latitude, Longitude) 
values ('hararabbit', 'delevigne.hara@gmail.com', 'jiogwrhgoi', 'ihow5ihesto', 'Hara', 'Delevigne', 56.890805, 9.813984);

insert into Users (Username, Email, Password, Salt, FirstName, LastName, Latitude, Longitude) 
values ('michaelcoffee', 'michael.iftikhar@gmail.com', 'jiogwrhgoi', 'ihow5ihesto', 'Michael', 'Iftikhar', 57.020911, 9.884767);

insert into Users (Username, Email, Password, Salt, FirstName, LastName, Latitude, Longitude) 
values ('nadeem', 'nadeem.andersen@gmail.com', 'jiogwrhgoi', 'ihow5ihesto', 'Nadeem', 'Holm Andersen', 57.020910, 9.884768);

insert into Users (Username, Email, Password, Salt, FirstName, LastName, Latitude, Longitude) 
values ('marthaaa', 'marthatimber@hotmail.com', 'jiogwrhgoi', 'ihow5ihesto', 'Martha', 'Timber', 51.508239, -0.148277);

insert into Users (Username, Email, Password, Salt, FirstName, LastName, Latitude, Longitude) 
values ('carafox', 'foxcara@gmail.co.uk', 'jiogwrhgoi', 'ihow5ihesto', 'Cara', 'Aubergine', 51.277240, 1.076924);

insert into Users (Username, Email, Password, Salt, FirstName, LastName, Latitude, Longitude) 
values ('jimmythekid', 'jeremiahstone@gmail.com', 'jiogwrhgoi', 'ihow5ihesto', 'Jeremiah', 'Stone', 52.360846, 4.894499);

insert into Users (Username, Email, Password, Salt, FirstName, LastName, Latitude, Longitude) 
values ('christianchristian', 'christianolssen@gmail.com', 'jiogwrhgoi', 'ihow5ihesto', 'Christian', 'Olssen', 55.664418, 12.606224);

insert into Users (Username, Email, Password, Salt, FirstName, LastName, Latitude, Longitude) 
values ('piapia', 'piaolssen@gmail.com', 'jiogwrhgoi', 'ihow5ihesto', 'Pia', 'Olssen', 55.580717, 12.926846);

insert into Skills (Name) 
values ('Guitarist');

insert into Skills (Name)
values ('Drummer');

insert into Skills (Name)
values ('Pianist');

insert into Skills (Name)
values ('Trumpeter');

insert into UserSkills (UserID, SkillID) 
values ((select ID from Users where Username = 'greg79'), (select ID from Skills where Name = 'Guitarist'));

insert into UserSkills (UserID, SkillID) 
values ((select ID from Users where Username = 'greg79'), (select ID from Skills where Name = 'Drummer'));

insert into UserSkills (UserID, SkillID) 
values ((select ID from Users where Username = 'michaelcoffee'), (select ID from Skills where Name = 'Guitarist'));

insert into UserSkills (UserID, SkillID) 
values ((select ID from Users where Username = 'marthaaa'), (select ID from Skills where Name = 'Pianist'));

insert into UserSkills (UserID, SkillID) 
values ((select ID from Users where Username = 'nadeem'), (select ID from Skills where Name = 'Trumpeter'));

insert into Bands (Name, Description, Latitude, Longitude, InviteMessage)
values ('The Mosquitos', 'Buzzing at night.', 56.890805, 9.813984, 'Come buzz with us.');

insert into Bands (Name, Description, Latitude, Longitude, InviteMessage)
values ('Steak eaters', 'No vegans allowed.', 57.020911, 9.884767, 'Hope you''re not vegan.');

insert into Bands (Name, Description, Latitude, Longitude, InviteMessage)
values ('Peace on Earth', 'World peace, baby!', 57.020911, 9.884767, 'Peace in?');

insert into Bands (Name, Description, Latitude, Longitude)
values('Poleyn', 'This is the description of Poleyn', 56.976405, 9.910906);

insert into Bands (Name, Description, Latitude, Longitude)
values('Dansk Rap', 'This is the description of Dansk Rap', 57.050832, 9.910391);

insert into Bands (Name, Description, Latitude, Longitude)
values('LaLaLa', 'This is the description of LaLaLa', 55.708916, 12.483776);


select * from Users;
select * from Skills;
select * from UserSkills;
select * from Bands;