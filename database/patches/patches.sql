SET			NOCOUNT, ANSI_PADDING, ANSI_WARNINGS, CONCAT_NULL_YIELDS_NULL, ARITHABORT, QUOTED_IDENTIFIER, ANSI_NULLS, XACT_ABORT ON
SET			NUMERIC_ROUNDABORT OFF

IF	0=1
BEGIN
	----------------------------------------------------------------------------------------
	RAISERROR ('yyyyMMdd: Mahame kolona yyy na xxx', 10, 1) WITH NOWAIT
	----------------------------------------------------------------------------------------

	IF @@TRANCOUNT > 0 ROLLBACK

	BEGIN TRY 
		 BEGIN TRAN PATCH

		--- your sql code here
	
		PRINT 'OK: ' + CONVERT(VARCHAR, @@TRANCOUNT) + ', ' + CONVERT(VARCHAR, GETDATE(), 121)
		COMMIT
    END TRY
    BEGIN CATCH
		PRINT 'Err (line ' + CONVERT(VARCHAR, ERROR_LINE()) + '): ' + ERROR_MESSAGE() + ', ' + CONVERT(VARCHAR, GETDATE(), 121)
		IF @@TRANCOUNT > 0 ROLLBACK
		;THROW
    END CATCH
END
GO
----------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------

IF	OBJECT_ID('dbo.sys_Users') IS NULL
BEGIN
	----------------------------------------------------------------------------------------
	RAISERROR ('20260824: Add sys_Users table', 10, 1) WITH NOWAIT
	----------------------------------------------------------------------------------------

	IF @@TRANCOUNT > 0 ROLLBACK

	BEGIN TRY 
		BEGIN TRAN PATCH

		CREATE TABLE sys_Users (
				ID				UNIQUEIDENTIFIER	NOT NULL
				, Email			NVARCHAR(255) 		NOT NULL
				, PassHash		VARCHAR(255)		NOT NULL
				, CU			NVARCHAR(255)		NOT NULL
				, CD			DATETIME2(3)		NOT NULL
				, LU			NVARCHAR(255)		NOT NULL
				, LD			DATETIME2(3)		NOT NULL

				, CONSTRAINT PK_sys_Users_ID PRIMARY KEY NONCLUSTERED (ID)
				, CONSTRAINT CLS_sys_Users_ID UNIQUE CLUSTERED (ID)
				, CONSTRAINT UNQ_sys_Users_Email UNIQUE	(Email)
		)

		PRINT 'OK: ' + CONVERT(VARCHAR, @@TRANCOUNT) + ', ' + CONVERT(VARCHAR, GETDATE(), 121)
		COMMIT
    END TRY
    BEGIN CATCH
		PRINT 'Err (line ' + CONVERT(VARCHAR, ERROR_LINE()) + '): ' + ERROR_MESSAGE() + ', ' + CONVERT(VARCHAR, GETDATE(), 121)
		IF @@TRANCOUNT > 0 ROLLBACK
		;THROW
    END CATCH
END
GO

IF	OBJECT_ID('dbo.sys_Roles') IS NULL
BEGIN
	----------------------------------------------------------------------------------------
	RAISERROR ('20260824: Add table sys_Roles and columns in sys_Users', 10, 1) WITH NOWAIT
	----------------------------------------------------------------------------------------

	IF @@TRANCOUNT > 0 ROLLBACK

	BEGIN TRY 
		 BEGIN TRAN PATCH

		CREATE TABLE sys_Roles (
					  ID		UNIQUEIDENTIFIER	NOT NULL
					, Name		NVARCHAR(50)		NOT NULL

					, CU		NVARCHAR(255)		NOT NULL
					, CD		DATETIME2(3)		NOT NULL
					, LU		NVARCHAR(255)		NOT NULL
					, LD		DATETIME2(3)		NOT NULL

					, CONSTRAINT PK_sys_Roles_ID PRIMARY KEY NONCLUSTERED (ID)
					, CONSTRAINT CL_sys_Roles_ID UNIQUE CLUSTERED (ID)
					, CONSTRAINT UQ_sys_Roles_Name UNIQUE (Name)
		)

		INSERT		dbo.sys_Roles (ID, Name, CD, CU, LD, LU)
		VALUES		  (NEWID(), 'Admin', GETDATE(), 'system', GETDATE(), 'system')
					, (NEWID(), 'Organizer', GETDATE(), 'system', GETDATE(), 'system')
					, (NEWID(), 'Player', GETDATE(), 'system', GETDATE(), 'system')

		ALTER TABLE sys_Users ADD
					  Username	NVARCHAR(50)		NOT NULL
					, Role_ID	UNIQUEIDENTIFIER	NOT NULL
					, CONSTRAINT FK_sys_Users_sys_Roles_Role_ID
						FOREIGN KEY (Role_ID)
						REFERENCES sys_Roles (ID)
		PRINT 'OK: ' + CONVERT(VARCHAR, @@TRANCOUNT) + ', ' + CONVERT(VARCHAR, GETDATE(), 121)
		COMMIT
    END TRY
    BEGIN CATCH
		PRINT 'Err (line ' + CONVERT(VARCHAR, ERROR_LINE()) + '): ' + ERROR_MESSAGE() + ', ' + CONVERT(VARCHAR, GETDATE(), 121)
		IF @@TRANCOUNT > 0 ROLLBACK
		;THROW
    END CATCH
