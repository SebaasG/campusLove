Campus Love - Proyecto de Aplicación de Citas con IA
Descripción
Campus Love es una aplicación de citas orientada a la comunidad universitaria,
desarrollada en C# con arquitectura hexagonal. El proyecto integra funcionalidades de visualización de perfiles,
matches, mensajes y estadísticas.

para iniciar clone el programa y cree la base de datos
# SQL Base De Datos
 ```sql
CREATE TABLE Country (
    id INT AUTO_INCREMENT PRIMARY KEY,
    name VARCHAR(80) NOT NULL
);

CREATE TABLE Region (
    id INT AUTO_INCREMENT PRIMARY KEY,
    name VARCHAR(60) NOT NULL,
    countryId INT,
    FOREIGN KEY (countryId) REFERENCES Country(id)
);


CREATE TABLE City (
    id INT AUTO_INCREMENT PRIMARY KEY,
    name VARCHAR(60) NOT NULL,
    regionId INT,
    FOREIGN KEY (regionId) REFERENCES Region(id)
);

CREATE TABLE Career (
    id INT AUTO_INCREMENT PRIMARY KEY,
    name VARCHAR(80) NOT NULL
);

CREATE TABLE Interests (
    id INT AUTO_INCREMENT PRIMARY KEY,
    name VARCHAR(80) NOT NULL
);

CREATE TABLE Gender (
    id INT AUTO_INCREMENT PRIMARY KEY,
    name VARCHAR(45) NOT NULL
);

CREATE TABLE Users (
    doc VARCHAR(20) PRIMARY KEY,
    name VARCHAR(55) NOT NULL,
    lastName VARCHAR(55) NOT NULL,
    age INT NOT NULL,
    genderId INT,
    cityId INT,
    careerId INT,
    FOREIGN KEY (genderId) REFERENCES Gender(id),
    FOREIGN KEY (cityId) REFERENCES City(id),
    FOREIGN KEY (careerId) REFERENCES Career(id)
);

CREATE TABLE Credentials (
    id INT AUTO_INCREMENT PRIMARY KEY,
    docUser VARCHAR(20)NOT NULL,
    username VARCHAR(100) NOT NULL UNIQUE,
    password VARCHAR(255) NOT NULL,
    FOREIGN KEY (docUser) REFERENCES Users(doc)
);

CREATE TABLE Profiles (
    id INT AUTO_INCREMENT PRIMARY KEY,
    userId VARCHAR(20),
    phrase TEXT,
    email VARCHAR (120) NOT NULL,
    createdAt DATETIME DEFAULT CURRENT_TIMESTAMP,
    isActive BOOLEAN DEFAULT TRUE,
    FOREIGN KEY (userId) REFERENCES Users(doc)
);

CREATE TABLE UserInterests (
    userId VARCHAR(20),
    interestId INT,
    PRIMARY KEY (userId, interestId),
    FOREIGN KEY (userId) REFERENCES Users(doc),
    FOREIGN KEY (interestId) REFERENCES Interests(id)
);

CREATE TABLE TypeInteraction (
    id INT AUTO_INCREMENT PRIMARY KEY,
    name VARCHAR(50) NOT NULL
);

CREATE TABLE Interaction (
    id INT AUTO_INCREMENT PRIMARY KEY,
    fromUser VARCHAR(20),
    toUser VARCHAR(20),
    typeId INT,
    createdAt DATETIME DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (fromUser) REFERENCES Users(doc),
    FOREIGN KEY (toUser) REFERENCES Users(doc),
    FOREIGN KEY (typeId) REFERENCES TypeInteraction(id)
);

CREATE TABLE Matches (
    id INT AUTO_INCREMENT PRIMARY KEY,
    user1 VARCHAR(20),
    user2 VARCHAR(20),
    createdAt DATETIME DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (user1) REFERENCES Users(doc),
    FOREIGN KEY (user2) REFERENCES Users(doc)
);

CREATE TABLE Messages (
    id INT AUTO_INCREMENT PRIMARY KEY,
    fromUser VARCHAR(20),
    toUser VARCHAR(20),
    content TEXT NOT NULL,
    createdAt DATETIME DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (fromUser) REFERENCES Users(doc),
    FOREIGN KEY (toUser) REFERENCES Users(doc)
);



CREATE TABLE Credits (
    userId VARCHAR(20) PRIMARY KEY,
    dailyCredits INT,
    lastResetDate DATE NOT NULL,
    FOREIGN KEY (userId) REFERENCES Users(doc)
);


CREATE TABLE Stats (
    userId VARCHAR(20) PRIMARY KEY,
    likesReceived INT DEFAULT 0,
    likesGiven INT DEFAULT 0,
    matchesCount INT DEFAULT 0,
    lastLikeDate DATETIME,
    FOREIGN KEY (userId) REFERENCES Users(doc)
);

  ```

