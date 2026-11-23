-- ServiceDesk_SeedTestData.sql (rekonstruiert am 23.11.2026)
-- Testdaten fuer die lokale ServiceDeskDb (SQL Server / LocalDB).
-- Erwartete Tabellen: dbo.Categories, dbo.Employees, dbo.Tickets.
-- Testtickets beginnen mit [TEST]. Vorhandene Daten werden nicht geloescht/aktualisiert.
-- Bei erneutem Ausfuehren werden bereits vorhandene Testdaten uebersprungen.
-- Status: 0=Offen, 1=InBearbeitung, 2=Abgeschlossen.
-- Prioritaet: 0=Niedrig, 1=Mittel, 2=Hoch.

IF DB_NAME() <> N'ServiceDeskDb'
    THROW 50001, 'Bitte die Datenbank ServiceDeskDb als aktive Datenbank waehlen.', 1;

IF OBJECT_ID(N'dbo.Categories', N'U') IS NULL
   OR OBJECT_ID(N'dbo.Employees', N'U') IS NULL
   OR OBJECT_ID(N'dbo.Tickets', N'U') IS NULL
    THROW 50002, 'ServiceDesk-Tabellen fehlen. Zuerst EF-Core-Migrationen ausfuehren.', 1;

SET NOCOUNT ON;
SET XACT_ABORT ON;

