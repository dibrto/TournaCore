SET			NOCOUNT, ANSI_PADDING, ANSI_WARNINGS, CONCAT_NULL_YIELDS_NULL, ARITHABORT, QUOTED_IDENTIFIER, ANSI_NULLS, XACT_ABORT ON
SET			NUMERIC_ROUNDABORT OFF

IF	0=1
BEGIN
	----------------------------------------------------------------------------------------
	RAISERROR ('yyyyMMdd: Mahame kolona yyy na xxx', 10, 1) WITH NOWAIT
	----------------------------------------------------------------------------------------

	IF @@TRANCOUNT > 0 ROLLBACK BEGIN TRAN
	DECLARE		@ERR INT
	SET			@ERR = 0

	--- your sql code here
	SET			@ERR = @ERR+@@ERROR

	IF @ERR = 0 BEGIN
		PRINT 'OK: ' + CONVERT(VARCHAR, @@TRANCOUNT) + ', ' + { fn CURRENT_TIME() }
		COMMIT
	END ELSE BEGIN
		PRINT '!!!!!!!!!!!!! Err: ' + CONVERT(VARCHAR, @ERR) + ' !!!!!!!!!!!!!!!!!!!' + ', ' + CONVERT(VARCHAR, GETDATE(), 121)
		ROLLBACK
	END
END
GO
----------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------

IF	OBJECT_ID('sys_Users') IS NULL
BEGIN
	----------------------------------------------------------------------------------------
	RAISERROR ('20260824: Add sys_Users table', 10, 1) WITH NOWAIT
	----------------------------------------------------------------------------------------

	IF @@TRANCOUNT > 0 ROLLBACK BEGIN TRAN
	DECLARE		@ERR INT
	SET			@ERR = 0

	CREATE TABLE sys_Users (
			ID				UNIQUEIDENTIFIER	NOT NULL
			, Email			NVARCHAR(255) 		NOT NULL
			, PassHash		VARCHAR(255)		NOT NULL
			, CD			DATETIME2(3)		NOT NULL
			, CU			NVARCHAR(255)		NOT NULL
			, LD			DATETIME2(3)		NOT NULL
			, LU			NVARCHAR(255)		NOT NULL

			, CONSTRAINT PK_sys_Users_ID PRIMARY KEY NONCLUSTERED (ID)
			, CONSTRAINT CLS_sys_Users_ID UNIQUE CLUSTERED (ID)
			, CONSTRAINT UNQ_sys_Users_Email UNIQUE	(Email)
	)
	SET			@ERR = @ERR+@@ERROR

	IF @ERR = 0 BEGIN
		PRINT 'OK: ' + CONVERT(VARCHAR, @@TRANCOUNT) + ', ' + { fn CURRENT_TIME() }
		COMMIT
	END ELSE BEGIN
		PRINT '!!!!!!!!!!!!! Err: ' + CONVERT(VARCHAR, @ERR) + ' !!!!!!!!!!!!!!!!!!!' + ', ' + CONVERT(VARCHAR, GETDATE(), 121)
		ROLLBACK
	END
END
GO

IF	OBJECT_ID('sys_Roles') IS NULL
BEGIN
	----------------------------------------------------------------------------------------
	RAISERROR ('20260824: Add table sys_Roles and columns in sys_Users', 10, 1) WITH NOWAIT
	----------------------------------------------------------------------------------------

	IF @@TRANCOUNT > 0 ROLLBACK BEGIN TRAN
	DECLARE		@ERR INT
	SET			@ERR = 0

	CREATE TABLE sys_Roles (
				  ID		UNIQUEIDENTIFIER	NOT NULL
				, Name		NVARCHAR(50)		NOT NULL

				, CD		DATETIME2(3)		NOT NULL
				, CU		NVARCHAR(255)		NOT NULL
				, LD		DATETIME2(3)		NOT NULL
				, LU		NVARCHAR(255)		NOT NULL

				, CONSTRAINT PK_sys_Roles_ID PRIMARY KEY NONCLUSTERED (ID)
				, CONSTRAINT CL_sys_Roles_ID UNIQUE CLUSTERED (ID)
				, CONSTRAINT UQ_sys_Roles_Name UNIQUE (Name)
	)
	SET			@ERR = @ERR+@@ERROR

	INSERT		dbo.sys_Roles (ID, Name, CD, CU, LD, LU)
	VALUES		  (NEWID(), 'Admin', GETDATE(), 'system', GETDATE(), 'system')
				, (NEWID(), 'Organizer', GETDATE(), 'system', GETDATE(), 'system')
				, (NEWID(), 'Player', GETDATE(), 'system', GETDATE(), 'system')
	SET			@ERR = @ERR+@@ERROR

	ALTER TABLE sys_Users ADD
				  Username	NVARCHAR(50)		NOT NULL
				, Role_ID	UNIQUEIDENTIFIER	NOT NULL
				, CONSTRAINT FK_sys_Users_sys_Roles_Role_ID
					FOREIGN KEY (Role_ID)
					REFERENCES sys_Roles (ID)
	SET			@ERR = @ERR+@@ERROR

	IF @ERR = 0 BEGIN
		PRINT 'OK: ' + CONVERT(VARCHAR, @@TRANCOUNT) + ', ' + { fn CURRENT_TIME() }
		COMMIT
	END ELSE BEGIN
		PRINT '!!!!!!!!!!!!! Err: ' + CONVERT(VARCHAR, @ERR) + ' !!!!!!!!!!!!!!!!!!!' + ', ' + CONVERT(VARCHAR, GETDATE(), 121)
		ROLLBACK
	END
END
GO