# Procedures y Triggers
 ```sql
DELIMITER //

CREATE PROCEDURE registerUser(
    IN _Doc VARCHAR(20),
    IN _Name VARCHAR(55),
    IN _Lastname VARCHAR(55),
    IN _Age INT,
    IN _GenderId INT,
    IN _CityId INT,
    IN _CareerId INT,
    IN _UserName VARCHAR(50),
    IN _Password VARCHAR(255),
    IN _Phrase TEXT,
    IN _Email VARCHAR(120)
)
BEGIN
    INSERT INTO Users (doc, name, lastName, age, genderId, cityId, careerId)
    VALUES (_Doc, _Name, _Lastname, _Age, _GenderId, _CityId, _CareerId);
 
    INSERT INTO Credentials (docUser, username, password)
    VALUES (_Doc, _UserName, _Password);

    INSERT INTO Profiles (userId, phrase, email, isActive)
    VALUES (_Doc, _Phrase, _Email, TRUE);

    INSERT INTO Stats (userId, likesReceived, likesGiven, matchesCount, lastLikeDate)
    VALUES (_Doc, 0, 0, 0, NULL);

    INSERT INTO Credits (userId, dailyCredits, lastResetDate)
    VALUES (_Doc, 10, CURDATE());
END //

DELIMITER ;

DELIMITER //

CREATE PROCEDURE loginUser(
    IN _UserName VARCHAR(50)
)
BEGIN
    SELECT password FROM Credentials WHERE username = _UserName;
END //

DELIMITER ;

DELIMITER $$

CREATE TRIGGER after_like_insert
AFTER INSERT ON Interaction
FOR EACH ROW
BEGIN
    -- Solo nos interesan los likes (typeId = 1)
    IF NEW.typeId = 1 THEN
        -- Verifica si el usuario objetivo ya había dado like al que ahora lo hizo
        IF EXISTS (
            SELECT 1 FROM Interaction
            WHERE fromUser = NEW.toUser AND toUser = NEW.fromUser AND typeId = 1
        ) THEN
            -- Verifica que el match aún no exista
            IF NOT EXISTS (
                SELECT 1 FROM Matches
                WHERE (user1 = NEW.fromUser AND user2 = NEW.toUser)
                   OR (user1 = NEW.toUser AND user2 = NEW.fromUser)
            ) THEN
                -- Inserta el match
                INSERT INTO Matches (user1, user2) VALUES (NEW.fromUser, NEW.toUser);
            END IF;
        END IF;
    END IF;
END$$

DELIMITER ;


DELIMITER $$

CREATE TRIGGER after_like_insert
AFTER INSERT ON Interaction
FOR EACH ROW
BEGIN
    -- Solo nos interesan los likes (typeId = 1)
    IF NEW.typeId = 1 THEN
        -- Verifica si el usuario objetivo ya había dado like al que ahora lo hizo
        IF EXISTS (
            SELECT 1 FROM Interaction
            WHERE fromUser = NEW.toUser AND toUser = NEW.fromUser AND typeId = 1
        ) THEN
            -- Verifica que el match aún no exista
            IF NOT EXISTS (
                SELECT 1 FROM Matches
                WHERE (user1 = NEW.fromUser AND user2 = NEW.toUser)
                   OR (user1 = NEW.toUser AND user2 = NEW.fromUser)
            ) THEN
                -- Inserta el match
                INSERT INTO Matches (user1, user2) VALUES (NEW.fromUser, NEW.toUser);
            END IF;
        END IF;
    END IF;
END$$

DELIMITER ;


  ```


