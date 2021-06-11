USE [SoccerBet]
GO


SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Bet] (
    [Id]            UNIQUEIDENTIFIER NOT NULL,
    [UserId]        NVARCHAR(450) NOT NULL,
    [MatchId]       UNIQUEIDENTIFIER NOT NULL,
    [HomeScoreBoard] INT              NOT NULL,
    [AwayScoreBoard] INT              NOT NULL,
    [CreatedAt]      DATETIME         NOT NULL,
    
);


GO
CREATE NONCLUSTERED INDEX [IX_Users_Id]
    ON [dbo].[AspNetUsers]([Id] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Matchs_Id]
    ON [dbo].[Matchs]([Id] ASC);


GO
ALTER TABLE [dbo].[Bet]
    ADD CONSTRAINT [PK_Bet] PRIMARY KEY CLUSTERED ([Id] ASC);


GO
ALTER TABLE [dbo].[Bet]
    ADD CONSTRAINT [FK_User_Id] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers] ([Id]);


GO
ALTER TABLE [dbo].[Bet]
    ADD CONSTRAINT [FK_Matchs_Id] FOREIGN KEY ([MatchId]) REFERENCES [dbo].[Matchs] ([Id]);


