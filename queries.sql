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