# Inserciones 
 ```sql
INSERT INTO Country (name) VALUES ('Colombia');
INSERT INTO Country (name) VALUES ('México');
INSERT INTO Country (name) VALUES ('Argentina');


INSERT INTO Region (name, countryId) VALUES ('Cundinamarca', 1);
INSERT INTO Region (name, countryId) VALUES ('Ciudad de México', 2);
INSERT INTO Region (name, countryId) VALUES ('Buenos Aires', 3);

INSERT INTO City (name, regionId) VALUES ('Bogotá', 1);
INSERT INTO City (name, regionId) VALUES ('Medellín', 1);
INSERT INTO City (name, regionId) VALUES ('CDMX', 2);
INSERT INTO City (name, regionId) VALUES ('Monterrey', 2);
INSERT INTO City (name, regionId) VALUES ('Buenos Aires', 3);

INSERT INTO Career (name) VALUES ('Ingeniería de Sistemas');
INSERT INTO Career (name) VALUES ('Medicina');
INSERT INTO Career (name) VALUES ('Arquitectura');

-- Insertar intereses
INSERT INTO Interests (name) VALUES ('Deportes');
INSERT INTO Interests (name) VALUES ('Tecnología');
INSERT INTO Interests (name) VALUES ('Música');
INSERT INTO Interests (name) VALUES ('Viajes');

INSERT INTO Gender (name) VALUES ('Masculino');
INSERT INTO Gender (name) VALUES ('Femenino');
INSERT INTO Gender (name) VALUES ('Otro');

INSERT INTO Users (doc, name, lastName, age, genderId, cityId, careerId) 
VALUES ('1234567890', 'Juan', 'Pérez', 25, 1, 1, 1);

INSERT INTO Users (doc, name, lastName, age, genderId, cityId, careerId) 
VALUES ('0987654321', 'Ana', 'Gómez', 28, 2, 2, 2);

-- Insertar credenciales
INSERT INTO Credentials (docUser, username, password) 
VALUES ('1234567890', 'juanperez', 'password123');

INSERT INTO Credentials (docUser, username, password) 
VALUES ('0987654321', 'anagomez', 'password456');


INSERT INTO UserInterests (userId, interestId) VALUES ('1234567890', 1);
INSERT INTO UserInterests (userId, interestId) VALUES ('1234567890', 2);
INSERT INTO UserInterests (userId, interestId) VALUES ('0987654321', 3);
INSERT INTO UserInterests (userId, interestId) VALUES ('0987654321', 4);

INSERT INTO TypeInteraction (name) VALUES ('Like');
INSERT INTO TypeInteraction (name) VALUES ('Mensaje');

INSERT INTO Interaction (fromUser, toUser, typeId) 
VALUES ('1234567890', '0987654321', 1);

INSERT INTO Interaction (fromUser, toUser, typeId) 
VALUES ('0987654321', '1234567890', 2);

INSERT INTO Matches (user1, user2) 
VALUES ('1234567890', '0987654321');

INSERT INTO Credits (userId, dailyCredits, lastResetDate) 
VALUES ('1234567890', 5, '2025-05-01');

INSERT INTO Credits (userId, dailyCredits, lastResetDate) 
VALUES ('0987654321', 5, '2025-05-01');

INSERT INTO Stats (userId, likesReceived, likesGiven, matchesCount) 
VALUES ('1234567890', 10, 5, 1);

INSERT INTO Stats (userId, likesReceived, likesGiven, matchesCount) 
VALUES ('0987654321', 12, 3, 1);

select * from profiles;
  ```
