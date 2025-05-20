-- Base de datos para red social de citas y mensajes

USE CSL;

-- Tabla de países
CREATE TABLE Country (
    id INT AUTO_INCREMENT PRIMARY KEY,
    name VARCHAR(80) NOT NULL
);

-- Tabla de regiones, relacionada con Country
CREATE TABLE Region (
    id INT AUTO_INCREMENT PRIMARY KEY,
    name VARCHAR(60) NOT NULL,
    countryId INT,
    FOREIGN KEY (countryId) REFERENCES Country(id)
);

-- Tabla de ciudades, relacionada con Region
CREATE TABLE City (
    id INT AUTO_INCREMENT PRIMARY KEY,
    name VARCHAR(60) NOT NULL,
    regionId INT,
    FOREIGN KEY (regionId) REFERENCES Region(id)
);

-- Tabla de carreras universitarias
CREATE TABLE Career (
    id INT AUTO_INCREMENT PRIMARY KEY,
    name VARCHAR(80) NOT NULL
);

-- Tabla de intereses o hobbies
CREATE TABLE Interests (
    id INT AUTO_INCREMENT PRIMARY KEY,
    name VARCHAR(80) NOT NULL
);

-- Tabla de géneros
CREATE TABLE Gender (
    id INT AUTO_INCREMENT PRIMARY KEY,
    name VARCHAR(45) NOT NULL
);

-- Tabla de usuarios principales con referencias a género, ciudad y carrera
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

-- Tabla de credenciales para login, con usuario y password
CREATE TABLE Credentials (
    id INT AUTO_INCREMENT PRIMARY KEY,
    docUser VARCHAR(20) NOT NULL,
    username VARCHAR(100) NOT NULL UNIQUE,
    password VARCHAR(255) NOT NULL,
    FOREIGN KEY (docUser) REFERENCES Users(doc)
);

-- Tabla con perfiles adicionales (frase, email, estado)
CREATE TABLE Profiles (
    id INT AUTO_INCREMENT PRIMARY KEY,
    userId VARCHAR(20) NOT NULL,
    phrase TEXT,
    email VARCHAR(120) NOT NULL,
    createdAt DATETIME DEFAULT CURRENT_TIMESTAMP,
    isActive BOOLEAN DEFAULT TRUE,
    FOREIGN KEY (userId) REFERENCES Users(doc)
);

-- Tabla que relaciona usuarios con intereses (muchos a muchos)
CREATE TABLE UserInterests (
    userId VARCHAR(20),
    interestId INT,
    PRIMARY KEY (userId, interestId),
    FOREIGN KEY (userId) REFERENCES Users(doc),
    FOREIGN KEY (interestId) REFERENCES Interests(id)
);

-- Tipos de interacción (por ejemplo: like, dislike, etc)
CREATE TABLE TypeInteraction (
    id INT AUTO_INCREMENT PRIMARY KEY,
    name VARCHAR(50) NOT NULL
);

-- Tabla que almacena interacciones entre usuarios
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

-- Tabla que almacena los matches entre usuarios
CREATE TABLE Matches (
    id INT AUTO_INCREMENT PRIMARY KEY,
    user1 VARCHAR(20),
    user2 VARCHAR(20),
    createdAt DATETIME DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (user1) REFERENCES Users(doc),
    FOREIGN KEY (user2) REFERENCES Users(doc)
);

-- Tabla de mensajes entre usuarios
CREATE TABLE Messages (
    id INT AUTO_INCREMENT PRIMARY KEY,
    fromUser VARCHAR(20),
    toUser VARCHAR(20),
    content TEXT NOT NULL,
    createdAt DATETIME DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (fromUser) REFERENCES Users(doc),
    FOREIGN KEY (toUser) REFERENCES Users(doc)
);

-- Tabla para manejar créditos diarios por usuario
CREATE TABLE Credits (
    userId VARCHAR(20) PRIMARY KEY,
    dailyCredits INT,
    lastResetDate DATE NOT NULL,
    FOREIGN KEY (userId) REFERENCES Users(doc)
);

-- Tabla para estadísticas por usuario
CREATE TABLE Stats (
    userId VARCHAR(20) PRIMARY KEY,
    likesReceived INT DEFAULT 0,
    likesGiven INT DEFAULT 0,
    matchesCount INT DEFAULT 0,
    lastLikeDate DATETIME,
    FOREIGN KEY (userId) REFERENCES Users(doc)
);

-- PROCEDIMIENTO para registrar un nuevo usuario y sus datos relacionados
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
    -- Inserta usuario en tabla Users
    INSERT INTO Users (doc, name, lastName, age, genderId, cityId, careerId)
    VALUES (_Doc, _Name, _Lastname, _Age, _GenderId, _CityId, _CareerId);

    -- Inserta credenciales para login
    INSERT INTO Credentials (docUser, username, password)
    VALUES (_Doc, _UserName, _Password);

    -- Inserta perfil con frase y email
    INSERT INTO Profiles (userId, phrase, email, isActive)
    VALUES (_Doc, _Phrase, _Email, TRUE);

    -- Inicializa estadísticas del usuario
    INSERT INTO Stats (userId, likesReceived, likesGiven, matchesCount, lastLikeDate)
    VALUES (_Doc, 0, 0, 0, NULL);

    -- Inicializa créditos diarios para el usuario
    INSERT INTO Credits (userId, dailyCredits, lastResetDate)
    VALUES (_Doc, 10, CURDATE());
END //

-- PROCEDIMIENTO para obtener el password de un usuario (login)
CREATE PROCEDURE loginUser(
    IN _UserName VARCHAR(50)
)
BEGIN
    SELECT password FROM Credentials WHERE username = _UserName;
END //

-- TRIGGER para detectar match cuando dos usuarios se dan like mutuamente
CREATE TRIGGER after_like_insert
AFTER INSERT ON Interaction
FOR EACH ROW
BEGIN
    IF NEW.typeId = 1 THEN -- Asumiendo que 1 es tipo 'like'
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
                -- Inserta el nuevo match
                INSERT INTO Matches (user1, user2) VALUES (NEW.fromUser, NEW.toUser);
            END IF;
        END IF;
    END IF;
END //

-- TRIGGER para actualizar estadísticas de likes al insertar una interacción de tipo 'like'
CREATE TRIGGER update_stats_on_like
AFTER INSERT ON Interaction
FOR EACH ROW
BEGIN
    IF NEW.typeId = 1 THEN
        UPDATE Stats
        SET likesGiven = likesGiven + 1,
            lastLikeDate = NOW()
        WHERE userId = NEW.fromUser;

        UPDATE Stats
        SET likesReceived = likesReceived + 1
        WHERE userId = NEW.toUser;
    END IF;
END //

-- TRIGGER para actualizar estadísticas de matches al insertar un nuevo match
CREATE TRIGGER update_stats_on_match
AFTER INSERT ON Matches
FOR EACH ROW
BEGIN
    UPDATE Stats
    SET matchesCount = matchesCount + 1
    WHERE userId = NEW.user1;

    UPDATE Stats
    SET matchesCount = matchesCount + 1
    WHERE userId = NEW.user2;
END //

DELIMITER ;
