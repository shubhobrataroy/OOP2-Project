CREATE TABLE [dbo].[LeavePeriods] (
    [StartingDate] VARCHAR (10) NOT NULL,
    [EndingDate]   VARCHAR (10) NOT NULL,
    PRIMARY KEY CLUSTERED ([StartingDate] ASC, [EndingDate] ASC),
    CONSTRAINT [LeavePeriods_DateConstraint] CHECK ([StartingDate] like '__-__-____' AND [EndingDate] like '__-__-____')
);

