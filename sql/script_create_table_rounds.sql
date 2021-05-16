USE [SoccerBet]
GO

/****** Object: Table [dbo].[Rounds] Script Date: 15/05/2021 23:54:54 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Rounds] (
    [Id]       UNIQUEIDENTIFIER NOT NULL,
    [LeagueId] UNIQUEIDENTIFIER NOT NULL,
    [Number]   INT              NOT NULL
);


GO
CREATE NONCLUSTERED INDEX [IX_Rounds_LeagueId]
    ON [dbo].[Rounds]([LeagueId] ASC);


GO
ALTER TABLE [dbo].[Rounds]
    ADD CONSTRAINT [PK_Rounds] PRIMARY KEY CLUSTERED ([Id] ASC);


GO
ALTER TABLE [dbo].[Rounds]
    ADD CONSTRAINT [FK_Rounds_League_LeagueId] FOREIGN KEY ([LeagueId]) REFERENCES [dbo].[League] ([Id]);


