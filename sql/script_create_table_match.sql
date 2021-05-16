USE [SoccerBet]
GO

/****** Object: Table [dbo].[Matchs] Script Date: 15/05/2021 23:55:38 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Matchs] (
    [Id]             UNIQUEIDENTIFIER NOT NULL,
    [LeagueId]       UNIQUEIDENTIFIER NOT NULL,
    [RoundId]        UNIQUEIDENTIFIER NOT NULL,
    [HomeTeam]       VARCHAR (200)    NOT NULL,
    [AwayTeam]       VARCHAR (200)    NOT NULL,
    [HomeScoreBoard] INT              NULL,
    [AwayScoreBoard] INT              NULL,
    [MatchDate]      DATETIME         NOT NULL,
    [CreatedAt]      DATETIME         NOT NULL,
    [UpdatedAt]      DATETIME         NOT NULL
);


GO
CREATE NONCLUSTERED INDEX [IX_Matchs_LeagueId]
    ON [dbo].[Matchs]([LeagueId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Matchs_RoundId]
    ON [dbo].[Matchs]([RoundId] ASC);


GO
ALTER TABLE [dbo].[Matchs]
    ADD CONSTRAINT [PK_Matchs] PRIMARY KEY CLUSTERED ([Id] ASC);


GO
ALTER TABLE [dbo].[Matchs]
    ADD CONSTRAINT [FK_Matchs_League_LeagueId] FOREIGN KEY ([LeagueId]) REFERENCES [dbo].[League] ([Id]);


GO
ALTER TABLE [dbo].[Matchs]
    ADD CONSTRAINT [FK_Matchs_Rounds_RoundId] FOREIGN KEY ([RoundId]) REFERENCES [dbo].[Rounds] ([Id]);


