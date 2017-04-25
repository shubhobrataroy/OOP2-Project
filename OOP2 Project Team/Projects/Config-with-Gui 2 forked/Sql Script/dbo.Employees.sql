CREATE TABLE [dbo].[Employees] (
    [EmployeeID]   INT          NOT NULL,
    [EmployeeName] VARCHAR (40) NULL,
    [Department]   VARCHAR (20) NULL,
    [Designation]  VARCHAR (30) NULL,
    [Salary]       INT          NULL,
    PRIMARY KEY CLUSTERED ([EmployeeID] ASC)
);

