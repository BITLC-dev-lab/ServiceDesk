DECLARE @CatId INT = (SELECT TOP 1 Id FROM Categories);
DECLARE @EmpId INT = (SELECT TOP 1 Id FROM Employees);

INSERT INTO Tickets (Title, Description, Status, Priority, CreatedAt, EditDate, CompleteDate, CategoryId, EmployeeId) VALUES
('Laptop startet nicht', 'Bildschirm bleibt nach dem Einschalten schwarz', 0, 2, '2026-11-18 09:15:00', NULL, NULL, @CatId, @EmpId),
('Passwort vergessen', 'Benutzerkonto ist nach 3 Fehlversuchen gesperrt', 2, 1, '2026-11-17 14:20:00', '2026-11-17 14:45:00', '2026-11-17 14:50:00', @CatId, @EmpId),
('VPN-Verbindung bricht ab', 'Homeoffice-Zugang trennt sich alle 10 Minuten', 1, 2, '2026-11-19 08:30:00', '2026-11-19 10:00:00', NULL, @CatId, @EmpId),
('Doppelte Abbuchung', 'Rechnung Nr. 4021 wurde zweimal vom Konto abgebucht', 0, 2, '2026-11-19 11:05:00', NULL, NULL, @CatId, @EmpId),
('Dark Mode für das Portal', 'Wunsch nach einem dunklen Farbschema in der Weboberfläche', 0, 0, '2026-11-15 16:00:00', NULL, NULL, @CatId, @EmpId),
('Frage zu Öffnungszeiten', 'Allgemeine Anfrage zur Erreichbarkeit am Wochenende', 2, 0, '2026-11-16 10:10:00', '2026-11-16 11:00:00', '2026-11-16 11:05:00', @CatId, @EmpId);