USE master;
GO 

DROP DATABASE DnDEZDB;

CREATE DATABASE DnDEZDB;
GO

USE DnDEZDB;
GO

CREATE TABLE Images(
	ImageId INT IDENTITY(101, 1),
	ImagePath NVARCHAR(1000) NOT NULL,

	--Constraints
	--Primary Key
	CONSTRAINT images_imageid_pk PRIMARY KEY (ImageId)
);

CREATE TABLE [Users](
	UserId INT IDENTITY(101, 1),
	[FirstName] NVARCHAR(40) NOT NULL,
	[LastName] NVARCHAR(40) NOT NULL,
	UserName NVARCHAR(40) NOT NULL,
	[Password] NVARCHAR(30) NOT NULL,
	ImageId INT,
	Active BIT NOT NULL DEFAULT '1',

	--Constraints
	--PrimaryKey
	CONSTRAINT users_userid_pk PRIMARY KEY (UserId),

	--Foreign Key
	CONSTRAINT users_imageid_fk FOREIGN KEY (ImageId) REFERENCES Images(ImageId)
);


CREATE TABLE [Character](
	CharacterId INT IDENTITY(101, 1),
	UserId INT,
	[Name] NVARCHAR(100) NOT NULL,
	Race NVARCHAR(10) NOT NULL,
	Class NVARCHAR(9) NOT NULL,
	[Level] INT NOT NULL,
	ImageId INT,
	Active BIT NOT NULL DEFAULT '1',

	--Constraints
	--Primary Key
	CONSTRAINT character_characterid_pk PRIMARY KEY (CharacterId),

	--Foreign Key
	CONSTRAINT character_userid_fk FOREIGN KEY (UserId) REFERENCES [Users](UserId),
	CONSTRAINT character_imageid_fk FOREIGN KEY (ImageId) REFERENCES Images(ImageId),

	--Check
	CONSTRAINT character_race_ck CHECK (Race in ('dragonborn', 'dwarf', 'elf', 'gnome', 'half-elf', 'half-orc', 'halfling', 'human', 'tiefling')),
	CONSTRAINT character_class_ck CHECK (Class in ('barbarian', 'bard', 'cleric', 'druid', 'fighter', 'monk', 'paladin', 'ranger', 'rogue', 'sorcerer', 'warlock', 'wizard')),
	CONSTRAINT character_level_ck CHECK ([Level] >= 1 AND [Level] <= 20)
);

CREATE TABLE Ability_Scores(
	[Index] NVARCHAR(3) NOT NULL,
	[Name] NVARCHAR(3) NOT NULL,

	--Constraints
	CONSTRAINT abilityscores_index_pk PRIMARY KEY ([Index]),

	--Check
	CONSTRAINT abilityscores_index_ck CHECK ([Index] in ('str', 'dex', 'con', 'int', 'wis', 'cha')),
	CONSTRAINT abilityscores_name_ck CHECK ([Name] in ('Str', 'Dex', 'Con', 'Int', 'Wis', 'Cha'))
);

CREATE TABLE Char_Ability_Scores(
	CharacterId INT,
	[Index] NVARCHAR(3),
	[Value] INT NOT NULL,
	RacialBonus BIT DEFAULT '0' NOT NULL,

	--Constraints
	--Primary Key
	CONSTRAINT charabilityscores_characterabilityid_pk PRIMARY KEY (CharacterId, [Index]),

	--Foreign Key
	CONSTRAINT charabilityscores_characterid_fk FOREIGN KEY (CharacterId) REFERENCES [Character](CharacterId),
	CONSTRAINT charabilityscores_index_fk FOREIGN KEY ([Index]) REFERENCES Ability_Scores([Index]),

	--Check
	CONSTRAINT charabilityscores_value_ck CHECK ([Value] >= 1 AND [Value] <= 20)
);

INSERT INTO Images 
	(ImagePath)
VALUES
	('Images\Users\defaultProfilePic.png'),
	('Images\Characters\defaultCharacterImage.png')

INSERT INTO Ability_Scores
	([Index], [Name])
VALUES	
	('str', 'Str'),
	('dex', 'Dex'),
	('con', 'Con'),
	('int', 'Int'),
	('wis', 'Wis'),
	('cha', 'Cha')


--Test Data
INSERT INTO [Users]
		(FirstName, LastName, UserName, [Password])
VALUES
	('Eli', 'Reid', 'hajile7', '12345'),
	('Dominic', 'Nutaitis', 'PapaDomm', '12345'),
	('Ethan', 'Thomas', 'EthanChrist', '12345')



INSERT INTO [Character]
	(UserId, [Name], Race, Class, [Level])
VALUES
	(101, 'Kreiger', 'gnome', 'wizard', 5),
	(102, 'Roland', 'half-orc', 'barbarian', 1),
	(103, 'EthanChrist', 'dragonborn', 'paladin', 20)

INSERT INTO Char_Ability_Scores
	(CharacterId, [Index], [Value])
VALUES
	(101, 'str', 10),
	(101, 'dex', 10),
	(101, 'con', 8),
	(101, 'int', 18),
	(101, 'wis', 8),
	(101, 'cha', 16),

	(102, 'str', 4),
	(102, 'dex', 4),
	(102, 'con', 4),
	(102, 'int', 8),
	(102, 'wis', 2),
	(102, 'cha', 20)


select * from Users

select * from Images

select * from [Character]