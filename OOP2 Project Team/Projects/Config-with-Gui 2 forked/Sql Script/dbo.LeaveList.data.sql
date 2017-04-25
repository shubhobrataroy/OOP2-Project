SET IDENTITY_INSERT [dbo].[LeaveList] ON
INSERT INTO [dbo].[LeaveList] ([LeaveID], [EmployeeID], [LeaveType], [LeavingDate], [JoiningDate], [Balance], [DaysCount], [Comment]) VALUES (1, 2, N'Casual', N'01-02-2016', N'05-02-2016', 5, 9, N'Test')
INSERT INTO [dbo].[LeaveList] ([LeaveID], [EmployeeID], [LeaveType], [LeavingDate], [JoiningDate], [Balance], [DaysCount], [Comment]) VALUES (2, 9, N'Casual', N'01-02-2016', N'04-02-2016', 5, 6, N'Test2')
SET IDENTITY_INSERT [dbo].[LeaveList] OFF
