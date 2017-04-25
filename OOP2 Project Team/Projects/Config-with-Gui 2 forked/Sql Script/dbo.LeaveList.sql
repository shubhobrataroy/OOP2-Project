CREATE TABLE [dbo].[LeaveList] (
    [LeaveID]     INT          IDENTITY (1, 1) NOT NULL,
    [EmployeeID]  INT          NULL,
    [LeaveType]   VARCHAR (20) NULL,
    [LeavingDate] VARCHAR (20) NULL,
    [JoiningDate] VARCHAR (20) NULL,
    [Balance]     INT          NULL,
    [DaysCount]   INT          NULL,
    [Comment]     VARCHAR (20) NULL,
    CONSTRAINT [Leave_LeaveList_DateConstraint] CHECK ([LeavingDate] like '__-__-____' AND [JoiningDate] like '__-__-____')
);

