CREATE TABLE FileStorage (
	id UNIQUEIDENTIFIER NOT NULL PRIMARY KEY DEFAULT NEWID(),
	createTime datetime NOT NULL,
	name VARCHAR(100) NOT NULL,
	extension VARCHAR(10) NOT NULL,
    url NVARCHAR(2000) NOT NULL,
	sizeMB NVARCHAR(max) NOT NULL,
	
	user_id uniqueidentifier NOT NULL,
	FOREIGN KEY (user_id) REFERENCES Users(id)
);