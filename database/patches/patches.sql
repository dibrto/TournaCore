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
			ID				UNIQUEIDENTIFIER	NOT NULL	CONSTRAINT DEF_sys_Users_ID DEFAULT NEWID()
			, Email			NVARCHAR(255) 		NOT NULL
			, PassHash		VARCHAR(255)		NOT NULL
			, CD			DATETIME2(3)		NOT NULL	CONSTRAINT DEF_sys_Users_CD DEFAULT SYSDATETIME()
			, CU			NVARCHAR(255)		NOT NULL	CONSTRAINT DEF_sys_Users_CU DEFAULT N'system'
			, LD			DATETIME2(3)		NOT NULL	CONSTRAINT DEF_sys_Users_LD DEFAULT SYSDATETIME()
			, LU			NVARCHAR(255)		NOT NULL	CONSTRAINT DEF_sys_Users_LU DEFAULT N'system'

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