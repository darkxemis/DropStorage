CREATE TABLE ResetPasswordLink (
	id UNIQUEIDENTIFIER NOT NULL PRIMARY KEY DEFAULT NEWID(),
	createTime datetime NOT NULL,
	expirationDate datetime NOT NULL,

	user_id uniqueidentifier NOT NULL,
	FOREIGN KEY (user_id) REFERENCES Users(id)
);