CREATE TABLE [dbo].[MusteriBasvuru] (
    [MusteriBasvuru_UID] UNIQUEIDENTIFIER NOT NULL,
    [MusteriNo]          VARCHAR (10)     NULL,
    [Basvurutipi]        INT              NULL,
    [BasvuruDurumu]      INT              NULL,
    [HataAciklama]       VARCHAR (150)    NULL,
    [Kayit_Zaman]        DATE             NOT NULL,
    [Kayit_Yapan]        VARCHAR (50)     NOT NULL,
    [Kayit_Durum]        VARCHAR (25)     NOT NULL,
    CONSTRAINT [PK_MusteriBasvuru] PRIMARY KEY CLUSTERED ([MusteriBasvuru_UID] ASC)
);

