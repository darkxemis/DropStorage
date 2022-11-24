CREATE TABLE LogStatus (
	id UNIQUEIDENTIFIER NOT NULL PRIMARY KEY DEFAULT NEWID(),
	createTime datetime NOT NULL,
	endpoint VARCHAR(100) NULL,
	description VARCHAR(2000) NULL,
    parameterRecived NVARCHAR(max) NULL,
	parameterSended NVARCHAR(max) NULL,
	isError BIT NULL,
	
	user_id uniqueidentifier,
	FOREIGN KEY (user_id) REFERENCES Users(id)
);