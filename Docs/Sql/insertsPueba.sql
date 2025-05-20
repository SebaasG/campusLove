-- Países
INSERT INTO Country (name) VALUES ('Colombia');
INSERT INTO Country (name) VALUES ('México');
INSERT INTO Country (name) VALUES ('Argentina');
INSERT INTO Country (name) VALUES ('Perú');
INSERT INTO Country (name) VALUES ('Chile');

-- Regiones
INSERT INTO Region (name, countryId) VALUES ('Cundinamarca', 1);
INSERT INTO Region (name, countryId) VALUES ('Santander', 1);
INSERT INTO Region (name, countryId) VALUES ('Antioquia', 1);
INSERT INTO Region (name, countryId) VALUES ('Ciudad de México', 2);
INSERT INTO Region (name, countryId) VALUES ('Buenos Aires', 3);
INSERT INTO Region (name, countryId) VALUES ('Lima', 4);
INSERT INTO Region (name, countryId) VALUES ('Santiago', 5);

-- Ciudades Colombia
INSERT INTO City (name, regionId) VALUES ('Bogotá', 1);
INSERT INTO City (name, regionId) VALUES ('Medellín', 3);
INSERT INTO City (name, regionId) VALUES ('Bucaramanga', 2);
INSERT INTO City (name, regionId) VALUES ('Cúcuta', 2);
INSERT INTO City (name, regionId) VALUES ('Manizales', 1);
INSERT INTO City (name, regionId) VALUES ('Barranquilla', 1);
INSERT INTO City (name, regionId) VALUES ('Cartagena', 1);

-- Ciudades México
INSERT INTO City (name, regionId) VALUES ('CDMX', 4);
INSERT INTO City (name, regionId) VALUES ('Monterrey', 4);
INSERT INTO City (name, regionId) VALUES ('Guadalajara', 4);

-- Ciudades Argentina
INSERT INTO City (name, regionId) VALUES ('Buenos Aires', 5);
INSERT INTO City (name, regionId) VALUES ('Córdoba', 5);
INSERT INTO City (name, regionId) VALUES ('Rosario', 5);

-- Ciudades Perú
INSERT INTO City (name, regionId) VALUES ('Lima', 6);
INSERT INTO City (name, regionId) VALUES ('Arequipa', 6);

-- Ciudades Chile
INSERT INTO City (name, regionId) VALUES ('Santiago', 7);
INSERT INTO City (name, regionId) VALUES ('Valparaíso', 7);

-- Carreras generales por ámbito de estudio
INSERT INTO Career (name) VALUES ('Ingeniería');
INSERT INTO Career (name) VALUES ('Salud');
INSERT INTO Career (name) VALUES ('Ciencias Sociales');
INSERT INTO Career (name) VALUES ('Humanidades');
INSERT INTO Career (name) VALUES ('Artes y Diseño');
INSERT INTO Career (name) VALUES ('Ciencias Económicas');
INSERT INTO Career (name) VALUES ('Ciencias Naturales');
INSERT INTO Career (name) VALUES ('Ciencias Exactas');
INSERT INTO Career (name) VALUES ('Tecnología e Informática');
INSERT INTO Career (name) VALUES ('Educación');
INSERT INTO Career (name) VALUES ('Derecho');
INSERT INTO Career (name) VALUES ('Administración');
INSERT INTO Career (name) VALUES ('Comunicación y Medios');
INSERT INTO Career (name) VALUES ('Arquitectura y Urbanismo');

-- Géneros
INSERT INTO Gender (name) VALUES ('Masculino');
INSERT INTO Gender (name) VALUES ('Femenino');
INSERT INTO Gender (name) VALUES ('Otro');

-- Intereses
INSERT INTO Interests (name) VALUES ('Deportes');
INSERT INTO Interests (name) VALUES ('Tecnología');
INSERT INTO Interests (name) VALUES ('Música');
INSERT INTO Interests (name) VALUES ('Viajes');
INSERT INTO Interests (name) VALUES ('Lectura');
INSERT INTO Interests (name) VALUES ('Cine');
INSERT INTO Interests (name) VALUES ('Arte');
INSERT INTO Interests (name) VALUES ('Gastronomía');

-- Usuarios de prueba
INSERT INTO Users (doc, name, lastName, age, genderId, cityId, careerId) 
VALUES ('1234567890', 'Juan', 'Pérez', 25, 1, 1, 1);
INSERT INTO Users (doc, name, lastName, age, genderId, cityId, careerId) 
VALUES ('0987654321', 'Ana', 'Gómez', 28, 2, 2, 2);
INSERT INTO Users (doc, name, lastName, age, genderId, cityId, careerId) 
VALUES ('1122334455', 'Carlos', 'Ramírez', 30, 1, 3, 3);
INSERT INTO Users (doc, name, lastName, age, genderId, cityId, careerId) 
VALUES ('5566778899', 'María', 'López', 22, 2, 7, 5);

-- Credenciales de prueba
INSERT INTO Credentials (docUser, username, password) 
VALUES ('1234567890', 'juanperez', 'password123');
INSERT INTO Credentials (docUser, username, password) 
VALUES ('0987654321', 'anagomez', 'password456');
INSERT INTO Credentials (docUser, username, password) 
VALUES ('1122334455', 'carlosramirez', 'pass789');
INSERT INTO Credentials (docUser, username, password) 
VALUES ('5566778899', 'marialopez', 'pass321');

-- Intereses asignados
INSERT INTO UserInterests (userId, interestId) VALUES ('1234567890', 1);
INSERT INTO UserInterests (userId, interestId) VALUES ('1234567890', 2);
INSERT INTO UserInterests (userId, interestId) VALUES ('0987654321', 3);
INSERT INTO UserInterests (userId, interestId) VALUES ('0987654321', 4);
INSERT INTO UserInterests (userId, interestId) VALUES ('1122334455', 5);
INSERT INTO UserInterests (userId, interestId) VALUES ('5566778899', 6);

-- Tipos de interacción
INSERT INTO TypeInteraction (name) VALUES ('Like');
INSERT INTO TypeInteraction (name) VALUES ('Mensaje');

-- Interacciones de ejemplo
INSERT INTO Interaction (fromUser, toUser, typeId) 
VALUES ('1234567890', '0987654321', 1);
INSERT INTO Interaction (fromUser, toUser, typeId) 
VALUES ('0987654321', '1234567890', 2);

-- Matches de ejemplo
INSERT INTO Matches (user1, user2) 
VALUES ('1234567890', '0987654321');

-- Créditos de ejemplo
INSERT INTO Credits (userId, dailyCredits, lastResetDate) 
VALUES ('1234567890', 5, '2025-05-01');
INSERT INTO Credits (userId, dailyCredits, lastResetDate) 
VALUES ('0987654321', 5, '2025-05-01');

-- Estadísticas de ejemplo
INSERT INTO Stats (userId, likesReceived, likesGiven, matchesCount) 
VALUES ('1234567890', 10, 5, 1);
INSERT INTO Stats (userId, likesReceived, likesGiven, matchesCount) 
VALUES ('0987654321', 12, 3, 1);