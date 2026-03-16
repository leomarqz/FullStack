INSERT INTO Notes (Title, Content, PublishedAt, IsPublished, CreatedAt, UpdatedAt, IsDeleted)
VALUES 
('Introducción a .NET', 'Resumen sobre el framework .NET y sus ventajas.', '2025-08-28 10:00:00', 1, '2025-08-28 09:00:00', '2025-08-28 09:00:00', 0),
('Novedades en C# 12', 'Breve repaso de las nuevas características de C# 12.', NULL, 0, '2025-08-28 09:30:00', '2025-08-28 09:30:00', 0),
('¿Qué es Docker?', 'Explicación resumida sobre contenedores y Docker.', '2025-08-28 11:00:00', 1, '2025-08-28 10:00:00', '2025-08-28 10:00:00', 0),
('GitHub Copilot', 'Descripción corta de cómo Copilot ayuda a programar.', NULL, 0, '2025-08-28 10:30:00', '2025-08-28 10:30:00', 0);

SELECT * FROM Notes;

SELECT Id, Title, Content, PublishedAt, IsDeleted FROM Notes;

SELECT Id, Title, Content, PublishedAt, IsDeleted FROM Notes 
    WHERE PublishedAt IS NOT NULL ORDER BY PublishedAt DESC;


-- Roles
INSERT INTO AspNetRoles (Id, Name, NormalizedName, ConcurrencyStamp)
VALUES 
('240a778b-a089-455f-94af-9f9df4d13f8c', 'Reader', 'READER', NULL),
('e4baf39c-6356-43b4-a7af-ad4bf7a9fa27', 'Writer', 'WRITER', NULL),
('92d0dffc-f1db-49ec-8392-954f2dff387f', 'Admin', 'ADMIN', NULL);

-- Asignar roles a usuarios
-- Usuario Admin se le asigno el rol de Admin (Usuario leomarqz main)
INSERT INTO AspNetUserRoles (UserId, RoleId)
VALUES ('9284cddd-104c-4f6e-8c87-56c370f47981', '92d0dffc-f1db-49ec-8392-954f2dff387f');

-- Usuario Writer se le asigno el rol de Writer (Usuario leomarqz2020)
INSERT INTO AspNetUserRoles (UserId, RoleId)
VALUES ('e30674cb-0f16-4f45-9753-7d20b063d2cd', 'e4baf39c-6356-43b4-a7af-ad4bf7a9fa27');

-- Usuario Reader se le asigno el rol de Reader (Usuario Pepe)
INSERT INTO AspNetUserRoles (UserId, RoleId)
VALUES ('41567275-352e-477a-bdc9-55e8a5d200ed', '240a778b-a089-455f-94af-9f9df4d13f8c');