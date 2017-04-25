CREATE TABLE [dbo].[Entitlements] (
    [EmployeeID]      INT            NOT NULL,
    [LeaveType]       VARCHAR (20)   NOT NULL,
    [EntitlementType] VARCHAR (20)   NULL,
    [ValidFrom]       VARCHAR (10)   NULL,
    [ValidTo]         VARCHAR (10)   NULL,
    [Balance]         DECIMAL (5, 2) NULL,
    PRIMARY KEY CLUSTERED ([EmployeeID] ASC, [LeaveType] ASC),
    CONSTRAINT [Entitlements_DateConstraint] CHECK ([ValidFrom] like '__-__-____' AND [ValidTo] like '__-__-____')
);

