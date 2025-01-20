-- Create the Database (if not already created)
CREATE DATABASE DashboardApp;
GO

-- Switch to the new database
USE DashboardApp;
GO

-- Create the Users table
CREATE TABLE Users (
    Id INT IDENTITY(1,1) PRIMARY KEY,      -- Auto-incrementing primary key
    Name NVARCHAR(100) NOT NULL,            -- User name
    Email NVARCHAR(100) NOT NULL UNIQUE,    -- User email, unique constraint
    CreatedAt DATETIME NOT NULL DEFAULT GETDATE() -- Timestamp when the user was created, defaults to current time
);
GO

-- Create the Activities table
CREATE TABLE Activities (
    Id INT IDENTITY(1,1) PRIMARY KEY,      -- Auto-incrementing primary key
    UserId INT NOT NULL,                    -- Foreign key to the Users table
    ActivityDescription NVARCHAR(255) NOT NULL, -- Description of the activity
    CreatedAt DATETIME NOT NULL DEFAULT GETDATE(), -- Timestamp when the activity was logged, defaults to current time
    FOREIGN KEY (UserId) REFERENCES Users(Id) ON DELETE CASCADE -- Foreign key constraint with cascading delete
);
GO
