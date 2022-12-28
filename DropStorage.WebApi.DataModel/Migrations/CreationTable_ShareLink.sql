CREATE TABLE ShareLink (
	id UNIQUEIDENTIFIER NOT NULL PRIMARY KEY DEFAULT NEWID(),
	expirationDate datetime NOT NULL,

	user_id uniqueidentifier NOT NULL,
	FOREIGN KEY (user_id) REFERENCES Users(id)
);

GO

CREATE TABLE ShareLinkFileStorage (
	id INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	idShareLink UNIQUEIDENTIFIER NOT NULL,
	idFileStorage UNIQUEIDENTIFIER NOT NULL,

	FOREIGN KEY (idShareLink) REFERENCES ShareLink(id),
	FOREIGN KEY (idFileStorage) REFERENCES FileStorage(id)
);

GO

CREATE INDEX ShareLinkFileStorage   
    ON ShareLinkFileStorage (idShareLink);   
