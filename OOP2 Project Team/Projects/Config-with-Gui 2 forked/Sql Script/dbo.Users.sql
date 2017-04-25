CREATE TABLE [dbo].[Users] (
    [UserName]   VARCHAR (16) NOT NULL,
    [UserPass]   VARCHAR (16) NULL,
    [UserRole]   VARCHAR (10) NULL,
    [EmployeeID] INT          NOT NULL,
    PRIMARY KEY CLUSTERED ([UserName] ASC),
    CONSTRAINT [Leave_Users_UserNameConstraint] CHECK ([UserName] like '%____%'),
    CONSTRAINT [Leave_Users_UserPassConstraint] CHECK ([UserPass] like '%____%')
);

