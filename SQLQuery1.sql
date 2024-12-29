
-- Users Table
CREATE TABLE [dbo].[Users] (
    [Id] INT IDENTITY(1,1) PRIMARY KEY,
    [Name] NVARCHAR(250) NOT NULL,
    [Email] NVARCHAR(100) NOT NULL,
    [Password] NVARCHAR(255) NOT NULL,
    [PhoneNumber] NVARCHAR(50) NULL,
    [Role] NVARCHAR(50) DEFAULT 'User',
    [LoyaltyPoints] INT DEFAULT 0,
    [RegistrationDate] DATETIME NOT NULL DEFAULT GETDATE(),
    CONSTRAINT UQ_Users_Email UNIQUE ([Email])
);

-- Tables Table
CREATE TABLE [dbo].[Tables] (
    [Id] INT IDENTITY(1,1) PRIMARY KEY,
    [Number] INT NOT NULL,
    [Capacity] INT NOT NULL,
    [IsAvailable] BIT DEFAULT 1 NOT NULL,
    [Location] NVARCHAR(50) NOT NULL
);

-- Reservations Table
CREATE TABLE [dbo].[Reservations] (
    [Id] INT IDENTITY(1,1) PRIMARY KEY,
    [UserId] INT NOT NULL,
    [TableId] INT NOT NULL,
    [ReservationDate] DATE NOT NULL,
    [ReservationTime] TIME NOT NULL,
    [Duration] TIME NOT NULL,
    [NumberOfGuests] INT NOT NULL,
    [Status] NVARCHAR(50) DEFAULT 'Pending' NOT NULL,
    [SpecialRequests] NVARCHAR(500) NULL,
    [ContactPhone] NVARCHAR(50) NOT NULL,
    CONSTRAINT FK_Reservations_Users FOREIGN KEY ([UserId]) 
        REFERENCES [dbo].[Users] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT FK_Reservations_Tables FOREIGN KEY ([TableId]) 
        REFERENCES [dbo].[Tables] ([Id]) ON DELETE NO ACTION
);

-- Adăugăm datele inițiale pentru mese
SET IDENTITY_INSERT [dbo].[Tables] ON;
INSERT INTO [dbo].[Tables] ([Id], [Number], [Capacity], [Location], [IsAvailable]) VALUES
(1, 1, 2, 'Interior', 1),
(2, 2, 4, 'Interior', 1),
(3, 3, 6, 'Interior', 1),
(4, 4, 4, 'Terrace', 1),
(5, 5, 6, 'Terrace', 1),
(6, 6, 8, 'VIP', 1);
SET IDENTITY_INSERT [dbo].[Tables] OFF;