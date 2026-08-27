DROP VIEW IF EXISTS dbo.v_sys_Users
GO

-- SELECT * FROM dbo.v_sys_Users

CREATE VIEW dbo.v_sys_Users
AS
/*------------------------------------------------------------------------

	TournaCore Project
	Copyright (c) 2026-2026 TournaCore

	Data-access view for v_sys_Users

*/------------------------------------------------------------------------

SELECT		u.ID
			, u.Email
			, u.Username
			, u.Role_ID
			, role.Name		AS RoleName
			, u.PassHash
			, u.CD
			, u.CU
			, u.LD
			, u.LU
FROM		dbo.sys_Users u
JOIN		dbo.sys_Roles role
ON			role.ID = u.Role_ID

GO