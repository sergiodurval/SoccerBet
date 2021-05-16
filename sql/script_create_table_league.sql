USE [SoccerBet]
GO

/****** Object: Table [dbo].[League] Script Date: 15/05/2021 23:52:50 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[League] (
    [Id]      UNIQUEIDENTIFIER NOT NULL,
    [Country] VARCHAR (200)    NOT NULL,
    [Name]    VARCHAR (200)    NOT NULL
);


