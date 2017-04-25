CREATE TABLE [dbo].[LeaveApplications] (
    [EmployeeID]       INT           NULL,
    [LeaveType]        VARCHAR (20)  NULL,
    [LeavingDate]      VARCHAR (10)  NULL,
    [JoiningDate]      VARCHAR (10)  NULL,
    [LeaveDescription] VARCHAR (100) NULL,
    CONSTRAINT [Leave_LeaveApplications_DateConstraint] CHECK ([LeavingDate] like '__-__-____' AND [JoiningDate] like '__-__-____')
);