END
GO

IF OBJECT_ID('dbo.trm_Tournaments') IS NULL
BEGIN
	----------------------------------------------------------------------------------------
    RAISERROR ('20260831: Add table trm_Tournaments', 10, 1) WITH NOWAIT
	----------------------------------------------------------------------------------------

	IF @@TRANCOUNT > 0 ROLLBACK

	BEGIN TRY 
		 BEGIN TRAN PATCH

		CREATE TABLE trm_Tournaments (
				  ID		UNIQUEIDENTIFIER	NOT NULL
				, Name		NVARCHAR(50)		NOT NULL
				, StartDate	DATETIME2(3)		NOT NULL
				, EndDate	DATETIME2(3)		NOT NULL

				, CU		NVARCHAR(255)		NOT NULL
				, CD		DATETIME2(3)		NOT NULL
				, LU		NVARCHAR(255)		NOT NULL
				, LD		DATETIME2(3)		NOT NULL

				, CONSTRAINT PK_trm_Tournaments_ID PRIMARY KEY NONCLUSTERED (ID)
				, CONSTRAINT CL_trm_Tournaments_ID UNIQUE CLUSTERED (ID)
		)	

		PRINT 'OK: ' + CONVERT(VARCHAR, @@TRANCOUNT) + ', ' + CONVERT(VARCHAR, GETDATE(), 121)
		COMMIT
    END TRY
    BEGIN CATCH
		PRINT 'Err (line ' + CONVERT(VARCHAR, ERROR_LINE()) + '): ' + ERROR_MESSAGE() + ', ' + CONVERT(VARCHAR, GETDATE(), 121)
		IF @@TRANCOUNT > 0 ROLLBACK
		;THROW
    END CATCH
END
GO

IF COL_LENGTH('dbo.trm_Tournaments', 'Owner_ID') IS NULL
BEGIN
	----------------------------------------------------------------------------------------
	RAISERROR ('20260909: Add col Owner_ID in trm_Tournaments', 10, 1) WITH NOWAIT
	----------------------------------------------------------------------------------------

	IF @@TRANCOUNT > 0 ROLLBACK

	BEGIN TRY 
		 BEGIN TRAN PATCH

		ALTER TABLE dbo.trm_Tournaments ADD 
			Owner_ID UNIQUEIDENTIFIER NULL
			, CONSTRAINT FK_trm_Tournaments_sys_Users_Owner_ID FOREIGN KEY (Owner_ID) REFERENCES dbo.sys_Users (ID)

		EXEC		('
		UPDATE		tur
		SET			tur.Owner_ID = u.ID
		FROM		dbo.trm_Tournaments tur
		JOIN		dbo.sys_Users u
		ON			u.Email = tur.CU
		WHERE		tur.Owner_ID IS NULL
		')

		ALTER TABLE dbo.trm_Tournaments 
			ALTER COLUMN Owner_ID UNIQUEIDENTIFIER NOT NULL
	
		PRINT 'OK: ' + CONVERT(VARCHAR, @@TRANCOUNT) + ', ' + CONVERT(VARCHAR, GETDATE(), 121)
		COMMIT
    END TRY
    BEGIN CATCH
		PRINT 'Err (line ' + CONVERT(VARCHAR, ERROR_LINE()) + '): ' + ERROR_MESSAGE() + ', ' + CONVERT(VARCHAR, GETDATE(), 121)
		IF @@TRANCOUNT > 0 ROLLBACK
		;THROW
    END CATCH
END
GO