BEGIN TRY
    BEGIN TRANSACTION;

    -- 1) Kategorien (6 Beispielkategorien; Vorhandenes bleibt erhalten)
    INSERT INTO dbo.Categories (Name)
    SELECT c.Name
    FROM (VALUES
        (N'Hardware'),
        (N'Software'),
        (N'Netzwerk'),
        (N'Zugriffsrechte'),
        (N'Drucker'),
        (N'Sonstiges')
    ) AS c(Name)
    WHERE NOT EXISTS (SELECT 1 FROM dbo.Categories AS old WHERE old.Name = c.Name);

    -- 2) Mitarbeiter (8 Testmitarbeiter mit Beispiel-E-Mail-Domain)
    INSERT INTO dbo.Employees (FirstName, LastName, Email)
    SELECT e.FirstName, e.LastName, e.Email
    FROM (VALUES
        (N'Lena', N'Hoffmann', N'lena.hoffmann.test@example.com'),
        (N'Jonas', N'Becker', N'jonas.becker.test@example.com'),
        (N'Mehmet', N'Yilmaz', N'mehmet.yilmaz.test@example.com'),
        (N'Sarah', N'Klein', N'sarah.klein.test@example.com'),
        (N'Tim', N'Wagner', N'tim.wagner.test@example.com'),
        (N'Nina', N'Schulz', N'nina.schulz.test@example.com'),
        (N'Paul', N'Richter', N'paul.richter.test@example.com'),
        (N'Mia', N'Neumann', N'mia.neumann.test@example.com')
    ) AS e(FirstName, LastName, Email)
    WHERE NOT EXISTS (SELECT 1 FROM dbo.Employees AS old WHERE old.Email = e.Email);

    -- 3) Tickets (25 Datensaetze; alle am Titel [TEST] erkennbar)
    ;WITH TestTickets (Title, Description, CategoryName, EmployeeEmail, Status, Priority, DaysAgo) AS
    (
        SELECT * FROM (VALUES
            (N'[TEST] Drucker im Vertrieb druckt nicht', N'Der Drucker reagiert nicht auf Druckauftraege.', N'Drucker', N'lena.hoffmann.test@example.com', 0, 2, 1),
            (N'[TEST] VPN-Verbindung bricht regelmaessig ab', N'VPN verliert alle zehn Minuten die Verbindung.', N'Netzwerk', N'jonas.becker.test@example.com', 1, 2, 2),
            (N'[TEST] Office-Aktivierung fehlgeschlagen', N'Lizenzmeldung beim Start von Office.', N'Software', N'lena.hoffmann.test@example.com', 1, 1, 3),
            (N'[TEST] Zugriff auf Projektordner fehlt', N'Berechtigung fuer gemeinsamen Ordner fehlt.', N'Zugriffsrechte', NULL, 0, 1, 4),
            (N'[TEST] Monitor zeigt kein Bild', N'Externer Monitor bleibt schwarz.', N'Hardware', N'mehmet.yilmaz.test@example.com', 2, 0, 7),
            (N'[TEST] WLAN im Konferenzraum instabil', N'WLAN-Signal bricht waehrend Meetings ab.', N'Netzwerk', N'jonas.becker.test@example.com', 0, 2, 2),
            (N'[TEST] Passwort zuruecksetzen', N'Mitarbeiter kann sich nicht anmelden.', N'Zugriffsrechte', N'sarah.klein.test@example.com', 2, 1, 5),
            (N'[TEST] Teams startet nicht', N'Teams bleibt beim Anmelden stehen.', N'Software', N'tim.wagner.test@example.com', 1, 1, 3),
            (N'[TEST] Laptop laesst sich nicht einschalten', N'Nach dem Laden keine Reaktion auf den Einschaltknopf.', N'Hardware', N'mehmet.yilmaz.test@example.com', 0, 2, 1),
            (N'[TEST] Neuen Netzwerkdrucker einrichten', N'Neuer Arbeitsplatz benoetigt Druckerzuordnung.', N'Drucker', N'nina.schulz.test@example.com', 2, 0, 10),
            (N'[TEST] E-Mail-Synchronisation fehlerhaft', N'Outlook zeigt keine neuen Nachrichten.', N'Software', N'tim.wagner.test@example.com', 1, 2, 4),
            (N'[TEST] Freigabe fuer Teamlaufwerk', N'Zugriff fuer neuen Projektmitarbeiter benoetigt.', N'Zugriffsrechte', NULL, 0, 1, 2),
            (N'[TEST] Tastatur funktioniert nicht', N'Mehrere Tasten reagieren nicht.', N'Hardware', N'paul.richter.test@example.com', 2, 0, 11),
            (N'[TEST] Videokonferenz ohne Ton', N'Mikrofon wird im Browser nicht erkannt.', N'Hardware', N'mia.neumann.test@example.com', 1, 1, 6),
            (N'[TEST] Softwareinstallation anfragen', N'Fachanwendung muss auf neuem Geraet installiert werden.', N'Software', N'nina.schulz.test@example.com', 0, 0, 3),
            (N'[TEST] Internetseite im Buero nicht erreichbar', N'Externe Webseite kann nicht aufgerufen werden.', N'Netzwerk', N'jonas.becker.test@example.com', 2, 1, 9),
            (N'[TEST] Benutzerkonto entsperren', N'Konto nach mehreren Anmeldeversuchen gesperrt.', N'Zugriffsrechte', N'sarah.klein.test@example.com', 1, 2, 2),
            (N'[TEST] Drucker druckt Streifen', N'Tonerkartusche oder Trommel muss geprueft werden.', N'Drucker', N'lena.hoffmann.test@example.com', 0, 1, 4),
            (N'[TEST] Bluetooth-Maus verbindet sich nicht', N'Maus verliert Verbindung zum Notebook.', N'Hardware', N'paul.richter.test@example.com', 2, 0, 12),
            (N'[TEST] Scanner nicht verfuegbar', N'Scanner wird in der Anwendung nicht angezeigt.', N'Drucker', N'nina.schulz.test@example.com', 1, 1, 5),
            (N'[TEST] Sicherheitsupdate pruefen', N'Update wird auf einem Geraet wiederholt angeboten.', N'Software', N'tim.wagner.test@example.com', 0, 1, 1),
            (N'[TEST] IP-Telefon ohne Verbindung', N'Telefon registriert sich nicht am Server.', N'Netzwerk', N'mia.neumann.test@example.com', 1, 2, 5),
            (N'[TEST] Bildschirmfreigabe funktioniert nicht', N'Freigabe in Videokonferenz wird blockiert.', N'Software', NULL, 0, 0, 3),
            (N'[TEST] Ordner versehentlich geloescht', N'Projektordner soll aus Backup wiederhergestellt werden.', N'Sonstiges', N'sarah.klein.test@example.com', 2, 2, 8),
            (N'[TEST] Arbeitsplatz fuer neuen Mitarbeiter', N'Notebook und Zugriffsrechte vorbereiten.', N'Sonstiges', NULL, 0, 1, 1)
        ) AS v(Title, Description, CategoryName, EmployeeEmail, Status, Priority, DaysAgo)
    )
    INSERT INTO dbo.Tickets
        (Title, Description, Status, Priority, CreatedAt, EditDate, CompleteDate, CategoryId, EmployeeId)
    SELECT
        t.Title, t.Description, t.Status, t.Priority,
        DATEADD(DAY, -t.DaysAgo, GETDATE()),
        CASE WHEN t.Status IN (1, 2) THEN DATEADD(HOUR, 1, DATEADD(DAY, -t.DaysAgo, GETDATE())) ELSE NULL END,
        CASE WHEN t.Status = 2 THEN DATEADD(HOUR, 2, DATEADD(DAY, -t.DaysAgo, GETDATE())) ELSE NULL END,
        c.Id, e.Id
    FROM TestTickets AS t
    INNER JOIN dbo.Categories AS c ON c.Name = t.CategoryName
    LEFT JOIN dbo.Employees AS e ON e.Email = t.EmployeeEmail
    WHERE NOT EXISTS (SELECT 1 FROM dbo.Tickets AS old WHERE old.Title = t.Title);

    COMMIT TRANSACTION;
END TRY
BEGIN CATCH
    IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
    THROW;
END CATCH;

-- Kontrollabfrage: zeigt alle erkennbar markierten Testtickets.
SELECT t.Id, t.Title, c.Name AS Kategorie, t.Status, t.Priority, t.CreatedAt
FROM dbo.Tickets AS t
INNER JOIN dbo.Categories AS c ON c.Id = t.CategoryId
WHERE t.Title LIKE N'[[]TEST] %'
ORDER BY t.Id;