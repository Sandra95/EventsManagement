
-- Script START
IF NOT EXISTS (SELECT * from INFORMATION_SCHEMA.TABLES where TABLE_NAME = 'EventsRegistrations' AND TABLE_SCHEMA = 'dbo')
BEGIN
	CREATE TABLE [dbo].[EventsRegistrations](
	[Id] UNIQUEIDENTIFIER default NEWID(),
	[EventId] [UNIQUEIDENTIFIER] NOT NULL,
	[RegistrationId] [UNIQUEIDENTIFIER]  NOT NULL,
	CONSTRAINT [PK_EventsRegistrations] PRIMARY KEY CLUSTERED ([Id]),
	CONSTRAINT [FK_EventsRegistrations_EventId] FOREIGN KEY (EventId) REFERENCES [Events](Id),
	CONSTRAINT [FK_EventsRegistrations_RegistrationId] FOREIGN KEY (RegistrationId) REFERENCES [Registrations](Id),
    CONSTRAINT [UC_EventsRegistrations_EventId_RegistrationId] UNIQUE NONCLUSTERED 
	(
		[Id] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
	)	ON [PRIMARY]

END
-- Script END