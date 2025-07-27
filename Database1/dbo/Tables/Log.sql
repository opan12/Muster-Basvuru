CREATE TABLE [dbo].[Log] (
    [LogId]     UNIQUEIDENTIFIER NOT NULL,
    [Log_Zaman] DATE             NOT NULL,
    [Log_Yapan] VARCHAR (50)     NOT NULL,
    CONSTRAINT [PK_Log] PRIMARY KEY CLUSTERED ([LogId] ASC)
);

